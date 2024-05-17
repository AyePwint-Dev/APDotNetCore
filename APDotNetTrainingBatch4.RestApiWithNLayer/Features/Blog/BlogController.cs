using APDotNetTrainingBatch4.RestApiWithNLayer.Db;
using Microsoft.EntityFrameworkCore;

namespace APDotNetTrainingBatch4.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _blBlog;
        public BlogController()
        {
            _blBlog = new BL_Blog();
        }
        [HttpGet]
        public IActionResult Read()
        {
            var lst = _blBlog.GetBlogs();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _blBlog.GetBlog(id);            
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            var result = _blBlog.CreateBlog(blog);

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }
        [HttpPut]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }

            var result = _blBlog.UpdateBlog(id, blog);
            string message = result > 0 ? "Update Successful" : "Update Fail";
            return Ok(message);
        }
        [HttpPatch]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            item.BlogId = blog.BlogId;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
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
            var result = _blBlog.UpdateBlog(id, item);
            string message = result > 0 ? "Update Successful" : "Update Fail";
            return Ok(message);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            
            var result = _blBlog.DeleteBlog(item.BlogId);
            string message = result > 0 ? "Delete Successful" : "Delete Fail";
            return Ok(message);
        }
    }
}
