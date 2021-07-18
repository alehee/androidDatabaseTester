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

                await conn.OpenAsync();
                status = new Tuple<bool, string>(true, "Connection aquired!");
            }
            catch (Exception e)
            {
                status = new Tuple<bool, string>(false, e.Message);
            }
            finally
            {
                await conn.CloseAsync();
            }

            return status;
        }
        /// ==========

        /// F: TRY TO GET SELECT QUERY

        /// ==========
    }
}
