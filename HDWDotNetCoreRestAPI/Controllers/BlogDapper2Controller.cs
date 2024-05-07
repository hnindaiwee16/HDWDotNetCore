using Dapper;
using HDWDotNetCore.ConsoleApp.Services;
using HDWDotNetCore.Shared;
using HDWDotNetCoreRestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Data;
using System.Data.SqlClient;

namespace HDWDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private readonly DapperService _dapperService = new DapperService(ConnectionStrings.stringBuilder.ConnectionString);

    [HttpGet]
        public IActionResult BlogGet()
        {
            string query = ("select * from dotNet");
            var lst = _dapperService.Query<BlogModel>(query);
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
            int result = _dapperService.Execute(query, model);
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

            int result = _dapperService.Execute(query, model);              
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }
        private BlogModel? FindByID(int id)
        {
            String query = "select * from dotNet where blogId = @BlogId";
            var item =  _dapperService.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id});
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
            int result = _dapperService.Execute(query, model);
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

            int result = _dapperService.Execute(query, item);
            string message = result > 0 ? "Deletion Successful" : "Deletion Failed";
            return Ok(message);
        }
    }
}
