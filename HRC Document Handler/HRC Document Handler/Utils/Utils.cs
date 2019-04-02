using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler
{
    public class Utility
    {
        public static string correction(string value, string conditionValue = "0")
        {
            return (value == conditionValue ? "default" : value.ToString());
        }

        public static void deleteWebTable(string table)
        {
            MySql mySql = new MySql(true);
            string command = "DELETE FROM hrportalweb." + table + ";";
            mySql.execute(command);
            mySql.dbClose();
        }
        
        public static void deleteTable(string table)
        {
            MySql mySql = new MySql();
            string command = "DELETE FROM " + table + ";";
            mySql.execute(command);
            mySql.dbClose();
        }

        public static bool hasWriteAccessToFolder(string folderPath)
        {
            try
            {
                return Directory.Exists(folderPath);

            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        public static string DateCorrect(int num)
        {
            return (num < 10 ? "0"+num.ToString() : num.ToString());
        }

        public static void AbortExcel()
        {
            foreach (Process clsProcess in Process.GetProcesses())
                if (clsProcess.ProcessName.Equals("EXCEL"))  //Process Excel?
                    clsProcess.Kill();
        }

    }
}
