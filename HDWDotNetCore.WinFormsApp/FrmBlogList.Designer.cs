namespace HDWDotNetCore.WinFormsApp
{
    partial class FrmBlogList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvData = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colEdit = new DataGridViewButtonColumn();
            colDelete = new DataGridViewButtonColumn();
            colTitle = new DataGridViewTextBoxColumn();
            colAuthor = new DataGridViewTextBoxColumn();
            colContent = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { colId, colEdit, colDelete, colTitle, colAuthor, colContent });
            dgvData.Location = new Point(79, 118);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowTemplate.Height = 25;
            dgvData.Size = new Size(448, 150);
            dgvData.TabIndex = 0;
            dgvData.CellContentClick += dgvData_CellContentClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "blogId";
            colId.HeaderText = "ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colEdit
            // 
            colEdit.HeaderText = "Edit";
            colEdit.Name = "colEdit";
            colEdit.ReadOnly = true;
            colEdit.Resizable = DataGridViewTriState.True;
            colEdit.SortMode = DataGridViewColumnSortMode.Automatic;
            colEdit.Text = "Edit";
            colEdit.UseColumnTextForButtonValue = true;
            // 
            // colDelete
            // 
            colDelete.HeaderText = "Delete";
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            colDelete.Resizable = DataGridViewTriState.True;
            colDelete.SortMode = DataGridViewColumnSortMode.Automatic;
            colDelete.Text = "Delete";
            colDelete.UseColumnTextForButtonValue = true;
            // 
            // colTitle
            // 
            colTitle.DataPropertyName = "blogTitle";
            colTitle.HeaderText = "Title";
            colTitle.Name = "colTitle";
            colTitle.ReadOnly = true;
            // 
            // colAuthor
            // 
            colAuthor.DataPropertyName = "blogAuthor";
            colAuthor.HeaderText = "Author";
            colAuthor.Name = "colAuthor";
            colAuthor.ReadOnly = true;
            // 
            // colContent
            // 
            colContent.DataPropertyName = "blogContent";
            colContent.HeaderText = "Content";
            colContent.Name = "colContent";
            colContent.ReadOnly = true;
            // 
            // FrmBlogList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvData);
            Name = "FrmBlogList";
            Text = "BlogList";
            Load += FrmBlogList_Load;
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvData;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewButtonColumn colEdit;
        private DataGridViewButtonColumn colDelete;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colAuthor;
        private DataGridViewTextBoxColumn colContent;
    }
}