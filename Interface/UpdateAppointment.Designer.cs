using System.ComponentModel;

namespace SchedulingApplication
{
    partial class UpdateAppointment
  {
        private System.Windows.Forms.Label exceptionLabel;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.comboBoxVisitTypes = new System.Windows.Forms.ComboBox();
            this.comboBoxLocations = new System.Windows.Forms.ComboBox();
            this.appointmentDayLabel = new System.Windows.Forms.Label();
            this.comboBoxTimeSlots = new System.Windows.Forms.ComboBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.comboBoxUsers = new System.Windows.Forms.ComboBox();
            this.comboBoxCustomers = new System.Windows.Forms.ComboBox();
            this.visitTypeLabel = new System.Windows.Forms.Label();
            this.appointmentTitle = new System.Windows.Forms.Label();
            this.appointmentTimeLabel = new System.Windows.Forms.Label();
            this.locationLabel = new System.Windows.Forms.Label();
            this.consultantLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.customerNameLabel = new System.Windows.Forms.Label();
            this.exceptionLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.descriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxVisitTypes
            // 
            this.comboBoxVisitTypes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxVisitTypes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxVisitTypes.FormattingEnabled = true;
            this.comboBoxVisitTypes.Location = new System.Drawing.Point(291, 175);
            this.comboBoxVisitTypes.Name = "comboBoxVisitTypes";
            this.comboBoxVisitTypes.Size = new System.Drawing.Size(140, 21);
            this.comboBoxVisitTypes.TabIndex = 55;
            // 
            // comboBoxLocations
            // 
            this.comboBoxLocations.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxLocations.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxLocations.FormattingEnabled = true;
            this.comboBoxLocations.Location = new System.Drawing.Point(108, 232);
            this.comboBoxLocations.Name = "comboBoxLocations";
            this.comboBoxLocations.Size = new System.Drawing.Size(140, 21);
            this.comboBoxLocations.TabIndex = 54;
            // 
            // appointmentDayLabel
            // 
            this.appointmentDayLabel.AutoSize = true;
            this.appointmentDayLabel.Location = new System.Drawing.Point(107, 110);
            this.appointmentDayLabel.Name = "appointmentDayLabel";
            this.appointmentDayLabel.Size = new System.Drawing.Size(88, 13);
            this.appointmentDayLabel.TabIndex = 53;
            this.appointmentDayLabel.Text = "Appointment Day";
            // 
            // comboBoxTimeSlots
            // 
            this.comboBoxTimeSlots.FormattingEnabled = true;
            this.comboBoxTimeSlots.Location = new System.Drawing.Point(108, 175);
            this.comboBoxTimeSlots.Name = "comboBoxTimeSlots";
            this.comboBoxTimeSlots.Size = new System.Drawing.Size(140, 21);
            this.comboBoxTimeSlots.TabIndex = 52;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(108, 126);
            this.dateTimePicker.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(211, 20);
            this.dateTimePicker.TabIndex = 51;
            // 
            // comboBoxUsers
            // 
            this.comboBoxUsers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxUsers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxUsers.FormattingEnabled = true;
            this.comboBoxUsers.Location = new System.Drawing.Point(291, 75);
            this.comboBoxUsers.Name = "comboBoxUsers";
            this.comboBoxUsers.Size = new System.Drawing.Size(140, 21);
            this.comboBoxUsers.TabIndex = 50;
            // 
            // comboBoxCustomers
            // 
            this.comboBoxCustomers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxCustomers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCustomers.FormattingEnabled = true;
            this.comboBoxCustomers.Location = new System.Drawing.Point(108, 75);
            this.comboBoxCustomers.Name = "comboBoxCustomers";
            this.comboBoxCustomers.Size = new System.Drawing.Size(140, 21);
            this.comboBoxCustomers.TabIndex = 49;
            // 
            // visitTypeLabel
            // 
            this.visitTypeLabel.AutoSize = true;
            this.visitTypeLabel.Location = new System.Drawing.Point(291, 159);
            this.visitTypeLabel.Name = "visitTypeLabel";
            this.visitTypeLabel.Size = new System.Drawing.Size(53, 13);
            this.visitTypeLabel.TabIndex = 48;
            this.visitTypeLabel.Text = "Visit Type";
            // 
            // appointmentTitle
            // 
            this.appointmentTitle.AutoSize = true;
            this.appointmentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appointmentTitle.Location = new System.Drawing.Point(188, 29);
            this.appointmentTitle.Name = "appointmentTitle";
            this.appointmentTitle.Size = new System.Drawing.Size(149, 16);
            this.appointmentTitle.TabIndex = 47;
            this.appointmentTitle.Text = "Update Appointment";
            // 
            // appointmentTimeLabel
            // 
            this.appointmentTimeLabel.AutoSize = true;
            this.appointmentTimeLabel.Location = new System.Drawing.Point(105, 159);
            this.appointmentTimeLabel.Name = "appointmentTimeLabel";
            this.appointmentTimeLabel.Size = new System.Drawing.Size(92, 13);
            this.appointmentTimeLabel.TabIndex = 46;
            this.appointmentTimeLabel.Text = "Appointment Time";
            // 
            // locationLabel
            // 
            this.locationLabel.AutoSize = true;
            this.locationLabel.Location = new System.Drawing.Point(108, 211);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(48, 13);
            this.locationLabel.TabIndex = 45;
            this.locationLabel.Text = "Location";
            // 
            // consultantLabel
            // 
            this.consultantLabel.AutoSize = true;
            this.consultantLabel.Location = new System.Drawing.Point(291, 59);
            this.consultantLabel.Name = "consultantLabel";
            this.consultantLabel.Size = new System.Drawing.Size(57, 13);
            this.consultantLabel.TabIndex = 44;
            this.consultantLabel.Text = "Consultant";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(107, 264);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 43;
            this.descriptionLabel.Text = "Description";
            // 
            // customerNameLabel
            // 
            this.customerNameLabel.AutoSize = true;
            this.customerNameLabel.Location = new System.Drawing.Point(107, 59);
            this.customerNameLabel.Name = "customerNameLabel";
            this.customerNameLabel.Size = new System.Drawing.Size(82, 13);
            this.customerNameLabel.TabIndex = 42;
            this.customerNameLabel.Text = "Customer Name";
            // 
            // exceptionLabel
            // 
            this.exceptionLabel.Location = new System.Drawing.Point(0, 0);
            this.exceptionLabel.Name = "exceptionLabel";
            this.exceptionLabel.Size = new System.Drawing.Size(100, 23);
            this.exceptionLabel.TabIndex = 58;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(260, 386);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(99, 41);
            this.CancelButton.TabIndex = 40;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(149, 386);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(99, 41);
            this.SaveButton.TabIndex = 39;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(108, 280);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(323, 67);
            this.descriptionTextBox.TabIndex = 56;
            this.descriptionTextBox.Text = "";
            // 
            // messageLabel
            // 
            this.messageLabel.Location = new System.Drawing.Point(12, 350);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(498, 33);
            this.messageLabel.TabIndex = 57;
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UpdateAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(522, 450);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.comboBoxVisitTypes);
            this.Controls.Add(this.comboBoxLocations);
            this.Controls.Add(this.appointmentDayLabel);
            this.Controls.Add(this.comboBoxTimeSlots);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.comboBoxUsers);
            this.Controls.Add(this.comboBoxCustomers);
            this.Controls.Add(this.visitTypeLabel);
            this.Controls.Add(this.appointmentTitle);
            this.Controls.Add(this.appointmentTimeLabel);
            this.Controls.Add(this.locationLabel);
            this.Controls.Add(this.consultantLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.customerNameLabel);
            this.Controls.Add(this.exceptionLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "UpdateAppointment";
            this.Text = "Update Appointment";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label messageLabel;

        private System.Windows.Forms.RichTextBox descriptionTextBox;

        private System.Windows.Forms.ComboBox comboBoxVisitTypes;
        private System.Windows.Forms.ComboBox comboBoxLocations;
        private System.Windows.Forms.Label appointmentDayLabel;
        private System.Windows.Forms.ComboBox comboBoxTimeSlots;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.ComboBox comboBoxUsers;
        private System.Windows.Forms.ComboBox comboBoxCustomers;
        private System.Windows.Forms.Label visitTypeLabel;
        private System.Windows.Forms.Label appointmentTimeLabel;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.Label consultantLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label customerNameLabel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button SaveButton;

        private System.Windows.Forms.Label appointmentTitle;

        #endregion
    }
}