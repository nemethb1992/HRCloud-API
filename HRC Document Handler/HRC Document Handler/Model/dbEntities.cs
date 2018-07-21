using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Model
{
    class dbEntities
    {

        SQLiteConnection connSqlite;

        public dbEntities()
        {
            SetupDb();
            SqliteReaderExecute("CREATE TABLE IF NOT EXISTS ConnectionSMTP ( `mailserver` TEXT NOT NULL, `port` INTEGER NOT NULL, `ssl` INTEGER NOT NULL DEFAULT 1, `login` TEXT NOT NULL, `password` TEXT NOT NULL, `sender_email` TEXT DEFAULT '' )");
            SqliteReaderExecute("CREATE TABLE IF NOT EXISTS `FolderLocate` ( `url` TEXT NOT NULL, `username` TEXT NOT NULL, `password` TEXT NOT NULL )");
            
        }

        private void SetupDb()
        {
            string connectionString = "Data Source = InnerDb.db";
            connSqlite = new SQLiteConnection(connectionString);
        }
        public bool dbOpen()
        {
            try
            {
                connSqlite.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        private bool dbClose()
        {
            try
            {
                connSqlite.Close();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }
        public void SqliteQueryExecute(string query)
        {
            if (this.dbOpen() == true)
            {
                var command = connSqlite.CreateCommand();
                command.CommandText = query;
                command.ExecuteNonQuery();
                connSqlite.Close();
            }
        }
        public string SqliteReaderExecute(string query)
        {
            string data = "";
            if (this.dbOpen() == true)
            {
                var command = connSqlite.CreateCommand();
                command.CommandText = query;
                SQLiteDataReader sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    data = sdr.GetValue(0).ToString();
                }
                sdr.Close();
                connSqlite.Close();
            }
            return data;
        }


        //Specific
        public List<MailServer_m> ConnectionSMTP_DataSource(string query)
        {
            List<MailServer_m> list = new List<MailServer_m>();
            if (this.dbOpen() == true)
            {
                var command = connSqlite.CreateCommand();
                command.CommandText = query;
                SQLiteDataReader sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    bool ssl = true;
                    if (Convert.ToInt32(sdr["ssl"]) == 0)
                    {
                        ssl = false;
                    }
                    list.Add(new MailServer_m
                    {
                        mailserver = sdr["mailserver"].ToString(),
                        port = Convert.ToInt32(sdr["port"]),
                        ssl = ssl,
                        login = sdr["login"].ToString(),
                        password = sdr["password"].ToString(),
                        sender_email = sdr["sender_email"].ToString()
                    });
                }
                sdr.Close();
                connSqlite.Close();
            }
            return list;
        }
        public List<FolderUrl_m> Folder_DataSource(string query)
        {
            List<FolderUrl_m> list = new List<FolderUrl_m>();
            if (this.dbOpen() == true)
            {
                var command = connSqlite.CreateCommand();
                command.CommandText = query;
                SQLiteDataReader sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    list.Add(new FolderUrl_m
                    {
                        url = sdr["url"].ToString(),
                        username = sdr["username"].ToString(),
                        password = sdr["password"].ToString()
                    });
                }
                sdr.Close();
                connSqlite.Close();
            }
            return list;
        }
        

    }
}
