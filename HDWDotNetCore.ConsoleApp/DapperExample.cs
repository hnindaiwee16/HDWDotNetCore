using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HDWDotNetCore.ConsoleApp
{
    internal class DapperExample
    {
        IDbConnection db = new SqlConnection(ConnectionStrings.stringBuilder.ConnectionString);
        public void Run()
        {
            //Read();
            //Edit(90);
            //Create("title_new", "author_new", "content_new");
            //Update(8, "title8", "author8", "content8");
            Delete(4);
        }
        private void Read()
        {
            
            List<BlogDto> lst = db.Query<BlogDto>("select * from dotNet").ToList();
            foreach (BlogDto blog in lst)
            {
                Console.WriteLine(blog.BlogId);
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogContent);
                Console.WriteLine("_____________________________");
            }
        }
        private void Edit(int id)
        {
            var item = db.Query<BlogDto>("select * from dotNet where blogId = @BlogId", new BlogDto { BlogId = id }).FirstOrDefault();
            if(item is null)
            {
                Console.WriteLine("No data found");
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
            
            string query = @"INSERT INTO [dbo].[dotNet]
            ([BlogTitle]
             ,[BlogAuthor]
                ,[BlogContent])
            VALUES
            (@BlogTitle
            ,@BlogAuthor,@BlogContent)";
            int result = db.Execute(query, item);

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }
        private void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string query = @"UPDATE [dbo].[dotNet]
            SET [BlogTitle] = @BlogTitle
             ,[BlogAuthor] = @BlogAuthor
                ,[BlogContent] = @BlogContent
            WHERE BlogId = @BlogId";

            int result = db.Execute(query, item);

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            Console.WriteLine(message);
        }
        private void Delete(int id)
        {   
            var item  = new BlogDto { BlogId = id };

            string query = @"DELETE FROM  [dbo].[dotNet]
            WHERE BlogId = @BlogId";

            int result = db.Execute(query, item);

            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            Console.WriteLine(message);
        }
    }
}
