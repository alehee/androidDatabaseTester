using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace androidDatabaseTester
{
    public partial class Main : ContentPage
    {
        const string VERSION = "1.0.0t";

        Connection connection;

        public Main()
        {
            InitializeComponent();
        }

        private void Button_MySQL(object sender, EventArgs e)
        {
            testConnection();
        }

        private void Button_Select(object sender, EventArgs e)
        {
            testSelectQuery();
        }

        private void testConnection()
        {
            string ip = E_IP.Text.ToString();
            string user = E_User.Text.ToString();
            string pass = E_Password.Text.ToString();
            string db = E_Database.Text.ToString();

            try
            {
                L_Log.Text = "Initializing connection!";

                connection = new Connection(ip, user, pass, db);

                Tuple<bool, string> connectionStatus = connection.getConnection();

                if (connectionStatus.Item1)
                {
                    L_Log.Text = connectionStatus.Item2;
                }
                else
                {
                    L_Log.Text = "There's a problem:\n" + connectionStatus.Item2;
                }
            }
            catch(Exception e)
            {
                L_Log.Text = "Oops! An exception poped out:\n"+e.Message;
            }
        }

        private void testSelectQuery()
        {

        }
    }
}
