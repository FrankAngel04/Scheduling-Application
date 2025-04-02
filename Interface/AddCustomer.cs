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
    public partial class AddCustomer : Form
    {
        private Address address;
        private City city;
        private Country country;
        private Customer customer;
        private string addressId;
        private string customerId;
        private string cityId;
        private string countryId;

        public AddCustomer()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            zipCodeTextBox.KeyPress += zipCodeTextBox_KeyPress;
            phoneNumberTextBox.KeyPress += phoneNumberTextBox_KeyPress;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateCustomerFields();

                countryId = GetCountryId(countryTextBox.Text.Trim()).ToString();
                cityId = GetCityId(cityTextBox.Text.Trim(), countryId).ToString();
                addressId = GetNewId(Queries.AddressIndex).ToString();
                customerId = GetNewId(Queries.CustomerIndex).ToString();

                address = new Address(
                    int.Parse(addressId),
                    addressTextBox.Text.Trim(),
                    "",
                    int.Parse(cityId),
                    zipCodeTextBox.Text.Trim(),
                    phoneNumberTextBox.Text.Trim(),
                    DateTime.Now,
                    User.CurrentUser.UserName,
                    DateTime.Now,
                    User.CurrentUser.UserName
                );

                customer = new Customer(
                    int.Parse(customerId),
                    nameTextBox.Text.Trim(),
                    int.Parse(addressId),
                    1,
                    DateTime.Now,
                    User.CurrentUser.UserName,
                    DateTime.Now,
                    User.CurrentUser.UserName
                );

                SaveCustomerToDatabase();

                // Update CustomerRecords grid
                CustomerRecords customerRecordsForm = (CustomerRecords)Application.OpenForms["CustomerRecords"];
                customerRecordsForm.showdata();
                customerRecordsForm.UpdateExceptionLabel("Customer added successfully!", System.Drawing.Color.Green);
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

        private int GetCityId(string cityName, string countryId)
        {
            DBConnection.OpenConnection();
            using (var conn = DBConnection.conn)
            {
                var query = @"
        SELECT cityId
        FROM city
        WHERE city = @CityName AND countryId = @CountryId";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CityName", cityName);
                    cmd.Parameters.AddWithValue("@CountryId", countryId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return int.Parse(reader["cityId"].ToString());
                        }
                    }
                }

                // If city does not exist, create it
                int newCityId = GetNewId(Queries.CityIndex);
                city = new City(newCityId, cityName, int.Parse(countryId), DateTime.Now, User.CurrentUser.UserName, DateTime.Now, User.CurrentUser.UserName);
                SaveCityToDatabase(city);

                return city.CityId;
            }
        }

        private int GetCountryId(string countryName)
        {
            DBConnection.OpenConnection();
            using (var conn = DBConnection.conn)
            {
                var query = "SELECT countryId FROM country WHERE country = @CountryName";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CountryName", countryName);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return int.Parse(reader["countryId"].ToString());
                        }
                    }
                }

                // If country does not exist, create it
                int newCountryId = GetNewId(Queries.CountryIndex);
                country = new Country(newCountryId, countryName, DateTime.Now, User.CurrentUser.UserName, DateTime.Now, User.CurrentUser.UserName);
                SaveCountryToDatabase(country);

                return country.CountryId;
            }
        }

        private void SaveCityToDatabase(City city)
        {
            DBConnection.OpenConnection();
            using (var conn = DBConnection.conn)
            {
                var query = Queries.AddCity;
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CityId", city.CityId);
                    cmd.Parameters.AddWithValue("@City", city.CityName);
                    cmd.Parameters.AddWithValue("@CountryId", city.CountryId);
                    cmd.Parameters.AddWithValue("@CreatedBy", city.CreatedBy);
                    cmd.Parameters.AddWithValue("@LastUpdateBy", city.LastUpdateBy);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveCountryToDatabase(Country country)
        {
            DBConnection.OpenConnection();
            using (var conn = DBConnection.conn)
            {
                var query = Queries.AddCountry;
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CountryId", country.CountryId);
                    cmd.Parameters.AddWithValue("@Country", country.CountryName);
                    cmd.Parameters.AddWithValue("@CreatedBy", country.CreatedBy);
                    cmd.Parameters.AddWithValue("@LastUpdateBy", country.LastUpdateBy);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveCustomerToDatabase()
        {
            DBConnection.OpenConnection();
            using (var conn = DBConnection.conn)
            {
                using (var transaction = conn.BeginTransaction())
                {
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.Transaction = transaction;

                    // Insert Address
                    cmd.CommandText = Queries.AddAddress;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@AddressId", address.AddressId);
                    cmd.Parameters.AddWithValue("@Address", address.AddressLine);
                    cmd.Parameters.AddWithValue("@CityId", address.CityId);
                    cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                    cmd.Parameters.AddWithValue("@PhoneNumber", address.Phone);
                    cmd.Parameters.AddWithValue("@CreatedBy", address.CreatedBy);
                    cmd.Parameters.AddWithValue("@LastUpdateBy", address.LastUpdateBy);
                    cmd.ExecuteNonQuery();

                    // Insert Customer
                    cmd.CommandText = Queries.AddCustomer;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@AddressId", customer.AddressId);
                    cmd.Parameters.AddWithValue("@Active", customer.Active);
                    cmd.Parameters.AddWithValue("@CreatedBy", customer.CreatedBy);
                    cmd.Parameters.AddWithValue("@LastUpdateBy", customer.LastUpdateBy);
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

        private int GetNewId(string query)
        {
            DBConnection.OpenConnection();
            using (var conn = DBConnection.conn)
            {
                return Convert.ToInt32(new MySqlCommand(query, conn).ExecuteScalar()) + 1;
            }
        }
    }
}
