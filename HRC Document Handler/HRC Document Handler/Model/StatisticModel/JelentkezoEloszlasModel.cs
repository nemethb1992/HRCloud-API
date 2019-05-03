using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Model.StatisticModel
{
    class JelentkezoEloszlasModel
    {
        public string projekt_megnevezes { get; set; }
        public int darab { get; set; }

        static public List<JelentkezoEloszlasModel> GetByProjekt(DateTime from, DateTime to)
        {
            List<JelentkezoEloszlasModel> list = new List<JelentkezoEloszlasModel>();
            MySql mySql = new MySql();

            if (mySql.dbOpen() == true)
            {
                string command = "SELECT projektek.megnevezes_projekt, count(jelentkezesek.id) as darab FROM jelentkezesek LEFT JOIN projektek ON jelentkezesek.projekt_id = projektek.id WHERE jelentkezesek.reg_date >= '" + from.Year + "." + Utility.DateCorrect(from.Month) + "." + Utility.DateCorrect(from.Day) + "' AND jelentkezesek.reg_date <= '" + to.Year + "." + Utility.DateCorrect(to.Month) + "." + Utility.DateCorrect(to.Day) + "' GROUP BY jelentkezesek.projekt_id";
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();

                while (mySql.sdr.Read())
                {
                    list.Add(new JelentkezoEloszlasModel
                    {
                        darab = Convert.ToInt32(mySql.sdr["darab"]),
                        projekt_megnevezes = mySql.sdr["projekt_megnevezes"].ToString(),
                    });
                }
                mySql.sdr.Close();
                mySql.dbClose();
            }
            return list;

        }


    }
}
