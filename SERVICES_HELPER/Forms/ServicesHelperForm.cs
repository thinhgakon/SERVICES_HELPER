using SERVICES_HELPER.Utils;
using System.Diagnostics;
using DotNetEnv;
using System.ServiceProcess;
using System.Management;

namespace SERVICES_HELPER
{
    public partial class ServicesHelperForm : Form
    {
        private bool isGuest = false;
        private string userName = string.Empty;

        public ServicesHelperForm(string userName, bool isGuest)
        {
            InitializeComponent();
            Env.Load();
            this.userName = userName;
            this.isGuest = isGuest;
        }

        private void ServicesHelperForm_Load(object sender, EventArgs e)
        {
            this.cbbFilter.SelectedIndex = 0;
            LoadData();
        }

        public void LoadData()
        {
            this.dgvServices.DataSource = null;
            this.dgvServices.DataSource = Func.GetServices(this.cbbFilter.Text, this.txtSearchKey.Text);
            this.txtGitHubUrl.Text = "https://github.com/thinhgakon/TAMDIEP_SERVICES";
            this.txtDirectory.Text = "D:/PROD_SERVICE";
            this.txtUserName.Text = this.userName;
            
            this.btnCloneGitHub.Visible = !this.isGuest;
            this.btnUpdateGitHub.Visible = !this.isGuest;
            this.btnBuildProject.Visible = !this.isGuest;

            if (dgvServices.Columns["Name"] != null)
            {
                dgvServices.Columns["Name"].Width = 300;
            }
            if (dgvServices.Columns["Status"] != null)
            {
                dgvServices.Columns["Status"].Width = 150;
            }
            if (dgvServices.Columns["StartType"] != null)
            {
                dgvServices.Columns["StartType"].Width = 128;
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

        private async void btnCloneGitHub_Click(object sender, EventArgs e)
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
                string gitHubUrl = this.txtGitHubUrl.Text.Replace("https://", $"https://quyettm134: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@");

                this.taskProgressor.Visible = true;
                DisableControls();

                await Task.Run(() =>
                {
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
                    }

                    Invoke(() =>
                    {
                        MessageBox.Show("Clone git thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                this.taskProgressor.Visible = false;
                EnableControls();
            }
        }

        private async void btnUpdateGitHub_Click(object sender, EventArgs e)
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

                this.taskProgressor.Visible = true;
                DisableControls();

                await Task.Run(() =>
                {
                    ProcessStartInfo gitPull = new ProcessStartInfo
                    {
                        FileName = "git",
                        Arguments = $"pull",
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

                    Invoke(() =>
                    {
                        MessageBox.Show("Cập nhật code thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                this.taskProgressor.Visible = false;
                EnableControls();
            }
        }

        private async void btnBuildProject_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtDirectory.Text))
                {
                    MessageBox.Show("Chưa nhập đường dẫn thư mục", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var slnFile = Directory.GetFiles(this.txtDirectory.Text, "*.sln", SearchOption.AllDirectories).FirstOrDefault();
                if (slnFile == null)
                {
                    MessageBox.Show("Không tìm thấy file .sln trong thư mục", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.taskProgressor.Visible = true;
                DisableControls();

                await Task.Run(() =>
                {
                    // Nuget Restore
                    string nugetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utils", "nuget.exe");
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
                    string msBuildPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utils", "Enterprise", "MSBuild", "Current", "Bin", "MSBuild.exe");
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

                    Invoke(() =>
                    {
                        MessageBox.Show("Build project success!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                this.taskProgressor.Visible = false;
                EnableControls();
            }
        }

        private async void btnAddService_Click(object sender, EventArgs e)
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
                    else
                    {
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtServiceFolder))
                {
                    return;
                }

                this.taskProgressor.Visible = true;
                DisableControls();

                await Task.Run(() =>
                {
                    var exeFile = Directory.GetFiles(txtServiceFolder, "*.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
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

                        if (!string.IsNullOrEmpty(error))
                        {
                            MessageBox.Show($"Build service error: {error}\nOutput: {output}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    Invoke(() =>
                    {
                        MessageBox.Show("Build service success!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                this.taskProgressor.Visible = false;
                EnableControls();
            }
        }

        private async void btnRemoveService_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvServices.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một service!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.taskProgressor.Visible = true;
                DisableControls();

                await Task.Run(() =>
                {
                    string serviceName = this.dgvServices.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
                    var exeFile = string.Empty;
                    using (ManagementObject service = new ManagementObject($"Win32_Service.Name='{serviceName}'"))
                    {
                        exeFile = service["PathName"]?.ToString() ?? "N/A";
                    }

                    string installUtilPath = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe";
                    ProcessStartInfo installService = new ProcessStartInfo
                    {
                        FileName = installUtilPath,
                        Arguments = $"-u \"{exeFile}\"",
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

                        if (!string.IsNullOrEmpty(error))
                        {
                            MessageBox.Show($"Remove service error: {error}\nOutput: {output}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    Invoke(() =>
                    {
                        MessageBox.Show("Remove service success!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                this.taskProgressor.Visible = false;
                EnableControls();
            }
        }

        private async void menuStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvServices.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một service!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.taskProgressor.Visible = true;
                DisableControls();

                await Task.Run(() =>
                {
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

                        Invoke(() =>
                        {
                            LoadData();
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                this.taskProgressor.Visible = false;
                EnableControls();
            }
        }

        private async void menuStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvServices.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một service!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.taskProgressor.Visible = true;
                DisableControls();

                await Task.Run(() =>
                {
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

                        Invoke(() =>
                        {
                            LoadData();
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                this.taskProgressor.Visible = false;
                EnableControls();
            }
        }

        private async void menuRestart_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvServices.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một service!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.taskProgressor.Visible = true;
                DisableControls();

                await Task.Run(() =>
                {
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

                        Invoke(() =>
                        {
                            LoadData();
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                this.taskProgressor.Visible = false;
                EnableControls();
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
            using (ServicePropertiesForm propertiesForm = new ServicePropertiesForm(serviceName, this))
            {
                propertiesForm.ShowDialog();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData();
                e.SuppressKeyPress = true;
            }
        }

        private void DisableControls()
        {
            foreach (Control control in this.Controls)
            {
                control.Enabled = false;
            }
            this.Enabled = true;
        }

        private void EnableControls()
        {
            foreach (Control control in this.Controls)
            {
                control.Enabled = true;
            }
        }
    }
}
