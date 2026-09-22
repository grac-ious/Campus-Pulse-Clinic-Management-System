using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Campus_pulse
{
    public partial class FrmAdmin : Form
    {
        // Main layout
        private Panel pnlNavigation;
        private Panel pnlContent;

        // Navigation buttons
        private Button btnDashboard;
        private Button btnManageUsers;
        private Button btnManageAccess;
        private Button btnReports;
        private Button btnLogout;

        // Content controls
        private Label lblContentTitle;
        private Label lblContentDescription;

        public FrmAdmin()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // =========================================================
        // BUILD MAIN DASHBOARD
        // =========================================================
        private void BuildAdministratorDashboard()
        {
            // Hide the old Designer controls.
            HideOldDesignerControls();

            this.Text = "Campus Pulse - System Administrator";
            this.Width = 1100;
            this.Height = 650;
            this.MinimumSize = new Size(900, 550);
            this.BackColor = Color.White;

            // =====================================================
            // LEFT NAVIGATION PANEL
            // =====================================================

            pnlNavigation = new Panel();
            pnlNavigation.Dock = DockStyle.Left;
            pnlNavigation.Width = 250;
            pnlNavigation.BackColor = Color.FromArgb(35, 61, 105);

            this.Controls.Add(pnlNavigation);

            // Clinic Management title
            Label lblSystemTitle = new Label();
            lblSystemTitle.Text = "CLINIC MANAGEMENT SYSTEM";
            lblSystemTitle.ForeColor = Color.White;
            lblSystemTitle.Font = new Font(
                "Arial",
                12,
                FontStyle.Bold);
            lblSystemTitle.SetBounds(20, 20, 210, 30);

            pnlNavigation.Controls.Add(lblSystemTitle);

            // Role heading
            Label lblRole = new Label();
            lblRole.Text = "SYSTEM\r\nADMINISTRATOR";
            lblRole.ForeColor = Color.White;
            lblRole.Font = new Font(
                "Arial",
                13,
                FontStyle.Bold);
            lblRole.SetBounds(20, 70, 210, 55);

            pnlNavigation.Controls.Add(lblRole);

            // =====================================================
            // NAVIGATION BUTTONS
            // =====================================================

            btnDashboard = CreateNavigationButton("Dashboard");
            btnDashboard.SetBounds(20, 150, 210, 45);
            btnDashboard.Click += btnDashboard_Click;

            btnManageUsers = CreateNavigationButton(
                "Manage System Users");
            btnManageUsers.SetBounds(20, 200, 210, 45);
            btnManageUsers.Click += ManageSystemUsers_Click;

            btnManageAccess = CreateNavigationButton(
                "Manage User Access");
            btnManageAccess.SetBounds(20, 250, 210, 45);
            btnManageAccess.Click += button1_Click;

            btnReports = CreateNavigationButton(
                "Generate Reports");
            btnReports.SetBounds(20, 300, 210, 45);
            btnReports.Click += button3_Click;

            pnlNavigation.Controls.Add(btnDashboard);
            pnlNavigation.Controls.Add(btnManageUsers);
            pnlNavigation.Controls.Add(btnManageAccess);
            pnlNavigation.Controls.Add(btnReports);

            // =====================================================
            // LOGOUT BUTTON
            // =====================================================

            btnLogout = CreateNavigationButton("Logout");
            btnLogout.SetBounds(20, 520, 210, 45);
            btnLogout.Click += button2_Click;

            pnlNavigation.Controls.Add(btnLogout);

            // =====================================================
            // RIGHT CONTENT PANEL
            // =====================================================

            pnlContent = new Panel();
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.BackColor = Color.White;
            pnlContent.Padding = new Padding(35);

            this.Controls.Add(pnlContent);

            // Put navigation in front
            pnlNavigation.BringToFront();

            // Display dashboard when form opens
            ShowDashboard();
        }

        // =========================================================
        // CREATE NAVIGATION BUTTON
        // =========================================================
        private Button CreateNavigationButton(string text)
        {
            Button button = new Button();

            button.Text = text;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.FromArgb(35, 61, 105);
            button.ForeColor = Color.White;
            button.Font = new Font(
                "Arial",
                10,
                FontStyle.Regular);

            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(10, 0, 0, 0);
            button.Cursor = Cursors.Hand;

            return button;
        }

        // =========================================================
        // HIDE OLD DESIGNER CONTROLS
        // =========================================================
        private void HideOldDesignerControls()
        {
            foreach (Control control in this.Controls)
            {
                control.Visible = false;
            }
        }

        // =========================================================
        // DASHBOARD
        // =========================================================
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void ShowDashboard()
        {
            pnlContent.Controls.Clear();

            Label title = CreateContentTitle(
                "System Administrator Dashboard");

            pnlContent.Controls.Add(title);

            Label welcome = new Label();
            welcome.Text =
                "Welcome, " +
                Session.FullName +
                "\r\n\r\n" +
                "Use the navigation menu on the left to manage " +
                "system users, user access and management reports.";

            welcome.Font = new Font(
                "Arial",
                11,
                FontStyle.Regular);

            welcome.ForeColor = Color.FromArgb(60, 60, 60);
            welcome.SetBounds(40, 90, 650, 100);

            pnlContent.Controls.Add(welcome);
        }

        // =========================================================
        // MANAGE SYSTEM USERS
        // =========================================================
        private void ManageSystemUsers_Click(
            object sender,
            EventArgs e)
        {
            ShowManageSystemUsers();
        }

        private void ShowManageSystemUsers()
        {
            pnlContent.Controls.Clear();

            Label title = CreateContentTitle(
                "Manage System Users");

            pnlContent.Controls.Add(title);

            // Username
            Label lblUsername = CreateFieldLabel("Username");
            lblUsername.SetBounds(40, 90, 160, 30);
            pnlContent.Controls.Add(lblUsername);

            TextBox txtUsername = new TextBox();
            txtUsername.Text = "a.naidoo";
            txtUsername.ReadOnly = true;
            txtUsername.SetBounds(200, 85, 350, 30);
            pnlContent.Controls.Add(txtUsername);

            // Full Name
            Label lblFullName = CreateFieldLabel("Full Name");
            lblFullName.SetBounds(40, 135, 160, 30);
            pnlContent.Controls.Add(lblFullName);

            TextBox txtFullName = new TextBox();
            txtFullName.Text = "Ashley Naidoo";
            txtFullName.ReadOnly = true;
            txtFullName.SetBounds(200, 130, 350, 30);
            pnlContent.Controls.Add(txtFullName);

            // Role
            Label lblRole = CreateFieldLabel("Role");
            lblRole.SetBounds(40, 180, 160, 30);
            pnlContent.Controls.Add(lblRole);

            TextBox txtRole = new TextBox();
            txtRole.Text = "System Administrator";
            txtRole.ReadOnly = true;
            txtRole.SetBounds(200, 175, 350, 30);
            pnlContent.Controls.Add(txtRole);

            // Active Status
            Label lblStatus = CreateFieldLabel(
                "Active Status");

            lblStatus.SetBounds(40, 225, 160, 30);
            pnlContent.Controls.Add(lblStatus);

            TextBox txtStatus = new TextBox();
            txtStatus.Text = "Active";
            txtStatus.ReadOnly = true;
            txtStatus.SetBounds(200, 220, 350, 30);
            pnlContent.Controls.Add(txtStatus);

            // User list heading
            Label lblUserList = CreateFieldLabel(
                "User List");

            lblUserList.SetBounds(40, 280, 160, 30);
            pnlContent.Controls.Add(lblUserList);

            TextBox txtUserList = new TextBox();
            txtUserList.Text =
                "Username     |     Role     |     Status";
            txtUserList.ReadOnly = true;
            txtUserList.SetBounds(200, 275, 500, 30);
            pnlContent.Controls.Add(txtUserList);

            // Actions
            Label lblActions = new Label();
            lblActions.Text =
                "Actions:  ADD    EDIT    DEACTIVATE    SAVE";

            lblActions.Font = new Font(
                "Arial",
                10,
                FontStyle.Bold);

            lblActions.ForeColor =
                Color.FromArgb(90, 40, 40);

            lblActions.SetBounds(40, 340, 600, 30);

            pnlContent.Controls.Add(lblActions);
        }

        // =========================================================
        // MANAGE USER ACCESS
        // =========================================================
        private void button1_Click(object sender, EventArgs e)
        {
            ShowManageUserAccess();
        }

        private void ShowManageUserAccess()
        {
            pnlContent.Controls.Clear();

            Label title = CreateContentTitle(
                "Manage User Access");

            pnlContent.Controls.Add(title);

            Label description = new Label();

            description.Text =
                "Manage access and permissions for system users.\r\n\r\n" +
                "This section is restricted to the System Administrator.";

            description.Font = new Font(
                "Arial",
                11,
                FontStyle.Regular);

            description.ForeColor =
                Color.FromArgb(60, 60, 60);

            description.SetBounds(
                40,
                100,
                650,
                100);

            pnlContent.Controls.Add(description);
        }

        // =========================================================
        // GENERATE REPORTS
        // =========================================================
        private void button3_Click(object sender, EventArgs e)
        {
            ShowReports();
        }

        private void ShowReports()
        {
            pnlContent.Controls.Clear();

            Label title = CreateContentTitle(
                "Generate Reports");

            pnlContent.Controls.Add(title);

            Label description = new Label();

            description.Text =
                "Management reports can be generated from this section.\r\n\r\n" +
                "Select a report type and reporting period.";

            description.Font = new Font(
                "Arial",
                11,
                FontStyle.Regular);

            description.ForeColor =
                Color.FromArgb(60, 60, 60);

            description.SetBounds(
                40,
                100,
                650,
                100);

            pnlContent.Controls.Add(description);
        }

        // =========================================================
        // LOGOUT
        // =========================================================
        private void button2_Click(object sender, EventArgs e)
        {
            Session.Clear();

            FrmLogin login = new FrmLogin();
            login.Show();

            this.Close();
        }

        // =========================================================
        // CONTENT LABEL HELPERS
        // =========================================================
        private Label CreateContentTitle(string text)
        {
            Label label = new Label();

            label.Text = text;
            label.Font = new Font(
                "Arial",
                16,
                FontStyle.Bold);

            label.ForeColor =
                Color.FromArgb(35, 61, 105);

            label.SetBounds(
                40,
                30,
                700,
                40);

            return label;
        }

        private Label CreateFieldLabel(string text)
        {
            Label label = new Label();

            label.Text = text;
            label.Font = new Font(
                "Arial",
                10,
                FontStyle.Bold);

            label.ForeColor =
                Color.FromArgb(35, 61, 105);

            return label;
        }

        // =========================================================
        // OLD DESIGNER EVENTS
        // Kept so existing Designer connections do not break.
        // =========================================================

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
        }

        private void FrmAdmin_Load_1(object sender, EventArgs e)
        {
        }

        private void btnSeen_Click(object sender, EventArgs e)
        {
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void dgvAppointments_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
        }

        private void LoadAppointments()
        {
        }

        private void LoadStudentRecords()
        {
        }

        private void LoadStaffRecords()
        {
        }
    }
}