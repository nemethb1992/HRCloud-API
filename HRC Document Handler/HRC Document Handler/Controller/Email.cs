using ActiveUp.Net.Mail;
using HRC_Document_Handler.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Controller
{
    class Email
    {
        private static List<MailServer_m> SMTPdatas;
        public List<MailServer_m> SMTPdata { get { return SMTPdatas; } set { SMTPdatas = value; } }
        public Email()
        {
            SMTPdata = MailDataSource();
        }

        dbEntities dbE = new dbEntities();

        public class MailRepository
        {
            private Imap4Client client;

            public MailRepository(string mailServer, int port, bool ssl, string login, string password)
            {
                if (ssl)
                    Client.ConnectSsl(mailServer, port);
                else
                    Client.Connect(mailServer, port);
                Client.Login(login, password);
            }

            public IEnumerable<Message> GetAllMails(string mailBox)
            {
                return GetMails(mailBox, "ALL").Cast<Message>();
            }

            public IEnumerable<Message> GetUnreadMails(string mailBox)
            {
                return GetMails(mailBox, "UNSEEN").Cast<Message>();
            }

            protected Imap4Client Client
            {
                get { return client ?? (client = new Imap4Client()); }
            }

            private MessageCollection GetMails(string mailBox, string searchPhrase)
            {
                Mailbox mails = Client.SelectMailbox(mailBox);
                MessageCollection messages = mails.SearchParse(searchPhrase);
                return messages;
            }
        }
        public void ReadImap()
        {

            string[] seged;
            string path, fileName;
            byte[] attachment;
            List<object> attach = new List<object>(); ;
            var mailRepository = new MailRepository(
                                 SMTPdata[0].mailserver,
                                 SMTPdata[0].port,
                                 SMTPdata[0].ssl,
                                 SMTPdata[0].login,
                                 SMTPdata[0].password
                             );
            try
            {

                var emailList = mailRepository.GetUnreadMails("inbox");
                foreach (Message email in emailList)
                {
                    if (email.From.Email.ToString() == SMTPdata[0].sender_email)
                    {
                        seged = Regex.Split(email.BodyText.Text, "\r\n")[1].Split('-');
                        attachment = email.Attachments[0].BinaryContent;
                        fileName = email.Attachments[0].Filename;
                        path = FolderDataSource()[0].url + "\\" + seged[0] + "\\";
                        if (seged[0] == "")
                        {
                            path = FolderDataSource()[0].url + "\\Without ID\\";
                        }
                        try
                        {
                            Directory.CreateDirectory(path);
                            File.WriteAllBytes(path + fileName, attachment);
                        }
                        catch { }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public List<MailServer_m> MailDataSource()
        {
            string query = "SELECT * FROM ConnectionSMTP";
            return dbE.ConnectionSMTP_DataSource(query);
        }
        public List<FolderUrl_m> FolderDataSource()
        {
            string query = "SELECT * FROM FolderLocate";
            return dbE.Folder_DataSource(query);
        }
    }
}
