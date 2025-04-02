using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingApplication.Database
{
    public class DBConnection
    {
        public static MySqlConnection conn { get; set; }


        public static void OpenConnection()
        {
            string constr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;

            try
            {
                conn = new MySqlConnection(constr);

                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CloseConnection()
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                }

                conn = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool SetupDatabase()
        {
            bool dbInitialized = false;
            try
            {
                OpenConnection();
        
                MySqlCommand checkTablesCmd = new MySqlCommand(Queries.CheckTablesExistence, conn);
                int tableCount = Convert.ToInt32(checkTablesCmd.ExecuteScalar());
        
                MySqlCommand checkUsersCmd = new MySqlCommand(Queries.GetTotalUsers, conn);
                int userCount = Convert.ToInt32(checkUsersCmd.ExecuteScalar());
                if (tableCount < 6 || userCount == 0) // Initialize if less than 6 tables or no users
                {
                    MySqlCommand initDbCmd = new MySqlCommand(Queries.SetupDatabaseQuery, conn);
                    initDbCmd.ExecuteNonQuery();
                    dbInitialized = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        
            return dbInitialized;
        }
    }
}
