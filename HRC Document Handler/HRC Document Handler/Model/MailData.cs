using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Model
{
    class MailData
    {
        public int id { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public int hr_id { get; set; }
        public int state { get; set; }
        public string date { get; set; }

        public static List<MailData> GetMails(bool testMails = false)
        {
            List<MailData> list = new List<MailData>();
            MySql mySql;
            if(testMails)
            {
                mySql = new MySql(1);
            }
            else
            {
                mySql = new MySql();
            }
            if (mySql.dbOpen() == true)
            {
                string command = "SELECT * FROM email_storage WHERE state=0";
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();

                while (mySql.sdr.Read())
                {
                    list.Add(new MailData
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        to = mySql.sdr["to"].ToString(),
                        subject = mySql.sdr["subject"].ToString(),
                        content = mySql.sdr["content"].ToString(),
                        hr_id = Convert.ToInt32(mySql.sdr["hr_id"]),
                        state = Convert.ToInt32(mySql.sdr["state"]),
                        date = mySql.sdr["date"].ToString()
                    });
                }
                mySql.sdr.Close();
                mySql.dbClose();
            }
            return list;
        }

        public void setSent(bool testMails = false)
        {
            state = 1;
            MySql mySql;

            if (testMails)
            {
                mySql = new MySql(1);
            }
            else
            {
                mySql = new MySql();
            }
            mySql.execute("UPDATE `email_storage` SET `state` = 1 WHERE `id` = "+id+";");
        }
    }
}
