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
    public partial class AppointmentRecords : Form
    {
        // Default filter state
        public string FilterState = "All"; 
        public AppointmentRecords()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            
            // Populate comboBoxFilter with options
            comboBoxFilter.Items.Add("All");
            comboBoxFilter.Items.Add("Weekly");
            comboBoxFilter.Items.Add("Monthly");
            comboBoxFilter.SelectedIndex = 0; // Set default selected index

            showdata();
            appointmentLabel.BorderStyle = BorderStyle.Fixed3D;
            appointmentLabel.BackColor = System.Drawing.Color.LightGray;
            exceptionLabel.Location = new System.Drawing.Point(appointmentsDataGridView.Location.X, appointmentsDataGridView.Location.Y + appointmentsDataGridView.Height + 10);
        }

        private void AppointmentRecords_Load(object sender, EventArgs e)
        {
            appointmentsDataGridView.ClearSelection();
        }

        public void showdata()
        {
            appointmentsDataGridView.DataSource = GetAppointments(FilterState);
            AppointmentDGV();
            appointmentsDataGridView.ClearSelection();
        }

        private void AppointmentDGV()
        {
            // Setting column titles
            appointmentsDataGridView.Columns["userName"].HeaderText = "Consultant";
            appointmentsDataGridView.Columns["customerName"].HeaderText = "Customer Name";
            appointmentsDataGridView.Columns["phone"].HeaderText = "Phone";
            appointmentsDataGridView.Columns["description"].HeaderText = "Description";
            appointmentsDataGridView.Columns["location"].HeaderText = "Location";
            appointmentsDataGridView.Columns["type"].HeaderText = "Visit Type";
            appointmentsDataGridView.Columns["url"].HeaderText = "Visit Link";
            appointmentsDataGridView.Columns["start"].HeaderText = "Start";
            appointmentsDataGridView.Columns["end"].HeaderText = "End";

            // Hide unnecessary columns
            appointmentsDataGridView.Columns["customerId"].Visible = false;
            appointmentsDataGridView.Columns["addressId"].Visible = false;
            appointmentsDataGridView.Columns["cityId"].Visible = false;
            appointmentsDataGridView.Columns["countryId"].Visible = false;
            appointmentsDataGridView.Columns["appointmentId"].Visible = false;
            appointmentsDataGridView.Columns["userId"].Visible = false;

            // Set the display order for the columns
            appointmentsDataGridView.Columns["userName"].DisplayIndex = 0;
            appointmentsDataGridView.Columns["customerName"].DisplayIndex = 1;
            appointmentsDataGridView.Columns["phone"].DisplayIndex = 2;
            appointmentsDataGridView.Columns["description"].DisplayIndex = 3;
            appointmentsDataGridView.Columns["location"].DisplayIndex = 4;
            appointmentsDataGridView.Columns["type"].DisplayIndex = 5;
            appointmentsDataGridView.Columns["url"].DisplayIndex = 6;
            appointmentsDataGridView.Columns["start"].DisplayIndex = 7;
            appointmentsDataGridView.Columns["end"].DisplayIndex = 8;
        }
        
        // View calendar by all/month/week
        private void AppointmentFilter_Selected(object sender, EventArgs e) 
        {
            if (comboBoxFilter.SelectedIndex == 1)
            {
                FilterState = "Weekly";
            }
            else if (comboBoxFilter.SelectedIndex == 2)
            {
                FilterState = "Monthly";
            }
            else
            {
                FilterState = "All";
            }
            appointmentsDataGridView.DataSource = GetAppointments(FilterState);
            AppointmentDGV();
        }

        public DataTable GetAppointments(string filter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                DateTime today = DateTime.Now;
                DateTime startDate = DateTime.MinValue;
                DateTime endDate = DateTime.MaxValue;

                DBConnection.OpenConnection();
                var conn = DBConnection.conn;

                if (filter == "Weekly")
                {
                    // Get current weekly date
                    int delta = DayOfWeek.Monday - today.DayOfWeek;
                    startDate = today.AddDays(delta).Date;
                    endDate = startDate.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);
                }
                else if (filter == "Monthly")
                {
                    // Get current monthly date
                    startDate = new DateTime(today.Year, today.Month, 1);
                    endDate = startDate.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
                }

                // Get filtered appointments
                if (filter == "All")
                {
                    using (MySqlCommand cmd = new MySqlCommand(Queries.GetAppointmentTable, DBConnection.conn))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                else
                {
                    using (MySqlCommand cmd = new MySqlCommand(Queries.FilteredAppointments, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd HH:mm:ss"));

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(dataTable);
                    }
                }
                // Convert start and end columns to local time
                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["start"] is DateTime startUtc)
                    {
                        row["start"] = TimeZoneInfo.ConvertTimeFromUtc(startUtc, TimeZoneInfo.Local);
                    }

                    if (row["end"] is DateTime endUtc)
                    {
                        row["end"] = TimeZoneInfo.ConvertTimeFromUtc(endUtc, TimeZoneInfo.Local);
                    }
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

        private void AddButton_Click(object sender, EventArgs e)
        {
            new AddAppointment().Show();
            showdata();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (appointmentsDataGridView.SelectedRows.Count == 0) throw new Exception("Please select an appointment to update.");

                string selectedAppointmentId = appointmentsDataGridView.SelectedRows[0].Cells["appointmentId"].Value.ToString();
                UpdateAppointment updateForm = new UpdateAppointment(selectedAppointmentId);

                updateForm.FormClosed += (s, args) => 
                {
                    updateForm.RefreshDateTimePickerAndTimeSlots();
                };

                updateForm.Show();
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
                if (appointmentsDataGridView.SelectedRows.Count == 0) throw new Exception("Please select an appointment to delete.");

                // Get the selected appointment ID
                int selectedAppointmentId = Convert.ToInt32(appointmentsDataGridView.SelectedRows[0].Cells["appointmentId"].Value);

                // Show confirmation dialog
                var confirmResult = MessageBox.Show("Are you sure you want to delete this appointment?", 
                                                     "Confirm Delete", 
                                                     MessageBoxButtons.YesNo, 
                                                     MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        // Delete the appointment from the database
                        DBConnection.OpenConnection();
                        using (var conn = DBConnection.conn)
                        {
                            using (var transaction = conn.BeginTransaction())
                            {
                                MySqlCommand cmd = conn.CreateCommand();
                                cmd.Transaction = transaction;

                                cmd.CommandText = Queries.DeleteAppointment;
                                cmd.Parameters.AddWithValue("@AppointmentId", selectedAppointmentId);
                                cmd.ExecuteNonQuery();

                                transaction.Commit();
                            }
                        }

                        UpdateExceptionLabel("Appointment deleted successfully!", System.Drawing.Color.Green);

                        // Refresh the DataGridView to reflect changes
                        showdata();
                    }
                    catch (MySqlException sqlEx)
                    {
                        // Handle SQL-specific exceptions
                        exceptionLabel.ForeColor = System.Drawing.Color.Red;
                        exceptionLabel.Text = $"Database error: {sqlEx.Message}";
                    }
                    catch (Exception ex)
                    {
                        // Handle any other general exceptions
                        exceptionLabel.ForeColor = System.Drawing.Color.Red;
                        exceptionLabel.Text = $"An error occurred: {ex.Message}";
                    }
                    finally
                    {
                        // Ensure the connection is always closed
                        DBConnection.CloseConnection();
                    }
                }
                else
                {
                    // If 'No' is clicked, do nothing
                    UpdateExceptionLabel("Appointment deletion canceled.", System.Drawing.Color.Orange);
                }
            }
            catch (Exception ex)
            {
                exceptionLabel.ForeColor = System.Drawing.Color.Red;
                exceptionLabel.Text = $"Error selecting appointment: {ex.Message}";
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
           new LoginForm().Show();
        }
        
        private void customerLabel_Click(object sender, EventArgs e)
        {
            new CustomerRecords().Show();
            Close();
        }
        
        private void customerLabel_MouseEnter(object sender, EventArgs e)
        {
            customerLabel.BackColor = System.Drawing.Color.LightGray;
        }

        private void customerLabel_MouseLeave(object sender, EventArgs e)
        {
            customerLabel.BackColor = System.Drawing.Color.Transparent;
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
        
    }
}
