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
        const string VERSION = "1.0.0";

        Connection connection;

        bool tooManyRows = false;

        public Main()
        {
            InitializeComponent();

            L_Version.Text = "v. " + VERSION;
            
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

                SV.IsVisible = false;

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
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                await changeLoading(false);
            }
        }

        private async Task testSelectQuery()
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

            string qu = "";
            try
            {
                qu = E_Query.Text.ToString();
            }
            catch { }

            try
            {
                await changeLoading(true);

                await clearGrid();

                SV.IsVisible = true;

                connection = new Connection(ip, user, pass, db);

                Tuple<bool, string> connectionStatus = await connection.getConnectionAsync();

                if (connectionStatus.Item1)
                {
                    try
                    {
                        List<List<string>> queryResult = await connection.getQueryAsync(qu);
                        string info = "";

                        if (queryResult.Count == 1 && queryResult[0][0] == "!ERROR")
                            throw new Exception(queryResult[0][1]);
                        else
                        {
                            await buildGrid(queryResult);
                        }

                        if (tooManyRows)
                        {
                            tooManyRows = false;
                            info = "\nFor optimization we capped row limit to 50!";
                        }

                        showLog("Query result is visible down there!"+info, Color.Green);
                    }
                    catch(Exception e)
                    {
                        showLog("Oh no! Query loading just failed:\n" + e.Message, Color.Red);
                        System.Diagnostics.Debug.WriteLine(e.ToString());
                        SV.IsVisible = false;
                    }
                }
                else
                {
                    showLog("There's a connection problem:\n" + connectionStatus.Item2, Color.Red);
                    SV.IsVisible = false;
                }
            }
            catch (Exception e)
            {
                showLog("Oops! An exception poped out:\n" + e.Message, Color.Red);
                System.Diagnostics.Debug.WriteLine(e.ToString());
                SV.IsVisible = false;
            }
            finally
            {
                await changeLoading(false);
            }
        }

        private async Task clearGrid()
        {
            G_ShowQuery.Children.Clear();
        }

        private async Task buildGrid(List<List<string>> list)
        {
            int rowCount = 0;

            for(int i=0; i<list.Count; i++)
            {
                G_ShowQuery.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                for (int j=0; j<list[i].Count; j++)
                {
                    if(i==0)
                        G_ShowQuery.Children.Add(new Label { Text = list[i][j], FontAttributes = FontAttributes.Bold }, j, i);
                    else
                        G_ShowQuery.Children.Add(new Label { Text = list[i][j] }, j, i);
                    G_ShowQuery.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }
                rowCount++;

                if (rowCount > 50)
                {
                    tooManyRows = true;
                    break;
                }
            }
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
