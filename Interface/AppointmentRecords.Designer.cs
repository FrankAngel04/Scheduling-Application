using System.ComponentModel;

namespace SchedulingApplication
{
    partial class AppointmentRecords
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.DataGridView appointmentsDataGridView;
        private System.Windows.Forms.Label exceptionLabel;

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
            this.addButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.appointmentsDataGridView = new System.Windows.Forms.DataGridView();
            this.exceptionLabel = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            this.reportLabel = new System.Windows.Forms.Label();
            this.customerLabel = new System.Windows.Forms.Label();
            this.appointmentLabel = new System.Windows.Forms.Label();
            this.comboBoxFilter = new System.Windows.Forms.ComboBox();
            this.filterLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(291, 451);
            this.addButton.Margin = new System.Windows.Forms.Padding(2);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 37);
            this.addButton.TabIndex = 6;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(414, 451);
            this.updateButton.Margin = new System.Windows.Forms.Padding(2);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 37);
            this.updateButton.TabIndex = 7;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(537, 451);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 37);
            this.deleteButton.TabIndex = 8;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // appointmentsDataGridView
            // 
            this.appointmentsDataGridView.AllowUserToAddRows = false;
            this.appointmentsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.appointmentsDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.appointmentsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.appointmentsDataGridView.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.appointmentsDataGridView.Location = new System.Drawing.Point(38, 68);
            this.appointmentsDataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.appointmentsDataGridView.MultiSelect = false;
            this.appointmentsDataGridView.Name = "appointmentsDataGridView";
            this.appointmentsDataGridView.ReadOnly = true;
            this.appointmentsDataGridView.RowHeadersVisible = false;
            this.appointmentsDataGridView.RowHeadersWidth = 51;
            this.appointmentsDataGridView.RowTemplate.Height = 24;
            this.appointmentsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.appointmentsDataGridView.Size = new System.Drawing.Size(968, 348);
            this.appointmentsDataGridView.TabIndex = 9;
            // 
            // exceptionLabel
            // 
            this.exceptionLabel.AutoSize = true;
            this.exceptionLabel.ForeColor = System.Drawing.Color.Red;
            this.exceptionLabel.Location = new System.Drawing.Point(38, 301);
            this.exceptionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.exceptionLabel.Name = "exceptionLabel";
            this.exceptionLabel.Size = new System.Drawing.Size(0, 13);
            this.exceptionLabel.TabIndex = 10;
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(892, 451);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(75, 37);
            this.Exit.TabIndex = 11;
            this.Exit.Text = "Log Out";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // reportLabel
            // 
            this.reportLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportLabel.Location = new System.Drawing.Point(571, 32);
            this.reportLabel.Name = "reportLabel";
            this.reportLabel.Size = new System.Drawing.Size(152, 29);
            this.reportLabel.TabIndex = 17;
            this.reportLabel.Text = "Reports";
            this.reportLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.reportLabel.Click += new System.EventHandler(this.reportLabel_Click);
            this.reportLabel.MouseEnter += new System.EventHandler(this.reportLabel_MouseEnter);
            this.reportLabel.MouseLeave += new System.EventHandler(this.reportLabel_MouseLeave);
            // 
            // customerLabel
            // 
            this.customerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerLabel.Location = new System.Drawing.Point(282, 32);
            this.customerLabel.Name = "customerLabel";
            this.customerLabel.Size = new System.Drawing.Size(128, 29);
            this.customerLabel.TabIndex = 15;
            this.customerLabel.Text = "Customers";
            this.customerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.customerLabel.Click += new System.EventHandler(this.customerLabel_Click);
            this.customerLabel.MouseEnter += new System.EventHandler(this.customerLabel_MouseEnter);
            this.customerLabel.MouseLeave += new System.EventHandler(this.customerLabel_MouseLeave);
            // 
            // appointmentLabel
            // 
            this.appointmentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appointmentLabel.Location = new System.Drawing.Point(416, 32);
            this.appointmentLabel.Name = "appointmentLabel";
            this.appointmentLabel.Size = new System.Drawing.Size(149, 29);
            this.appointmentLabel.TabIndex = 16;
            this.appointmentLabel.Text = "Appointments";
            this.appointmentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxFilter
            // 
            this.comboBoxFilter.FormattingEnabled = true;
            this.comboBoxFilter.Location = new System.Drawing.Point(885, 40);
            this.comboBoxFilter.Name = "comboBoxFilter";
            this.comboBoxFilter.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFilter.TabIndex = 18;
            this.comboBoxFilter.SelectedIndexChanged += new System.EventHandler(this.AppointmentFilter_Selected);
            // 
            // filterLabel
            // 
            this.filterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterLabel.Location = new System.Drawing.Point(841, 43);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Size = new System.Drawing.Size(38, 15);
            this.filterLabel.TabIndex = 19;
            this.filterLabel.Text = "Filter";
            // 
            // AppointmentRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1033, 499);
            this.Controls.Add(this.filterLabel);
            this.Controls.Add(this.comboBoxFilter);
            this.Controls.Add(this.reportLabel);
            this.Controls.Add(this.appointmentLabel);
            this.Controls.Add(this.customerLabel);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.exceptionLabel);
            this.Controls.Add(this.appointmentsDataGridView);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.addButton);
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AppointmentRecords";
            this.Load += new System.EventHandler(this.AppointmentRecords_Load);
            ((System.ComponentModel.ISupportInitialize)(this.appointmentsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.Label filterLabel;

        private System.Windows.Forms.Label customerLabel;
        private System.Windows.Forms.Label appointmentLabel;
        private System.Windows.Forms.Label reportLabel;
    }
}