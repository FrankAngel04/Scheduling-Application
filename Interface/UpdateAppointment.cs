using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SchedulingApplication.Database;
using SchedulingApplication.Entities;

namespace SchedulingApplication
{
    public partial class UpdateAppointment : Form
    {
        private string appointmentId;

        public UpdateAppointment(string selectedAppointmentId)
        {
            InitializeComponent();
            appointmentId = selectedAppointmentId;
            LoadForm();
            StartPosition = FormStartPosition.CenterScreen;
            dateTimePicker.ValueChanged += dateTimePicker_ValueChanged;
            
            RefreshDateTimePickerAndTimeSlots();
        }

        private void LoadForm()
        {
            var customers = GetCustomerNames();
            var users = GetUserNames();
            comboBoxUsers.DataSource = new BindingSource(users, null);
            comboBoxCustomers.DataSource = new BindingSource(customers, null);
            comboBoxCustomers.DisplayMember = "Value";
            comboBoxCustomers.ValueMember = "Key";
            comboBoxUsers.DisplayMember = "Value";
            comboBoxUsers.ValueMember = "Key";
            comboBoxLocations.DataSource = Enum.GetNames(typeof(Locations));
            comboBoxVisitTypes.DataSource = Enum.GetNames(typeof(VisitTypes));

            UpdateAvailableSlots();
            LoadAppointmentDetails();
        }

        public void RefreshDateTimePickerAndTimeSlots()
        {
            dateTimePicker.Value = DateTime.Now;
            UpdateAvailableSlots();
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            UpdateAvailableSlots();
        }

        private void UpdateAvailableSlots()
        {
            DateTime selectedDate = dateTimePicker.Value;
            var availableSlots = GetAvailableSlots(selectedDate);
            comboBoxTimeSlots.DataSource = availableSlots;
        }

        // Eastern Standard Time is Default
        private bool IsWithinBusinessHours(DateTime start, DateTime end)
        {
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime startEST = TimeZoneInfo.ConvertTimeFromUtc(start, estZone);
            DateTime endEST = TimeZoneInfo.ConvertTimeFromUtc(end, estZone);

            bool isWeekday = startEST.DayOfWeek >= DayOfWeek.Monday && startEST.DayOfWeek <= DayOfWeek.Friday;
            bool isWithinHours = startEST.TimeOfDay >= new TimeSpan(9, 0, 0) && endEST.TimeOfDay <= new TimeSpan(17, 0, 0);

            return isWeekday && isWithinHours;
        }
        
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAppointmentFields();

                DateTime selectedDate = dateTimePicker.Value;
                string selectedTimeStr = comboBoxTimeSlots.SelectedItem.ToString();
                var startEndTime = ConvertStringToDateTime(selectedDate, selectedTimeStr);

                if (!IsWithinBusinessHours(startEndTime["StartTime"], startEndTime["EndTime"]))
                {
                    throw new Exception("Appointments must be scheduled during business hours (9:00 a.m. to 5:00 p.m., Monday–Friday, EST).");
                }

                var appointmentData = new Dictionary<string, string>
                {
                    { "CustomerId", ((KeyValuePair<int, string>)comboBoxCustomers.SelectedItem).Key.ToString() },
                    { "UserId", ((KeyValuePair<int, string>)comboBoxUsers.SelectedItem).Key.ToString() },
                    { "Description", descriptionTextBox.Text },
                    { "Location", comboBoxLocations.Text },
                    { "VisitType", comboBoxVisitTypes.Text },
                    { "Start", startEndTime["StartTime"].ToString("yyyy-MM-dd HH:mm:ss") },
                    { "End", startEndTime["EndTime"].ToString("yyyy-MM-dd HH:mm:ss") }
                };

                UpdateAppointmentInDatabase(appointmentData);

                AppointmentRecords appointmentRecordsForm = (AppointmentRecords)Application.OpenForms["AppointmentRecords"];
                appointmentRecordsForm.showdata();
                appointmentRecordsForm.UpdateExceptionLabel("Appointment updated successfully!", System.Drawing.Color.Green);
                Hide();
            }
            catch (Exception ex)
            {
                messageLabel.ForeColor = System.Drawing.Color.Red;
                messageLabel.Text = ex.Message;
            }
        }

        private void ValidateAppointmentFields()
        {
            descriptionTextBox.Text = descriptionTextBox.Text.Trim();
            comboBoxLocations.Text = comboBoxLocations.Text.Trim();
            comboBoxVisitTypes.Text = comboBoxVisitTypes.Text.Trim();

            if (string.IsNullOrWhiteSpace(descriptionTextBox.Text))
                throw new Exception("Description field cannot be empty or whitespace.");
            if (string.IsNullOrWhiteSpace(comboBoxLocations.Text))
                throw new Exception("Location field cannot be empty or whitespace.");
            if (string.IsNullOrWhiteSpace(comboBoxVisitTypes.Text))
                throw new Exception("Visit Type field cannot be empty or whitespace.");
            if (comboBoxCustomers.SelectedItem == null)
                throw new Exception("Please select a customer.");
            if (comboBoxUsers.SelectedItem == null)
                throw new Exception("Please select a consultant.");
            if (comboBoxTimeSlots.SelectedItem == null)
                throw new Exception("Please select a time slot.");
        }

        public Dictionary<string, DateTime> ConvertStringToDateTime(DateTime selectedDate, string selectedTimeStr)
        {
            TimeZoneInfo userTimeZone = TimeZoneInfo.Local;

            string[] times = selectedTimeStr.Split(new[] { " - " }, StringSplitOptions.None);
            DateTime startTime = DateTime.ParseExact(times[0], "hh:mm tt", null);
            DateTime endTime = DateTime.ParseExact(times[1], "hh:mm tt", null);

            DateTime startDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, startTime.Hour, startTime.Minute, 0);
            DateTime endDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, endTime.Hour, endTime.Minute, 0);

            DateTime startDateTimeUTC = TimeZoneInfo.ConvertTimeToUtc(startDateTime, userTimeZone);
            DateTime endDateTimeUTC = TimeZoneInfo.ConvertTimeToUtc(endDateTime, userTimeZone);

            return new Dictionary<string, DateTime> { { "StartTime", startDateTimeUTC }, { "EndTime", endDateTimeUTC } };
        }

        private void UpdateAppointmentInDatabase(Dictionary<string, string> appointmentData)
        {
            try
            {
                DBConnection.OpenConnection();
                using (var conn = DBConnection.conn)
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        MySqlCommand cmd = conn.CreateCommand();
                        cmd.Transaction = transaction;

                        cmd.CommandText = Queries.UpdateAppointment;
                        cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                        cmd.Parameters.AddWithValue("@CustomerId", appointmentData["CustomerId"]);
                        cmd.Parameters.AddWithValue("@UserId", appointmentData["UserId"]);
                        cmd.Parameters.AddWithValue("@Title", "not needed");
                        cmd.Parameters.AddWithValue("@Description", appointmentData["Description"]);
                        cmd.Parameters.AddWithValue("@Location", appointmentData["Location"]);
                        cmd.Parameters.AddWithValue("@Contact", User.CurrentUser.UserName);
                        cmd.Parameters.AddWithValue("@Type", appointmentData["VisitType"]);
                        cmd.Parameters.AddWithValue("@URL", "not needed");
                        cmd.Parameters.AddWithValue("@Start", appointmentData["Start"]);
                        cmd.Parameters.AddWithValue("@End", appointmentData["End"]);
                        cmd.Parameters.AddWithValue("@CreatedBy", User.CurrentUser.UserName);
                        cmd.Parameters.AddWithValue("@LastUpdateBy", User.CurrentUser.UserName);

                        try
                        {
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (MySqlException ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"SQL Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DBConnection.CloseConnection();
            }
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public Dictionary<int, string> GetCustomerNames()
        {
            Dictionary<int, string> customerNames = new Dictionary<int, string>();
            try
            {
                DBConnection.OpenConnection();
                using (MySqlCommand cmd = new MySqlCommand(Queries.GetCustomers, DBConnection.conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customerNames.Add(reader.GetInt32("customerId"), reader.GetString("customerName"));
                        }
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
            return customerNames;
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

        private List<string> GetAvailableSlots(DateTime date)
        {
            var allSlots = GenerateAllSlots(date);
            var bookedSlots = GetBookedSlots(date);

            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            var availableSlots = allSlots
                .Where(slot => !bookedSlots.Any(bookedSlot => bookedSlot.Item1 < slot.Item2 && bookedSlot.Item2 > slot.Item1) &&
                               slot.Item1.DayOfWeek >= DayOfWeek.Monday && slot.Item1.DayOfWeek <= DayOfWeek.Friday &&
                               TimeZoneInfo.ConvertTimeFromUtc(slot.Item1, estZone).TimeOfDay >= new TimeSpan(9, 0, 0) &&
                               TimeZoneInfo.ConvertTimeFromUtc(slot.Item2, estZone).TimeOfDay <= new TimeSpan(17, 0, 0))
                .ToList();

            return availableSlots
                .Select(slot => $"{TimeZoneInfo.ConvertTimeFromUtc(slot.Item1, TimeZoneInfo.Local):hh:mm tt} - {TimeZoneInfo.ConvertTimeFromUtc(slot.Item2, TimeZoneInfo.Local):hh:mm tt}")
                .ToList();
        }

        private List<Tuple<DateTime, DateTime>> GenerateAllSlots(DateTime date)
        {
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime startHourEST = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0, DateTimeKind.Unspecified);
            DateTime endHourEST = new DateTime(date.Year, date.Month, date.Day, 17, 0, 0, DateTimeKind.Unspecified);

            var allSlots = new List<Tuple<DateTime, DateTime>>();
            DateTime currentSlot = startHourEST;
            while (currentSlot < endHourEST)
            {
                allSlots.Add(new Tuple<DateTime, DateTime>(
                    TimeZoneInfo.ConvertTimeToUtc(currentSlot, estZone),
                    TimeZoneInfo.ConvertTimeToUtc(currentSlot.AddMinutes(30), estZone)));
                currentSlot = currentSlot.AddMinutes(30);
            }

            return allSlots;
        }

        public List<Tuple<DateTime, DateTime>> GetBookedSlots(DateTime date)
        {
            var bookedSlots = new List<Tuple<DateTime, DateTime>>();
            try
            {
                DBConnection.OpenConnection();
                using (var cmd = new MySqlCommand(Queries.GetAppointmentTimes, DBConnection.conn))
                {
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var start = reader.GetDateTime("start");
                            var end = reader.GetDateTime("end");
                            bookedSlots.Add(new Tuple<DateTime, DateTime>(start, end));
                        }
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

            return bookedSlots;
        }

        private bool IsSlotBooked(Tuple<DateTime, DateTime> slot, List<Tuple<DateTime, DateTime>> bookedSlots)
        {
            return bookedSlots.Any(bookedSlot => bookedSlot.Item1 < slot.Item2 && bookedSlot.Item2 > slot.Item1);
        }
        
        private List<string> ConvertSlotsToString(List<Tuple<DateTime, DateTime>> availableSlots)
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;
        
            return availableSlots.Select(slot =>
                    $"{TimeZoneInfo.ConvertTimeFromUtc(slot.Item1, localZone):hh:mm tt} - {TimeZoneInfo.ConvertTimeFromUtc(slot.Item2, localZone):hh:mm tt}")
                .ToList();
        }

        private void LoadAppointmentDetails()
        {
            try
            {
                DBConnection.OpenConnection();
                using (var conn = DBConnection.conn)
                {
                    var query = @"
                SELECT customerId, userId, description, location, type, start, end 
                FROM appointment 
                WHERE appointmentId = @AppointmentId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                comboBoxCustomers.SelectedValue = reader.GetInt32("customerId");
                                comboBoxUsers.SelectedValue = reader.GetInt32("userId");
                                descriptionTextBox.Text = reader.GetString("description");
                                comboBoxLocations.Text = reader.GetString("location");
                                comboBoxVisitTypes.Text = reader.GetString("type");
                                dateTimePicker.Value = reader.GetDateTime("start").ToLocalTime();

                                var start = reader.GetDateTime("start").ToLocalTime();
                                var end = reader.GetDateTime("end").ToLocalTime();
                                comboBoxTimeSlots.Text = $"{start:hh:mm tt} - {end:hh:mm tt}";
                            }
                        }
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
        }

    }
}
