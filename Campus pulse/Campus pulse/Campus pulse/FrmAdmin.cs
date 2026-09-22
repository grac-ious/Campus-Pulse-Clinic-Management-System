using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Campus_pulse
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

          
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
            Reason,
            Email
        FROM Appointments
        ORDER BY AppointmentDate DESC";

            using (MySqlConnection conn =
                new MySqlConnection(Database.ConnectionString))
            {
                MySqlDataAdapter adapter =
                    new MySqlDataAdapter(query, conn);

                DataTable table = new DataTable();
                adapter.Fill(table);

                dgvAppointments.DataSource = table;
            }
        }
        private void LoadStudentRecords()
        {
            string query = @"
        SELECT
            PatientID,
            StudentNumber,
            FullName,
            Email,
            Sex,
            ResidentialAddress
        FROM Users
        ORDER BY PatientID DESC";

            using (MySqlConnection conn =
                new MySqlConnection(Database.ConnectionString))
            {
                MySqlDataAdapter adapter =
                    new MySqlDataAdapter(query, conn);

                DataTable table = new DataTable();
                adapter.Fill(table);

                dgvAppointments.DataSource = table;
            }
        }

        private void FrmAdmin_Load_1(object sender, EventArgs e)
        {
            // You can leave this empty or remove it
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();

            login.Show();

            this.Close();
        }

        private void btnSeen_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a record first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                DataGridViewRow row = dgvAppointments.SelectedRows[0];

                using (MySqlConnection conn =
                    new MySqlConnection(Database.ConnectionString))
                {
                    string query = "";

                    // Appointment records
                    if (dgvAppointments.Columns.Contains("AppointmentID"))
                    {
                        string id =
                            row.Cells["AppointmentID"].Value.ToString();

                        query = "DELETE FROM Appointments " +
                                "WHERE AppointmentID = @ID";

                        using (MySqlCommand cmd =
                            new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Student records
                    else if (dgvAppointments.Columns.Contains("PatientID"))
                    {
                        string id =
                            row.Cells["PatientID"].Value.ToString();

                        query = "DELETE FROM Users " +
                                "WHERE PatientID = @ID";

                        using (MySqlCommand cmd =
                            new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Staff records
                    else if (dgvAppointments.Columns.Contains("EmployeeNumber"))
                    {
                        string id =
                            row.Cells["EmployeeNumber"].Value.ToString();

                        query = "DELETE FROM staffRecord " +
                                "WHERE EmployeeNumber = @ID";

                        using (MySqlCommand cmd =
                            new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    else
                    {
                        MessageBox.Show(
                            "The selected record type cannot be identified.",
                            "Delete Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }
                }

                MessageBox.Show(
                    "Record deleted successfully.",
                    "Deleted",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Refresh the table
                dgvAppointments.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not delete the record.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // Refresh appointments
                if (dgvAppointments.Columns.Contains("AppointmentID"))
                {
                    LoadAppointments();
                }

                // Refresh student records
                else if (dgvAppointments.Columns.Contains("PatientID"))
                {
                    LoadStudentRecords();
                }

                // Refresh staff records
                else if (dgvAppointments.Columns.Contains("EmployeeNumber"))
                {
                    LoadStaffRecords();
                }

                else
                {
                    MessageBox.Show(
                        "There are no records to refresh.",
                        "Refresh",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                MessageBox.Show(
                    "Records refreshed successfully.",
                    "Refresh",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not refresh records.\n\n" + ex.Message,
                    "Refresh Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // If nothing is loaded, load all appointment records first
            if (dgvAppointments.Rows.Count == 0)
            {
                LoadAppointments();
                return;
            }

            // Check if a row is selected
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Select an appointment record to update.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                DataGridViewRow row = dgvAppointments.SelectedRows[0];

                string appointmentID =
                    row.Cells["AppointmentID"].Value.ToString();

                string query = @"
            UPDATE Appointments
            SET
                StudentNumber = @StudentNumber,
                Initials = @Initials,
                AppointmentDate = @AppointmentDate,
                AppointmentTime = @AppointmentTime,
                Reason = @Reason,
                Email = @Email
            WHERE AppointmentID = @AppointmentID";

                using (MySqlConnection conn =
                    new MySqlConnection(Database.ConnectionString))
                {
                    using (MySqlCommand cmd =
                        new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@AppointmentID",
                            appointmentID);

                        cmd.Parameters.AddWithValue(
                            "@StudentNumber",
                            row.Cells["StudentNumber"].Value);

                        cmd.Parameters.AddWithValue(
                            "@Initials",
                            row.Cells["Initials"].Value);

                        cmd.Parameters.AddWithValue(
                            "@AppointmentDate",
                            row.Cells["AppointmentDate"].Value);

                        cmd.Parameters.AddWithValue(
                            "@AppointmentTime",
                            row.Cells["AppointmentTime"].Value);

                        cmd.Parameters.AddWithValue(
                            "@Reason",
                            row.Cells["Reason"].Value);

                        cmd.Parameters.AddWithValue(
                            "@Email",
                            row.Cells["Email"].Value);

                       

                        conn.Open();

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show(
                                "Appointment updated successfully.",
                                "Update Successful",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            LoadAppointments();
                        }
                        else
                        {
                            MessageBox.Show(
                                "No appointment was updated.",
                                "Update",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not update appointment.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void btnShowPatients_Click(object sender, EventArgs e)
        {


            MessageBox.Show(
        "This is Student Records",
        "Student Records",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information);

         string query = @"
        SELECT*
        FROM Users
        ORDER BY PatientID DESC";

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

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString =
       "Server=localhost;Database=healthsystemdb;Uid=root;Pwd=nibbles123;";

            string query = @"
        SELECT*
        FROM Appointments
        ORDER BY AppointmentID DESC";

            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
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

                dgvAppointments.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvAppointments.ReadOnly = true;

                dgvAppointments.AllowUserToAddRows = false;

                dgvAppointments.AllowUserToDeleteRows = false;

                dgvAppointments.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                MessageBox.Show(
                    "All appointment booking records are displayed.",
                    "Appointment Records",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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

        private void button1_Click(object sender, EventArgs e)
        {
            StaffRecords record = new StaffRecords();
            record.Show();

            this.Hide();
            return;
        }

        private void dgvAppointments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a staff member first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                string employeeNumber =
                    dgvAppointments.SelectedRows[0]
                    .Cells["EmployeeNumber"].Value.ToString();

                string query = @"
            UPDATE staffRecord
            SET Status = 'Active'
            WHERE EmployeeNumber = @EmployeeNumber";

                using (MySqlConnection conn =
                    new MySqlConnection(Database.ConnectionString))
                {
                    using (MySqlCommand cmd =
                        new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@EmployeeNumber",
                            employeeNumber);

                        conn.Open();

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show(
                                "Staff account has been activated.",
                                "Account Activated",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            LoadStaffRecords();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Staff account was not found.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error activating account:\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void LoadStaffRecords()
        {
            string query = @"
        SELECT *
        FROM staffRecord
        ORDER BY EmployeeNumber DESC";

            using (MySqlConnection conn =
                new MySqlConnection(Database.ConnectionString))
            {
                MySqlDataAdapter adapter =
                    new MySqlDataAdapter(query, conn);

                DataTable table = new DataTable();
                adapter.Fill(table);

                dgvAppointments.DataSource = table;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
       "This is the staff records.",
       "Staff Records",
       MessageBoxButtons.OK,
       MessageBoxIcon.Information
        );

            string query = "SELECT * FROM staffRecord";

            using (MySqlConnection conn = new MySqlConnection(Database.ConnectionString))
            {
                try
                {
                    conn.Open();

                    MySqlDataAdapter adapter =
                        new MySqlDataAdapter(query, conn);

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgvAppointments.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading staff records: " + ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a staff member first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                string employeeNumber =
                    dgvAppointments.SelectedRows[0]
                    .Cells["EmployeeNumber"].Value.ToString();

                string query = @"
            UPDATE staffRecord
            SET Status = 'Inactive'
            WHERE EmployeeNumber = @EmployeeNumber";

                using (MySqlConnection conn =
                    new MySqlConnection(Database.ConnectionString))
                {
                    using (MySqlCommand cmd =
                        new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@EmployeeNumber",
                            employeeNumber);

                        conn.Open();

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show(
                                "Staff account has been deactivated.",
                                "Account Deactivated",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            LoadStaffRecords();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Staff account was not found.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error deactivating account:\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}