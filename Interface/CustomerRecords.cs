using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SchedulingApplication.Database;
using SchedulingApplication.Entities;

namespace SchedulingApplication
{
    public partial class CustomerRecords : Form
    {
        public CustomerRecords()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            showdata();
            customerLabel.BorderStyle = BorderStyle.Fixed3D;
            customerLabel.BackColor = System.Drawing.Color.LightGray;
            
            exceptionLabel.Location = new System.Drawing.Point(customersDataGridView.Location.X, customersDataGridView.Location.Y + customersDataGridView.Height + 10);
        }

        private void CustomerRecords_Load(object sender, EventArgs e)
        {
            customersDataGridView.ClearSelection();
        }
        
        public void showdata()
        {
            customersDataGridView.DataSource = GetCustomers(Queries.CustomerTable);
            CustomerDGV();
            customersDataGridView.ClearSelection();
        }

        private void CustomerDGV()
        {
            customersDataGridView.Columns["customerId"].HeaderText = "Customer ID";
            customersDataGridView.Columns["customerName"].HeaderText = "Customer Name";
            customersDataGridView.Columns["address"].HeaderText = "Address";
            customersDataGridView.Columns["postalCode"].HeaderText = "Zip Code";
            customersDataGridView.Columns["phone"].HeaderText = "Phone";
            customersDataGridView.Columns["city"].HeaderText = "City";
            customersDataGridView.Columns["country"].HeaderText = "Country";

            customersDataGridView.Columns["customerId"].Visible = false;
            customersDataGridView.Columns["addressId"].Visible = false;
            customersDataGridView.Columns["cityId"].Visible = false;
            customersDataGridView.Columns["countryId"].Visible = false;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            new AddCustomer().Show();
            showdata(); // Refresh data after adding a customer
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (customersDataGridView.SelectedRows.Count == 0) throw new Exception("Please select a customer to update.");
            
                string selectedCustomerId = customersDataGridView.SelectedRows[0].Cells["customerId"].Value.ToString();
                new UpdateCustomer(selectedCustomerId).Show();
                showdata(); // Refresh data after adding a customer
            }
            catch (Exception ex)
            {
                exceptionLabel.ForeColor = System.Drawing.Color.Red;
                exceptionLabel.Text = ex.Message;
            }
        }

       private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (customersDataGridView.SelectedRows.Count == 0) throw new Exception("Please select a customer to delete.");

                // Get the selected customer ID
                int selectedCustomerId = Convert.ToInt32(customersDataGridView.SelectedRows[0].Cells["customerId"].Value);
                int addressId = Convert.ToInt32(customersDataGridView.SelectedRows[0].Cells["addressId"].Value);
                int cityId = Convert.ToInt32(customersDataGridView.SelectedRows[0].Cells["cityId"].Value);
                int countryId = Convert.ToInt32(customersDataGridView.SelectedRows[0].Cells["countryId"].Value);

                // Show confirmation dialog
                var confirmResult = MessageBox.Show("Are you sure you want to delete this customer?", 
                                                     "Confirm Delete", 
                                                     MessageBoxButtons.YesNo, 
                                                     MessageBoxIcon.Warning);

                // If the user clicked 'Yes', proceed with the deletion
                if (confirmResult == DialogResult.Yes)
                {
                    // Delete the customer from the database
                    DBConnection.OpenConnection();
                    using (var conn = DBConnection.conn)
                    {
                        using (var transaction = conn.BeginTransaction())
                        {
                            MySqlCommand cmd = conn.CreateCommand();
                            cmd.Transaction = transaction;

                            // Delete customer's appointments first
                            cmd.CommandText = Queries.DeleteCustomerAppointments;
                            cmd.Parameters.AddWithValue("@CustomerId", selectedCustomerId);
                            cmd.ExecuteNonQuery();

                            // Delete the customer
                            cmd.CommandText = Queries.DeleteCustomer;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@CustomerId", selectedCustomerId);
                            cmd.ExecuteNonQuery();

                            // Delete the address
                            cmd.CommandText = Queries.DeleteAddress;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@AddressId", addressId);
                            cmd.ExecuteNonQuery();

                            // Delete the city if no other addresses are using it
                            cmd.CommandText = Queries.DeleteCityIfUnused;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@CityId", cityId);
                            cmd.ExecuteNonQuery();

                            // Delete the country if no other cities are using it
                            cmd.CommandText = Queries.DeleteCountryIfUnused;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@CountryId", countryId);
                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                        }
                    }
                    DBConnection.CloseConnection();

                    UpdateExceptionLabel("Customer deleted successfully!", System.Drawing.Color.Green);

                    // Refresh the DataGridView to reflect changes
                    showdata();
                }
                else
                {
                    // If 'No' is clicked, do nothing
                    UpdateExceptionLabel("Customer deletion canceled.", System.Drawing.Color.Orange);
                }
            }
            catch (Exception ex)
            {
                exceptionLabel.ForeColor = System.Drawing.Color.Red;
                exceptionLabel.Text = ex.Message;
            }
        }


        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void appointmentLabel_Click(object sender, EventArgs e)
        {
            new AppointmentRecords().Show();
            Close();
        }
        
        private void appointmentLabel_MouseEnter(object sender, EventArgs e)
        {
            appointmentLabel.BackColor = System.Drawing.Color.LightGray;
        }
        
        private void appointmentLabel_MouseLeave(object sender, EventArgs e)
        {
            appointmentLabel.BackColor = System.Drawing.Color.Transparent;
        }
        
        private void reportLabel_Click(object sender, EventArgs e)
        {
            new Reports().Show();
            Close();
        }
        
        private void reportLabel_MouseEnter(object sender, EventArgs e)
        {
            reportLabel.BackColor = System.Drawing.Color.LightGray;
        }
        
        private void reportLabel_MouseLeave(object sender, EventArgs e)
        {
            reportLabel.BackColor = System.Drawing.Color.Transparent;
        }

        public void UpdateExceptionLabel(string message, System.Drawing.Color color)
        {
            exceptionLabel.ForeColor = color;
            exceptionLabel.Text = message;
        }

        
        public DataTable GetCustomers(string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                DBConnection.OpenConnection();

                using (MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DBConnection.CloseConnection();
            }

            return dataTable;
        }
    }
}
