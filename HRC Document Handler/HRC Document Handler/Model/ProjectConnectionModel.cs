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
        public string email { get; set; }
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
                        email = mySqlWeb.sdr["email"].ToString(),
                        date = mySqlWeb.sdr["date"].ToString()
                    });
                }
                mySqlWeb.sdr.Close();
                mySqlWeb.dbClose();
            }
            return list;
        }
        public static void insertDb(ProjectConnectionModel data, int applicantID)
        {
            MySql mySql = new MySql();
            mySql.execute(@"INSERT INTO `pmkcvtest`.`projekt_jelolt_kapcs` (`projekt_id`, `jelolt_id`,`hr_id`, `allapota`, `datum`) VALUES ("+data.projekt_id+","+ applicantID + ",default,default,'"+data.date+"') ");
        }

        }
}
