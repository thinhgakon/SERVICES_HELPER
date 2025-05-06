using SERVICES_HELPER.Utils;
using System.Diagnostics;
using DotNetEnv;
using System.ServiceProcess;

namespace SERVICES_HELPER
{
    public partial class ServicesHelperForm : Form
    {
        public ServicesHelperForm()
        {
            InitializeComponent();
            Env.Load();
        }

        private void ServicesHelperForm_Load(object sender, EventArgs e)
        {
            this.dgvServices.DataSource = null;
            this.dgvServices.DataSource = Func.GetServices();

            if (dgvServices.Columns["Name"] != null)
            {
                dgvServices.Columns["Name"].Width = 300;
            }
            if (dgvServices.Columns["Status"] != null)
            {
                dgvServices.Columns["Status"].Width = 150;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Chọn thư mục chứa git";
                folderDialog.ShowNewFolderButton = true;
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtDirectory.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void btnCloneGitHub_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtGitHubUrl.Text))
                {
                    MessageBox.Show("Chưa nhập link github", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(this.txtDirectory.Text))
                {
                    MessageBox.Show("Chưa nhập đường dẫn thư mục", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string repoName = Path.GetFileNameWithoutExtension(this.txtGitHubUrl.Text.TrimEnd('/'));
                string repoPath = Path.Combine(this.txtDirectory.Text, repoName);

                string gitHubToken = Env.GetString("GITHUB_TOKEN");
                string gitHubUrl = this.txtGitHubUrl.Text.Replace("https://", $"https://quyettm134:{gitHubToken}@");

                ProcessStartInfo gitClone = new ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = $"clone {gitHubUrl} \"{repoPath}\"",
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

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show($"Clone GitHub error: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Clone git repo thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnUpdateGitHub_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtGitHubUrl.Text))
                {
                    MessageBox.Show("Chưa nhập link github", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(this.txtDirectory.Text))
                {
                    MessageBox.Show("Chưa nhập đường dẫn thư mục", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show($"Update GitHub error: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Cập nhật code thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnBuildProject_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtDirectory.Text))
                {
                    MessageBox.Show("Chưa nhập đường dẫn thư mục", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var slnFiles = Directory.GetFiles(this.txtDirectory.Text, "*.sln", SearchOption.AllDirectories).ToList();
                if (!slnFiles.Any())
                {
                    MessageBox.Show("Không tìm thấy file .sln trong thư mục", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nugetPath = @"C:\nuget.exe";
                string msBuildPath = @"C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe";

                foreach (var slnFile in slnFiles)
                {
                    // Nuget Restore
                    ProcessStartInfo nugetRestore = new ProcessStartInfo
                    {
                        FileName = nugetPath,
                        Arguments = $"restore \"{slnFile}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    using (Process process = new Process { StartInfo = nugetRestore })
                    {
                        process.Start();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();
                        process.WaitForExit();

                        if (!string.IsNullOrEmpty(error))
                        {
                            MessageBox.Show($"Restore NuGet error: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // MSBuild
                    ProcessStartInfo buildProject = new ProcessStartInfo
                    {
                        FileName = msBuildPath,
                        Arguments = $"\"{slnFile}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    using (Process process = new Process { StartInfo = buildProject })
                    {
                        process.Start();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();
                        process.WaitForExit();

                        if (!string.IsNullOrEmpty(error))
                        {
                            MessageBox.Show($"Build error: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                MessageBox.Show("Build project thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            try
            {
                var txtServiceFolder = string.Empty;
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Chọn thư mục service";
                    folderDialog.ShowNewFolderButton = true;
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        txtServiceFolder = folderDialog.SelectedPath;
                    }
                }

                var exeFile = string.Empty;
                string debugPath = Path.Combine(txtServiceFolder, "bin", "Debug");
                if (Directory.Exists(debugPath))
                {
                    exeFile = Directory.GetFiles(debugPath, "*.exe", SearchOption.AllDirectories).FirstOrDefault();
                }
                if (exeFile == null)
                {
                    MessageBox.Show("Không tìm thấy file .exe!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string installUtilPath = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe";
                ProcessStartInfo installService = new ProcessStartInfo
                {
                    FileName = installUtilPath,
                    Arguments = $"\"{exeFile}\"",
                    Verb = "runas",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = installService })
                {
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(error) || !output.Contains("successfully installed"))
                    {
                        MessageBox.Show($"Build service error: {error}\nOutput: {output}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Build service thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ServicesHelperForm_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void menuStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvServices.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một service!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string serviceName = this.dgvServices.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
                using (ServiceController service = new ServiceController(serviceName))
                {
                    if (service.Status == ServiceControllerStatus.Running)
                    {
                        MessageBox.Show($"Service {serviceName} đang chạy!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (service.Status != ServiceControllerStatus.Stopped)
                    {
                        MessageBox.Show($"Service {serviceName} không ở trạng thái có thể khởi động (trạng thái hiện tại: {service.Status})", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                    ServicesHelperForm_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void menuStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvServices.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một service!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string serviceName = this.dgvServices.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
                using (ServiceController service = new ServiceController(serviceName))
                {
                    if (service.Status == ServiceControllerStatus.Stopped)
                    {
                        MessageBox.Show($"Service {serviceName} đã dừng!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (!service.CanStop)
                    {
                        MessageBox.Show($"Service {serviceName} không thể dừng!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                    ServicesHelperForm_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void menuRestart_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvServices.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một service!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string serviceName = this.dgvServices.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
                using (ServiceController service = new ServiceController(serviceName))
                {
                    if (service.Status == ServiceControllerStatus.Stopped)
                    {
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                    }
                    else
                    {
                        if (!service.CanStop)
                        {
                            MessageBox.Show($"Service {serviceName} không thể dừng để khởi động lại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        service.Stop();
                        service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                    }

                    ServicesHelperForm_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void menuProperties_Click(object sender, EventArgs e)
        {
            if (this.dgvServices.SelectedCells.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một service!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string serviceName = this.dgvServices.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
            using (ServicePropertiesForm propertiesForm = new ServicePropertiesForm(serviceName))
            {
                propertiesForm.ShowDialog();
            }
        }
    }
}
