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
            components = new System.ComponentModel.Container();
            dgvServices = new DataGridView();
            contextMenuService = new ContextMenuStrip(components);
            menuStart = new ToolStripMenuItem();
            menuStop = new ToolStripMenuItem();
            menuRestart = new ToolStripMenuItem();
            menuProperties = new ToolStripMenuItem();
            btnBuildProject = new Button();
            btnAddService = new Button();
            label1 = new Label();
            txtGitHubUrl = new TextBox();
            label2 = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            btnBrowse = new Button();
            txtDirectory = new TextBox();
            btnRemoveService = new Button();
            btnUpdateGitHub = new Button();
            btnCloneGitHub = new Button();
            txtSearchKey = new TextBox();
            btnSearch = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvServices).BeginInit();
            contextMenuService.SuspendLayout();
            SuspendLayout();
            // 
            // dgvServices
            // 
            dgvServices.AllowUserToAddRows = false;
            dgvServices.AllowUserToDeleteRows = false;
            dgvServices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvServices.ContextMenuStrip = contextMenuService;
            dgvServices.Location = new Point(20, 45);
            dgvServices.Name = "dgvServices";
            dgvServices.ReadOnly = true;
            dgvServices.Size = new Size(609, 393);
            dgvServices.TabIndex = 0;
            // 
            // contextMenuService
            // 
            contextMenuService.Items.AddRange(new ToolStripItem[] { menuStart, menuStop, menuRestart, menuProperties });
            contextMenuService.Name = "contextMenuService";
            contextMenuService.Size = new Size(128, 92);
            // 
            // menuStart
            // 
            menuStart.Name = "menuStart";
            menuStart.Size = new Size(127, 22);
            menuStart.Text = "Start";
            menuStart.Click += menuStart_Click;
            // 
            // menuStop
            // 
            menuStop.Name = "menuStop";
            menuStop.Size = new Size(127, 22);
            menuStop.Text = "Stop";
            menuStop.Click += menuStop_Click;
            // 
            // menuRestart
            // 
            menuRestart.Name = "menuRestart";
            menuRestart.Size = new Size(127, 22);
            menuRestart.Text = "Restart";
            menuRestart.Click += menuRestart_Click;
            // 
            // menuProperties
            // 
            menuProperties.Name = "menuProperties";
            menuProperties.Size = new Size(127, 22);
            menuProperties.Text = "Properties";
            menuProperties.Click += menuProperties_Click;
            // 
            // btnBuildProject
            // 
            btnBuildProject.Location = new Point(647, 119);
            btnBuildProject.Name = "btnBuildProject";
            btnBuildProject.Size = new Size(131, 33);
            btnBuildProject.TabIndex = 2;
            btnBuildProject.Text = "Build Project";
            btnBuildProject.UseVisualStyleBackColor = true;
            btnBuildProject.Click += btnBuildProject_Click;
            // 
            // btnAddService
            // 
            btnAddService.Location = new Point(647, 173);
            btnAddService.Name = "btnAddService";
            btnAddService.Size = new Size(131, 33);
            btnAddService.TabIndex = 3;
            btnAddService.Text = "Add Service";
            btnAddService.UseVisualStyleBackColor = true;
            btnAddService.Click += btnAddService_Click;
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
            // btnBrowse
            // 
            btnBrowse.Location = new Point(740, 446);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(38, 25);
            btnBrowse.TabIndex = 8;
            btnBrowse.Text = "...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtDirectory
            // 
            txtDirectory.Location = new Point(507, 447);
            txtDirectory.Name = "txtDirectory";
            txtDirectory.Size = new Size(227, 23);
            txtDirectory.TabIndex = 9;
            // 
            // btnRemoveService
            // 
            btnRemoveService.Location = new Point(647, 227);
            btnRemoveService.Name = "btnRemoveService";
            btnRemoveService.Size = new Size(131, 33);
            btnRemoveService.TabIndex = 10;
            btnRemoveService.Text = "Remove Service";
            btnRemoveService.UseVisualStyleBackColor = true;
            btnRemoveService.Click += btnRemoveService_Click;
            // 
            // btnUpdateGitHub
            // 
            btnUpdateGitHub.Location = new Point(647, 66);
            btnUpdateGitHub.Name = "btnUpdateGitHub";
            btnUpdateGitHub.Size = new Size(131, 33);
            btnUpdateGitHub.TabIndex = 1;
            btnUpdateGitHub.Text = "Update GitHub";
            btnUpdateGitHub.UseVisualStyleBackColor = true;
            btnUpdateGitHub.Click += btnUpdateGitHub_Click;
            // 
            // btnCloneGitHub
            // 
            btnCloneGitHub.Location = new Point(647, 14);
            btnCloneGitHub.Name = "btnCloneGitHub";
            btnCloneGitHub.Size = new Size(131, 33);
            btnCloneGitHub.TabIndex = 11;
            btnCloneGitHub.Text = "Clone GitHub";
            btnCloneGitHub.UseVisualStyleBackColor = true;
            btnCloneGitHub.Click += btnCloneGitHub_Click;
            // 
            // txtSearchKey
            // 
            txtSearchKey.Location = new Point(20, 12);
            txtSearchKey.Name = "txtSearchKey";
            txtSearchKey.Size = new Size(531, 23);
            txtSearchKey.TabIndex = 12;
            txtSearchKey.KeyDown += txtSearchKey_KeyDown;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(557, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(72, 23);
            btnSearch.TabIndex = 13;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // ServicesHelperForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 477);
            Controls.Add(btnSearch);
            Controls.Add(txtSearchKey);
            Controls.Add(btnCloneGitHub);
            Controls.Add(btnRemoveService);
            Controls.Add(txtDirectory);
            Controls.Add(btnBrowse);
            Controls.Add(label2);
            Controls.Add(txtGitHubUrl);
            Controls.Add(label1);
            Controls.Add(btnAddService);
            Controls.Add(btnBuildProject);
            Controls.Add(btnUpdateGitHub);
            Controls.Add(dgvServices);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ServicesHelperForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SERVICES HELPER";
            Load += ServicesHelperForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvServices).EndInit();
            contextMenuService.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvServices;
        private Button btnBuildProject;
        private Button btnAddService;
        private Label label1;
        private TextBox txtGitHubUrl;
        private Label label2;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button btnBrowse;
        private TextBox txtDirectory;
        private Button btnRemoveService;
        private Button btnUpdateGitHub;
        private Button btnCloneGitHub;
        private ContextMenuStrip contextMenuService;
        private ToolStripMenuItem menuStart;
        private ToolStripMenuItem menuStop;
        private ToolStripMenuItem menuRestart;
        private ToolStripMenuItem menuProperties;
        private TextBox txtSearchKey;
        private Button btnSearch;
    }
}
