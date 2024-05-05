using APDotNetTrainingBatch4.RestApi.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.SqlClient;

namespace APDotNetTrainingBatch4.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        //Read
        [HttpGet]
        public IActionResult GetBlogs()
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogModel> lst = db.Query<BlogModel>("Select * from tbl_blog").ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateBlogs(BlogModel blog)
        {
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id,BlogModel blog )
        {
            return Ok();
        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id)
        {
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            return Ok();

        }
    }
}
