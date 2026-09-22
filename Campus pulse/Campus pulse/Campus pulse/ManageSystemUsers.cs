using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Campus_pulse
{
    public class ManageSystemUsers : Form
    {
        private Label lblTitle;
        private Label lblUsername;
        private Label lblFullName;
        private Label lblRole;
        private Label lblStatus;
        private Label lblDateCreated;

        private TextBox txtUsername;
        private TextBox txtFullName;
        private ComboBox cboRole;
        private TextBox txtStatus;
        private TextBox txtDateCreated;

        private Button btnPrevious;
        private Button btnNext;
        private Button btnActivate;
        private Button btnDeactivate;
        private Button btnBack;

        private List<SystemUser> users;
        private int currentIndex = 0;

        public ManageSystemUsers()
        {
            BuildForm();
            LoadUsers();
        }

        private void BuildForm()
        {
            this.Text = "Manage System Users";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 650;
            this.Height = 500;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            lblTitle = new Label();
            lblTitle.Text = "MANAGE SYSTEM USERS";
            lblTitle.Font = new System.Drawing.Font(
                "Arial",
                18,
                System.Drawing.FontStyle.Bold);
            lblTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            lblTitle.SetBounds(50, 25, 550, 45);

            lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.SetBounds(80, 100, 120, 25);

            txtUsername = new TextBox();
            txtUsername.ReadOnly = true;
            txtUsername.SetBounds(220, 95, 300, 30);

            lblFullName = new Label();
            lblFullName.Text = "Full Name:";
            lblFullName.SetBounds(80, 145, 120, 25);

            txtFullName = new TextBox();
            txtFullName.ReadOnly = true;
            txtFullName.SetBounds(220, 140, 300, 30);

            lblRole = new Label();
            lblRole.Text = "Role:";
            lblRole.SetBounds(80, 190, 120, 25);

            cboRole = new ComboBox();
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.SetBounds(220, 185, 300, 30);

            lblStatus = new Label();
            lblStatus.Text = "Status:";
            lblStatus.SetBounds(80, 235, 120, 25);

            txtStatus = new TextBox();
            txtStatus.ReadOnly = true;
            txtStatus.SetBounds(220, 230, 300, 30);

            lblDateCreated = new Label();
            lblDateCreated.Text = "Date Created:";
            lblDateCreated.SetBounds(80, 280, 120, 25);

            txtDateCreated = new TextBox();
            txtDateCreated.ReadOnly = true;
            txtDateCreated.SetBounds(220, 275, 300, 30);

            btnPrevious = new Button();
            btnPrevious.Text = "Previous";
            btnPrevious.SetBounds(80, 340, 110, 40);
            btnPrevious.Click += btnPrevious_Click;

            btnNext = new Button();
            btnNext.Text = "Next";
            btnNext.SetBounds(205, 340, 110, 40);
            btnNext.Click += btnNext_Click;

            btnActivate = new Button();
            btnActivate.Text = "Activate User";
            btnActivate.SetBounds(330, 340, 110, 40);
            btnActivate.Click += btnActivate_Click;

            btnDeactivate = new Button();
            btnDeactivate.Text = "Deactivate User";
            btnDeactivate.SetBounds(455, 340, 110, 40);
            btnDeactivate.Click += btnDeactivate_Click;

            btnBack = new Button();
            btnBack.Text = "Back";
            btnBack.SetBounds(270, 400, 110, 40);
            btnBack.Click += btnBack_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblFullName);
            this.Controls.Add(txtFullName);
            this.Controls.Add(lblRole);
            this.Controls.Add(cboRole);
            this.Controls.Add(lblStatus);
            this.Controls.Add(txtStatus);
            this.Controls.Add(lblDateCreated);
            this.Controls.Add(txtDateCreated);
            this.Controls.Add(btnPrevious);
            this.Controls.Add(btnNext);
            this.Controls.Add(btnActivate);
            this.Controls.Add(btnDeactivate);
            this.Controls.Add(btnBack);
        }

        private void LoadUsers()
        {
            users = new List<SystemUser>();

            string query = @"
                SELECT
                    u.UserID,
                    u.RoleID,
                    u.Username,
                    u.PasswordHash,
                    u.FullName,
                    u.IsActive,
                    r.RoleName
                FROM tbl_SystemUser u
                INNER JOIN tbl_Role r
                    ON u.RoleID = r.RoleID
                ORDER BY u.UserID;";

            try
            {
                using (MySqlConnection connection =
                       new MySqlConnection(Database.ConnectionString))
                {
                    connection.Open();

                    using (MySqlCommand command =
                           new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader =
                               command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new SystemUser
                                {
                                    UserID = reader.GetInt32("UserID"),
                                    RoleID = reader.GetInt32("RoleID"),
                                    Username = reader.GetString("Username"),
                                    PasswordHash = reader.GetString("PasswordHash"),
                                    FullName = reader.GetString("FullName"),
                                    IsActive = reader.GetBoolean("IsActive"),
                                    RoleName = reader.GetString("RoleName")
                                });
                            }
                        }
                    }
                }

                if (users.Count == 0)
                {
                    MessageBox.Show(
                        "No system users were found.",
                        "Manage System Users",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                currentIndex = 0;
                DisplayCurrentUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load system users.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DisplayCurrentUser()
        {
            if (users == null ||
                users.Count == 0 ||
                currentIndex < 0 ||
                currentIndex >= users.Count)
            {
                return;
            }

            SystemUser user = users[currentIndex];

            txtUsername.Text = user.Username;
            txtFullName.Text = user.FullName;
            txtStatus.Text = user.IsActive ? "Active" : "Inactive";

            cboRole.Items.Clear();
            cboRole.Items.Add(user.RoleName);
            cboRole.SelectedIndex = 0;

            txtDateCreated.Text = "User ID: " + user.UserID;

            btnPrevious.Enabled = currentIndex > 0;
            btnNext.Enabled = currentIndex < users.Count - 1;

            btnActivate.Enabled = !user.IsActive;
            btnDeactivate.Enabled = user.IsActive;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                DisplayCurrentUser();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < users.Count - 1)
            {
                currentIndex++;
                DisplayCurrentUser();
            }
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            ChangeUserStatus(true);
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            ChangeUserStatus(false);
        }

        private void ChangeUserStatus(bool activate)
        {
            if (users == null ||
                users.Count == 0 ||
                currentIndex < 0 ||
                currentIndex >= users.Count)
            {
                return;
            }

            SystemUser user = users[currentIndex];

            string query = @"
                UPDATE tbl_SystemUser
                SET IsActive = @IsActive
                WHERE UserID = @UserID;";

            try
            {
                using (MySqlConnection connection =
                       new MySqlConnection(Database.ConnectionString))
                {
                    connection.Open();

                    using (MySqlCommand command =
                           new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(
                            "@IsActive",
                            activate ? 1 : 0);

                        command.Parameters.AddWithValue(
                            "@UserID",
                            user.UserID);

                        command.ExecuteNonQuery();
                    }
                }

                user.IsActive = activate;
                DisplayCurrentUser();

                MessageBox.Show(
                    activate
                        ? "User activated successfully."
                        : "User deactivated successfully.",
                    "User Access",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to update user status.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmAdmin admin = new FrmAdmin();
            admin.Show();
            this.Close();
        }
    }
}
