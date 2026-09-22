using MySql.Data.MySqlClient;
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
    public partial class StaffRecords : Form
    {
        public StaffRecords()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            cmbRole.Items.Add("Clinic Administrator");
            cmbRole.Items.Add("System Administrator");
            cmbRole.Items.Add("Receptionist");

            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Check that all fields are filled
            if (string.IsNullOrWhiteSpace(txtEmployeeNumber.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(cmbRole.Text))
            {
                MessageBox.Show("Please fill in all fields.",
                                "Missing Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string connectionString = "server=localhost;database=healthsystemdb;user=root;password=12345678;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if employee number already exists
                    string checkQuery = "SELECT COUNT(*) FROM staffRecord WHERE EmployeeNumber = @EmployeeNumber";

                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@EmployeeNumber", txtEmployeeNumber.Text.Trim());

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("This employee number is already registered.",
                                            "Registration Error",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Hash the password
                    string hashedPassword = HashPassword(txtPassword.Text);

                    // Insert staff record
                    string insertQuery = @"
                INSERT INTO staffRecord
                (EmployeeNumber, FullName, Email, Password, PhoneNumber, Role)
                VALUES
                (@EmployeeNumber, @FullName, @Email, @Password, @PhoneNumber, @Role)";

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeNumber", txtEmployeeNumber.Text.Trim());
                        command.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                        command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text.Trim());
                        command.Parameters.AddWithValue("@Role", cmbRole.Text.Trim());

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Staff registered successfully!",
                                    "Registration Successful",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    // Clear fields
                    txtEmployeeNumber.Clear();
                    txtFullName.Clear();
                    txtEmail.Clear();
                    txtPassword.Clear();
                    txtPhoneNumber.Clear();
                    cmbRole.SelectedIndex = -1;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
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

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAdmin admin = new FrmAdmin();
            admin.Show();
            this.Hide();
            return;
        }
    }
 }

