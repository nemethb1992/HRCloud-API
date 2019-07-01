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
            string command = "INSERT INTO `vegzettsegek`(`id`, `megnevezes_vegzettseg`) VALUES (" + id + ",'" + megnevezes_vegzettseg + "')";
            mySqlWeb.execute(command);
            mySqlWeb.dbClose();
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
            mySqlWeb.dbClose();
        }
    }

    public class ModelFreelancerList
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string rid { get; set; }

        public static List<ModelFreelancerList> getFreelancerList(string command)
        {
            MySql mySql = new MySql();
            List<ModelFreelancerList> list = new List<ModelFreelancerList>();
            if (mySql.dbOpen() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelFreelancerList
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        name = mySql.sdr["name"].ToString(),
                        email = mySql.sdr["email"].ToString(),
                        rid = mySql.sdr["rid"].ToString()
                    });
                }
                mySql.sdr.Close();
            }
            return list;
        }

        public void insertWeb(MySql mySqlWeb)
        {
            string command = "INSERT INTO `freelancer_list`(`id`, `name`, `email`, `rid`) VALUES (" + id + ",'" + name + "','" + email + "','" + rid + "')";
            mySqlWeb.execute(command);
            mySqlWeb.dbClose();
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
            string command = "INSERT INTO `ertesulesek`(`id`, `ertesules_megnevezes`) VALUES (" + id + ",'" + ertesules_megnevezes + "')";
            mySqlWeb.execute(command);
            mySqlWeb.dbClose();
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
            string command = "INSERT INTO `projektek`(`id`, `megnevezes_projekt`, `statusz`, `fel_datum`) VALUES (" + id + ",'" + megnevezes_projekt + "'," + statusz + ",'" + fel_datum + "')";
            mySqlWeb.execute(command);
        }
    }
}
