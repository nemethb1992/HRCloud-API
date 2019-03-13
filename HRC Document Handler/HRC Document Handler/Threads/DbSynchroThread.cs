﻿using HRC_Document_Handler.Controller;
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
            Console.WriteLine(" - Database synchronisation is running... \n");
            while (true)
            {
                new DatabaseSynchronizer();
                Thread.Sleep(120000); 
            }
        }
    }
}
