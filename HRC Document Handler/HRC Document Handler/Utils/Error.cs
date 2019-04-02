using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Utils
{
    class Error
    {
        public static void Log(string text, string thread)
        {
            MySql mySql = new MySql();
            mySql.execute("INSERT INTO errorlog (text,thread,log_time) VALUES('" + text + "','" + thread + "','" + DateTime.Now.ToString() +"')");
            mySql.dbClose();
        }
    }
}
