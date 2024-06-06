using HDWDotNetCore.Shared;
using HDWDotNetCore.WinFormsApp.Model;
using HDWDotNetCore.WinFormsApp.Queries;

namespace HDWDotNetCore.WinFormsApp
{
    public partial class FrmBlog : Form
    {
        private readonly DapperService _dapperService;
        private readonly int _blogId;
        public FrmBlog()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.stringBuilder.ConnectionString);
        }
        public FrmBlog(int blogId)
        {
            InitializeComponent();
            _blogId = blogId;
            _dapperService = new DapperService(ConnectionStrings.stringBuilder.ConnectionString);

            var model = _dapperService.QueryFirstOrDefault<BlogModel>("select * from dotNet where blogid = @BlogId",
                new { BlogId = _blogId });

            titletextBox.Text = model.BlogTitle;
            authortextBox.Text = model.BlogAuthor;
            contenttextBox.Text = model.BlogContent;

            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BlogModel blog = new BlogModel();
                blog.BlogTitle = titletextBox.Text.Trim();
                blog.BlogAuthor = authortextBox.Text.Trim();
                blog.BlogContent = contenttextBox.Text.Trim();

                int result = _dapperService.Execute(BlogQuery.BlogCreate, blog);
                string message = result > 0 ? "Saving Successful." : "Saving Failed.";
                var messageBoxIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
                MessageBox.Show(message, "Blog", MessageBoxButtons.OK, messageBoxIcon);
                if (result > 0)
                    ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ClearControls()
        {
            titletextBox.Clear();
            authortextBox.Clear();
            contenttextBox.Clear();

            titletextBox.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var item = new BlogModel
                {
                    BlogId = _blogId,
                    BlogTitle = titletextBox.Text.Trim(),
                    BlogAuthor = authortextBox.Text.Trim(),
                    BlogContent = contenttextBox.Text.Trim(),
                };

                string query = @"UPDATE [dbo].[dotNet]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

                int result = _dapperService.Execute(query, item);
                string message = result > 0 ? "Updating Successful." : "Updating Failed.";
                MessageBox.Show(message);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

