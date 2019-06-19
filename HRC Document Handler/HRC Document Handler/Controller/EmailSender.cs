using HRC_Document_Handler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Utils
{
    class EmailSender
    {
        public EmailSender()
        {
            List<MailData> mails = MailData.GetMails();
            List<MailData> testMails = MailData.GetMails(true);
            if (mails.Count > 0)
            {
                foreach (var mail in mails)
                {
                    send(mail);
                    mail.setSent();
                }
            }

            if (testMails.Count > 0)
            {
                foreach (var mail in testMails)
                {
                    send(mail);
                    mail.setSent(true);
                }
            }

        }

        protected void testMailLoadup(string to, int db)
        {
            MySql mySql = new MySql();
            for (int i = 0; i < db; i++)
            {
                string sql = "INSERT INTO `email_storage` (`to`, `subject`, `content`, `hr_id`, `state`, `date`) VALUES ('"+ to + "', 'HR Portal -Phoenix Mecano Kecskemét kft.', 'Teszt', 1, 0, '2019.03.13');";
                mySql.execute(sql);
            }
            mySql.dbClose();
        }

        public void send(MailData email)
        {
            try
            {
                using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient())
                {
                    client.Host = "192.168.144.14";
                    client.Port = 25;
                    client.UseDefaultCredentials = true;

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.Subject = email.subject;
                        mail.Body = email.content;
                        mail.From = new MailAddress("hrportal@phoenix-mecano.hu");
                        mail.IsBodyHtml = true;
                        mail.To.Add(email.to);
                        client.Send(mail);
                    }
                }
            }
            catch (System.Net.Mail.SmtpException)
            {

            }
        }
    }
}
