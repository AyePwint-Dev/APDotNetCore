using APDotNetCoreConsoleApp.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APDotNetCoreConsoleApp.EFCoreExamples
{
    internal class EFCoreExample
    {
        //private readonly AppDbContext db = new AppDbContext(); //not modified and create db
        
        private readonly AppDbContext db;
        public EFCoreExample(AppDbContext db)
        {
            this.db = db;
        }

        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(11);
            //Create("titleTest", "AuthorTest", "ContentTest");
            //Update(1011, "titleTest1011", "AuthorTest1011", "ContentTest1011");
            Delete(1011);
        }
        private void Read()
        {
            var lst = db.Blogs.ToList();

            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-----------------------");
            }
        }
        private void Edit(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            /* Meaning
             foreach(BlogDto x in db.Blogs){
                if(x.BlogId == id)
             }
             */

            if (item is null) // if (item == null) => old version, if (item == null)=> new version ,'is null' can only use in new version
            {
                Console.WriteLine("No data found.");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }
        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            db.Blogs.Add(item);
            int result = db.SaveChanges(); //Click execute btn

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }
        private void Update(int id, string title, string author, string content)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;

            int result = db.SaveChanges();

            string message = result > 0 ? "Update Successful" : "Update Failed";
            Console.WriteLine(message);
        }
        private void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No Data Found");
                return;
            }
            db.Blogs.Remove(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "Delet Successful." : "Delete Failed";
            Console.Write(message);
        }
    }
}
