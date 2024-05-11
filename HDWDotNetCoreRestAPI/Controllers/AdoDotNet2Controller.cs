using HDWDotNetCoreRestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using HDWDotNetCore.Shared;
using System.Collections.Generic;

namespace HDWDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _dotNetService = new AdoDotNetService(ConnectionStrings.stringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult GetBlog()
        {
            string query = "select * from dotNet";
            var lst = _dotNetService.Query<BlogModel>(query);
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            string query = "select * from dotNet where BlogId = @BlogId";
            var lst = _dotNetService.QueryFirstOrDefault<BlogModel>(query,
                new AdoDotNetParameter("@BlogId", id));
            if(lst is null)
            {
                return NotFound("No data");
            }
            return Ok(lst);
        }
        [HttpPost]
        public IActionResult PostBlog(BlogModel blog)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.stringBuilder.ConnectionString);
            connection.Open();
            var item = FindById(blog.BlogId);
            string query = @"INSERT INTO [dbo].[dotNet]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor       
           ,@BlogContent)";
            var result = _dotNetService.Execute(query,
            new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
            new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
            new AdoDotNetParameter("@BlogContent", blog.BlogContent)
        );

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult PutBlog(int id,BlogModel blog)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.stringBuilder.ConnectionString);
            connection.Open();
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data to update");
            }
            string query = @"UPDATE [dbo].[dotNet]
            SET [BlogTitle] = @BlogTitle
             ,[BlogAuthor] = @BlogAuthor
                ,[BlogContent] = @BlogContent
            WHERE BlogId = @BlogId";

            var result = _dotNetService.Execute(query,
            new AdoDotNetParameter("@BlogId", id),
            new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
            new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
            new AdoDotNetParameter("@BlogContent", blog.BlogContent)
        );


            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            List<AdoDotNetParameter> parameters = new List<AdoDotNetParameter>();
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data by this ID to update");
            }
            string condition = string.Empty;
            if (!String.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += " [blogTitle] = @BlogTitle, ";
                parameters.Add("@BlogTitle",blog.BlogTitle);
            }
            if (!String.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += " [blogAuthor] = @BlogAuthor, ";
                parameters.Add("@BlogAuthor", blog.BlogAuthor);
            }
            if (!String.IsNullOrEmpty(blog.BlogContent))
            {
                condition += " [blogContent] = @BlogContent, ";
                parameters.Add("@BlogContent", blog.BlogContent);
            }
            parameters.Add("@BlogId", id);
            if (condition.Length == 0)
            {
                return NotFound("No data to update");
            }
            condition = condition.Substring(0, condition.Length - 2);
            //blog.BlogId = id;

            string query = $@"UPDATE [dbo].[dotNet]
                           SET {condition}
                           WHERE BlogId = @BlogId";

            var result = _dotNetService.Execute(query,parameters.ToArray());

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data by this ID to delete");
            }
            string query = @"DELETE FROM  [dbo].[dotNet]
            WHERE BlogId = @BlogId";
            int result = _dotNetService.Execute(query, new AdoDotNetParameter("BlogId",id));
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }
        
        private BlogModel FindById(int id)
        {
            string query = "select * from dotNet where BlogId = @BlogId";
            var lst = _dotNetService.QueryFirstOrDefault<BlogModel>(query,
                new AdoDotNetParameter("@BlogId", id));
            return lst;                  
        }
    }
}

