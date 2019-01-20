using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Model
{
    public class SMTPmodel
    {
        public string mailserver { get; set; }
        public int port { get; set; }
        public bool ssl { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
    public class FolderModel
    {
        public string url { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
