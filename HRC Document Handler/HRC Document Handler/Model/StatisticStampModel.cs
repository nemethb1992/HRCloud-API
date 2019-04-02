using HRC_Document_Handler.Enum;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Model.StatisticModel
{
    class StatisticStampModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public string date { get; set; }

        static public List<StatisticStampModel> GetList(int type)
        {
            List<StatisticStampModel> list = new List<StatisticStampModel>();
            MySql mySql = new MySql();

            if (mySql.dbOpen() == true)
            {
                DateTime date = DateTime.Today.AddMonths(-1);
                string command = "SELECT * FROM statistic_stamp WHERE date > '" + date.Year + "." + Utility.DateCorrect(date.Month) + "." + Utility.DateCorrect(date.Day) + "' AND type = "+type+"";
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();

                while (mySql.sdr.Read())
                {
                    list.Add(new StatisticStampModel
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        name = mySql.sdr["name"].ToString(),
                        type = Convert.ToInt32(mySql.sdr["type"]),
                        date = mySql.sdr["date"].ToString()
                    });
                }
                mySql.sdr.Close();
                mySql.dbClose();
            }
            return list;
        }
        public static bool AlreadyTaken(DateTime date,int type)
        {
            MySql mysql = new MySql();
            bool bind = mysql.bind("SELECT id FROM statistic_stamp WHERE date = '" + date.Year + "." + Utility.DateCorrect(date.Month) + "." + Utility.DateCorrect(date.Day) + "' AND type = " + type);
            mysql.dbClose();
            return bind;
        }
    }
}
