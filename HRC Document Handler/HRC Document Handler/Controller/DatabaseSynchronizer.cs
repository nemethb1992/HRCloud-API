using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Controller
{
    class DatabaseSynchronizer
    {
        public DatabaseSynchronizer()
        {

        }

        private void syncLayoutData()
        {
            Model.MySql webDb = new Model.MySql(true);
            Model.MySql innerDb = new Model.MySql();

        }

    }
}
