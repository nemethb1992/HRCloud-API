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
            int iter = 0;
            while (true)
            {
                date = DateTime.Now;
                
                Console.Clear();
                Console.WriteLine(new ConsoleRender().header());
                Console.WriteLine("Press 'x' to pause.\n");
                Console.WriteLine("Progess:\n");
                Console.WriteLine("Database synchronized - " + date);
                new DatabaseSynchronizer();
                iter++;
                Thread.Sleep(30000);
            }

        }
    }
}
