using HDWDotNetCore.RestAPIWithNLayer.DB;

namespace HDWDotNetCore.RestAPIWithNLayer.Features.Blog
{
    public class DA_Blog
    {
        private readonly AppDbContext _dbContext;

        public DA_Blog()
        {
            _dbContext = new AppDbContext();
        }
        public List<BlogModel> GetBlog()
        {
            var lst = _dbContext.Blogs.ToList();
            return lst;
        }
        public BlogModel GetBlog(int id)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            return item;
        }
        public int CreateBlog(BlogModel requestModel)
        {
            _dbContext.Blogs.Add(requestModel);
            var result = _dbContext.SaveChanges();
            return result;
        }
        public int UpdateBlog(int id, BlogModel requestModel)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null) return 0;
            item.BlogTitle = requestModel.BlogTitle;
            item.BlogAuthor = requestModel.BlogAuthor;
            item.BlogContent = requestModel.BlogContent;

            var result = _dbContext.SaveChanges();
            return result;
        }
        public int UpdateBlogByEach(int id, BlogModel requestModel)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            
            if (!String.IsNullOrEmpty(requestModel.BlogTitle))
            {
                item.BlogTitle = requestModel.BlogTitle;
            }
            if (!String.IsNullOrEmpty(requestModel.BlogAuthor))
            {
                item.BlogAuthor = requestModel.BlogAuthor;
            }
            if (!String.IsNullOrEmpty(requestModel.BlogContent))
            {
                item.BlogContent = requestModel.BlogContent;
            }
            var result = _dbContext.SaveChanges();
            return result;
        }
        public int DeleteBlog(int id)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null) return 0;

            _dbContext.Remove(item);
            var result = _dbContext.SaveChanges();
            return result;
        }
    }

}
