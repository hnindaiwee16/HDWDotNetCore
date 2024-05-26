using HDWDotNetCore.Shared;
using HDWDotNetCore.WinFormsApp.Model;
using HDWDotNetCore.WinFormsApp.Queries;

namespace HDWDotNetCore.WinFormsApp
{
    public partial class FrmBlog : Form
    {
        private readonly DapperService _dapperService;
        public FrmBlog()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.stringBuilder.ConnectionString);
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
    }
}
