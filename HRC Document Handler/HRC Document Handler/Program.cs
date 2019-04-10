using HRC_Document_Handler.Controller;
using HRC_Document_Handler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            do
            {
                string suspend = "";
                try
                {
                    do
                    {
                        Console.WriteLine("Press 'x' to pause.");
                        ThreadHandler thread = new ThreadHandler();
                        thread.Start();

                        suspend = Console.ReadLine();
                        if (suspend == "x")
                        {
                            thread.Pause();
                            Console.WriteLine("Suspended!");
                            Console.WriteLine("Press 'y' to start again.");
                            if (Console.ReadLine() == "y")
                            {
                                Console.Clear();
                                Console.WriteLine(cr.header());
                                Console.WriteLine("Started");
                                thread.Resume();
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
