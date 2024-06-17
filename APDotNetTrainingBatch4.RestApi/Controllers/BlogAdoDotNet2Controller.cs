using APDotNetTrainingBatch4.RestApi.Models;
using APDotNetTrainingBatch4.Shared;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;
using static APDotNetTrainingBatch4.Shared.AdoDotNetService;

namespace APDotNetTrainingBatch4.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        //private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        
        private readonly AdoDotNetService _adoDotNetService;

        public BlogAdoDotNet2Controller(AdoDotNetService adoDotNetService)
        {
            _adoDotNetService = adoDotNetService;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "Select * from tbl_blog";            
            var lst = _adoDotNetService.Query<BlogModel>(query);
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "Select * from tbl_blog where BlogId = @BlogId";

            //# Multiple param add from controller
            //AdoDotNetRequestParameter[] parameters =  new AdoDotNetRequestParameter[1];
            //parameters[0] = new AdoDotNetRequestParameter("@BlogId", id);
            //var lst = _adoDotNetService.Query<BlogModel>(query, parameters);

            //# Add multiple params with ','
            //var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetRequestParameter("@BlogId", id));
            var item = FindById(id);

            if( item is not null) {                 
                return Ok(item); 
            }
            return NotFound("No Data Found");
        }
        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {           
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                               ([BlogTitle]
                               ,[BlogAuthor]
                               ,[BlogContent])
                                VALUES
                               (@BlogTitle
                               ,@BlogAuthor
                               ,@BlogContent)";  

            int result = _adoDotNetService.Execute(query, 
                new AdoDotNetRequestParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetRequestParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetRequestParameter("@BlogContent", blog.BlogContent)
                );

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);           
        }
        //homework put,patch,delet
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,  BlogModel blog)
        {
            var item = FindById(id);
            if (item == null)
            {
                return NotFound("No data Found");
            }            
            string query = @"UPDATE[dbo].[Tbl_Blog]
                             SET[BlogTitle] = @BlogTitle
                                  ,[BlogAuthor] = @BlogAuthor
                                  ,[BlogContent] = @BlogContent
                              WHERE BlogId = @BlogId";

            blog.BlogId = id;
            int result = _adoDotNetService.Execute(query, 
                new AdoDotNetRequestParameter("@BlogId", blog.BlogId),
                new AdoDotNetRequestParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetRequestParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetRequestParameter("@BlogContent", blog.BlogContent)
                );           
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item == null)
            {
                return NotFound("No data Found");
            }           
            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += "[BlogContent] =@BlogContent, ";
            }
            //codition null => got an error
            if (conditions.Length == 0)
            {
                return NotFound("No data Found");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.BlogId = id;

            string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";

            List<AdoDotNetRequestParameter> parametersLst = new List<AdoDotNetRequestParameter>();
            parametersLst.Add(new AdoDotNetRequestParameter("@BlogId", blog.BlogId));

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                parametersLst.Add(new AdoDotNetRequestParameter("@BlogTitle", blog.BlogTitle));
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                parametersLst.Add(new AdoDotNetRequestParameter("@BlogAuthor", blog.BlogAuthor));
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                parametersLst.Add(new AdoDotNetRequestParameter("@BlogContent", blog.BlogContent));

            }
            
            int result = _adoDotNetService.Execute(query, parametersLst.ToArray()); 
            
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);
            if (item == null)
            {
                return NotFound("No data Found");
            }
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE BlogId = @BlogId";
            int result = _adoDotNetService.Execute(query,new AdoDotNetRequestParameter("@BlogId", id));
            
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            Console.WriteLine(message);
            return Ok(message);
        }
        private BlogModel? FindById(int id)
        {
            string query = "Select * from tbl_blog where blogid = @BlogId";  
            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetRequestParameter("@BlogId", id));
            return item;
        }
    }
}
