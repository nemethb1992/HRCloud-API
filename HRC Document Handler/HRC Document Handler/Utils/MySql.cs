using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRC_Document_Handler.Model.Outdated_m;

namespace HRC_Document_Handler.Model
{
    public class MySql
    {
        //string connectionString = "Data Source = s7.nethely.hu; Initial Catalog = pmkcvtest; User ID=pmkcvtest; Password=pmkcvtest2018";
        //string connectionString = "Data Source = 192.168.144.189; Port=3306; Initial Catalog = pmkcvtest; User ID=hr-admin; Password=pmhr2018";
        //string connectionString = "Data Source = vpn.phoenix-mecano.hu; Port=29920; Initial Catalog = pmkcvtest; User ID=hr-admin; Password=pmhr2018";
        private const string CONNECTION_URL_1 = "Data Source = 192.168.144.189; Port=3306; Initial Catalog = pmkcvtest; User ID=hr-admin; Password=pmhr2018; charset=utf8;";
        private const string CONNECTION_URL_2 = "Data Source = 192.168.144.189; Port=3306; Initial Catalog = pmhrdemo; User ID=hr-admin; Password=pmhr2018;  charset=utf8;";
        private const string WEB_DATABASE_CONNECTION = "Data Source = mysql.nethely.hu; Port=3306; Initial Catalog = hrportalweb; User ID=hrportalweb; Password=pmhr2018!;  charset=utf8;";

        public MySqlConnection conn;
        public MySqlCommand cmd;
        public MySqlDataReader sdr;

        public MySql(bool publicDb = false)
        {
            if (conn == null)
            {
                try
                {
                    conn = new MySqlConnection((publicDb ? WEB_DATABASE_CONNECTION : CONNECTION_URL_1));
                }
                catch (MySqlException mysqlex)
                {
                    Console.WriteLine(mysqlex.ToString());
                    throw;
                }
            }
        }
        public bool dbOpen()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        public bool dbClose()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
        public void execute(string query)
        {
            if (this.dbOpen() == true)
            {
                cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            dbClose();
        }

  

        public List<string> uniqueList(string command, string table, int b)
        {
            List<string> dataSource = new List<string>();
            if (dbOpen() == true)
            {
                cmd = new MySqlCommand(command, conn);
                sdr = cmd.ExecuteReader();
                int i;
                while (sdr.Read())
                {
                    for (i = 0; i < b; i++)
                    {
                        dataSource.Add(sdr[i].ToString());
                    }
                }
                sdr.Close();
            }
            return dataSource;
        }

        public bool bind(string query)
        {
            bool valid = false;

            if (this.dbOpen() == true)
            {
                int seged = 0;
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    seged = Convert.ToInt32(sdr[0]);
                }
                sdr.Close();
                if (seged != 0)
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                }
            }
            return valid;
        }

        public List<ModelFullApplicant> JeloltExtended_MySql_listQuery(string query)
        {
            List<ModelFullApplicant> items = new List<ModelFullApplicant>();
            if (this.dbOpen() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelFullApplicant
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        nev = sdr["nev"].ToString(),
                        email = sdr["email"].ToString(),
                        telefon = sdr["telefon"].ToString(),
                        lakhely = sdr["lakhely"].ToString(),
                        ertesult = sdr["ertesules_megnevezes"].ToString(),
                        id_ertesult = Convert.ToInt32(sdr["id_ertesult"]),
                        szuldatum = Convert.ToInt32(sdr["szuldatum"]),
                        neme = sdr["neme"].ToString(),
                        id_neme = Convert.ToInt32(sdr["id_neme"]),
                        tapasztalat_ev = Convert.ToInt32(sdr["tapasztalat_ev"]),
                        munkakor = sdr["munkakor"].ToString(),
                        munkakor2 = sdr["munkakor2"].ToString(),
                        munkakor3 = sdr["munkakor3"].ToString(),
                        id_munkakor = Convert.ToInt32(sdr["id_munkakor"]),
                        id_munkakor2 = Convert.ToInt32(sdr["id_munkakor2"]),
                        id_munkakor3 = Convert.ToInt32(sdr["id_munkakor3"]),
                        vegz_terulet = sdr["vegz_terulet"].ToString(),
                        id_vegz_terulet = Convert.ToInt32(sdr["id_vegz_terulet"]),
                        nyelvtudas = sdr["nyelvtudas"].ToString(),
                        nyelvtudas2 = sdr["nyelvtudas2"].ToString(),
                        id_nyelvtudas = Convert.ToInt32(sdr["id_nyelvtudas"]),
                        id_nyelvtudas2 = Convert.ToInt32(sdr["id_nyelvtudas2"]),
                        reg_date = sdr["reg_date"].ToString(),
                        megjegyzes = sdr["megjegyzes"].ToString(),
                        folderUrl = sdr["folderUrl"].ToString(),
                    });
                }
                sdr.Close();
            }
            return items;
        }


        public string SqlSingleQuery(string query, string field)
        {
            string data = "";
            if (this.dbOpen() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    data = sdr[field].ToString();
                    break;
                }
                sdr.Close();
            }
            return data;
        }
        
        public SMTPmodel SMTPdataIMAP()
        {
            SMTPmodel data = null;
            if (this.dbOpen() == true)
            {
                cmd = new MySqlCommand("SELECT * FROM ConnectionSMTP WHERE type = 'imap'", conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    bool ssl = true;
                    if (Convert.ToInt32(sdr["ssl"]) == 0)
                    {
                        ssl = false;
                    }
                    data = new SMTPmodel
                    {
                        mailserver = sdr["mailserver"].ToString(),
                        port = Convert.ToInt32(sdr["port"]),
                        ssl = ssl,
                        login = sdr["login"].ToString(),
                        password = "pmhr2018"
                    };
                }
                sdr.Close();
            }
            return data;
        }

        public FolderModel ApplicantURL()
        {
            FolderModel data = null;
            if (this.dbOpen() == true)
            {
                cmd = new MySqlCommand("SELECT * FROM ROOTurl WHERE id = 0", conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    data = new FolderModel
                    {
                        url = sdr["url"].ToString(),
                    };
                }
                sdr.Close();
            }
            return data;
        }

        public FolderModel StatisticURL()
        {
            FolderModel data = null;
            if (this.dbOpen() == true)
            {
                cmd = new MySqlCommand("SELECT * FROM ROOTurl WHERE id = 1", conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    data = new FolderModel
                    {
                        url = sdr["url"].ToString(),
                    };
                }
                sdr.Close();
            }
            return data;
        }
    }
}
