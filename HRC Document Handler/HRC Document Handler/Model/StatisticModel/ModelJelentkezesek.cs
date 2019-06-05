using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Model.StatisticModel
{
    public class ModelJelentkezesek
    {
        public int id { get; set; }
        public int projekt_id { get; set; }
        public string projekt_megnevezes { get; set; }
        public int kategoria { get; set; }
        public string reg_date { get; set; }

        public static List<ModelJelentkezesek> getJelentkezesekWeb(string query)
        {
            MySql mySql = new MySql(true);
            List<ModelJelentkezesek> items = new List<ModelJelentkezesek>();
            if (mySql.dbOpen() == true)
            {
                mySql.cmd = new MySqlCommand(query, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    items.Add(new ModelJelentkezesek
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        projekt_id = Convert.ToInt32(mySql.sdr["projekt_id"]),
                        projekt_megnevezes = mySql.sdr["projekt_megnevezes"].ToString(),
                        kategoria = Convert.ToInt32(mySql.sdr["kategoria"]),
                        reg_date = mySql.sdr["reg_date"].ToString()

                    });
                }
                mySql.sdr.Close();
            }
            return items;
        }

        public static List<ModelJelentkezesek> getJelentkezesekInner(string query)
        {
            MySql mySql = new MySql();
            List<ModelJelentkezesek> items = new List<ModelJelentkezesek>();
            if (mySql.dbOpen() == true)
            {
                mySql.cmd = new MySqlCommand(query, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    items.Add(new ModelJelentkezesek
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        projekt_id = Convert.ToInt32(mySql.sdr["projekt_id"]),
                        projekt_megnevezes = mySql.sdr["projekt_megnevezes"].ToString(),
                        kategoria = Convert.ToInt32(mySql.sdr["kategoria"]),
                        reg_date = mySql.sdr["reg_date"].ToString()

                    });
                }
                mySql.sdr.Close();
            }
            return items;
        }

        public static List<ModelJelentkezesek> getJelentkezesekInner(DateTime from, DateTime to,string query)
        {
            MySql mySql = new MySql();
            List<ModelJelentkezesek> items = new List<ModelJelentkezesek>();
            if (mySql.dbOpen() == true)
            {
                mySql.cmd = new MySqlCommand(query, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    items.Add(new ModelJelentkezesek
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        projekt_id = Convert.ToInt32(mySql.sdr["projekt_id"]),
                        projekt_megnevezes = mySql.sdr["projekt_megnevezes"].ToString(),
                        kategoria = Convert.ToInt32(mySql.sdr["kategoria"]),
                        reg_date = mySql.sdr["reg_date"].ToString()

                    });
                }
                mySql.sdr.Close();
            }
            return items;
        }

        public void Insert(MySql mysql)
        {
            string command = "INSERT INTO `jelentkezesek`(`projekt_id`, `projekt_megnevezes`, `kategoria`, `reg_date`) VALUES (" + projekt_id + ",'" + projekt_megnevezes + "'," + kategoria + ",'" + reg_date + "')";
            mysql.execute(command);
        }
    }
}
