using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SchedulingApplication.Database;
using SchedulingApplication.Entities;

namespace SchedulingApplication
{
    public partial class UpdateCustomer : Form
    {
        private Address address;
        private City city;
        private Country country;
        private Customer customer;

        public UpdateCustomer(string customerId)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            zipCodeTextBox.KeyPress += zipCodeTextBox_KeyPress;
            phoneNumberTextBox.KeyPress += phoneNumberTextBox_KeyPress;
            LoadCustomerData(customerId);
        }

        private void LoadCustomerData(string customerId)
        {
            DBConnection.OpenConnection();
            using (var conn = DBConnection.conn)
            {
                string query = @"
                    SELECT c.customerName, a.address, a.postalCode, a.phone, ci.city, co.country, a.addressId, ci.cityId, co.countryId
                    FROM customer c
                    JOIN address a ON c.addressId = a.addressId
                    JOIN city ci ON a.cityId = ci.cityId
                    JOIN country co ON ci.countryId = co.countryId
                    WHERE c.customerId = @CustomerId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nameTextBox.Text = reader["customerName"].ToString();
                            addressTextBox.Text = reader["address"].ToString();
                            zipCodeTextBox.Text = reader["postalCode"].ToString();
                            phoneNumberTextBox.Text = reader["phone"].ToString();
                            cityTextBox.Text = reader["city"].ToString();
                            countryTextBox.Text = reader["country"].ToString();
                            
                            address = new Address(
                                int.Parse(reader["addressId"].ToString()),
                                reader["address"].ToString(),
                                "",
                                int.Parse(reader["cityId"].ToString()),
                                reader["postalCode"].ToString(),
                                reader["phone"].ToString(),
                                DateTime.Now,
                                User.CurrentUser.UserName,
                                DateTime.Now,
                                User.CurrentUser.UserName
                            );

                            city = new City(
                                int.Parse(reader["cityId"].ToString()),
                                reader["city"].ToString(),
                                int.Parse(reader["countryId"].ToString()),
                                DateTime.Now,
                                User.CurrentUser.UserName,
                                DateTime.Now,
                                User.CurrentUser.UserName
                            );

                            country = new Country(
                                int.Parse(reader["countryId"].ToString()),
                                reader["country"].ToString(),
                                DateTime.Now,
                                User.CurrentUser.UserName,
                                DateTime.Now,
                                User.CurrentUser.UserName
                            );

                            customer = new Customer(
                                int.Parse(customerId),
                                reader["customerName"].ToString(),
                                int.Parse(reader["addressId"].ToString()),
                                1,
                                DateTime.Now,
                                User.CurrentUser.UserName,
                                DateTime.Now,
                                User.CurrentUser.UserName
                            );
                        }
                    }
                }
            }
            DBConnection.CloseConnection();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateCustomerFields();

                address.AddressLine = addressTextBox.Text.Trim();
                address.PostalCode = zipCodeTextBox.Text.Trim();
                address.Phone = phoneNumberTextBox.Text.Trim();
                address.LastUpdate = DateTime.Now;
                address.LastUpdateBy = User.CurrentUser.UserName;

                customer.CustomerName = nameTextBox.Text.Trim();
                customer.LastUpdate = DateTime.Now;
                customer.LastUpdateBy = User.CurrentUser.UserName;

                city.CityName = cityTextBox.Text.Trim();
                city.LastUpdate = DateTime.Now;
                city.LastUpdateBy = User.CurrentUser.UserName;

                country.CountryName = countryTextBox.Text.Trim();
                country.LastUpdate = DateTime.Now;
                country.LastUpdateBy = User.CurrentUser.UserName;

                UpdateCustomerInDatabase();

                // Update CustomerRecords grid
                CustomerRecords customerRecordsForm = (CustomerRecords)Application.OpenForms["CustomerRecords"];
                customerRecordsForm.showdata();
                customerRecordsForm.UpdateExceptionLabel("Customer updated successfully!", System.Drawing.Color.Green);
                Hide();
            }
            catch (Exception ex)
            {
                messageLabel.ForeColor = System.Drawing.Color.Red;
                messageLabel.Text = ex.Message;
            }
        }

        private void ValidateCustomerFields()
        {
            nameTextBox.Text = nameTextBox.Text.Trim();
            addressTextBox.Text = addressTextBox.Text.Trim();
            cityTextBox.Text = cityTextBox.Text.Trim();
            zipCodeTextBox.Text = zipCodeTextBox.Text.Trim();
            countryTextBox.Text = countryTextBox.Text.Trim();
            phoneNumberTextBox.Text = phoneNumberTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                throw new Exception("Name field cannot be empty or whitespace.");
            if (string.IsNullOrWhiteSpace(addressTextBox.Text))
                throw new Exception("Address field cannot be empty or whitespace.");
            if (string.IsNullOrEmpty(cityTextBox.Text))
                throw new Exception("City field cannot be empty.");
            if (string.IsNullOrEmpty(zipCodeTextBox.Text))
                throw new Exception("Zip Code field cannot be empty.");
            if (!Regex.IsMatch(zipCodeTextBox.Text, @"^\d{5}$"))
                throw new Exception("Zip Code must be 5 digits long and contain only numbers.");
            if (string.IsNullOrEmpty(countryTextBox.Text))
                throw new Exception("Country field cannot be empty.");
            if (string.IsNullOrWhiteSpace(phoneNumberTextBox.Text))
                throw new Exception("Phone number field cannot be empty or whitespace.");
            if (!Regex.IsMatch(phoneNumberTextBox.Text, @"^\d{3}-\d{4}$"))
                throw new Exception("Phone number must be in the format xxx-xxxx.");
        }

        private void UpdateCustomerInDatabase()
        {
            DBConnection.OpenConnection();
            using (var conn = DBConnection.conn)
            {
                using (var transaction = conn.BeginTransaction())
                {
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.Transaction = transaction;

                   // Update Country
                    cmd.CommandText = Queries.UpdateCountry;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CountryId", country.CountryId);
                    cmd.Parameters.AddWithValue("@Country", country.CountryName);
                    cmd.Parameters.AddWithValue("@LastUpdateBy", User.CurrentUser.UserName);
                    cmd.ExecuteNonQuery();

                    // Update City
                    cmd.CommandText = Queries.UpdateCity;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CityId", city.CityId);
                    cmd.Parameters.AddWithValue("@City", city.CityName);
                    cmd.Parameters.AddWithValue("@CountryId", city.CountryId);
                    cmd.Parameters.AddWithValue("@LastUpdateBy", User.CurrentUser.UserName);
                    cmd.ExecuteNonQuery();

                    // Update Address
                    cmd.CommandText = Queries.UpdateAddress;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@AddressId", address.AddressId);
                    cmd.Parameters.AddWithValue("@Address", address.AddressLine);
                    cmd.Parameters.AddWithValue("@CityId", address.CityId);
                    cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                    cmd.Parameters.AddWithValue("@PhoneNumber", address.Phone);
                    cmd.Parameters.AddWithValue("@LastUpdateBy", User.CurrentUser.UserName);
                    cmd.ExecuteNonQuery();

                    // Update Customer
                    cmd.CommandText = Queries.UpdateCustomer;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@AddressId", customer.AddressId);
                    cmd.Parameters.AddWithValue("@Active", customer.Active);
                    cmd.Parameters.AddWithValue("@LastUpdateBy", User.CurrentUser.UserName);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
            DBConnection.CloseConnection();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void phoneNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = phoneNumberTextBox.Text.Replace("-", "");
            if (text.Length > 3)
            {
                text = text.Insert(3, "-");
            }
            phoneNumberTextBox.TextChanged -= phoneNumberTextBox_TextChanged;
            phoneNumberTextBox.Text = text;
            phoneNumberTextBox.SelectionStart = text.Length;
            phoneNumberTextBox.TextChanged += phoneNumberTextBox_TextChanged;
        }

        private void phoneNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar))
            {
                string text = phoneNumberTextBox.Text.Replace("-", "");
                if (text.Length >= 7)
                {
                    e.Handled = true;
                }
            }
        }

        private void zipCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (zipCodeTextBox.Text.Length >= 5 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
