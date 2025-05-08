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

        }

        private void btnGuestLogin_Click(object sender, EventArgs e)
        {
            ServicesHelperForm servicesForm = new ServicesHelperForm("Guest", true);
            servicesForm.FormClosed += (s, args) => this.Show();
            servicesForm.Show();
            this.Hide();
        }
    }
}
