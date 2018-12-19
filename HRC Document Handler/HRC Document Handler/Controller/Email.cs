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
        private static SMTPmodel SMTPdatas;
        public SMTPmodel SMTPdata { get { return SMTPdatas; } set { SMTPdatas = value; } }
        public Email()
        {
            SMTPdata = MailDataSource();
        }

        Model.SQLite dbE = new Model.SQLite();

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
            Applicant app = new Applicant();
            app.SearchExpired();
            return;

            string path, fileName;
            byte[] attachment = null;
            var mailRepository = new MailRepository(SMTPdata.mailserver, SMTPdata.port, SMTPdata.ssl, SMTPdata.login, SMTPdata.password);
            try
            {
                var emailList = mailRepository.GetUnreadMails("inbox");
                foreach (Message email in emailList)
                {
                    if (email.From.Email.ToString() == "jelentkezes@profession.hu")
                    {
                        Profession prof =  new Profession(email.BodyText.Text);
                        string profId = prof.Insert();
                        if(profId != null)
                        {
                            byte[] attach = email.Attachments[0].BinaryContent;
                            fileName = email.Attachments[0].Filename;
                            path = FolderDataSource().url + "\\ProfessionDocuments\\" + profId + "\\";
                            prof.SaveDocuments(path, fileName, attach);
                        }

                    }

                    if (email.From.Email.ToString() == SMTPdata.sender_email)
                    {
                        string seged = Regex.Split(email.BodyText.Text, "\r\n")[1].Split('-')[0];
                        try
                        {
                            attachment = email.Attachments[0].BinaryContent;
                            fileName = email.Attachments[0].Filename;
                            path = FolderDataSource().url + "\\" + seged + "\\";
                            if (seged == "")
                            {
                                path = FolderDataSource().url + "\\Without ID\\";
                            }
                            try
                            {
                                Directory.CreateDirectory(path);
                                File.WriteAllBytes(path + fileName, attachment);
                            }
                            catch { }
                        }
                        catch (Exception)
                        {
                        }
   
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public SMTPmodel MailDataSource()
        {
            string query = "SELECT * FROM ConnectionSMTP";
            return dbE.SMTPdata(query);
        }
        public FolderModel FolderDataSource()
        {
            string query = "SELECT * FROM FolderLocate";
            return dbE.Folder_DataSource(query);
        }
    }
}
