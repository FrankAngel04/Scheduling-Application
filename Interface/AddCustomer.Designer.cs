using System.ComponentModel;

namespace SchedulingApplication
{
    partial class AddCustomer
   {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.TextBox phoneNumberTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.Label phoneNumberLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;

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
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.phoneNumberTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.addressLabel = new System.Windows.Forms.Label();
            this.phoneNumberLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.cityLabel = new System.Windows.Forms.Label();
            this.countryLabel = new System.Windows.Forms.Label();
            this.cityTextBox = new System.Windows.Forms.TextBox();
            this.countryTextBox = new System.Windows.Forms.TextBox();
            this.zipCodeLabel = new System.Windows.Forms.Label();
            this.zipCodeTextBox = new System.Windows.Forms.TextBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.addCustomerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(158, 82);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(151, 20);
            this.nameTextBox.TabIndex = 0;
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(158, 124);
            this.addressTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(151, 20);
            this.addressTextBox.TabIndex = 1;
            // 
            // phoneNumberTextBox
            // 
            this.phoneNumberTextBox.Location = new System.Drawing.Point(158, 313);
            this.phoneNumberTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.phoneNumberTextBox.Name = "phoneNumberTextBox";
            this.phoneNumberTextBox.Size = new System.Drawing.Size(151, 20);
            this.phoneNumberTextBox.TabIndex = 5;
            this.phoneNumberTextBox.TextChanged += new System.EventHandler(this.phoneNumberTextBox_TextChanged);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(158, 67);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(38, 13);
            this.nameLabel.TabIndex = 3;
            this.nameLabel.Text = "Name:";
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(158, 109);
            this.addressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(48, 13);
            this.addressLabel.TabIndex = 4;
            this.addressLabel.Text = "Address:";
            // 
            // phoneNumberLabel
            // 
            this.phoneNumberLabel.AutoSize = true;
            this.phoneNumberLabel.Location = new System.Drawing.Point(158, 298);
            this.phoneNumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.phoneNumberLabel.Name = "phoneNumberLabel";
            this.phoneNumberLabel.Size = new System.Drawing.Size(81, 13);
            this.phoneNumberLabel.TabIndex = 5;
            this.phoneNumberLabel.Text = "Phone Number:";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(158, 378);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(56, 22);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(253, 378);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(56, 22);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // cityLabel
            // 
            this.cityLabel.Location = new System.Drawing.Point(158, 156);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Size = new System.Drawing.Size(48, 14);
            this.cityLabel.TabIndex = 11;
            this.cityLabel.Text = "City:";
            // 
            // countryLabel
            // 
            this.countryLabel.Location = new System.Drawing.Point(158, 253);
            this.countryLabel.Name = "countryLabel";
            this.countryLabel.Size = new System.Drawing.Size(50, 15);
            this.countryLabel.TabIndex = 12;
            this.countryLabel.Text = "Country:";
            // 
            // cityTextBox
            // 
            this.cityTextBox.Location = new System.Drawing.Point(158, 173);
            this.cityTextBox.Name = "cityTextBox";
            this.cityTextBox.Size = new System.Drawing.Size(151, 20);
            this.cityTextBox.TabIndex = 2;
            // 
            // countryTextBox
            // 
            this.countryTextBox.Location = new System.Drawing.Point(158, 271);
            this.countryTextBox.Name = "countryTextBox";
            this.countryTextBox.Size = new System.Drawing.Size(151, 20);
            this.countryTextBox.TabIndex = 4;
            // 
            // zipCodeLabel
            // 
            this.zipCodeLabel.Location = new System.Drawing.Point(158, 208);
            this.zipCodeLabel.Name = "zipCodeLabel";
            this.zipCodeLabel.Size = new System.Drawing.Size(60, 16);
            this.zipCodeLabel.TabIndex = 15;
            this.zipCodeLabel.Text = "Zip Code:";
            // 
            // zipCodeTextBox
            // 
            this.zipCodeTextBox.Location = new System.Drawing.Point(158, 227);
            this.zipCodeTextBox.Name = "zipCodeTextBox";
            this.zipCodeTextBox.Size = new System.Drawing.Size(151, 20);
            this.zipCodeTextBox.TabIndex = 3;
            // 
            // messageLabel
            // 
            this.messageLabel.Location = new System.Drawing.Point(12, 342);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(426, 23);
            this.messageLabel.TabIndex = 16;
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addCustomerLabel
            // 
            this.addCustomerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCustomerLabel.Location = new System.Drawing.Point(158, 9);
            this.addCustomerLabel.Name = "addCustomerLabel";
            this.addCustomerLabel.Size = new System.Drawing.Size(151, 48);
            this.addCustomerLabel.TabIndex = 17;
            this.addCustomerLabel.Text = "Add Customer";
            this.addCustomerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(450, 429);
            this.Controls.Add(this.addCustomerLabel);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.zipCodeTextBox);
            this.Controls.Add(this.zipCodeLabel);
            this.Controls.Add(this.countryTextBox);
            this.Controls.Add(this.cityTextBox);
            this.Controls.Add(this.countryLabel);
            this.Controls.Add(this.cityLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.phoneNumberLabel);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.phoneNumberTextBox);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AddCustomer";
            this.Text = "Add Customer Form";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Label addCustomerLabel;

        private System.Windows.Forms.Label zipCodeLabel;
        private System.Windows.Forms.TextBox zipCodeTextBox;

        private System.Windows.Forms.TextBox cityTextBox;
        private System.Windows.Forms.TextBox countryTextBox;

        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.Label countryLabel;
    }
}