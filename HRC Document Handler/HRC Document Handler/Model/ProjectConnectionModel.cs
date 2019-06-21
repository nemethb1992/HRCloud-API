using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Model
{
    class ProjectConnectionModel
    {
        public int id { get; set; }
        public int projekt_id { get; set; }
        public int jelolt_id { get; set; }
        public string date { get; set; }

        public static List<ProjectConnectionModel> getListWeb(string command)
        {
            List<ProjectConnectionModel> list = new List<ProjectConnectionModel>();
            MySql mySqlWeb = new MySql(true);
            if (mySqlWeb.dbOpen() == true)
            {
                mySqlWeb.cmd = new MySqlCommand(command, mySqlWeb.conn);
                mySqlWeb.sdr = mySqlWeb.cmd.ExecuteReader();

                while (mySqlWeb.sdr.Read())
                {
                    list.Add(new ProjectConnectionModel
                    {
                        id = Convert.ToInt32(mySqlWeb.sdr["id"]),
                        projekt_id = Convert.ToInt32(mySqlWeb.sdr["projekt_id"]),
                        jelolt_id = Convert.ToInt32(mySqlWeb.sdr["jelolt_id"]),
                        date = mySqlWeb.sdr["date"].ToString()
                    });
                }
                mySqlWeb.sdr.Close();
                mySqlWeb.dbClose();
            }
            return list;
        }
        
        public static void insertDb(ProjectConnectionModel data, int applicantID, bool freelancer = false)
        {
            string selectCommand = "SELECT count(`projekt_jelolt_kapcs`.`id`) as count FROM `projekt_jelolt_kapcs` WHERE `projekt_id` = " + data.projekt_id + " AND `jelolt_id` = " + applicantID + "";
            string insertCommand = @"INSERT INTO `projekt_jelolt_kapcs` (`projekt_id`, `jelolt_id`,`hr_id`, `allapota`, `datum`) VALUES (" + data.projekt_id + "," + applicantID + ",default,default,'" + data.date + "') ";
            if (freelancer)
            {
                selectCommand = "SELECT count(`projekt_jelolt_kapcs_kulsos`.`id`) as count FROM `projekt_jelolt_kapcs_kulsos` WHERE `projekt_id` = " + data.projekt_id + " AND `jelolt_id` = " + applicantID + "";
                insertCommand = @"INSERT INTO `projekt_jelolt_kapcs_kulsos` (`projekt_id`, `jelolt_id`, `datum`) VALUES (" + data.projekt_id + "," + applicantID + ",'" + data.date + "') ";
            }
            MySql mySql = new MySql();
            string result = mySql.SqlSingleQuery(selectCommand, "count");
            if (result == "0")
            {
                mySql.execute(insertCommand);
            }
            mySql.dbClose();
        }

    }
}
