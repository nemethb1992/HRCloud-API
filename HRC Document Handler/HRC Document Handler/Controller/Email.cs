
using HRC_Document_Handler.Model;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
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
        Model.MySql mySql = new Model.MySql();

        public Email()
        {
            SMTPdata = mySql.SMTPdataIMAP();
        }

        //public class MailRepository
        //{
        //    private Imap4Client client;

        //    public MailRepository(string mailServer, int port, bool ssl, string login, string password)
        //    {
        //        if (ssl)
        //            Client.ConnectSsl(mailServer, port);
        //        else
        //            Client.Connect(mailServer, port);
        //        Client.Login(login, password);
        //    }

        //    public IEnumerable<Message> GetAllMails(string mailBox)
        //    {
        //        return GetMails(mailBox, "ALL").Cast<Message>();
        //    }

        //    public IEnumerable<Message> GetUnreadMails(string mailBox)
        //    {
        //        return GetMails(mailBox, "UNSEEN").Cast<Message>();
        //    }

        //    protected Imap4Client Client
        //    {
        //        get { return client ?? (client = new Imap4Client()); }
        //    }

        //    private MessageCollection GetMails(string mailBox, string searchPhrase)
        //    {
        //        Mailbox mails = Client.SelectMailbox(mailBox);
        //        MessageCollection messages = mails.SearchParse(searchPhrase);
        //        return messages;
        //    }
        //}

        public List<MimeKit.MimeMessage> GetUnreadMails()
        {
            var messages = new List<MimeKit.MimeMessage>();

            using (var client = new ImapClient())
            {
                client.Connect(SMTPdata.mailserver, SMTPdata.port, SMTPdata.ssl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(SMTPdata.login, SMTPdata.password);

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                var results = inbox.Search(SearchOptions.All, SearchQuery.Not(SearchQuery.Seen));
                foreach (var uniqueId in results.UniqueIds)
                {
                    MimeKit.MimeMessage message = inbox.GetMessage(uniqueId);

                    messages.Add(message);

                    //Mark message as read
                    //inbox.AddFlags(uniqueId, MessageFlags.Seen, true);
                }

                client.Disconnect(true);
            }

            return messages;
        }


        public void ReadImap()
        {
            //Applicant app = new Applicant();
            //app.SearchExpired();
            //return;

            string path, fileName;
            byte[] attachment = null;
            try
            {
                List<MimeMessage> emailList = GetUnreadMails();
                foreach (MimeMessage email in emailList)
                {
                    string from = (email.From.ToString().Split('<')[1]).Split('>')[0];
                    if (from.Equals("jelentkezes@profession.hu"))
                    {
                        string seged = email.HtmlBody.ToString();
                        Profession prof = new Profession(email.HtmlBody.ToString());
                        string profId = prof.Insert();
                        if (profId != null)
                        {
                            //= email.Attachments[0].BinaryContent
                            
                            foreach(MimePart mimePart in email.Attachments)
                            {
                                string attach = mimePart.ToString();
                                Console.WriteLine(mimePart.FileName);
                                Console.WriteLine(attach);
                                //fileName = mimePart.FileName;
                                //path = mySql.ProfessionURL() + profId + "\\";
                                //prof.SaveDocuments(path, fileName, attach);
                            }
                        }
                    }

                    //if (email.From.Email.ToString() == "jelentkezes@phoenix-mecano.hu")
                    //{
                    //    string seged = Regex.Split(email.BodyText.Text, "\r\n")[1].Split('-')[0];
                    //    try
                    //    {
                    //        attachment = email.Attachments[0].BinaryContent;
                    //        fileName = email.Attachments[0].Filename;
                    //        path = mySql.ApplicantURL() + seged + "\\";
                    //        if (seged == "")
                    //        {
                    //            path = mySql.ApplicantURL() + "Without ID\\";
                    //        }
                    //        try
                    //        {
                    //            Directory.CreateDirectory(path);
                    //            File.WriteAllBytes(path + fileName, attachment);
                    //        }
                    //        catch { }
                    //    }
                    //    catch (Exception)
                    //    {
                    //    }
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        //public void ReadImap()
        //{
        //    //Applicant app = new Applicant();
        //    //app.SearchExpired();
        //    //return;

        //    string path, fileName;
        //    byte[] attachment = null;
        //    var mailRepository = new MailRepository(SMTPdata.mailserver, SMTPdata.port, SMTPdata.ssl, SMTPdata.login, SMTPdata.password);
        //    try
        //    {
        //        var emailList = mailRepository.GetUnreadMails("INBOX");
        //        foreach (Message email in emailList)
        //        {

        //            if (email.From.Email.ToString() == "jelentkezes@profession.hu")
        //            {
        //                Profession prof = new Profession(email.BodyText.Text);
        //                string profId = prof.Insert();
        //                if (profId != null)
        //                {
        //                    byte[] attach = email.Attachments[0].BinaryContent;
        //                    fileName = email.Attachments[0].Filename;
        //                    path = mySql.ProfessionURL() + profId + "\\";
        //                    prof.SaveDocuments(path, fileName, attach);
        //                }
        //            }

        //            if (email.From.Email.ToString() == "jelentkezes@phoenix-mecano.hu")
        //            {
        //                string seged = Regex.Split(email.BodyText.Text, "\r\n")[1].Split('-')[0];
        //                try
        //                {
        //                    attachment = email.Attachments[0].BinaryContent;
        //                    fileName = email.Attachments[0].Filename;
        //                    path = mySql.ApplicantURL() + seged + "\\";
        //                    if (seged == "")
        //                    {
        //                        path = mySql.ApplicantURL() + "Without ID\\";
        //                    }
        //                    try
        //                    {
        //                        Directory.CreateDirectory(path);
        //                        File.WriteAllBytes(path + fileName, attachment);
        //                    }
        //                    catch { }
        //                }
        //                catch (Exception)
        //                {
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}



    }
}
