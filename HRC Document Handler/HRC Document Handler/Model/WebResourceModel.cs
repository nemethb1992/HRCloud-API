using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Model
{
    class WebResourceModel
    {
    }
    public class ModelVegzettseg
    {
        public int id { get; set; }
        public string megnevezes_vegzettseg { get; set; }

        public static List<ModelVegzettseg> getVegzettsegek(string command)
        {
            MySql mySql = new MySql();
            List<ModelVegzettseg> list = new List<ModelVegzettseg>();
            if (mySql.dbOpen() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelVegzettseg
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        megnevezes_vegzettseg = mySql.sdr["megnevezes_vegzettseg"].ToString(),
                    });
                }
                mySql.sdr.Close();
            }
            return list;
        }

        public void insertWeb(MySql mySqlWeb)
        {
            string command = "INSERT INTO `hrportalweb`.`vegzettsegek`(`id`, `megnevezes_vegzettseg`) VALUES (" + id + ",'" + megnevezes_vegzettseg + "')";
            mySqlWeb.execute(command);
        }
    }

    public class ModelNyelv
    {
        public int id { get; set; }
        public string nyelv { get; set; }

        public static List<ModelNyelv> getNyelv(string query)
        {
            MySql mySql = new MySql();
            List<ModelNyelv> items = new List<ModelNyelv>();
            if (mySql.dbOpen() == true)
            {
                mySql.cmd = new MySqlCommand(query, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    items.Add(new ModelNyelv
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        nyelv = mySql.sdr["megnevezes_nyelv"].ToString(),

                    });
                }
                mySql.sdr.Close();
            }
            return items;
        }

        public void insertWeb(MySql mySqlWeb)
        {
            string command = "INSERT INTO `nyelv`(`id`, `megnevezes_nyelv`) VALUES (" + id + ",'" + nyelv + "')";
            mySqlWeb.execute(command);
        }
    }

    public class ModelRegisztraltak
    {
        public string email { get; set; }
        public int projekt_id { get; set; }
        public string reg_date { get; set; }

        public static List<ModelRegisztraltak> getRegisztraltak(string query)
        {
            MySql mySql = new MySql(true);
            List<ModelRegisztraltak> items = new List<ModelRegisztraltak>();
            if (mySql.dbOpen() == true)
            {
                mySql.cmd = new MySqlCommand(query, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    items.Add(new ModelRegisztraltak
                    {
                        email = mySql.sdr["email"].ToString(),
                        projekt_id = Convert.ToInt32(mySql.sdr["projekt_id"]),
                        reg_date = mySql.sdr["reg_date"].ToString()

                    });
                }
                mySql.sdr.Close();
            }
            return items;
        }

        public void Insert(MySql mysql)
        {
            string command = "INSERT INTO `regisztraltak`(`email`, `projekt_id`, `reg_date`) VALUES ('" + email + "'," + projekt_id + ",'" + reg_date + "')";
            mysql.execute(command);
        }
    }

    public class ModelErtesulesek
    {
        public int id { get; set; }
        public string ertesules_megnevezes { get; set; }

        public static List<ModelErtesulesek> getErtesulesek(string command)
        {
            MySql mySql = new MySql();
            List<ModelErtesulesek> list = new List<ModelErtesulesek>();
            if (mySql.dbOpen() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelErtesulesek
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        ertesules_megnevezes = mySql.sdr["ertesules_megnevezes"].ToString(),
                    });
                }
                mySql.sdr.Close();
            }
            return list;
        }

        public void insertWeb(MySql mySqlWeb)
        {
            string command = "INSERT INTO `hrportalweb`.`ertesulesek`(`id`, `ertesules_megnevezes`) VALUES (" + id + ",'" + ertesules_megnevezes + "')";
            mySqlWeb.execute(command);
        }

    }

    public class ModelProjektek
    {
        public int id { get; set; }
        public string megnevezes_projekt { get; set; }
        public int statusz { get; set; }
        public string fel_datum { get; set; }

        public static List<ModelProjektek> getProjektek(string command)
        {
            MySql mySql = new MySql();
            List<ModelProjektek> list = new List<ModelProjektek>();
            if (mySql.dbOpen() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelProjektek
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        megnevezes_projekt = mySql.sdr["megnevezes_projekt"].ToString(),
                        statusz = Convert.ToInt32(mySql.sdr["statusz"]),
                        fel_datum = mySql.sdr["fel_datum"].ToString()
                    });
                }
                mySql.sdr.Close();
            }
            return list;
        }

        public void insertWeb(MySql mySqlWeb)
        {
            string command = "INSERT INTO `hrportalweb`.`projektek`(`id`, `megnevezes_projekt`, `statusz`, `fel_datum`) VALUES (" + id + ",'" + megnevezes_projekt + "'," + statusz + ",'" + fel_datum + "')";
            mySqlWeb.execute(command);
        }
    }
}
