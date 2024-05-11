using HDWDotNetCoreRestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HDWDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlog()
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.stringBuilder.ConnectionString);
            connection.Open();
            string query = "SELECT * FROM [dbo].[dotNet]";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            string query = "select * from dotNet where BlogId = @BlogId";

            SqlConnection connection = new SqlConnection(ConnectionStrings.stringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("No data found.");
            }

            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };

            return Ok(item);
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
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult PutBlog(int id,BlogModel blog)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.stringBuilder.ConnectionString);
            connection.Open();
            var item = FindById(id);
            if (item.Rows.Count == 0)
            {
                return NotFound("No data to update");
            }
            string query = @"UPDATE [dbo].[dotNet]
            SET [BlogTitle] = @BlogTitle
             ,[BlogAuthor] = @BlogAuthor
                ,[BlogContent] = @BlogContent
            WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.stringBuilder.ConnectionString);
            connection.Open();
            var item = FindById(id);
            if (item.Rows.Count == 0)
            {
                return NotFound("No data by this ID to update");
            }
            string condition = string.Empty;
            if (!String.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += " [blogTitle] = @BlogTitle, ";
            }
            if (!String.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += " [blogAuthor] = @BlogAuthor, ";
            }
            if (!String.IsNullOrEmpty(blog.BlogContent))
            {
                condition += " [blogContent] = @BlogContent, ";
            }
            if (condition.Length == 0)
            {
                return NotFound("No data to update");
            }
            condition = condition.Substring(0, condition.Length - 2);
            //blog.BlogId = id;

            string query = $@"UPDATE [dbo].[dotNet]
                           SET {condition}
                           WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!String.IsNullOrEmpty(blog.BlogTitle))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            }
            if (!String.IsNullOrEmpty(blog.BlogAuthor))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            }
            if (!String.IsNullOrEmpty(blog.BlogContent))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            }         
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);
            if (item.Rows.Count == 0)
            {
                return NotFound("No data by this ID to delete");
            }
            SqlConnection connection = new SqlConnection(ConnectionStrings.stringBuilder.ConnectionString);
            connection.Open();
            string query = @"DELETE FROM  [dbo].[dotNet]
            WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }
        
        private DataTable FindById(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.stringBuilder.ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from dotNet where BlogId = @BlogId", connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();
            return dt;                   
        }
    }
}

