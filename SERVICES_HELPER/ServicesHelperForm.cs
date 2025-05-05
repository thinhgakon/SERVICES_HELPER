using SERVICES_HELPER.Utils;
using System.Diagnostics;

namespace SERVICES_HELPER
{
    public partial class ServicesHelperForm : Form
    {
        public ServicesHelperForm()
        {
            InitializeComponent();
        }

        private void ServicesHelperForm_Load(object sender, EventArgs e)
        {
            this.dgvServices.DataSource = null;
            this.dgvServices.DataSource = Func.GetServices();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtGitHubUrl.Text))
                {
                    MessageBox.Show("Chưa nhập link github", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(this.txtDirectory.Text))
                {
                    MessageBox.Show("Chưa nhập đường dẫn thư mục", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Directory.Exists(this.txtDirectory.Text))
                {
                    ProcessStartInfo gitPull = new ProcessStartInfo
                    {
                        FileName = "git",
                        Arguments = $"pull origin main",
                        WorkingDirectory = this.txtDirectory.Text,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    using (Process process = new Process { StartInfo = gitPull })
                    {
                        process.Start();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();
                        process.WaitForExit();
                    }
                }

                else
                {
                    string formattedUrl = this.txtGitHubUrl.Text.Replace("https://", $"https://quyettm134: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@");
                    ProcessStartInfo gitClone = new ProcessStartInfo
                    {
                        FileName = "git",
                        Arguments = $"clone {formattedUrl} \"{this.txtDirectory.Text}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    using (Process process = new Process { StartInfo = gitClone })
                    {
                        process.Start();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();
                        process.WaitForExit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
