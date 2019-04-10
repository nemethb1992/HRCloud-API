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
    class AutoApplicantHandlerThread
    {
        public static void listener()
        {
            Console.WriteLine("\n - Applicant handling automatisation is running...");
            while (true)
            {
                try
                {
                    new ApplicantHandling();
                }
                catch (Exception e)
                {
                    Error.Log(e.ToString(), "ApplicantHandling");
                }
                Thread.Sleep(1800000);
            }
        }
    }
}
