using HDWDotNetCoreRestAPI.Db;
using HDWDotNetCoreRestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HDWDotNetCoreRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _dbContext = new AppDbContext();

        [HttpGet]
        public ActionResult Get()
        {
            var lst = _dbContext.Blogs.ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public ActionResult Edit(int id)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data found");
            }
            return Ok(item);
        }
        [HttpPost]
        public ActionResult Write(BlogModel model)
        {
            _dbContext.Blogs.Add(model);
            var result = _dbContext.SaveChanges();
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, BlogModel model)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data found");
            }
            item.BlogTitle = model.BlogTitle;
            item.BlogAuthor = model.BlogAuthor;
            item.BlogContent = model.BlogContent;

            var result = _dbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public ActionResult Patch(int id, BlogModel model)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data found");
            }
            if (!String.IsNullOrEmpty(model.BlogTitle))
            {
                item.BlogTitle = model.BlogTitle;
            }
            if (!String.IsNullOrEmpty(model.BlogAuthor))
            {
                item.BlogTitle = model.BlogAuthor;
            }
            if (!String.IsNullOrEmpty(model.BlogContent))
            {
                item.BlogTitle = model.BlogContent;
            }
            var result = _dbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data found");
            }
            _dbContext.Blogs.Remove(item);
            var result = _dbContext.SaveChanges();
            string message = result > 0 ? "Deletion Successful" : "Deletion Failed";
            return Ok(message);
        }
    }
}
