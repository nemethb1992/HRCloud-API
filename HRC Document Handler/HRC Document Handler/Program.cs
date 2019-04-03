using HRC_Document_Handler.Controller;
using HRC_Document_Handler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Deployment;
using HRC_Document_Handler.Threads;
using HRC_Document_Handler.Utils;
using HRC_Document_Handler.Sandbox;
using HRC_Document_Handler.Enum;

namespace HRC_Document_Handler
{

    public class Program
    {
        public static void Main()
        {
            ConsoleRender cr = new ConsoleRender();
            Console.WriteLine(cr.header());
            Thread dbThread = new Thread(new ThreadStart(DbSynchroThread.listener));
            Thread mailThread = new Thread(new ThreadStart(MailSenderThread.listener));
            Thread statisticThread = new Thread(new ThreadStart(AutoStatisticThread.listener));
            do
            {
                string suspend = "";
                try
                {
                    do
                    {
                        dbThread.Start();
                        mailThread.Start();
                        statisticThread.Start();
                        Console.WriteLine("Press 'x' to pause.");
                        suspend = Console.ReadLine();
                        if (suspend == "x")
                        {
                            statisticThread.Suspend();
                            mailThread.Suspend();
                            dbThread.Suspend();
                            Console.WriteLine("Suspended");
                            Console.WriteLine("Press 'y' to start again.");
                            if (Console.ReadLine() == "y")
                            {
                                Console.Clear();
                                Console.WriteLine(cr.header());
                                Console.WriteLine("Started");
                                statisticThread.Suspend();
                                mailThread.Resume();
                                dbThread.Resume();
                            }
                        }

                    } while (suspend != "y" && suspend != "n");
                }
                catch (Exception)
                {
                }
             } while (true);
        }

    }
}
