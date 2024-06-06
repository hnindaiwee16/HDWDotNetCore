using HDWDotNetCore.Shared;
using HDWDotNetCore.WinFormsApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDWDotNetCore.WinFormsApp
{
    public partial class FrmBlogList : Form
    {
        private readonly DapperService _dapperService;
        public FrmBlogList()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.stringBuilder.ConnectionString);
        }

        private void FrmBlogList_Load(object sender, EventArgs e)
        {
            BlogList();
        }
        private void BlogList()
        {
            List<BlogModel> lst = _dapperService.Query<BlogModel>("select * from dotNet");
            dgvData.DataSource = lst;
        }
        private void DeleteBlog(int id)
        {
            string query = @"Delete From [dbo].[dotNet] WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, new { BlogId = id });
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            MessageBox.Show(message);
            BlogList();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            #region If Case

            var blogId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colId"].Value);

            if (e.ColumnIndex == (int)EnumFormControlType.Edit)
            {
                FrmBlog frm = new FrmBlog(blogId);
                frm.ShowDialog();

                BlogList();
            }
            else if (e.ColumnIndex == (int)EnumFormControlType.Delete)
            {
                var dialogResult = MessageBox.Show("Are you sure want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes) return;

                DeleteBlog(blogId);
            }
            else if(e.ColumnIndex == (int)EnumFormControlType.Edit)
            {

            }

            #endregion
        }
    }
}
