namespace SERVICES_HELPER
{
    partial class ServicePropertiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServicePropertiesForm));
            tabControl1 = new TabControl();
            generalTab = new TabPage();
            txtExePath = new TextBox();
            label4 = new Label();
            richTextBox1 = new RichTextBox();
            cbbStartupType = new ComboBox();
            label3 = new Label();
            lblServiceName = new Label();
            label2 = new Label();
            label1 = new Label();
            recoveryTab = new TabPage();
            label11 = new Label();
            label10 = new Label();
            txtRestartServiceAfter = new TextBox();
            txtResetFailCount = new TextBox();
            cbbSubsequentFailures = new ComboBox();
            cbbSecondFailure = new ComboBox();
            cbbFirstFailure = new ComboBox();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            btnOK = new Button();
            btnCancel = new Button();
            btnApply = new Button();
            tabControl1.SuspendLayout();
            generalTab.SuspendLayout();
            recoveryTab.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(generalTab);
            tabControl1.Controls.Add(recoveryTab);
            tabControl1.Dock = DockStyle.Top;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(365, 234);
            tabControl1.TabIndex = 0;
            // 
            // generalTab
            // 
            generalTab.Controls.Add(txtExePath);
            generalTab.Controls.Add(label4);
            generalTab.Controls.Add(richTextBox1);
            generalTab.Controls.Add(cbbStartupType);
            generalTab.Controls.Add(label3);
            generalTab.Controls.Add(lblServiceName);
            generalTab.Controls.Add(label2);
            generalTab.Controls.Add(label1);
            generalTab.Location = new Point(4, 24);
            generalTab.Name = "generalTab";
            generalTab.Padding = new Padding(3);
            generalTab.Size = new Size(357, 206);
            generalTab.TabIndex = 0;
            generalTab.Text = "General";
            generalTab.UseVisualStyleBackColor = true;
            // 
            // txtExePath
            // 
            txtExePath.Location = new Point(103, 45);
            txtExePath.Name = "txtExePath";
            txtExePath.ReadOnly = true;
            txtExePath.ScrollBars = ScrollBars.Horizontal;
            txtExePath.Size = new Size(239, 23);
            txtExePath.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(9, 82);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 12;
            label4.Text = "Description:";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(104, 79);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(238, 57);
            richTextBox1.TabIndex = 11;
            richTextBox1.Text = "";
            // 
            // cbbStartupType
            // 
            cbbStartupType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbStartupType.FormattingEnabled = true;
            cbbStartupType.Items.AddRange(new object[] { "Automatic", "Manual", "Disabled" });
            cbbStartupType.Location = new Point(105, 153);
            cbbStartupType.Name = "cbbStartupType";
            cbbStartupType.Size = new Size(237, 23);
            cbbStartupType.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 155);
            label3.Name = "label3";
            label3.Size = new Size(77, 15);
            label3.TabIndex = 9;
            label3.Text = "Startup type: ";
            // 
            // lblServiceName
            // 
            lblServiceName.AutoSize = true;
            lblServiceName.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblServiceName.Location = new Point(105, 18);
            lblServiceName.Name = "lblServiceName";
            lblServiceName.Size = new Size(40, 15);
            lblServiceName.TabIndex = 4;
            lblServiceName.Text = "label4";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(9, 47);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 1;
            label2.Text = "Exe path:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 17);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 0;
            label1.Text = "Service name:";
            // 
            // recoveryTab
            // 
            recoveryTab.Controls.Add(label11);
            recoveryTab.Controls.Add(label10);
            recoveryTab.Controls.Add(txtRestartServiceAfter);
            recoveryTab.Controls.Add(txtResetFailCount);
            recoveryTab.Controls.Add(cbbSubsequentFailures);
            recoveryTab.Controls.Add(cbbSecondFailure);
            recoveryTab.Controls.Add(cbbFirstFailure);
            recoveryTab.Controls.Add(label9);
            recoveryTab.Controls.Add(label8);
            recoveryTab.Controls.Add(label7);
            recoveryTab.Controls.Add(label6);
            recoveryTab.Controls.Add(label5);
            recoveryTab.Location = new Point(4, 24);
            recoveryTab.Name = "recoveryTab";
            recoveryTab.Padding = new Padding(3);
            recoveryTab.Size = new Size(357, 206);
            recoveryTab.TabIndex = 1;
            recoveryTab.Text = "Recovery";
            recoveryTab.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(261, 167);
            label11.Name = "label11";
            label11.Size = new Size(50, 15);
            label11.TabIndex = 11;
            label11.Text = "minutes";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(261, 131);
            label10.Name = "label10";
            label10.Size = new Size(31, 15);
            label10.TabIndex = 10;
            label10.Text = "days";
            // 
            // txtRestartServiceAfter
            // 
            txtRestartServiceAfter.Location = new Point(172, 164);
            txtRestartServiceAfter.Name = "txtRestartServiceAfter";
            txtRestartServiceAfter.Size = new Size(81, 23);
            txtRestartServiceAfter.TabIndex = 9;
            // 
            // txtResetFailCount
            // 
            txtResetFailCount.Location = new Point(172, 127);
            txtResetFailCount.Name = "txtResetFailCount";
            txtResetFailCount.Size = new Size(81, 23);
            txtResetFailCount.TabIndex = 8;
            // 
            // cbbSubsequentFailures
            // 
            cbbSubsequentFailures.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbSubsequentFailures.FormattingEnabled = true;
            cbbSubsequentFailures.Items.AddRange(new object[] { "Take No Action", "Restart the Service", "Restart the Computer" });
            cbbSubsequentFailures.Location = new Point(172, 91);
            cbbSubsequentFailures.Name = "cbbSubsequentFailures";
            cbbSubsequentFailures.Size = new Size(177, 23);
            cbbSubsequentFailures.TabIndex = 7;
            // 
            // cbbSecondFailure
            // 
            cbbSecondFailure.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbSecondFailure.FormattingEnabled = true;
            cbbSecondFailure.Items.AddRange(new object[] { "Take No Action", "Restart the Service", "Restart the Computer" });
            cbbSecondFailure.Location = new Point(172, 55);
            cbbSecondFailure.Name = "cbbSecondFailure";
            cbbSecondFailure.Size = new Size(177, 23);
            cbbSecondFailure.TabIndex = 6;
            // 
            // cbbFirstFailure
            // 
            cbbFirstFailure.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbFirstFailure.FormattingEnabled = true;
            cbbFirstFailure.Items.AddRange(new object[] { "Take No Action", "Restart the Service", "Restart the Computer" });
            cbbFirstFailure.Location = new Point(172, 21);
            cbbFirstFailure.Name = "cbbFirstFailure";
            cbbFirstFailure.Size = new Size(177, 23);
            cbbFirstFailure.TabIndex = 5;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(24, 167);
            label9.Name = "label9";
            label9.Size = new Size(112, 15);
            label9.TabIndex = 4;
            label9.Text = "Restart service after:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(24, 130);
            label8.Name = "label8";
            label8.Size = new Size(118, 15);
            label8.TabIndex = 3;
            label8.Text = "Reset fail count after:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(24, 93);
            label7.Name = "label7";
            label7.Size = new Size(113, 15);
            label7.TabIndex = 2;
            label7.Text = "Subsequent failures:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(24, 57);
            label6.Name = "label6";
            label6.Size = new Size(85, 15);
            label6.TabIndex = 1;
            label6.Text = "Second failure:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 23);
            label5.Name = "label5";
            label5.Size = new Size(68, 15);
            label5.TabIndex = 0;
            label5.Text = "First failure:";
            // 
            // btnOK
            // 
            btnOK.Location = new Point(113, 236);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 12;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(200, 236);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(286, 236);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(75, 23);
            btnApply.TabIndex = 14;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += BtnApply_Click;
            // 
            // ServicePropertiesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(365, 266);
            Controls.Add(btnApply);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ServicePropertiesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Properties";
            tabControl1.ResumeLayout(false);
            generalTab.ResumeLayout(false);
            generalTab.PerformLayout();
            recoveryTab.ResumeLayout(false);
            recoveryTab.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage generalTab;
        private Label label2;
        private Label label1;
        private TabPage recoveryTab;
        private Label lblServiceName;
        private ComboBox cbbStartupType;
        private Label label3;
        private Label label4;
        private RichTextBox richTextBox1;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label11;
        private Label label10;
        private TextBox txtRestartServiceAfter;
        private TextBox txtResetFailCount;
        private ComboBox cbbSubsequentFailures;
        private ComboBox cbbSecondFailure;
        private ComboBox cbbFirstFailure;
        private Button btnOK;
        private Button btnCancel;
        private Button btnApply;
        private TextBox txtExePath;
    }
}