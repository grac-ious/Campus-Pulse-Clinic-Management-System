namespace Campus_pulse
{
    partial class ClinicAdministrator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.lblClinicName = new System.Windows.Forms.Label();
            this.dgvUpcomingAppointments = new System.Windows.Forms.DataGridView();
            this.lblTotalPatients = new System.Windows.Forms.Label();
            this.lblPendingAppointments = new System.Windows.Forms.Label();
            this.lblTodayAppointments = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lstActivity = new System.Windows.Forms.ListBox();
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpcomingAppointments)).BeginInit();
            this.pnlDashboard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pnlSidebar.Controls.Add(this.lblClinicName);
            this.pnlSidebar.Controls.Add(this.button6);
            this.pnlSidebar.Controls.Add(this.button5);
            this.pnlSidebar.Controls.Add(this.button4);
            this.pnlSidebar.Controls.Add(this.button3);
            this.pnlSidebar.Controls.Add(this.button2);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Location = new System.Drawing.Point(12, 73);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(229, 554);
            this.pnlSidebar.TabIndex = 0;
            this.pnlSidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSidebar_Paint);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.Location = new System.Drawing.Point(30, 112);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(134, 37);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnPatients_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(30, 170);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 37);
            this.button2.TabIndex = 2;
            this.button2.Text = "Patients    ";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(30, 227);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(134, 37);
            this.button3.TabIndex = 3;
            this.button3.Text = "Appointment";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(30, 284);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(134, 37);
            this.button4.TabIndex = 4;
            this.button4.Text = " Doctors";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(30, 339);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(134, 37);
            this.button5.TabIndex = 5;
            this.button5.Text = "Reports";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(30, 452);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(120, 37);
            this.button6.TabIndex = 6;
            this.button6.Text = "Logout ";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // lblClinicName
            // 
            this.lblClinicName.AutoSize = true;
            this.lblClinicName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblClinicName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClinicName.Location = new System.Drawing.Point(0, 0);
            this.lblClinicName.Name = "lblClinicName";
            this.lblClinicName.Size = new System.Drawing.Size(181, 25);
            this.lblClinicName.TabIndex = 1;
            this.lblClinicName.Text = "CAMPUS PULSE";
            this.lblClinicName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvUpcomingAppointments
            // 
            this.dgvUpcomingAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUpcomingAppointments.Location = new System.Drawing.Point(57, 99);
            this.dgvUpcomingAppointments.Name = "dgvUpcomingAppointments";
            this.dgvUpcomingAppointments.RowHeadersWidth = 51;
            this.dgvUpcomingAppointments.RowTemplate.Height = 24;
            this.dgvUpcomingAppointments.Size = new System.Drawing.Size(898, 280);
            this.dgvUpcomingAppointments.TabIndex = 1;
            // 
            // lblTotalPatients
            // 
            this.lblTotalPatients.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPatients.Location = new System.Drawing.Point(754, 33);
            this.lblTotalPatients.Name = "lblTotalPatients";
            this.lblTotalPatients.Size = new System.Drawing.Size(225, 50);
            this.lblTotalPatients.TabIndex = 2;
            this.lblTotalPatients.Text = "label1";
            // 
            // lblPendingAppointments
            // 
            this.lblPendingAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingAppointments.Location = new System.Drawing.Point(414, 33);
            this.lblPendingAppointments.Name = "lblPendingAppointments";
            this.lblPendingAppointments.Size = new System.Drawing.Size(284, 50);
            this.lblPendingAppointments.TabIndex = 3;
            this.lblPendingAppointments.Text = "label2";
            // 
            // lblTodayAppointments
            // 
            this.lblTodayAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTodayAppointments.Location = new System.Drawing.Point(81, 33);
            this.lblTodayAppointments.Name = "lblTodayAppointments";
            this.lblTodayAppointments.Size = new System.Drawing.Size(242, 37);
            this.lblTodayAppointments.TabIndex = 4;
            this.lblTodayAppointments.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(542, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(250, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Administrator Dashboard";
            // 
            // lstActivity
            // 
            this.lstActivity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstActivity.FormattingEnabled = true;
            this.lstActivity.ItemHeight = 18;
            this.lstActivity.Location = new System.Drawing.Point(231, 414);
            this.lstActivity.Name = "lstActivity";
            this.lstActivity.Size = new System.Drawing.Size(375, 148);
            this.lstActivity.TabIndex = 12;
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.Controls.Add(this.label1);
            this.pnlDashboard.Controls.Add(this.dgvUpcomingAppointments);
            this.pnlDashboard.Controls.Add(this.lblTotalPatients);
            this.pnlDashboard.Controls.Add(this.lstActivity);
            this.pnlDashboard.Controls.Add(this.lblPendingAppointments);
            this.pnlDashboard.Controls.Add(this.lblTodayAppointments);
            this.pnlDashboard.Location = new System.Drawing.Point(285, 73);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Size = new System.Drawing.Size(999, 616);
            this.pnlDashboard.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 414);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 18);
            this.label1.TabIndex = 13;
            this.label1.Text = "Recent Activity";
            // 
            // ClinicAdministrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 680);
            this.Controls.Add(this.pnlDashboard);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlSidebar);
            this.Name = "ClinicAdministrator";
            this.Text = "ClinicAdministrator";
            this.Load += new System.EventHandler(this.ClinicAdministrator_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpcomingAppointments)).EndInit();
            this.pnlDashboard.ResumeLayout(false);
            this.pnlDashboard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label lblClinicName;
        private System.Windows.Forms.DataGridView dgvUpcomingAppointments;
        private System.Windows.Forms.Label lblTotalPatients;
        private System.Windows.Forms.Label lblPendingAppointments;
        private System.Windows.Forms.Label lblTodayAppointments;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstActivity;
        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.Label label1;
    }
}