using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HDWDotNetCore.ConsoleApp
{
    internal class EFCoreExample
    {
        private readonly AppDbContext db = new AppDbContext();
        public void Run()
        {
            //Read();
            //Create("title_EF", "author_EF", "content_EF");
            //Edit(8);
            //Update(7,"title_7", "author_7", "content_7");
            Delete(2);
        }
        private void Read()
        {
            var lst = db.Blogs.ToList();
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
            var blog = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            Console.WriteLine(blog.BlogId);
            Console.WriteLine(blog.BlogTitle);
            Console.WriteLine(blog.BlogAuthor);
            Console.WriteLine(blog.BlogContent);
            Console.WriteLine("_____________________________");
        }
        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            db.Blogs.Add(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }
        private void Update(int id, string title, string author, string content)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null) 
            {
                Console.WriteLine("No data found");
                return;
            }
            item.BlogId = id;
            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;
            int result = db.SaveChanges();

            string message = result > 0 ? "Updating Successful" : "Updaing Failed";
            Console.WriteLine(message);

        }
        private void Delete(int id)
        {
            var blog = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                Console.WriteLine("No data to delete");
                return;
            }
            db.Blogs.Remove(blog);
            int result = db.SaveChanges();
            string message = result > 0 ? "Deletion Successful" : "Deletion Failed";
            Console.WriteLine(message);

        }

    }
}
