using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SERVICES_HELPER
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUsername.Text))
            {
                MessageBox.Show("Username không được để trống!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(this.txtPassword.Text))
            {
                MessageBox.Show("Password không được để trống!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.txtUsername.Text == "admin" && this.txtPassword.Text == "admin")
            {
                ServicesHelperForm servicesForm = new ServicesHelperForm("Admin", false);
                servicesForm.FormClosed += (s, args) =>
                {
                    this.Show();
                    this.txtUsername.Text = string.Empty;
                    this.txtPassword.Text = string.Empty;
                };
                servicesForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnGuestLogin_Click(object sender, EventArgs e)
        {
            ServicesHelperForm servicesForm = new ServicesHelperForm("Guest", true);
            servicesForm.FormClosed += (s, args) =>
            {
                this.Show();
                this.txtUsername.Text = string.Empty;
                this.txtPassword.Text = string.Empty;
            };
            servicesForm.Show();
            this.Hide();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
    }
}
