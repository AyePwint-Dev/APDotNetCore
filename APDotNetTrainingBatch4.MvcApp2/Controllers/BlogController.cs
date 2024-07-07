using APDotNetTrainingBatch4.MvcApp2.Db;
using APDotNetTrainingBatch4.MvcApp2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static APDotNetTrainingBatch4.MvcApp2.Models.BlogModel;

namespace APDotNetTrainingBatch4.MvcApp2.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db)
        {
            _db = db;
        }
        [ActionName("Index")]
        public async Task<IActionResult> BlogIndex()
        {
            var lst = await _db.Blogs
                .OrderByDescending(x=> x.BlogId)
                .ToListAsync();
            return View("BlogIndex",lst);
        }
        [ActionName("BlogCreate")]
        public IActionResult BlogCreate()
        {            
            return View("BlogCreate");
        }
        [HttpPost]
        [ActionName("BlogSave")]
        public async Task<IActionResult> BlogSave(BlogModel blog)
        {
            await _db.Blogs.AddAsync(blog);
            var result = await _db.SaveChangesAsync();
            var message = new MessageModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Successful." : "Saving Failed."
            };
            return Json(message);
        }
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            var item = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if(item is null)
            {
                return Redirect("/Blog");
            }
            return View("BlogEdit", item);
        }
        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult>BlogUpdate(int id, BlogModel blog)
        {
            var item = await _db.Blogs.FirstOrDefaultAsync( x=> x.BlogId == id);
            if(item is null)
            {
                return Redirect("/Blog");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            var result = await _db.SaveChangesAsync();
            var message = new MessageModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Updating Successful." : "Updating Failed."
            };
            return Json(message);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(BlogModel blog)
        {
            var item = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == blog.BlogId);
            if (item is null)
            {
                return Redirect("/Blog");
            }
            _db.Blogs.Remove(item);

            var result = await _db.SaveChangesAsync();
            var message = new MessageModel()
            {
                IsSuccess = result>0,
                Message = result>0?"Deleteing Success.":"Deleting Failed."
            };
            return Json(message);
        }
    }
}
