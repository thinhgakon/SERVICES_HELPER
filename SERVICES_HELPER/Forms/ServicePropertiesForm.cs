using System;
using System.Diagnostics;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SERVICES_HELPER
{
    public partial class ServicePropertiesForm : Form
    {
        private readonly string _serviceName;
        private readonly ServicesHelperForm _helperForm;

        public ServicePropertiesForm(string serviceName, ServicesHelperForm helperForm)
        {
            _serviceName = serviceName;
            _helperForm = helperForm;
            InitializeComponent();
            LoadServiceProperties();
            this.FormClosed += (s, e) => _helperForm.LoadData();
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
                }

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
                    TextBox txtResetFailCount = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["txtResetFailCount"] as TextBox;
                    TextBox txtRestartServiceAfter = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["txtRestartServiceAfter"] as TextBox;

                    Match resetMatch = Regex.Match(output, @"RESET_PERIOD \(in seconds\) : (\d+)");
                    if (resetMatch.Success)
                    {
                        txtResetFailCount.Text = resetMatch.Groups[1].Value;
                    }
                    else
                    {
                        txtResetFailCount.Text = "0";
                    }

                    Match actionsMatch = Regex.Match(output, @"FAILURE_ACTIONS\s+:\s+([\s\S]+?)(?=\s*(REBOOT_MESSAGE|COMMAND_LINE|\Z))", RegexOptions.Singleline);
                    if (actionsMatch.Success)
                    {
                        string actionsText = actionsMatch.Groups[1].Value;
                        string[] actionLines = actionsText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] actionTypes = new string[3];
                        string restartDelay = "1";

                        int actionIndex = 0;
                        foreach (string line in actionLines)
                        {
                            if (actionIndex >= 3) break;

                            Match actionMatch = Regex.Match(line, @"(\w+)\s+--\s+Delay = (\d+)\s+milliseconds");
                            if (actionMatch.Success)
                            {
                                string action = actionMatch.Groups[1].Value.ToUpper();
                                string delay = actionMatch.Groups[2].Value;

                                if (action == "RESTART")
                                {
                                    actionTypes[actionIndex] = "Restart the Service";
                                    restartDelay = (int.Parse(delay) / 1000).ToString();
                                }
                                else if (action == "REBOOT")
                                {
                                    actionTypes[actionIndex] = "Restart the Computer";
                                }
                                else
                                {
                                    actionTypes[actionIndex] = "Take No Action";
                                }
                                actionIndex++;
                            }
                        }

                        cbbFirstFailure.SelectedItem = actionTypes[0] ?? "Take No Action";
                        cbbSecondFailure.SelectedItem = actionTypes[1] ?? "Take No Action";
                        cbbSubsequentFailures.SelectedItem = actionTypes[2] ?? "Take No Action";
                        txtRestartServiceAfter.Text = restartDelay;
                    }
                    else
                    {
                        cbbFirstFailure.SelectedItem = "Take No Action";
                        cbbSecondFailure.SelectedItem = "Take No Action";
                        cbbSubsequentFailures.SelectedItem = "Take No Action";
                        txtRestartServiceAfter.Text = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbbStartupType = this.Controls.OfType<TabControl>().First().TabPages["generalTab"].Controls["cbbStartupType"] as ComboBox;
                string selectedStartupType = cbbStartupType.SelectedItem?.ToString();
                string wmiStartupType = selectedStartupType switch
                {
                    "Automatic" => "Automatic",
                    "Manual" => "Manual",
                    "Disabled" => "Disabled",
                    _ => throw new Exception("Invalid Startup Type")
                };

                using (ManagementObject service = new ManagementObject($"Win32_Service.Name='{_serviceName}'"))
                {
                    uint result = (uint)service.InvokeMethod("ChangeStartMode", new object[] { wmiStartupType });
                    if (result != 0)
                    {
                        MessageBox.Show($"Lỗi khi thay đổi Startup Type cho {_serviceName}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                ComboBox cbbFirstFailure = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["cbbFirstFailure"] as ComboBox;
                ComboBox cbbSecondFailure = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["cbbSecondFailure"] as ComboBox;
                ComboBox cbbSubsequentFailures = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["cbbSubsequentFailures"] as ComboBox;
                TextBox txtResetFailCount = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["txtResetFailCount"] as TextBox;
                TextBox txtRestartServiceAfter = this.Controls.OfType<TabControl>().First().TabPages["recoveryTab"].Controls["txtRestartServiceAfter"] as TextBox;

                if (!int.TryParse(txtResetFailCount.Text, out int resetPeriod) || resetPeriod < 0)
                {
                    MessageBox.Show("Reset fail count after phải là số không âm!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!int.TryParse(txtRestartServiceAfter.Text, out int restartDelay) || restartDelay < 0)
                {
                    MessageBox.Show("Restart service after phải là số không âm!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string failureActions = "";
                failureActions += cbbFirstFailure.SelectedItem.ToString() switch
                {
                    "Restart the Service" => $"restart/{restartDelay}",
                    "Restart the Computer" => "reboot/0",
                    _ => "none/0"
                };
                failureActions += "/";
                failureActions += cbbSecondFailure.SelectedItem.ToString() switch
                {
                    "Restart the Service" => $"restart/{restartDelay}",
                    "Restart the Computer" => "reboot/0",
                    _ => "none/0"
                };
                failureActions += "/";
                failureActions += cbbSubsequentFailures.SelectedItem.ToString() switch
                {
                    "Restart the Service" => $"restart/{restartDelay}",
                    "Restart the Computer" => "reboot/0",
                    _ => "none/0"
                };

                ProcessStartInfo scFailure = new ProcessStartInfo
                {
                    FileName = "sc",
                    Arguments = $"failure \"{_serviceName}\" reset= {resetPeriod} actions= {failureActions}",
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.BtnApply_Click(sender, e);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
