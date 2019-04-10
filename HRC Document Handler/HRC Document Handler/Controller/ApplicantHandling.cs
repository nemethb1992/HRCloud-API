using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Controller
{
    class ApplicantHandling
    {
        MySql mySql;
        public ApplicantHandling()
        {
            mySql = new MySql();
            PassivateApplicants(GetOldApplicants());
        }

        protected List<string> GetOldApplicants()
        {
            List<string> list = new List<string>();
            DateTime date = DateTime.Today.AddMonths(-3);
            string command = "SELECT email FROM jeloltek " +
                "LEFT JOIN interjuk_kapcs ON jeloltek.id = interjuk_kapcs.jelolt_id " +
                "WHERE jeloltek.reg_date < '" + date.Year + "." + Utility.DateCorrect(date.Month) + "." + Utility.DateCorrect(date.Day) + "' AND interjuk_kapcs.jelolt_id IS NULL AND statusz = 1;";
            list = mySql.uniqueList(command, new string[] { "email" });
            mySql.dbClose();
            return list;
        }

        protected void PassivateApplicants(List<string> emailAddresses)
        {
            if(emailAddresses.Count == 0)
            {
                return;
            }
            foreach (var email in emailAddresses)
            {
                mySql.execute("UPDATE jeloltek SET statusz = 0 WHERE email = '"+email+"'");
            }
            mySql.dbClose();
        }

    }
}
