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

namespace HRC_Document_Handler
{

    public class Program
    {
        public static void Main()
        {
            ConsoleRender cr = new ConsoleRender();
            Console.WriteLine(cr.header());
            Model.MySql mySql = new Model.MySql();
            int count = Convert.ToInt32(mySql.SqlSingleQuery("SELECT count(mailserver) as count FROM ConnectionSMTP", "count"));
            mySql.dbClose();
            Thread mailThread = new System.Threading.Thread(new ThreadStart(ImapThread.getMail));
            Thread dbThread = new System.Threading.Thread(new ThreadStart(DbSynchroThread.synchronize));
            do {
                if (count > 0)
                {
            
                        string suspend = "";
                    //Thread tid2 = new Thread(new ThreadStart(MyThread.Thread2));
                    try
                    {

                        do
                        {
                            mailThread.Start();
                            //dbThread.Start();
                        suspend = Console.ReadLine();
                        if (suspend == "x")
                        {
                            mailThread.Suspend();
                            dbThread.Suspend();
                            Console.WriteLine("Suspended");
                            Console.WriteLine("Press 'y' to start again.");
                            if (Console.ReadLine() == "y")
                                {
                                    Console.Clear();
                                    Console.WriteLine(cr.header());
                                    Console.WriteLine("Started");
                                    mailThread.Resume();
                                    dbThread.Resume();
                                }
                        }

                        } while (suspend != "y" && suspend != "n");
                    }
                    catch (Exception)
                    {
                    }


                }
                else
                {
                    mailThread.Start();
                }
            } while (true);

        }

    }
}
