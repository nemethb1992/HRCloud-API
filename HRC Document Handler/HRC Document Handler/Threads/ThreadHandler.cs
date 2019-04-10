using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Threads
{
    class ThreadHandler
    {
        private Thread databaseSynchronisation;
        private Thread mailSender;
        private Thread statisticHandler;
        private Thread applicantHandler;
        public ThreadHandler()
        {
            databaseSynchronisation = new Thread(new ThreadStart(DbSynchroThread.listener));
            mailSender = new Thread(new ThreadStart(MailSenderThread.listener));
            statisticHandler = new Thread(new ThreadStart(AutoStatisticThread.listener));
            applicantHandler = new Thread(new ThreadStart(AutoApplicantHandlerThread.listener));
        }
        public void Start()
        {
            if (databaseSynchronisation != null)
            {
                databaseSynchronisation.Start();
            }
            if (mailSender != null)
            {
                mailSender.Start();
            }
            if (statisticHandler != null)
            {
                statisticHandler.Start();
            }
            if (applicantHandler != null)
            {
                applicantHandler.Start();
            }
        }
        public void Pause()
        {
            if(databaseSynchronisation.IsAlive)
            {
                databaseSynchronisation.Suspend();
            }
            if (mailSender.IsAlive)
            {
                mailSender.Suspend();
            }
            if (statisticHandler.IsAlive)
            {
                statisticHandler.Suspend();
            }
            if (applicantHandler.IsAlive)
            {
                applicantHandler.Suspend();
            }
        }
        public void Resume()
        {
            if (databaseSynchronisation.IsAlive)
            {
                databaseSynchronisation.Resume();
            }
            if (mailSender.IsAlive)
            {
                mailSender.Resume();
            }
            if (statisticHandler.IsAlive)
            {
                statisticHandler.Resume();
            }
            if (applicantHandler.IsAlive)
            {
                applicantHandler.Resume();
            }
        }
    }
}
