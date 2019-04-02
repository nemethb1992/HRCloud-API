using HRC_Document_Handler.Controller;
using System;
using System.Threading;

namespace HRC_Document_Handler.Threads
{
    class AutoStatisticThread
    {
        public static void listener()
        {
            Console.WriteLine("\n - Statistic automatisation is running...");
            while (true)
            {
                try
                {
                    new AutomatizedStatistics();
                }
                catch (Exception)
                {
                    //Hibalog
                }
                Thread.Sleep(1800000);
            }
        }
    }
}
