using HRC_Document_Handler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Threads
{
    class MailSenderThread
    {
        public static void listener()
        {
            Console.WriteLine("\n - Mail handler is running...");
            while (true)
            {
                new EmailSender();
                //Console.WriteLine("\n (Mail) Last activity: - " + DateTime.Now);
                Thread.Sleep(60000);
            }
        }
    }
}
