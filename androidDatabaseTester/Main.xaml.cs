using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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

            L_Version.Text = VERSION;
        }

        private async void Button_MySQL(object sender, EventArgs e)
        {
            await testConnection();
        }

        private async void Button_Select(object sender, EventArgs e)
        {
            await testSelectQuery();
        }

        private async Task testConnection()
        {
            string ip = "";
            try
            {
                ip = E_IP.Text.ToString();
            }
            catch { }

            string user = "";
            try
            {
                user = E_User.Text.ToString();
            }
            catch { }

            string pass = "";
            try
            {
                pass = E_Password.Text.ToString();
            }
            catch { }

            string db = "";
            try
            {
                db = E_Database.Text.ToString();
            }
            catch { }

            try
            {
                await changeLoading(true);

                connection = new Connection(ip, user, pass, db);

                Tuple<bool, string> connectionStatus = await connection.getConnectionAsync();

                if (connectionStatus.Item1)
                {
                    showLog(connectionStatus.Item2, Color.Green);
                }
                else
                {
                    showLog("There's a connection problem:\n" + connectionStatus.Item2, Color.Red);
                }
            }
            catch (Exception e)
            {
                showLog("Oops! An exception poped out:\n" + e.Message, Color.Red);
            }
            finally
            {
                await changeLoading(false);
            }
        }

        private async Task testSelectQuery()
        {
            
        }

        private async Task<Grid> buildGrid(List<List<string>> list)
        {

            return null;
        }

        private async Task changeLoading(bool status)
        {
            if (status)
            {
                AI.IsRunning = true;
                AI.IsVisible = true;
            }
            else
            {
                AI.IsRunning = false;
                AI.IsVisible = false;
            }
        }

        private void showLog(string text = "", Xamarin.Forms.Color color = default)
        {
            L_Log.TextColor = color;
            L_Log.Text = text;
        }
    }
}
