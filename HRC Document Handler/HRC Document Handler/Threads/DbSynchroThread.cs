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
        public static void listener()
        {
            Console.WriteLine("\n - Database synchronisation is running...");
            while (true)
            {
                try
                {
                    //new DatabaseSynchronizer();
                }
                catch (Exception e)
                {
                    Error.Log(e.ToString(), "DatabaseSynchronizer");
                }
                Thread.Sleep(120000); 
            }
        }
    }
}
