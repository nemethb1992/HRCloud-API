using HRC_Document_Handler.Controller;
using HRC_Document_Handler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Threads
{
    class DbSynchroThread
    {
        public static void synchronize()
        {

            DateTime date = new DateTime();
            Console.WriteLine("Database synchronisation...");
            while (true)
            {
                date = DateTime.Now;
                Console.WriteLine(new ConsoleRender().header()+ "\nPress 'x' to pause.\n\nProgess:\n\nDatabase synchronized - " + date);
                new DatabaseSynchronizer();
                Thread.Sleep(120000);
            }

        }
    }
}
