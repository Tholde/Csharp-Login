using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDMysql
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void loginLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text.Trim();
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            string confpass = confirmPasswordTextBox.Text.Trim();
            //MessageBox.Show("Button clicked");
            if (nameTextBox.Text.Trim().Length < 3 || usernameTextBox.Text.Trim().Length < 3)
            {
                MessageBox.Show("Fullname or username are empty or ( >3 ).");
                return;
            }
            if (passwordTextBox.Text.Trim().Length < 8 || confirmPasswordTextBox.Text.Trim().Length < 8)
            {
                MessageBox.Show("Password are empty or ( >8 character ).");
                return;
            }
            if(passwordTextBox.Text == confirmPasswordTextBox.Text)
            {
                User user = new User
                {
                    Fullname = nameTextBox.Text,
                    Username = usernameTextBox.Text,
                    Password = passwordTextBox.Text,
                };
                DBUser dBUser = new DBUser();
                bool success = dBUser.InsertUser(user);
                if (success)
                {
                    MessageBox.Show("Registration Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Login login = new Login();
                    login.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Registration no much.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void nameTextBox_Click(object sender, EventArgs e)
        {
            nameTextBox.Text = "";
        }

        private void usernameTextBox_Click(object sender, EventArgs e)
        {
            usernameTextBox.Text = "";
        }

        private void passwordTextBox_Click(object sender, EventArgs e)
        {
            passwordTextBox.Text = "";
            passwordTextBox.PasswordChar = '*';
        }

        private void confirmPasswordTextBox_Click(object sender, EventArgs e)
        {
            confirmPasswordTextBox.Text = "";
            confirmPasswordTextBox.PasswordChar = '*';
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            nameTextBox.Text = "";
            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
            confirmPasswordTextBox.Text = "";
        }
    }
}
