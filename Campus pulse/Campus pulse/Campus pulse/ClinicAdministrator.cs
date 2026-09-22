using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
namespace Campus_pulse
{
    public partial class ClinicAdministrator : Form
    {
        public ClinicAdministrator()
        {
            InitializeComponent();

            pnlDashboard.Visible = false;
        }

        private void ClinicAdministrator_Load(object sender, EventArgs e)
        {

        }

        private void pnlSidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            pnlDashboard.Visible = true;

            // Load dashboard information
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            try
            {
                string connectionString =
                    "server=localhost;database=healthsystemdb;user=root;password=nibbles123;";

                using (MySqlConnection con =
                    new MySqlConnection(connectionString))
                {
                    con.Open();

                    // ============================
                    // TOTAL PATIENTS
                    // ============================

                    string patientQuery =
                        "SELECT COUNT(*) FROM Users";

                    using (MySqlCommand cmd =
                        new MySqlCommand(patientQuery, con))
                    {
                        int totalPatients =
                            Convert.ToInt32(cmd.ExecuteScalar());

                        lblTotalPatients.Text =
                            "Total Patients: " + totalPatients;
                    }


                    // ============================
                    // TODAY'S APPOINTMENTS
                    // ============================

                    string todayQuery = @"
                SELECT COUNT(*)
                FROM Appointments
                WHERE DATE(AppointmentDate) = CURDATE()";

                    using (MySqlCommand cmd =
                        new MySqlCommand(todayQuery, con))
                    {
                        int todayAppointments =
                            Convert.ToInt32(cmd.ExecuteScalar());

                        lblTodayAppointments.Text =
                            "Today's Appointments: " +
                            todayAppointments;
                    }


                    // ============================
                    // PENDING APPOINTMENTS
                    // ============================

                    string pendingQuery = @"
                SELECT COUNT(*)
                FROM Appointments
                WHERE Status = 'Pending'";

                    using (MySqlCommand cmd =
                        new MySqlCommand(pendingQuery, con))
                    {
                        int pendingAppointments =
                            Convert.ToInt32(cmd.ExecuteScalar());

                        lblPendingAppointments.Text =
                            "Pending Appointments: " +
                            pendingAppointments;
                    }


                    // ============================
                    // TOTAL DOCTORS
                    // ============================

                    //string doctorQuery =
                    //    "SELECT COUNT(*) FROM Doctors";

                    //using (MySqlCommand cmd =
                    //    new MySqlCommand(doctorQuery, con))
                    //{
                    //    int doctors =
                    //        Convert.ToInt32(cmd.ExecuteScalar());

                    //    lblDoctors.Text =
                    //        "Doctors: " + doctors;
                    //}


                    // ============================
                    // UPCOMING APPOINTMENTS
                    // ============================

                    string appointmentQuery = @"
    SELECT
        StudentNumber AS Patient,
        Doctor,
        AppointmentDate AS Date,
        AppointmentTime AS Time,
        Reason,
        Status
    FROM Appointments
    WHERE AppointmentDate >= CURDATE()
    ORDER BY AppointmentDate, AppointmentTime
    LIMIT 10";


                    using (MySqlDataAdapter adapter =
                        new MySqlDataAdapter(
                            appointmentQuery, con))
                    {
                        DataTable table =
                            new DataTable();

                        adapter.Fill(table);

                        dgvUpcomingAppointments.DataSource =
                            table;
                    }


                    // ============================
                    // ACTIVITY FEED
                    // ============================

                    lstActivity.Items.Clear();

                    lstActivity.Items.Add(
                        "New patient registered");

                    lstActivity.Items.Add(
                        "Appointment created");

                    lstActivity.Items.Add(
                        "Appointment approved");

                    lstActivity.Items.Add(
                        "Patient information updated");

                    lstActivity.Items.Add(
                        "Administrator logged in");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load dashboard.\n\n" +
                    ex.Message,
                    "Dashboard Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmLogin home = new FrmLogin();
            home.Show();

            this.Hide();
            return;
        }
    }

}