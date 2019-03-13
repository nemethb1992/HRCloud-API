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
            if(mails.Count > 0)
            {
                foreach (var mail in mails)
                {
                    send(mail);
                    mail.setSent();
                }
            }

        }

        protected void testMailLoadup(string to, int db)
        {
            Model.MySql mySql = new Model.MySql();
            for (int i = 0; i < db; i++)
            {
                string sql = "INSERT INTO `email_storage` (`to`, `subject`, `content`, `hr_id`, `state`, `date`) VALUES ('"+ to + "', 'HR Portal -Phoenix Mecano Kecskemét kft.', 'Teszt', 1, 0, '2019.03.13');";
                mySql.execute(sql);
            }
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
            catch (System.Net.Mail.SmtpException ex)
            {

            }
        }
    }
}
