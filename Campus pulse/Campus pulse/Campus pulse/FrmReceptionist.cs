using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Campus_pulse
{
    public partial class FrmReceptionist : Form
    {
        public FrmReceptionist()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            cmbGender.Items.Clear();
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");

            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;

            LoadPatients();
        }

        private void LoadPatients()
        {
            string connectionString =
                "Server=localhost;" +
                "Database=healthsystemdb;" +
                "Uid=root;" +
                "Pwd=12345678;";

            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT
                    PatientID,
                    StudentNumber,
                    FullName,
                    Email,
                    Sex,
                    CellphoneNumber,
                    ResidentialAddress
                FROM Users
                ORDER BY PatientID DESC";

                    using (MySqlDataAdapter adapter =
                        new MySqlDataAdapter(query, conn))
                    {
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        dgvPatients.DataSource = table;
                    }
                }

                dgvPatients.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvPatients.ReadOnly = true;

                dgvPatients.AllowUserToAddRows = false;

                dgvPatients.AllowUserToDeleteRows = false;

                dgvPatients.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;
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

        // Session information
        public static class Session
        {
            public static string StudentNumber { get; set; }
            public static string PatientID { get; set; }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        { 
            string studentNumber = txtStudentNumber.Text.Trim();
    string fullName = txtName.Text.Trim();
    string email = txtEmail.Text.Trim();
    string cellphone = txtCellphoneNumber.Text.Trim();
    string address = txtResidentialAddress.Text.Trim();

    // ==========================================
    // VALIDATE STUDENT NUMBER
    // ==========================================

    if (string.IsNullOrWhiteSpace(studentNumber))
    {
        MessageBox.Show(
            "Student Number is required.",
            "Validation Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtStudentNumber.Focus();
        return;
    }

    if (!System.Text.RegularExpressions.Regex.IsMatch(
        studentNumber, @"^\d{8}$"))
    {
        MessageBox.Show(
            "Student Number must be exactly 8 digits.",
            "Validation Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtStudentNumber.Focus();
        return;
    }

    // ==========================================
    // VALIDATE FULL NAME
    // ==========================================

    if (string.IsNullOrWhiteSpace(fullName))
    {
        MessageBox.Show(
            "Full Name is required.",
            "Validation Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtName.Focus();
        return;
    }

    if (fullName.Length < 3)
    {
        MessageBox.Show(
            "Full Name must contain at least 3 characters.",
            "Validation Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtName.Focus();
        return;
    }

    if (!System.Text.RegularExpressions.Regex.IsMatch(
        fullName, @"^[A-Za-z ]+$"))
    {
        MessageBox.Show(
            "Full Name must contain letters and spaces only.",
            "Validation Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtName.Focus();
        return;
    }

    // ==========================================
    // VALIDATE EMAIL
    // ==========================================

    if (string.IsNullOrWhiteSpace(email))
    {
        MessageBox.Show(
            "Email is required.",
            "Validation Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtEmail.Focus();
        return;
    }

    if (!System.Text.RegularExpressions.Regex.IsMatch(
        email,
        @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
    {
        MessageBox.Show(
            "Please enter a valid email address.\nExample: student@example.com",
            "Validation Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtEmail.Focus();
        return;
    }

    // ==========================================
    // VALIDATE GENDER
    // ==========================================

    if (cmbGender.SelectedIndex == -1)
    {
        MessageBox.Show(
            "Please select Male or Female.",
            "Validation Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        cmbGender.Focus();
        return;
    }

    string sex = cmbGender.SelectedItem.ToString();

    // ==========================================
    // VALIDATE CELLPHONE NUMBER
    // ==========================================

    if (string.IsNullOrWhiteSpace(cellphone))
    {
        MessageBox.Show(
            "Cellphone Number is required.",
            "Validation Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtCellphoneNumber.Focus();
        return;
    }

    if (!System.Text.RegularExpressions.Regex.IsMatch(
        cellphone, @"^0\d{9}$"))
    {
        MessageBox.Show(
            "Cellphone Number must be exactly 10 digits and start with 0.\n" +
            "Example: 0812345678",
            "Validation Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtCellphoneNumber.Focus();
        return;
    }

    // ==========================================
    // VALIDATE RESIDENTIAL ADDRESS
    // ==========================================

    if (string.IsNullOrWhiteSpace(address))
    {
        MessageBox.Show(
            "Residential Address is required.",
            "Validation Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtResidentialAddress.Focus();
        return;
    }

    if (address.Length < 5)
    {
        MessageBox.Show(
            "Please enter a valid Residential Address.",
            "Validation Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        txtResidentialAddress.Focus();
        return;
    }

    // ==========================================
    // MYSQL CONNECTION
    // ==========================================

    string connectionString =
        "Server=localhost;" +
        "Database=healthsystemdb;" +
        "Uid=root;" +
        "Pwd=12345678;";

    try
    {
        using (MySqlConnection conn =
            new MySqlConnection(connectionString))
        {
            conn.Open();

            // ==========================================
            // CHECK FOR DUPLICATE STUDENT NUMBER
            // ==========================================

            string checkQuery =
                "SELECT COUNT(*) FROM Users " +
                "WHERE StudentNumber = @StudentNumber";

            using (MySqlCommand checkCmd =
                new MySqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue(
                    "@StudentNumber", studentNumber);

                int count = Convert.ToInt32(
                    checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show(
                        "This Student Number is already registered.",
                        "Duplicate Student",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtStudentNumber.Focus();
                    return;
                }
            }

            // ==========================================
            // GENERATE PATIENT ID
            // ==========================================

            string patientID = "PAT-" +
                DateTime.Now.ToString("yyyyMMddHHmmss");

            // ==========================================
            // INSERT USER
            // ==========================================

            string query = @"
                INSERT INTO Users
                (
                    PatientID,
                    StudentNumber,
                    FullName,
                    Email,
                    Sex,
                    CellphoneNumber,
                    ResidentialAddress
                )
                VALUES
                (
                    @PatientID,
                    @StudentNumber,
                    @FullName,
                    @Email,
                    @Sex,
                    @CellphoneNumber,
                    @ResidentialAddress
                )";

            using (MySqlCommand cmd =
                new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue(
                    "@PatientID", patientID);

                cmd.Parameters.AddWithValue(
                    "@StudentNumber", studentNumber);

                cmd.Parameters.AddWithValue(
                    "@FullName", fullName);

                cmd.Parameters.AddWithValue(
                    "@Email", email);

                cmd.Parameters.AddWithValue(
                    "@Sex", sex);

                cmd.Parameters.AddWithValue(
                    "@CellphoneNumber", cellphone);

                cmd.Parameters.AddWithValue(
                    "@ResidentialAddress", address);

                cmd.ExecuteNonQuery();
            }

            // ==========================================
            // SAVE SESSION
            // ==========================================

            Session.StudentNumber = studentNumber;
            Session.PatientID = patientID;

            // ==========================================
            // SUCCESS
            // ==========================================

            MessageBox.Show(
                "Account Created Successfully!\n\n" +
                "Patient ID: " + patientID,
                "Registration Successful",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // ==========================================
            // CLEAR FORM
            // ==========================================

            txtStudentNumber.Clear();
            txtName.Clear();
            txtEmail.Clear();
            cmbGender.SelectedIndex = -1;
            txtCellphoneNumber.Clear();
            txtResidentialAddress.Clear();

            LoadPatients();
        }
    }
    catch (MySqlException ex)
    {
        MessageBox.Show(
            "MySQL Database Error:\n\n" + ex.Message,
            "Database Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
    catch (Exception ex)
    {
        MessageBox.Show(
            "Error:\n\n" + ex.Message,
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmLogin loging = new FrmLogin();
            loging.Show();
            this.Hide();
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Hide();
            return;
        }

        private void FrmRegister_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtResidentialAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPatients();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string studentNumber = txtSearchStudentNumber.Text.Trim();

            if (!System.Text.RegularExpressions.Regex.IsMatch(studentNumber, @"^\d{8}$"))
            {
                MessageBox.Show("Student Number must be exactly 8 digits.");
                return;
            }

            string query = "SELECT * FROM Users WHERE StudentNumber = @StudentNumber";

            using (MySqlConnection conn = new MySqlConnection(
                "Server=localhost;Database=healthsystemdb;Uid=root;Pwd=nibbles123;"))
            {
                conn.Open();

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue(
                        "@StudentNumber", studentNumber);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvPatients.DataSource = dt;

                    if (dt.Rows.Count > 0)
                        MessageBox.Show("Student found.");
                    else
                        MessageBox.Show("Student not found.");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a student to delete.");
                return;
            }

            string patientID = dgvPatients.SelectedRows[0]
                .Cells["PatientID"].Value.ToString();

            DialogResult result = MessageBox.Show(
                "Are you sure you want to permanently delete this record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            string connectionString =
                "Server=localhost;Database=healthsystemdb;Uid=root;Pwd=nibbles123;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM Users WHERE PatientID = @PatientID";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PatientID", patientID);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Record deleted permanently.");

            LoadPatients();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient first.");
                return;
            }

            // Get PatientID from selected record
            string patientID = dgvPatients.SelectedRows[0]
                .Cells["PatientID"].Value.ToString();

            // Check required fields
            if (string.IsNullOrWhiteSpace(txtStudentNumber.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                cmbGender.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtCellphoneNumber.Text) ||
                string.IsNullOrWhiteSpace(txtResidentialAddress.Text))
            {
                MessageBox.Show("Please enter all information.");
                return;
            }

            string connectionString =
                "Server=localhost;Database=healthsystemdb;Uid=root;Pwd=nibbles123;";

            string query = @"
        UPDATE Users
        SET
            StudentNumber = @StudentNumber,
            FullName = @FullName,
            Email = @Email,
            Sex = @Sex,
            CellphoneNumber = @CellphoneNumber,
            ResidentialAddress = @ResidentialAddress
        WHERE PatientID = @PatientID";

            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd =
                        new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@StudentNumber",
                            txtStudentNumber.Text.Trim());

                        cmd.Parameters.AddWithValue(
                            "@FullName",
                            txtName.Text.Trim());

                        cmd.Parameters.AddWithValue(
                            "@Email",
                            txtEmail.Text.Trim());

                        cmd.Parameters.AddWithValue(
                            "@Sex",
                            cmbGender.Text);

                        cmd.Parameters.AddWithValue(
                            "@CellphoneNumber",
                            txtCellphoneNumber.Text.Trim());

                        cmd.Parameters.AddWithValue(
                            "@ResidentialAddress",
                            txtResidentialAddress.Text.Trim());

                        // PatientID identifies the record
                        // PatientID itself is NOT changed
                        cmd.Parameters.AddWithValue(
                            "@PatientID",
                            patientID);

                        int rowsAffected = cmd.ExecuteNonQuery(); 
                        
                        if (rowsAffected > 0)
                        { 
                            MessageBox.Show("Patient information updated successfully.");
                        }
                        else 
                        {
                           MessageBox.Show("No changes were made."); }
                        }
                } MessageBox.Show(
                    "Patient information updated and saved successfully.");

                // Show the new information in the table
                LoadPatients();

                txtStudentNumber.Clear(); 
                txtName.Clear();
                txtEmail.Clear(); 
                cmbGender.SelectedIndex = -1;
                txtCellphoneNumber.Clear(); 
                txtResidentialAddress.Clear();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(
                    "Database Error:\n\n" + ex.Message);
            }
        }

        private void btnBookAppointment_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient first.");
                return;
            }

            string patientID = dgvPatients.SelectedRows[0]
                .Cells["PatientID"].Value.ToString();

            string studentNumber = dgvPatients.SelectedRows[0]
                .Cells["StudentNumber"].Value.ToString();

            Session.PatientID = patientID;
            Session.StudentNumber = studentNumber;

            FrmAppointment appointment = new FrmAppointment();
            appointment.Show();

            this.Hide();
        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvPatients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

            // Make sure the user clicked a valid row
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvPatients.Rows[e.RowIndex];

            // Display selected patient information in the textboxes
            txtStudentNumber.Text = row.Cells["StudentNumber"].Value?.ToString();
            txtName.Text = row.Cells["FullName"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            cmbGender.Text = row.Cells["Sex"].Value?.ToString();
            txtCellphoneNumber.Text = row.Cells["CellphoneNumber"].Value?.ToString();
            txtResidentialAddress.Text = row.Cells["ResidentialAddress"].Value?.ToString();
        }
    }
}