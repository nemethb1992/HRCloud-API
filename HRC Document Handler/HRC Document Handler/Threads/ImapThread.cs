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
    public class ImapThread
    {
            public static void getMail()
            {

                DateTime date = new DateTime();
                Console.WriteLine("E-Mail Service Active...");
                Email email = new Email();
                while (true) 
                {
                    date = DateTime.Now;
                    Console.Clear();
                    Console.WriteLine(new ConsoleRender().header());
                    Console.WriteLine("Press 'x' to pause.\n");
                    Console.WriteLine("Progess:\n");
                    Console.WriteLine("E-mail synchronized - " + date);
                    email.ReadImap();
                    Thread.Sleep(30000);
                }

          }
    }
}
