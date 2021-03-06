﻿
using ActiveUp.Net.Mail;
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
    class EmailImap
    {
        private static SMTPmodel SMTPdatas;
        public SMTPmodel SMTPdata { get { return SMTPdatas; } set { SMTPdatas = value; } }
        Model.MySql mySql = new Model.MySql();

        public EmailImap()
        {
            SMTPdata = mySql.SMTPdataIMAP();
            mySql.dbClose();
        }

        public List<MimeMessage> GetUnreadMails()
        {
            var messages = new List<MimeMessage>();

            using (var client = new ImapClient())
            {
                client.Connect(SMTPdata.mailserver, SMTPdata.port, SMTPdata.ssl);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(SMTPdata.login, SMTPdata.password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadWrite);
                var results = inbox.Search(SearchOptions.All, SearchQuery.NotSeen);
                foreach (var uniqueId in results.UniqueIds)
                {
                    MimeMessage message = inbox.GetMessage(uniqueId);
                    messages.Add(message);
                }

                client.Disconnect(true);
            }

            return messages;
        }


        public void ReadImap()
        {
            string appURL = mySql.ApplicantURL().url;
            string profURl = mySql.ProfessionURL().url;
            mySql.dbClose();
            try
            {
                List<MimeMessage> emailList = GetUnreadMails();
                Console.WriteLine("Email-ek száma: "+emailList.Count.ToString());
                int otherType = 0;
                int savedDocs = 0;
                int savedApplicants = 0;
                foreach (MimeMessage email in emailList)
                {
                    string from = "";
                    try
                    {
                        from = (email.From.ToString().Split('<')[1]).Split('>')[0];
                    }
                    catch (Exception)
                    {
                        
                    }

                    if (from.Equals("jelentkezes@profession.hu"))
                    {
                        bool hasMessage = false;
                        foreach (MimeKit.MimePart mimePart in email.Attachments)
                        {
                            hasMessage = true;
                        }
                        if(hasMessage == true)
                        {
                            string seged = email.HtmlBody.ToString();
                            try
                            {
                                Profession prof = new Profession(seged);
                                string profId = prof.Insert();
                                savedApplicants++;
                                if (profId != null)
                                {
                                    string path = appURL + profId + "\\";
                                    foreach (MimeKit.MimePart mimePart in email.Attachments)
                                    {
                                        using (var memory = new MemoryStream())
                                        {
                                            mimePart.Content.DecodeTo(memory);
                                            var bytes = memory.ToArray();
                                            prof.SaveDocuments(path, mimePart.FileName, bytes);
                                            savedDocs++;
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                otherType++;
                                Console.WriteLine(otherType);
                            }

                        }
                    }

                    //if (email.From.Equals("studio@betapress.hu"))
                    //{
                    //    string seged = Regex.Split(email.HtmlBody, "\r\n")[1].Split('-')[0];

                    //    //Applicant applicant = new Applicant();
                    //        string path = appURL + seged + "\\";
                    //        foreach (MimeKit.MimePart mimePart in email.Attachments)
                    //        {
                    //            using (var memory = new MemoryStream())
                    //            {
                    //            if (seged == "")
                    //            {
                    //                 path = appURL + "Without ID\\";
                    //            }
                    //            mimePart.Content.DecodeTo(memory);
                    //                var bytes = memory.ToArray();
                    //            Applicant.SaveDocument(path, mimePart.FileName, bytes);
                    //            }
                    //        }
                    //}

                }

                Console.WriteLine("Mentett jelöltek száma:" + savedApplicants);
                Console.WriteLine("Mentett dokumentumok száma:" + savedDocs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
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


        //public void ReadImap()
        //{
        //    Applicant app = new Applicant();
        //    app.SearchExpired();
        //    return;

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
