using System;
using System.IO;
using System.Windows.Forms;

namespace SchedulingApplication.Database
{
    public class Logger
    {
        // Login_History should be in Path: SchedulingApplication/bin/debug/Login_History.txt
        private static string fileName = "Login_History.txt";

        public static void LoginLogger(string userName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine($"User '{userName}' logged in at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to log user activity: {ex.Message}");
            }
        }
    }
}