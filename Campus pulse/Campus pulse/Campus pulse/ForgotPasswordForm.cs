using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campus_pulse
{
    public partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmLogin admin = new FrmLogin();

            admin.Show();
            this.Hide();
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder result = new StringBuilder();

                foreach (byte b in hash)
                {
                    result.Append(b.ToString("x2"));
                }

                return result.ToString();
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string newPassword = txtNewPassword.Text;

            if (email == "" || newPassword == "")
            {
                MessageBox.Show("Please enter your email and new password.");
                return;
            }

            // Hash the new password
            
            string hashedPassword = HashPassword(txtNewPassword.Text);
            string connectionString =
                "server=localhost;database=healthsystemdb;" +
                "user=root;password=nibbles123;";

            using (MySqlConnection con =
                new MySqlConnection(connectionString))
            {
                con.Open();

                string query =
                    "UPDATE staffrecord SET Password = @password " +
                    "WHERE Email = @email";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@password", hashedPassword);

                    cmd.Parameters.AddWithValue("@email", email);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Password reset successfully!");

                        FrmLogin login = new FrmLogin();
                        login.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Email not found.");
                    }
                }
            }
        }
    }
}
