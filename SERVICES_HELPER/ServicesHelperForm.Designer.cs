namespace SERVICES_HELPER
{
    partial class ServicesHelperForm
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
            dgvServices = new DataGridView();
            btnUpdate = new Button();
            btnBuild = new Button();
            btnAdd = new Button();
            label1 = new Label();
            txtGitHubUrl = new TextBox();
            label2 = new Label();
            txtDirectory = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvServices).BeginInit();
            SuspendLayout();
            // 
            // dgvServices
            // 
            dgvServices.AllowUserToAddRows = false;
            dgvServices.AllowUserToDeleteRows = false;
            dgvServices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvServices.Location = new Point(20, 14);
            dgvServices.Name = "dgvServices";
            dgvServices.ReadOnly = true;
            dgvServices.Size = new Size(609, 424);
            dgvServices.TabIndex = 0;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(647, 14);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(131, 33);
            btnUpdate.TabIndex = 1;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnBuild
            // 
            btnBuild.Location = new Point(647, 67);
            btnBuild.Name = "btnBuild";
            btnBuild.Size = new Size(131, 33);
            btnBuild.TabIndex = 2;
            btnBuild.Text = "Build";
            btnBuild.UseVisualStyleBackColor = true;
            btnBuild.Click += btnBuild_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(647, 121);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(131, 33);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(20, 448);
            label1.Name = "label1";
            label1.Size = new Size(96, 20);
            label1.TabIndex = 4;
            label1.Text = "GitHub URL:";
            // 
            // txtGitHubUrl
            // 
            txtGitHubUrl.Location = new Point(122, 447);
            txtGitHubUrl.Name = "txtGitHubUrl";
            txtGitHubUrl.Size = new Size(259, 23);
            txtGitHubUrl.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(422, 448);
            label2.Name = "label2";
            label2.Size = new Size(79, 20);
            label2.TabIndex = 7;
            label2.Text = "Directory:";
            // 
            // txtDirectory
            // 
            txtDirectory.Location = new Point(507, 449);
            txtDirectory.Name = "txtDirectory";
            txtDirectory.Size = new Size(259, 23);
            txtDirectory.TabIndex = 8;
            // 
            // ServicesHelperForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 477);
            Controls.Add(txtDirectory);
            Controls.Add(label2);
            Controls.Add(txtGitHubUrl);
            Controls.Add(label1);
            Controls.Add(btnAdd);
            Controls.Add(btnBuild);
            Controls.Add(btnUpdate);
            Controls.Add(dgvServices);
            Name = "ServicesHelperForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SERVICES HELPER";
            Load += ServicesHelperForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvServices).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvServices;
        private Button btnUpdate;
        private Button btnBuild;
        private Button btnAdd;
        private Label label1;
        private TextBox txtGitHubUrl;
        private Label label2;
        private TextBox txtDirectory;
    }
}
