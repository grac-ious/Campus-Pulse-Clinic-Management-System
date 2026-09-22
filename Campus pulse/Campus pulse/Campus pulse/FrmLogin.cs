using System;
using System.Drawing;
using System.Windows.Forms;

namespace Campus_pulse
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Please enter username and password.",
                    "Login Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                AuthenticationManager authenticationManager =
                    new AuthenticationManager();

                SystemUser user =
                    authenticationManager.Login(username, password);

                if (user == null)
                {
                    MessageBox.Show(
                        "Invalid username, password, or account is inactive.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                // Store the logged-in user's information
                Session.UserID = user.UserID;
                Session.Username = user.Username;
                Session.FullName = user.FullName;
                Session.RoleName = user.RoleName;

                // Open the correct dashboard according to the user's role
                if (user.RoleName == "Clinic Administrator")
                {
                    ClinicAdministrator admin =
                        new ClinicAdministrator();

                    admin.Show();
                    this.Hide();
                }
                else if (user.RoleName == "System Administrator")
                {
                    FrmAdmin admin =
                        new FrmAdmin();

                    admin.Show();
                    this.Hide();
                }
                else if (user.RoleName == "Receptionist")
                {
                    FrmReceptionist receptionist =
                        new FrmReceptionist();

                    receptionist.Show();
                    this.Hide();
                }
                else
                {
                    Session.Clear();

                    MessageBox.Show(
                        "The user's role is not recognised.",
                        "Login Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An error occurred while logging in:\n\n" + ex.Message,
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            lblForgotPassword.BackColor = Color.Blue;
            lblForgotPassword.ForeColor = Color.White;
            lblForgotPassword.TextAlign =
                ContentAlignment.MiddleCenter;
            lblForgotPassword.Cursor = Cursors.Hand;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgotPassword =
                new ForgotPasswordForm();

            forgotPassword.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void txtStudentNumber_TextChanged(object sender, EventArgs e)
        {
        }
    }
}