
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Campus_pulse
{
    public partial class FrmAppointment : Form
    {
        public FrmAppointment()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            txtStudentNumber.Text =
                FrmReceptionist.Session.StudentNumber;

            txtStudentNumber.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtInitials.ReadOnly = true;
            cmbGender.Enabled = false;

            LoadStudentDetails();
            LoadAppointments();

            chkDoctor1.CheckedChanged += chkDoctor1_CheckedChanged;
            chkDoctor2.CheckedChanged += chkDoctor2_CheckedChanged;
        }

        private void chkDoctor1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDoctor1.Checked)
            {
                chkDoctor2.Checked = false;
            }
        }
        
private string GetStudentSymptoms()
        {
            string symptoms = "No symptoms recorded.";

            string patientID = FrmReceptionist.Session.PatientID;
            string studentNumber = FrmReceptionist.Session.StudentNumber;

            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(Database.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT SymptomName
                FROM Symptoms
                WHERE PatientID = @PatientID
                AND StudentNumber = @StudentNumber
                ORDER BY DateRecorded DESC
                LIMIT 1";

                    using (MySqlCommand cmd =
                        new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@PatientID",
                            patientID);

                        cmd.Parameters.AddWithValue(
                            "@StudentNumber",
                            studentNumber);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            symptoms = result.ToString();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                symptoms = "Unable to retrieve symptoms: " + ex.Message;
            }

            return symptoms;
        }


        private void chkDoctor2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDoctor2.Checked)
            {
                chkDoctor1.Checked = false;
            }
        }

        private void btnSubmitAppointment_Click(object sender, EventArgs e)
        {
            // Generate a unique Appointment ID
            string appointmentID = "APP-" +
                DateTime.Now.ToString("yyyyMMddHHmmssfff");

            string selectedDoctor = "";
            

            if (chkDoctor1.Checked)
            {
                selectedDoctor = chkDoctor1.Text;
            }
            else if (chkDoctor2.Checked)
            {
                selectedDoctor = chkDoctor2.Text;
            }
            else
            {
                MessageBox.Show(
                    "Please select a doctor.",
                    "Doctor Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            string selectedSymptoms = GetStudentSymptoms();
            try
            {
                // Connect to MySQL
                using (MySqlConnection conn =
                    new MySqlConnection(Database.ConnectionString))
                {
                    conn.Open();

                    string query = @"
    INSERT INTO Appointments
    (
        AppointmentID,
        StudentNumber,
        Initials,
        AppointmentDate,
        AppointmentTime,
        Doctor,
        Reason,
        Email
    )
    VALUES
    (
        @AppointmentID,
        @StudentNumber,
        @Initials,
        @AppointmentDate,
        @AppointmentTime,
        @Doctor,
        @Reason,
        @Email
    )";

                    using (MySqlCommand cmd =
                        new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@AppointmentID",
                            appointmentID);
                        cmd.Parameters.AddWithValue(
    "@Doctor",
    selectedDoctor);

                        cmd.Parameters.AddWithValue(
                            "@StudentNumber",
                            FrmReceptionist.Session.StudentNumber);

                        cmd.Parameters.AddWithValue(
                            "@Initials",
                            txtInitials.Text.Trim());

                        cmd.Parameters.AddWithValue(
                            "@AppointmentDate",
                            dtpDate.Value.Date);

                        cmd.Parameters.AddWithValue(
                            "@AppointmentTime",
                            cmbTime.Text.Trim());

                        cmd.Parameters.AddWithValue(
                            "@Reason",
                            txtReason.Text.Trim());

                        cmd.Parameters.AddWithValue(
                            "@Email",
                            txtEmail.Text.Trim());

                        // Save appointment
                        cmd.ExecuteNonQuery();
                    }
                }

                // Send confirmation email
                try
                {
                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress(
                        "insuranceprojectcompany@gmail.com");

                    mail.To.Add(txtEmail.Text.Trim());

                    mail.Subject = "Appointment Confirmation";

                    mail.Body =
                        "Dear Student,\n\n" +

                        "Appointment ID: " +
                        appointmentID + "\n" +

                        "Student Number: " +
                        txtStudentNumber.Text + "\n" +

                        "Initials: " +
                        txtInitials.Text + "\n\n" +

                        "Your appointment has been created successfully.\n\n" +

                        "Appointment Details:\n" +

                        "Date: " +
                        dtpDate.Value.ToShortDateString() + "\n" +

                        "Time: " +
                        cmbTime.Text + "\n" +
                        "Doctor: " +
                        selectedDoctor + "\n" +
                        "Selected Symptoms: " +
                        selectedSymptoms + "\n" +

                        "Reason: " +
                        txtReason.Text + "\n\n" +

                        "Please keep your Appointment ID for future reference.\n\n" +

                        "Thank you,\n" +
                        "Campus Health Clinic";

                    SmtpClient smtp =
                        new SmtpClient("smtp.gmail.com", 587);

                    smtp.Credentials =
                        new NetworkCredential(
                            "insuranceprojectcompany@gmail.com",
                            "fsti aoby fste bgws");

                    smtp.EnableSsl = true;

                    smtp.Send(mail);

                    MessageBox.Show(
                        "Appointment created successfully!\n\n" +
                        "Appointment ID: " +
                        appointmentID +
                        "\n\nConfirmation email sent.",
                        "Appointment Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception emailEx)
                {
                    MessageBox.Show(
                        "Appointment was saved successfully.\n\n" +
                        "However, the confirmation email could not be sent.\n\n" +
                        emailEx.Message,
                        "Appointment Saved",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                ClearFields();
                LoadAppointments();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(
                    "MySQL Database Error:\n\n" +
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            // Student Number is kept because it comes from the session
            txtReason.Clear();
            cmbTime.SelectedIndex = -1;

            chkDoctor1.Checked = false;
            chkDoctor2.Checked = false;

            dtpDate.Value = DateTime.Today;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void LoadStudentDetails()
        {
            string studentNumber = FrmReceptionist.Session.StudentNumber;

            using (MySqlConnection conn =
                new MySqlConnection(Database.ConnectionString))
            {
                conn.Open();

                string query = @"
            SELECT FullName, Email, Sex
            FROM Users
            WHERE StudentNumber = @StudentNumber";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentNumber", studentNumber);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtEmail.Text = reader["Email"].ToString();
                            cmbGender.Text = reader["Sex"].ToString();

                            string fullName = reader["FullName"].ToString();

                            // Get initials from the full name
                            string[] names = fullName.Split(' ');

                            string initials = "";

                            foreach (string name in names)
                            {
                                if (!string.IsNullOrWhiteSpace(name))
                                    initials += name[0].ToString().ToUpper();
                            }

                            txtInitials.Text = initials;
                        }
                    }
                }
            }
        }
        private void LoadAppointments()
        {
            string query = @"
        SELECT
            AppointmentID,
            StudentNumber,
            Initials,
            AppointmentDate,
            AppointmentTime,
            Doctor,
            Reason,
            Email
        FROM Appointments
        ORDER BY AppointmentDate DESC";

            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(Database.ConnectionString))
                {
                    conn.Open();

                    using (MySqlDataAdapter adapter =
                        new MySqlDataAdapter(query, conn))
                    {
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        dgvAppointments.DataSource = table;
                    }
                }

                dgvAppointments.ReadOnly = true;
                dgvAppointments.AllowUserToAddRows = false;
                dgvAppointments.AllowUserToDeleteRows = false;
                dgvAppointments.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                dgvAppointments.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(
                    "Database Error:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void FrmAppointment_Load(object sender, EventArgs e)
        {
            txtStudentNumber.Text = FrmReceptionist.Session.StudentNumber;
        
        }

        // Back to Main Menu
        private void button1_Click(object sender, EventArgs e)
        {
            FrmReceptionist reception = new FrmReceptionist();
            reception.Show();
            this.Hide();
            return;
        }

        // Exit application
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Hide();
            return;
        }

        private void DropDownList(object sender, EventArgs e)
        {

        }
    }
}

