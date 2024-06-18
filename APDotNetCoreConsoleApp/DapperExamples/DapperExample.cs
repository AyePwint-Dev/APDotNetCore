using APDotNetCoreConsoleApp.Dtos;
using APDotNetCoreConsoleApp.Services;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace APDotNetCoreConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public DapperExample(SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            _sqlConnectionStringBuilder = sqlConnectionStringBuilder;
        }

        public void Run()
        {
            // Read();
            // Edit(1);
            // Edit(11);
            //Create("titleTest", "AuthorTest", "ContentTest");
            //Update(10,"titleTest2", "AuthorTest2", "ContentTest2");
            Delete(10);
        }
        public void Read()
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = db.Query<BlogDto>("Select * from tbl_blog").ToList();
            //dynamic => can be anything lst[0] = fasgkbka();

            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-----------------------");
            }
        }
        public void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDto>("Select * from tbl_blog where blogid = @BlogId", new BlogDto { BlogId = id }).FirstOrDefault();
            //FirstOrDefault => exist value or null
            //int => exist int value or 0
            //C# 'var' not effect on the memory but 'var' effect on the javascript

            //if(item == null){} //old version
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

        }
        public void Create(string title, string author, string content)
        {
            var item = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                               ([BlogTitle]
                               ,[BlogAuthor]
                               ,[BlogContent])
                                VALUES
                               (@BlogTitle
                               ,@BlogAuthor
                               ,@BlogContent)";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }
        public void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto()
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string query = @"UPDATE[dbo].[Tbl_Blog]
                             SET[BlogTitle] = @BlogTitle
                                  ,[BlogAuthor] = @BlogAuthor
                                  ,[BlogContent] = @BlogContent
                              WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Update Successful" : "Update Failed";
            Console.WriteLine(message);
        }
        public void Delete(int id)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            var item = new BlogDto()
            {
                BlogId = id,
            };
            string query = "Delete from tbl_blog where blogid = @BlogId";
            int result = db.Execute(query, item);

            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            Console.WriteLine(message);

        }
    }

}

