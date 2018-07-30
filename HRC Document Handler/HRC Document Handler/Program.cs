using HRC_Document_Handler.Controller;
using HRC_Document_Handler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRC_Document_Handler
{
    public class MyThread
    {
        public static void Thread1()
        {

            DateTime date = new DateTime();
            Console.WriteLine("E-Mail Service Active...");
            Email email = new Email();
            dbEntities dbE = new dbEntities();
            int iter = 0;
            while (true)
            {
                date = DateTime.Now;
                Console.Clear();
                Console.WriteLine("(HR Cloud)\tE-mail API v1.0 - Phoenix Mecano Kecskem�t kft.\n");
                Console.WriteLine("Press 'x' to pause.\n");
                Console.WriteLine("Progess:\n");
                Console.WriteLine("Synchronized - " + date);
                email.ReadImap();
                iter++;
                Thread.Sleep(3000);
            }

        }
    }
    public class Program
    {
        public static void Main()
        {
            dbEntities dbE = new dbEntities();
            Email email = new Email();
            Console.WriteLine("(HR Cloud)\tE-mail API v1.0 - Phoenix Mecano Kecskem�t kft.\n");
            int iteration = 0;
            string setupSmtp = "";
            int count = Convert.ToInt32(dbE.SqliteReaderExecute("SELECT count(mailserver) FROM connectionSMTP"));
            Thread tid1 = new System.Threading.Thread(new ThreadStart(MyThread.Thread1));
            if (count > 0)
            {
                string suspend = "";
                //Thread tid2 = new Thread(new ThreadStart(MyThread.Thread2));
                try
                {
                    tid1.Start();
                }
                catch (Exception)
                {
                    setup();
                    tid1.Start();
                }
            //    do { 
            //    //if (suspend == "x")
            //    //{
            //    //    tid1.Suspend();
            //    //    Console.WriteLine("Suspended");
            //    //    Console.WriteLine("Press 'y' to start again.");
            //    //    if (Console.ReadLine() == "y")
            //    //    {
            //    //        tid1.Resume();
            //    //        Console.WriteLine("Started");
            //    //    }
            //    //}

            //} while (suspend != "y" && suspend != "n") ;
            }
            else
            {
                setup();
                tid1.Start();
            }


            void setup()
            {
                Console.Clear();
                Console.WriteLine("(HR Cloud)\tE-mail API v1.0 - Phoenix Mecano Kecskem�t kft.\n");
                Console.WriteLine("Setup SMTP connection!");

                string mailserver, login, password, url, sender_email;
                int port, ssl;
                Console.WriteLine("Mail Server:");
                mailserver = Console.ReadLine();
                Console.WriteLine("Port:");
                port = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("SSL: (yes : 1 / no : 0)");
                ssl = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Login:");
                login = Console.ReadLine();
                Console.WriteLine("Password:");
                password = Console.ReadLine();
                Console.WriteLine("Megfigyelt e-mail c�m::");
                sender_email = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("(HR Cloud)\tE-mail API v1.0 - Phoenix Mecano Kecskem�t kft.\n");
                Console.WriteLine("Mail Server:\t" + mailserver);
                Console.WriteLine("Port:\t" + port.ToString());
                Console.WriteLine("SSL:\t" + ssl);
                Console.WriteLine("Login:\t" + login);
                Console.WriteLine("Megfigyelt e-mail c�m: " + sender_email + "\n");
                Console.WriteLine("Adja meg a f�jlrendszer gy�ker�t:");
                url = Console.ReadLine();
                dbE.SqliteQueryExecute("DELETE FROM ConnectionSMTP");
                dbE.SqliteQueryExecute("INSERT INTO ConnectionSMTP ( `mailserver`, `port`, `ssl`, `login`, `password`, `sender_email` ) VALUES ('" + mailserver + "'," + port + "," + ssl + ",'" + login + "','" + password + "','" + sender_email + "')");
                dbE.SqliteQueryExecute("DELETE FROM FolderLocate");
                dbE.SqliteQueryExecute("INSERT INTO FolderLocate ( `url`, `username`, `password` ) VALUES ('" + url + "','','')");
            }




            //tid2.Start();
            Console.ReadKey();

        }

    }
}
