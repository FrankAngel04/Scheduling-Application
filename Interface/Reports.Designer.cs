namespace SchedulingApplication
{
    partial class Reports
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView appointmentTypesByMonthDataGridView;
        private System.Windows.Forms.DataGridView userScheduleDataGridView;
        private System.Windows.Forms.DataGridView customerAppointmentCountDataGridView;
        private System.Windows.Forms.Label labelAppointmentTypes;
        private System.Windows.Forms.Label labelUserSchedule;
        private System.Windows.Forms.Label labelCustomerAppointmentCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.appointmentTypesByMonthDataGridView = new System.Windows.Forms.DataGridView();
            this.userScheduleDataGridView = new System.Windows.Forms.DataGridView();
            this.customerAppointmentCountDataGridView = new System.Windows.Forms.DataGridView();
            this.labelAppointmentTypes = new System.Windows.Forms.Label();
            this.labelUserSchedule = new System.Windows.Forms.Label();
            this.labelCustomerAppointmentCount = new System.Windows.Forms.Label();
            this.comboBoxConsultant = new System.Windows.Forms.ComboBox();
            this.comboBoxMonthFilter = new System.Windows.Forms.ComboBox();
            this.backButton = new System.Windows.Forms.Button();
            this.logOutButton = new System.Windows.Forms.Button();
            this.consultantLabel = new System.Windows.Forms.Label();
            this.monthsLabel = new System.Windows.Forms.Label();
            this.messageMonthLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.messageUserLabel = new System.Windows.Forms.Label();
            this.messageLocationLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentTypesByMonthDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userScheduleDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerAppointmentCountDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // appointmentTypesByMonthDataGridView
            // 
            this.appointmentTypesByMonthDataGridView.AllowUserToAddRows = false;
            this.appointmentTypesByMonthDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.appointmentTypesByMonthDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.appointmentTypesByMonthDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.appointmentTypesByMonthDataGridView.Location = new System.Drawing.Point(592, 350);
            this.appointmentTypesByMonthDataGridView.Name = "appointmentTypesByMonthDataGridView";
            this.appointmentTypesByMonthDataGridView.ReadOnly = true;
            this.appointmentTypesByMonthDataGridView.RowHeadersVisible = false;
            this.appointmentTypesByMonthDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.appointmentTypesByMonthDataGridView.Size = new System.Drawing.Size(388, 150);
            this.appointmentTypesByMonthDataGridView.TabIndex = 0;
            // 
            // userScheduleDataGridView
            // 
            this.userScheduleDataGridView.AllowUserToAddRows = false;
            this.userScheduleDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.userScheduleDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.userScheduleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userScheduleDataGridView.Location = new System.Drawing.Point(12, 47);
            this.userScheduleDataGridView.Name = "userScheduleDataGridView";
            this.userScheduleDataGridView.ReadOnly = true;
            this.userScheduleDataGridView.RowHeadersVisible = false;
            this.userScheduleDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.userScheduleDataGridView.Size = new System.Drawing.Size(968, 254);
            this.userScheduleDataGridView.TabIndex = 1;
            // 
            // customerAppointmentCountDataGridView
            // 
            this.customerAppointmentCountDataGridView.AllowUserToAddRows = false;
            this.customerAppointmentCountDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.customerAppointmentCountDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.customerAppointmentCountDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customerAppointmentCountDataGridView.Location = new System.Drawing.Point(12, 350);
            this.customerAppointmentCountDataGridView.Name = "customerAppointmentCountDataGridView";
            this.customerAppointmentCountDataGridView.ReadOnly = true;
            this.customerAppointmentCountDataGridView.RowHeadersVisible = false;
            this.customerAppointmentCountDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.customerAppointmentCountDataGridView.Size = new System.Drawing.Size(388, 150);
            this.customerAppointmentCountDataGridView.TabIndex = 2;
            // 
            // labelAppointmentTypes
            // 
            this.labelAppointmentTypes.AutoSize = true;
            this.labelAppointmentTypes.Location = new System.Drawing.Point(12, 334);
            this.labelAppointmentTypes.Name = "labelAppointmentTypes";
            this.labelAppointmentTypes.Size = new System.Drawing.Size(145, 13);
            this.labelAppointmentTypes.TabIndex = 3;
            this.labelAppointmentTypes.Text = "Appointment Types by Month";
            // 
            // labelUserSchedule
            // 
            this.labelUserSchedule.AutoSize = true;
            this.labelUserSchedule.Location = new System.Drawing.Point(12, 31);
            this.labelUserSchedule.Name = "labelUserSchedule";
            this.labelUserSchedule.Size = new System.Drawing.Size(77, 13);
            this.labelUserSchedule.TabIndex = 4;
            this.labelUserSchedule.Text = "User Schedule";
            // 
            // labelCustomerAppointmentCount
            // 
            this.labelCustomerAppointmentCount.AutoSize = true;
            this.labelCustomerAppointmentCount.Location = new System.Drawing.Point(592, 334);
            this.labelCustomerAppointmentCount.Name = "labelCustomerAppointmentCount";
            this.labelCustomerAppointmentCount.Size = new System.Drawing.Size(155, 13);
            this.labelCustomerAppointmentCount.TabIndex = 5;
            this.labelCustomerAppointmentCount.Text = "Appointment Count by Location";
            // 
            // comboBoxConsultant
            // 
            this.comboBoxConsultant.FormattingEnabled = true;
            this.comboBoxConsultant.Location = new System.Drawing.Point(859, 20);
            this.comboBoxConsultant.Name = "comboBoxConsultant";
            this.comboBoxConsultant.Size = new System.Drawing.Size(121, 21);
            this.comboBoxConsultant.TabIndex = 6;
            this.comboBoxConsultant.SelectedIndexChanged += new System.EventHandler(this.Consultant_Selected);
            // 
            // comboBoxMonthFilter
            // 
            this.comboBoxMonthFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMonthFilter.FormattingEnabled = true;
            this.comboBoxMonthFilter.Items.AddRange(new object[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
            this.comboBoxMonthFilter.Location = new System.Drawing.Point(406, 350);
            this.comboBoxMonthFilter.Name = "comboBoxMonthFilter";
            this.comboBoxMonthFilter.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMonthFilter.TabIndex = 7;
            this.comboBoxMonthFilter.SelectedIndexChanged += new System.EventHandler(this.MonthFilter_Selected);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(455, 536);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(99, 43);
            this.backButton.TabIndex = 8;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // logOutButton
            // 
            this.logOutButton.Location = new System.Drawing.Point(859, 536);
            this.logOutButton.Name = "logOutButton";
            this.logOutButton.Size = new System.Drawing.Size(99, 43);
            this.logOutButton.TabIndex = 9;
            this.logOutButton.Text = "Log Out";
            this.logOutButton.UseVisualStyleBackColor = true;
            this.logOutButton.Click += new System.EventHandler(this.logOutButton_Click);
            // 
            // consultantLabel
            // 
            this.consultantLabel.Location = new System.Drawing.Point(788, 23);
            this.consultantLabel.Name = "consultantLabel";
            this.consultantLabel.Size = new System.Drawing.Size(65, 18);
            this.consultantLabel.TabIndex = 10;
            this.consultantLabel.Text = "Consultant:";
            // 
            // monthsLabel
            // 
            this.monthsLabel.Location = new System.Drawing.Point(406, 335);
            this.monthsLabel.Name = "monthsLabel";
            this.monthsLabel.Size = new System.Drawing.Size(100, 12);
            this.monthsLabel.TabIndex = 11;
            this.monthsLabel.Text = "Months";
            // 
            // messageMonthLabel
            // 
            this.messageMonthLabel.Location = new System.Drawing.Point(12, 503);
            this.messageMonthLabel.Name = "messageMonthLabel";
            this.messageMonthLabel.Size = new System.Drawing.Size(388, 25);
            this.messageMonthLabel.TabIndex = 12;
            this.messageMonthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(372, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 32);
            this.label1.TabIndex = 13;
            this.label1.Text = "Report";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // messageUserLabel
            // 
            this.messageUserLabel.Location = new System.Drawing.Point(12, 304);
            this.messageUserLabel.Name = "messageUserLabel";
            this.messageUserLabel.Size = new System.Drawing.Size(237, 23);
            this.messageUserLabel.TabIndex = 14;
            this.messageUserLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // messageLocationLabel
            // 
            this.messageLocationLabel.Location = new System.Drawing.Point(592, 503);
            this.messageLocationLabel.Name = "messageLocationLabel";
            this.messageLocationLabel.Size = new System.Drawing.Size(388, 25);
            this.messageLocationLabel.TabIndex = 15;
            this.messageLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Reports
            // 
            this.ClientSize = new System.Drawing.Size(992, 611);
            this.Controls.Add(this.messageLocationLabel);
            this.Controls.Add(this.messageUserLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.messageMonthLabel);
            this.Controls.Add(this.monthsLabel);
            this.Controls.Add(this.consultantLabel);
            this.Controls.Add(this.logOutButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.comboBoxMonthFilter);
            this.Controls.Add(this.comboBoxConsultant);
            this.Controls.Add(this.labelCustomerAppointmentCount);
            this.Controls.Add(this.labelUserSchedule);
            this.Controls.Add(this.labelAppointmentTypes);
            this.Controls.Add(this.customerAppointmentCountDataGridView);
            this.Controls.Add(this.userScheduleDataGridView);
            this.Controls.Add(this.appointmentTypesByMonthDataGridView);
            this.Name = "Reports";
            this.Text = "Report Form";
            this.Load += new System.EventHandler(this.Reports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.appointmentTypesByMonthDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userScheduleDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerAppointmentCountDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label messageUserLabel;
        private System.Windows.Forms.Label messageLocationLabel;

        private System.Windows.Forms.Label messageMonthLabel;

        private System.Windows.Forms.Label consultantLabel;
        private System.Windows.Forms.Label monthsLabel;

        private System.Windows.Forms.ComboBox comboBoxConsultant;
        private System.Windows.Forms.ComboBox comboBoxMonthFilter;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button logOutButton;
    }
}
