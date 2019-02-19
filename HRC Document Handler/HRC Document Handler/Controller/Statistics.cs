using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Controller
{
    class Statistics
    {
        public static void JelentkezesLog(int jelolt_id, int projekt_id)
        {
             Model.MySql mySql = new Model.MySql();
             try
            {
                int weekNum = (DateTime.Now.DayOfYear / 7)+1;
                string command = "INSERT INTO `pmkcvtest`.`statisztika_jelentkezes`(`jelolt_id`,`projekt_id`,`kw`)"+
                                   " VALUES("+jelolt_id+", "+projekt_id+", "+ weekNum + ");";
                 mySql.execute(command);
             }
             catch (Exception)
             {
                 throw;
             }
             mySql.dbClose();
        }
    }
}
