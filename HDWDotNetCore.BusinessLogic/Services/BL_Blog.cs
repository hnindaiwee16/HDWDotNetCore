using HDWDotNetCore.NLayer.DataAccess.Services;
using HDWDotNetCore.NLayer.DataAccess.Models;

namespace HDWDotNetCore.RestAPIWithNLayer.Features.Blog
{
    public class BL_Blog
    {
        private readonly DA_Blog _daBlog;
        
        public BL_Blog()
        {
            _daBlog = new DA_Blog();
        }
        public List<BlogModel> GetBlog()
        {
            var lst = _daBlog.GetBlog();
            return lst;
        }
        public BlogModel GetBlog(int id)
        {
            var item = _daBlog.GetBlog(id);
            return item;
        }
        public int CreateBlog(BlogModel requestModel)
        {
            var result = _daBlog.CreateBlog(requestModel);
            return result;
        }
        public int UpdateBlog(int id, BlogModel requestModel)
        {
            var result = _daBlog.UpdateBlog(id, requestModel);
            return result;
        }
        public int UpdateByEach(int id, BlogModel requestModel)
        {
            var result = _daBlog.UpdateBlogByEach(id, requestModel);
            return result;
        }
        public int DeleteBlog(int id)
        {
            var result = _daBlog.DeleteBlog(id);
            return result;
        }
    }
}
