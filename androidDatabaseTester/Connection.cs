using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MySqlConnector;

namespace androidDatabaseTester
{
    class Connection
    {
        string ip;
        string user;
        string pass;
        string db;

        /// CONSTRUCTOR
        public Connection(string ipCon, string userCon, string passCon, string dbCon)
        {
            ip = ipCon;
            user = userCon;
            pass = passCon;
            db = dbCon;
        }
        /// ==========

        /// F: TRY TO CONNECT ASYNC
        public async Task<Tuple<bool, string>> getConnectionAsync()
        {
            Tuple<bool, string> status = null;
            MySqlConnection conn = null;

            try
            {
                string CONNECTION_STRING = "server=" + ip + ";port=3306;user=" + user + ";password=" + pass + ";database=" + db + ";";
                conn = new MySqlConnection(CONNECTION_STRING);

                await Task.Run(() => conn.Open());
                status = new Tuple<bool, string>(true, "Connection aquired!");
            }
            catch (Exception e)
            {
                status = new Tuple<bool, string>(false, e.Message);
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                await conn.CloseAsync();
            }

            return status;
        }
        /// ==========

        /// F: TRY TO GET SELECT QUERY
        public async Task<List<List<string>>> getQueryAsync(string qu)
        {
            List<List<string>> returnList = new List<List<string>>();
            MySqlConnection conn = null;

            try
            {
                string CONNECTION_STRING = "server=" + ip + ";port=3306;user=" + user + ";password=" + pass + ";database=" + db + ";";
                conn = new MySqlConnection(CONNECTION_STRING);

                await conn.OpenAsync();

                string sql = qu;
                MySqlCommand query = new MySqlCommand(sql, conn);
                query.CommandTimeout = 30;
                MySqlDataReader mySqlDataReader = await query.ExecuteReaderAsync();

                if (!mySqlDataReader.HasRows)
                    throw new Exception("There's no data for this query");
                else
                {
                    bool first = true;

                    while (mySqlDataReader.Read())
                    {
                        List<string> row = new List<string>();

                        if (first)
                        {
                            for (int i = 0; i < mySqlDataReader.FieldCount; i++)
                            {
                                row.Add(mySqlDataReader.GetName(i).ToString());
                            }
                            returnList.Add(row);
                            row = new List<string>();
                            first = false;
                        }

                        for (int i=0; i < mySqlDataReader.FieldCount; i++)
                        {
                            row.Add(mySqlDataReader.GetValue(i).ToString());
                        }
                        returnList.Add(row);
                    }
                }

                mySqlDataReader.Close();
            }
            catch (Exception e)
            {
                return new List<List<string>>() { new List<string>() { "!ERROR", e.Message } };
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                await conn.CloseAsync();
            }

            return returnList;
        }
        /// ==========
    }
}
