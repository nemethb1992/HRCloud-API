using HRC_Document_Handler.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Controller
{
    class Applicant
    {
        List<ModelFullApplicant> ApplicantList;

        public Applicant()
        {
            ApplicantList = new Outdated_cont().JeloltFullDataSource();
        }

        public int SearchExpired()
        {
            foreach (var item in ApplicantList)
            {
                string strStartDate = item.reg_date.Replace(".",null);
                CultureInfo provider = CultureInfo.InvariantCulture;
                DateTime applicantDate = DateTime.ParseExact(strStartDate, "yyyyMMdd", provider);
                DateTime oneYear = DateTime.Today.AddYears(-1);
                if(oneYear > applicantDate)
                Console.WriteLine(applicantDate + "  " + oneYear);
            }
            return 0;
        }


        public static void SaveDocument(string path, string fileName, byte[] file)
        {
            Directory.CreateDirectory(path);
            File.WriteAllBytes(path + fileName, file.ToArray());
        }

        ///<summary>
        ///<para>Visszatér az adott e-mail címhez tartozó jelölt id-vel.</para>
        ///<para>Ha nem szerepel az adott cím a táblában, 0-val tér vissza.</para> 
        ///<returns>return string</returns>
        ///</summary>
        public static int isExists(string email, bool publicDb = false)
        {
            int id = 0;
            MySql mySql = new MySql(publicDb);
            if (mySql.bind("SELECT count(id) FROM jeloltek WHERE email='" + email + "'"))
            {
                try
                {
                    id = Convert.ToInt32(mySql.SqlSingleQuery("SELECT id FROM jeloltek WHERE email='" + email + "'", "id"));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return id;
        }

    }
}
