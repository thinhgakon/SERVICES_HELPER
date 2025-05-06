using System;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;

namespace SERVICES_HELPER
{
    public partial class ServicePropertiesForm : Form
    {
        private readonly string _serviceName;

        public ServicePropertiesForm(string serviceName)
        {
            _serviceName = serviceName;
            InitializeComponent();
            LoadServiceProperties();
        }

        private void LoadServiceProperties()
        {
            try
            {
                this.lblServiceName.Text = _serviceName;

                using (ManagementObject service = new ManagementObject($"Win32_Service.Name='{_serviceName}'"))
                {
                    this.txtExePath.Text = service["PathName"]?.ToString() ?? "N/A";

                    string startMode = service["StartMode"]?.ToString();
                    ComboBox cbbStartupType = this.Controls.OfType<TabControl>().First().TabPages["generalTab"].Controls["cbbStartupType"] as ComboBox;
                    if (startMode == "Auto")
                        cbbStartupType.SelectedItem = "Automatic";
                    else if (startMode == "Manual")
                        cbbStartupType.SelectedItem = "Manual";
                    else if (startMode == "Disabled")
                        cbbStartupType.SelectedItem = "Disabled";

                    ProcessStartInfo scQuery = new ProcessStartInfo
                    {
                        FileName = "sc",
                        Arguments = $"qfailure \"{_serviceName}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    using (Process process = new Process { StartInfo = scQuery })
                    {
                        process.Start();
                        string output = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();

                        ComboBox cbbFirstFailure = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["cbbFirstFailure"] as ComboBox;
                        ComboBox cbbSecondFailure = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["cbbSecondFailure"] as ComboBox;
                        ComboBox cbbSubsequentFailures = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["cbbSubsequentFailures"] as ComboBox;

                        if (output.Contains("RESTART"))
                        {
                            cbbFirstFailure.SelectedItem = "Restart the Service";
                            cbbSecondFailure.SelectedItem = "Restart the Service";
                            cbbSubsequentFailures.SelectedItem = "Restart the Service";
                        }
                        else
                        {
                            cbbFirstFailure.SelectedItem = "Take No Action";
                            cbbSecondFailure.SelectedItem = "Take No Action";
                            cbbSubsequentFailures.SelectedItem = "Take No Action";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải properties: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                // Cập nhật Startup Type
                ComboBox cbbStartupType = this.Controls.OfType<TabControl>().First().TabPages["generalTab"].Controls["cbbStartupType"] as ComboBox;
                string selectedStartupType = cbbStartupType.SelectedItem?.ToString();
                string wmiStartupType = selectedStartupType switch
                {
                    "Automatic" => "Auto",
                    "Manual" => "Manual",
                    "Disabled" => "Disabled",
                    _ => throw new Exception("Invalid Startup Type")
                };

                using (ManagementObject service = new ManagementObject($"Win32_Service.Name='{_serviceName}'"))
                {
                    ManagementBaseObject result = (ManagementBaseObject)service.InvokeMethod("ChangeStartMode", new object[] { wmiStartupType });
                    if ((uint)result["ReturnValue"] != 0)
                    {
                        MessageBox.Show($"Lỗi khi thay đổi Startup Type cho {_serviceName}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Cập nhật Recovery settings
                ComboBox cbbFirstFailure = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["cbbFirstFailure"] as ComboBox;
                ComboBox cbbSecondFailure = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["cbbSecondFailure"] as ComboBox;
                ComboBox cbbSubsequentFailures = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["cbbSubsequentFailures"] as ComboBox;

                string failureActions = "";
                if (cbbFirstFailure.SelectedItem.ToString() == "Restart the Service")
                    failureActions += " restart/60000";
                else
                    failureActions += " none/0";
                if (cbbSecondFailure.SelectedItem.ToString() == "Restart the Service")
                    failureActions += " restart/60000";
                else
                    failureActions += " none/0";
                if (cbbSubsequentFailures.SelectedItem.ToString() == "Restart the Service")
                    failureActions += " restart/60000";
                else
                    failureActions += " none/0";

                ProcessStartInfo scFailure = new ProcessStartInfo
                {
                    FileName = "sc",
                    Arguments = $"failure \"{_serviceName}\" reset= 86400 actions= {failureActions}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (Process process = new Process { StartInfo = scFailure })
                {
                    process.Start();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show($"Cập nhật thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
