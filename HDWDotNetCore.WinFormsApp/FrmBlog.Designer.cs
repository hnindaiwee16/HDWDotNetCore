namespace HDWDotNetCore.WinFormsApp
{
    partial class FrmBlog
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            title = new Label();
            author = new Label();
            content = new Label();
            titletextBox = new TextBox();
            authortextBox = new TextBox();
            contenttextBox = new TextBox();
            btnCancel = new Button();
            btnSave = new Button();
            SuspendLayout();
            // 
            // title
            // 
            title.AutoSize = true;
            title.ForeColor = Color.Black;
            title.Location = new Point(283, 53);
            title.Name = "title";
            title.Size = new Size(32, 15);
            title.TabIndex = 1;
            title.Text = "Title:";
            // 
            // author
            // 
            author.AutoSize = true;
            author.ForeColor = Color.Black;
            author.Location = new Point(283, 98);
            author.Name = "author";
            author.Size = new Size(47, 15);
            author.TabIndex = 2;
            author.Text = "Author:";
            // 
            // content
            // 
            content.AutoSize = true;
            content.ForeColor = Color.Black;
            content.Location = new Point(283, 158);
            content.Name = "content";
            content.Size = new Size(53, 15);
            content.TabIndex = 3;
            content.Text = "Content:";
            // 
            // titletextBox
            // 
            titletextBox.Location = new Point(283, 72);
            titletextBox.Name = "titletextBox";
            titletextBox.Size = new Size(167, 23);
            titletextBox.TabIndex = 4;
            // 
            // authortextBox
            // 
            authortextBox.Location = new Point(283, 116);
            authortextBox.Name = "authortextBox";
            authortextBox.Size = new Size(167, 23);
            authortextBox.TabIndex = 5;
            // 
            // contenttextBox
            // 
            contenttextBox.Location = new Point(283, 176);
            contenttextBox.Name = "contenttextBox";
            contenttextBox.Size = new Size(167, 23);
            contenttextBox.TabIndex = 6;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(91, 137, 149);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(283, 216);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(21, 126, 57);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(375, 216);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.TextImageRelation = TextImageRelation.ImageAboveText;
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(contenttextBox);
            Controls.Add(authortextBox);
            Controls.Add(titletextBox);
            Controls.Add(content);
            Controls.Add(author);
            Controls.Add(title);
            ForeColor = Color.White;
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label title;
        private Label author;
        private Label content;
        private TextBox titletextBox;
        private TextBox authortextBox;
        private TextBox contenttextBox;
        private Button btnCancel;
        private Button btnSave;
    }
}
