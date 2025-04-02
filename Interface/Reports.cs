using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SchedulingApplication.Database;
using SchedulingApplication.Entities;

namespace SchedulingApplication
{
    public partial class Reports : Form
    {
        private string selectedUserId = Session.CurrentUserId.ToString();
        private string selectedUserName = Session.CurrentUserName;
        
        public Reports()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            
            PopulateConsultantComboBox();
            PopulateMonthComboBox();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            GenerateAppointmentTypesByMonthReport();
            GenerateUserScheduleReport();
            GenerateAppointmentCountByLocationReport();
            userScheduleDataGridView.ClearSelection();
            customerAppointmentCountDataGridView.ClearSelection();
            appointmentTypesByMonthDataGridView.ClearSelection();
        }

       private void PopulateMonthComboBox()
        {
            comboBoxMonthFilter.SelectedIndex = DateTime.Now.Month - 1; // Select current month by default
        }

        private void MonthFilter_Selected(object sender, EventArgs e)
        {
            GenerateAppointmentTypesByMonthReport();
        }

        private void GenerateAppointmentTypesByMonthReport()
        {
            var appointmentTypesByMonth = new DataTable();
            try
            {
                int selectedMonth = comboBoxMonthFilter.SelectedIndex + 1;
                int currentYear = DateTime.Now.Year;

                DBConnection.OpenConnection();
                using (var cmd = new MySqlCommand(Queries.GetAppointmentTypesByMonth, DBConnection.conn))
                {
                    cmd.Parameters.AddWithValue("@month", selectedMonth);
                    cmd.Parameters.AddWithValue("@year", currentYear);
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(appointmentTypesByMonth);
                    }
                }
                // Check if the DataTable is empty
                if (appointmentTypesByMonth.Rows.Count == 0)
                {
                    customerAppointmentCountDataGridView.DataSource = null;
                    
                    messageMonthLabel.ForeColor = System.Drawing.Color.Red;
                    messageMonthLabel.Text = "No data available for the selected month.";
                    return;
                }
                else
                {
                    messageMonthLabel.ForeColor = System.Drawing.Color.Green;
                    messageMonthLabel.Text = "Data available for the selected month.";
                }

                var columnType = appointmentTypesByMonth.Columns["Number of Appointments"].DataType;

                DataTable filteredSortedData;
                
                // Uses a lambda expression to sort the filtered rows in descending order by the Number of Appointments column
                // Filters and sorts based on the column type
                if (columnType == typeof(int))
                {
                    filteredSortedData = appointmentTypesByMonth.AsEnumerable()
                        .Where(row => row.Field<int>("Number of Appointments") > 0)
                        .OrderByDescending(row => row.Field<int>("Number of Appointments"))
                        .CopyToDataTable();
                }
                else if (columnType == typeof(long))
                {
                    filteredSortedData = appointmentTypesByMonth.AsEnumerable()
                        .Where(row => row.Field<long>("Number of Appointments") > 0)
                        .OrderByDescending(row => row.Field<long>("Number of Appointments"))
                        .CopyToDataTable();
                }
                else if (columnType == typeof(decimal))
                {
                    filteredSortedData = appointmentTypesByMonth.AsEnumerable()
                        .Where(row => row.Field<decimal>("Number of Appointments") > 0)
                        .OrderByDescending(row => row.Field<decimal>("Number of Appointments"))
                        .CopyToDataTable();
                }
                else if (columnType == typeof(double))
                {
                    filteredSortedData = appointmentTypesByMonth.AsEnumerable()
                        .Where(row => row.Field<double>("Number of Appointments") > 0)
                        .OrderByDescending(row => row.Field<double>("Number of Appointments"))
                        .CopyToDataTable();
                }
                else
                {
                    throw new InvalidOperationException("Unsupported column type for filtering and sorting");
                }

                customerAppointmentCountDataGridView.DataSource = filteredSortedData;

                // Set headers for the columns
                customerAppointmentCountDataGridView.Columns["Appointment Type"].HeaderText = "Appointment Type";
                customerAppointmentCountDataGridView.Columns["Number of Appointments"].HeaderText = "Number of Appointments";

                // Clear selection to prevent the first row from being highlighted
                customerAppointmentCountDataGridView.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DBConnection.CloseConnection();
            }
        }

        private void PopulateConsultantComboBox()
        {
            try
            {
                var users = GetUserNames();
                comboBoxConsultant.DataSource = new BindingSource(users, null);
                comboBoxConsultant.DisplayMember = "Value";
                comboBoxConsultant.ValueMember = "Key";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Dictionary<int, string> GetUserNames()
        {
            Dictionary<int, string> userNames = new Dictionary<int, string>();

            try
            {
                DBConnection.OpenConnection();

                using (var cmd = new MySqlCommand(Queries.GetUsers, DBConnection.conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userNames.Add(reader.GetInt32("userId"), reader.GetString("userName"));
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

            return userNames;
        }

        private void GenerateUserScheduleReport()
        {
            if (string.IsNullOrEmpty(selectedUserId) || string.IsNullOrEmpty(selectedUserName))
            {
                MessageBox.Show("Please select a consultant.");
                return;
            }

            var userSchedule = new DataTable();
            try
            {
                DBConnection.OpenConnection();
                using (var cmd = new MySqlCommand(Queries.GetUserSchedule, DBConnection.conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", selectedUserName);
                    cmd.Parameters.AddWithValue("@UserId", selectedUserId);
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(userSchedule);
                    }
                }

                // Check if the DataTable is empty
                if (userSchedule.Rows.Count == 0)
                {
                    userScheduleDataGridView.DataSource = null;
                    
                    messageUserLabel.ForeColor = System.Drawing.Color.Red;
                    messageUserLabel.Text = "No data available for the selected consultant.";
                    return;
                }
                else
                {
                    messageUserLabel.ForeColor = System.Drawing.Color.Green;
                    messageUserLabel.Text = "Data available for the selected consultant.";
                }
                
                // Convert start and end columns to local time
                foreach (DataRow row in userSchedule.Rows)
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
                
                // Sort the DataTable using a lambda expression
                var sortedUserSchedule = userSchedule.AsEnumerable()
                                                     .OrderBy(row => row.Field<DateTime>("start"))
                                                     .CopyToDataTable();

                userScheduleDataGridView.DataSource = sortedUserSchedule;

                // Set headers for the columns
                userScheduleDataGridView.Columns["userName"].HeaderText = "Consultant";
                userScheduleDataGridView.Columns["customerName"].HeaderText = "Customer Name";
                userScheduleDataGridView.Columns["phone"].HeaderText = "Phone";
                userScheduleDataGridView.Columns["description"].HeaderText = "Description";
                userScheduleDataGridView.Columns["location"].HeaderText = "Location";
                userScheduleDataGridView.Columns["type"].HeaderText = "Visit Type";
                userScheduleDataGridView.Columns["url"].HeaderText = "Visit Link";
                userScheduleDataGridView.Columns["start"].HeaderText = "Start";
                userScheduleDataGridView.Columns["end"].HeaderText = "End";

                // Hide the specified columns
                userScheduleDataGridView.Columns["customerId"].Visible = false;
                userScheduleDataGridView.Columns["addressId"].Visible = false;
                userScheduleDataGridView.Columns["cityId"].Visible = false;
                userScheduleDataGridView.Columns["countryId"].Visible = false;
                userScheduleDataGridView.Columns["appointmentId"].Visible = false;
                userScheduleDataGridView.Columns["userId"].Visible = false;

                // Set the display order for the columns
                userScheduleDataGridView.Columns["userName"].DisplayIndex = 0;
                userScheduleDataGridView.Columns["customerName"].DisplayIndex = 1;
                userScheduleDataGridView.Columns["phone"].DisplayIndex = 2;
                userScheduleDataGridView.Columns["description"].DisplayIndex = 3;
                userScheduleDataGridView.Columns["location"].DisplayIndex = 4;
                userScheduleDataGridView.Columns["type"].DisplayIndex = 5;
                userScheduleDataGridView.Columns["url"].DisplayIndex = 6;
                userScheduleDataGridView.Columns["start"].DisplayIndex = 7;
                userScheduleDataGridView.Columns["end"].DisplayIndex = 8;
                
                foreach (DataGridViewRow row in userScheduleDataGridView.Rows)
                {
                    row.Cells["start"].Value = ((DateTime)row.Cells["start"].Value).ToString("MM/dd/yyyy hh:mm tt");
                    row.Cells["end"].Value = ((DateTime)row.Cells["end"].Value).ToString("MM/dd/yyyy hh:mm tt");
                }
                
                // Clear selection to prevent the first row from being highlighted
                userScheduleDataGridView.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DBConnection.CloseConnection();
            }
        }

        private void Consultant_Selected(object sender, EventArgs e)
        {
            selectedUserId = ((KeyValuePair<int, string>)comboBoxConsultant.SelectedItem).Key.ToString();
            selectedUserName = ((KeyValuePair<int, string>)comboBoxConsultant.SelectedItem).Value;

            GenerateUserScheduleReport();
        }
        
        public DataTable GetConsultantSchedule(string userName, string userId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                DBConnection.OpenConnection();
                using (MySqlCommand consultantCMD = new MySqlCommand(Queries.GetUserSchedule, DBConnection.conn))
                {
                    consultantCMD.Parameters.AddWithValue("@UserName", userName);
                    consultantCMD.Parameters.AddWithValue("@UserId", userId);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(consultantCMD))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error ID: 12121");
            }
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
            return dataTable;
        }
        
        private void GenerateAppointmentCountByLocationReport()
        {
            var appointmentCountByLocation = new DataTable();
            try
            {
                DBConnection.OpenConnection();
                using (var cmd = new MySqlCommand(Queries.GetAppointmentCountByLocation, DBConnection.conn))
                {
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(appointmentCountByLocation);
                    }
                }

                // Check if the DataTable is empty
                if (appointmentCountByLocation.Rows.Count == 0)
                {
                    appointmentTypesByMonthDataGridView.DataSource = null;
                    
                    messageLocationLabel.ForeColor = System.Drawing.Color.Red;
                    messageLocationLabel.Text = "No data available for appointment count by location.";
                    return;
                }
                else
                {
                    messageLocationLabel.ForeColor = System.Drawing.Color.Green;
                    messageLocationLabel.Text = "Data available for appointment count by location.";
                }
                
                // Determine the type of the "appointment_count" column
                var columnType = appointmentCountByLocation.Columns["appointment_count"].DataType;

                DataTable sortedData;

                // Uses a lambda expression to sort the data by appointment count
                if (columnType == typeof(int))
                {
                    sortedData = appointmentCountByLocation.AsEnumerable()
                        .OrderBy(row => row.Field<int>("appointment_count"))
                        .CopyToDataTable();
                }
                else if (columnType == typeof(long))
                {
                    sortedData = appointmentCountByLocation.AsEnumerable()
                        .OrderBy(row => row.Field<long>("appointment_count"))
                        .CopyToDataTable();
                }
                else if (columnType == typeof(decimal))
                {
                    sortedData = appointmentCountByLocation.AsEnumerable()
                        .OrderBy(row => row.Field<decimal>("appointment_count"))
                        .CopyToDataTable();
                }
                else if (columnType == typeof(double))
                {
                    sortedData = appointmentCountByLocation.AsEnumerable()
                        .OrderBy(row => row.Field<double>("appointment_count"))
                        .CopyToDataTable();
                }
                else
                {
                    throw new InvalidOperationException("Unsupported column type for sorting");
                }

                appointmentTypesByMonthDataGridView.DataSource = sortedData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DBConnection.CloseConnection();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            new AppointmentRecords().Show();
            Close();
        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            Close();
            new LoginForm().Show();
        }
    }
}
