using APDotNetTrainingBatch4.RestApi.Db;
using APDotNetTrainingBatch4.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace APDotNetTrainingBatch4.RestApi.Controllers
{
    /*https://localhost:3000 =>domain url*/
    // api/bloc => end point
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogController()
        {
            _context = new AppDbContext();
        }
        [HttpGet]
        public IActionResult Read()
        {
            var lst = _context.Blogs.ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return NotFound("No data found.");
            }           
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _context.Blogs.Add(blog);
            var result=_context.SaveChanges();

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
            return Ok(message);
        }
        [HttpPut]
        public IActionResult Update(int id, BlogModel blog)
        {
            //edit whole resource
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            var result = _context.SaveChanges();
            string message = result > 0 ? "Update Successful" : "Update Fail";

            return Ok(message);
        }
        [HttpPatch]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogAuthor;
            }
            
            var result = _context.SaveChanges();
            string message = result > 0 ? "Update Successful" : "Update Fail";

            return Ok(message);
            
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            _context.Blogs.Remove(item);

            var result = _context.SaveChanges();
            string message = result > 0 ? "Delete Successful" : "Delete Fail";

            return Ok(message);
           
        }
    }
}
