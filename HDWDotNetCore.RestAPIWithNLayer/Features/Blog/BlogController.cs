using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HDWDotNetCore.RestAPIWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _bl_Blog;

        public BlogController()
        {
            _bl_Blog = new BL_Blog();
        }
        [HttpGet]
        public ActionResult Get()
        {
            var lst = _bl_Blog.GetBlog();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public ActionResult Edit(int id)
        {
            var item =  _bl_Blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data found");
            }
            return Ok(item);
        }
        [HttpPost]
        public ActionResult Write(BlogModel requestmodel)
        {
            var result = _bl_Blog.CreateBlog(requestmodel);
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, BlogModel requestmodel)
        {
            var item = _bl_Blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data found to update");
            }
            
            var result = _bl_Blog.UpdateBlog(id, requestmodel);
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public ActionResult Patch(int id, BlogModel requestModel)
        {
            var item = _bl_Blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data found to update");
            }
            var result = _bl_Blog.UpdateByEach(id, requestModel);
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = _bl_Blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data found to delete");
            }
            var result = _bl_Blog.DeleteBlog(id);
            string message = result > 0 ? "Deletion Successful" : "Deletion Failed";
            return Ok(message);
        }
    }
}
