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
        public void SaveDocuments(string path, string fileName, byte[] file)
        {
            Directory.CreateDirectory(path);
            File.WriteAllBytes(path + fileName, file);
        }

    }
}
