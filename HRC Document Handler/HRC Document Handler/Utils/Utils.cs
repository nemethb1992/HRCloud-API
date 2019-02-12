using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Utils
{
    public class Utils
    {
        public static string correction(string value, string conditionValue = "0")
        {
            return (value == conditionValue ? "default" : value.ToString());
        }
    }
}
