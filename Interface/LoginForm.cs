using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SchedulingApplication.Database;
using SchedulingApplication.Entities;

namespace SchedulingApplication
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            
            InitializeComponent();
            InitializeLanguage();
            StartPosition = FormStartPosition.CenterScreen;
            AcceptButton = loginButton;
            
            // Check and initialize the database
            if (DBConnection.SetupDatabase())
            {
                messageLabel.Text = "Database initialized successfully!";
            }
            
            string userTimezone = TimeZoneInfo.Local.DisplayName;
            string[] timezoneParts = userTimezone.Split(' ');
            string userLocation = timezoneParts.Length > 2 ? string.Join(" ", timezoneParts.Skip(1)) : timezoneParts[1];
            timeZoneDisplayLabel.Text = $"{userLocation}";
        }

        private void InitializeLanguage()
        {
            // Detect the user's language setting
            string userLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            if (userLanguage == "es") // Spanish
            {
                usernameLabel.Text = "Nombre de usuario:";
                passwordLabel.Text = "Contraseña:";
                loginButton.Text = "Iniciar sesión";
                ExitButton.Text = "Salir";
                timeZoneLabel.Text = "Zona horaria";
                messageLabel.Text = "Base de datos inicializada con éxito.";
            }
            else // Default to English
            {
                usernameLabel.Text = "Username:";
                passwordLabel.Text = "Password:";
                loginButton.Text = "Login";
                ExitButton.Text = "Exit";
                timeZoneLabel.Text = "Time Zone";
                messageLabel.Text = "Database initialized successfully!";
            }
            messageLabel.ForeColor = System.Drawing.Color.Green;
        }


        private void LoginForm_Resize(object sender, EventArgs e)
        {
            CenterMessageLabel();
        }

        private void CenterMessageLabel()
        {
            messageLabel.Location = new System.Drawing.Point((this.ClientSize.Width - messageLabel.Width) / 2, messageLabel.Top);
        }
        
       private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnection.OpenConnection();
                using (var loginCMD = new MySqlCommand(Queries.AuthenticateUser, DBConnection.conn))
                {
                    loginCMD.Parameters.AddWithValue("@username", usernameTextBox.Text);
                    loginCMD.Parameters.AddWithValue("@password", passwordTextBox.Text);
                    using (var reader = loginCMD.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                User.CurrentUser = new User(
                                    reader.GetInt32("userId"),
                                    reader.GetString("userName"),
                                    reader.GetString("password"),
                                    reader.GetByte("active"),
                                    reader.GetDateTime("createDate"),
                                    reader.GetString("createdBy"),
                                    reader.GetDateTime("lastUpdate"),
                                    reader.GetString("lastUpdateBy")
                                );
                                
                                // Log the user activity
                                Logger.LoginLogger(User.CurrentUser.UserName); 
                                
                                new CustomerRecords().Show();
                                Hide();
                                
                                // Check for upcoming appointments
                                CheckForUpcomingAppointments();
                            }
                        }
                        else
                        {
                            LoginFail();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DBConnection.CloseConnection();
            }
        }
        
        private void LoginFail()
        {
            string userLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            if (userLanguage == "es")
            {
                messageLabel.Text = "El nombre de usuario y la contraseña no coinciden.";
            }
            else
            {
                messageLabel.Text = "The username and password do not match.";
            }

            messageLabel.ForeColor = System.Drawing.Color.Red;
            CenterMessageLabel();
        }

        private void CheckForUpcomingAppointments()
        {
            try
            {
                DBConnection.OpenConnection();
                using (var cmd = new MySqlCommand(Queries.UpcomingAppointmentsDetails, DBConnection.conn))
                {
                    cmd.Parameters.AddWithValue("@currentTime", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@userId", User.CurrentUser.UserId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string clientName = reader.GetString("customerName");
                                DateTime appointmentTime = reader.GetDateTime("start").ToLocalTime();
                                MessageBox.Show($"You have an appointment with {clientName} at {appointmentTime:hh:mm tt}.", "Upcoming Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking for upcoming appointments: " + ex.Message);
            }
            finally
            {
                DBConnection.CloseConnection();
            }
        }
        
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}