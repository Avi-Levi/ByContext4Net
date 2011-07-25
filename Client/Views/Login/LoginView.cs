using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;


namespace Client.Views.Login
{
    public partial class LoginView : UserControl, ILoginView
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public event EventHandler Login;

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (this.Login != null)
            {
                this.Login(this, EventArgs.Empty);
            }
        }

        public LoginRequest GetLoginInfo()
        {
            LoginRequest login = new LoginRequest { UserName = this.txt_name.Text, Password = this.txt_password.Text };
            return login;
        }

        public void Clear()
        {
            this.txt_name.Text = this.txt_password.Text = string.Empty;
        }
    }
}
