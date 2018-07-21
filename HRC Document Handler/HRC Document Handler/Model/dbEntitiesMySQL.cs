using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRC_Document_Handler.Model.Outdated_m;

namespace HRC_Document_Handler.Model
{
    class dbEntitiesMySQL
    {
        //string connectionString = "Data Source = s7.nethely.hu; Initial Catalog = pmkcvtest; User ID=pmkcvtest; Password=pmkcvtest2018";
        //string connectionString = "Data Source = 192.168.144.189; Port=3306; Initial Catalog = pmkcvtest; User ID=hr-admin; Password=pmhr2018";
        //string connectionString = "Data Source = vpn.phoenix-mecano.hu; Port=29920; Initial Catalog = pmkcvtest; User ID=hr-admin; Password=pmhr2018";
        public static string innerDataSourceURL = "Data Source = innerDatabase.db";

        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataReader sdr;
        public dbEntitiesMySQL()
        {
            SetupDB();
        }
        private void SetupDB()
        {
            string connectionString = "Data Source = s7.nethely.hu; Initial Catalog = pmkcvtest; User ID=pmkcvtest; Password=pmkcvtest2018";
            conn = new MySqlConnection(connectionString);
        }
        public bool dbOpen()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool dbClose()
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
        public void MysqlQueryExecute(string query)
        {
            if (this.dbOpen() == true)
            {
                cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            dbClose();
        }
        public List<object> UserSession(string query)
        {

            List<object> list = new List<object>();
            //if (this.dbOpen() == true)
            //{
            //    cmd = new MySqlCommand(query, conn);
            //    sdr = cmd.ExecuteReader();
            //    while (sdr.Read())
            //    {
            //        list.Add(new object
            //        {
            //            id = Convert.ToInt32(sdr["id"]),
            //            username = sdr["username"].ToString(),
            //            name = sdr["name"].ToString(),
            //            email = sdr["email"].ToString(),
            //            kategoria = Convert.ToInt32(sdr["kategoria"]),
            //            jogosultsag = Convert.ToInt32(sdr["jogosultsag"]),
            //            validitas = Convert.ToInt32(sdr["validitas"]),
            //            belepve = sdr["belepve"].ToString(),
            //            reg_datum = sdr["reg_datum"].ToString(),
            //        });
            //    }
            //    sdr.Close();
            //}
            //dbClose();


            //conn.Close();
            return list;
        }
        public List<JeloltExtendedList> JeloltExtended_MySql_listQuery(string query)
        {
            List<JeloltExtendedList> items = new List<JeloltExtendedList>();
            if (this.dbOpen() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new JeloltExtendedList
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
            dbClose();
            return items;
        }
    }
}
