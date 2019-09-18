using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDDS.Login
{
    public partial class Auth : Form
    {
        public event AuthHandler Authenticate;

        public Auth()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbUsername.Text) && !string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                Authenticate?.Invoke(this, new AuthArgs(tbUsername.Text, tbPassword.Text));
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                DialogResult = DialogResult.Abort;
        }

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.SelectAll();
        }
    }

    public delegate void AuthHandler(object source, AuthArgs e);

    public class AuthArgs : EventArgs
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public AuthArgs(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
