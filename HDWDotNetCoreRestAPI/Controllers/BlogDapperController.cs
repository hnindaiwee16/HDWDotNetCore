using Dapper;
using HDWDotNetCoreRestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace HDWDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        IDbConnection db = new SqlConnection(ConnectionStrings.stringBuilder.ConnectionString);

    [HttpGet]
        public IActionResult BlogGet()
        {
            string query = ("select * from dotNet");
            List<BlogModel> lst = db.Query<BlogModel>(query).ToList();
            return Ok(lst);
        }
    
    [HttpPost]
        public ActionResult Write(BlogModel model)
        {
            string query = @"INSERT INTO [dbo].[dotNet]
            ([BlogTitle]
             ,[BlogAuthor]
                ,[BlogContent])
            VALUES
            (@BlogTitle
            ,@BlogAuthor,@BlogContent)";
            int result = db.Execute(query, model);
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public ActionResult BlogUpdate(int id, BlogModel model)
        {
            var item = FindByID(id);
            if(item is null)
            {
                return NotFound("No Data found to update");
            }
            model.BlogId = id;
            string query = @"UPDATE [dbo].[dotNet]
            SET [BlogTitle] = @BlogTitle
             ,[BlogAuthor] = @BlogAuthor
                ,[BlogContent] = @BlogContent
            WHERE BlogId = @BlogId";

            int result = db.Execute(query, model);              
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }
        private BlogModel FindByID(int id)
        {
            var item = db.Query<BlogModel>("select * from dotNet where blogId = @BlogId", new BlogModel { BlogId = id }).FirstOrDefault();
            return item;
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, BlogModel model)
        {
                     
            string condition = string.Empty;
            if (!String.IsNullOrEmpty(model.BlogTitle))
            {
               condition += " [blogTitle] = @BlogTitle, ";
            }
            if (!String.IsNullOrEmpty(model.BlogAuthor))
            {
                condition += " [blogAuthor] = @BlogAuthor, ";
            }
            if (!String.IsNullOrEmpty(model.BlogContent))
            {
                condition += " [blogContent] = @BlogContent, ";
            }
            if(condition.Length == 0)
            {
                return NotFound("No data to update");
            }
            condition = condition.Substring(0, condition.Length - 2);
            model.BlogId = id;

            //string query = "UPDATE [dbo].[dotNet] set {condition} where blogId = @BlogId";
            string query = $@"UPDATE [dbo].[dotNet]
                           SET {condition}
                           WHERE BlogId = @BlogId";
            int result = db.Execute(query, model);
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = FindByID(id);
            if (item is null)
            {
                return NotFound("No Data found to delete");
            }
            string query = @"DELETE FROM  [dbo].[dotNet]
            WHERE BlogId = @BlogId";

            int result = db.Execute(query, item);
            string message = result > 0 ? "Deletion Successful" : "Deletion Failed";
            return Ok(message);
        }
    }
}
