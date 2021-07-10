using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using MySqlConnector;

namespace androidDatabaseTester
{
    public partial class Main : ContentPage
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Button_MySQL(object sender, EventArgs e)
        {
            testMysql();
        }

        private void testMysql()
        {
            string ip = E_IP.Text.ToString();
            string user = E_User.Text.ToString();
            string pass = E_Password.Text.ToString();
            string db = E_Database.Text.ToString();
            string qu = E_Query.Text.ToString();

            try
            {
                string CONNECTION_STRING = "server=" + ip + ";port=3306;user=" + user + ";password=" + pass + ";database=" + db + ";";
                MySqlConnection conn = new MySqlConnection(CONNECTION_STRING);
                MySqlCommand query = new MySqlCommand(qu, conn);
                query.CommandTimeout = 30;

                conn.Open();

                MySqlDataReader mySqlDataReader = query.ExecuteReader();
                int fCount = 0;

                if (mySqlDataReader.HasRows)
                {
                    while (mySqlDataReader.Read())
                    {
                        fCount++;
                    }
                }

                conn.Close();
                L_Log.Text = "Connection good! fCount = " + fCount.ToString();
            }
            catch (Exception e)
            {
                L_Log.Text = e.ToString();
            }
        }
    }
}
