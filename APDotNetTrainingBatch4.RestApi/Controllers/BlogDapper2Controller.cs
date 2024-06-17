using APDotNetTrainingBatch4.RestApi.Models;
using Dapper;
using DotNetTrainingBatch4.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.SqlClient;

namespace APDotNetTrainingBatch4.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        //private readonly DapperService _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        //Read

        private readonly DapperService _dapperService;

        public BlogDapper2Controller(DapperService dappperService)
        {
            _dapperService = dappperService;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "Select * from tbl_blog";
            var lst = _dapperService.Query<BlogModel>(query);
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            //string query = "Select * from tbl_blog where blogid = @BlogId";
            //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            //var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
            //FirstOrDefalut => query top 2 and filter first one
            
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data Found");
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlogs(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                               ([BlogTitle]
                               ,[BlogAuthor]
                               ,[BlogContent])
                                VALUES
                               (@BlogTitle
                               ,@BlogAuthor
                               ,@BlogContent)";
            
            int result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id,BlogModel blog )
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data found");
            }
            blog.BlogId = id;
            string query = @"UPDATE[dbo].[Tbl_Blog]
                             SET[BlogTitle] = @BlogTitle
                                  ,[BlogAuthor] = @BlogAuthor
                                  ,[BlogContent] = @BlogContent
                              WHERE BlogId = @BlogId";
            int result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Update Successful" : "Update Failed";
            Console.WriteLine(message); 
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogModel blog)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data found");                
            }            
            string conditions = string.Empty;

            if(!string.IsNullOrEmpty(blog.BlogTitle)) {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += "[BlogContent] =@BlogContent,";
            }            
            if(conditions.Length == 0)
            {
                return NotFound("No data Found");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.BlogId = id;
            string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";
            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No data found");
            }
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            string query = "Delete from tbl_blog where blogid = @BlogId";
            int result = db.Execute(query, item);

            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            Console.WriteLine(message);
            return Ok(message);

        }
        private BlogModel? FindById(int id) //BlogModel? => meaning for null object allow
        {
            string query = "Select * from tbl_blog where blogid = @BlogId";            
            var item = _dapperService.QueryFirstOrDefault<BlogModel>(query,new BlogModel { BlogId=id });
            return item;
        }
    }
}
