using HRC_Document_Handler.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Controller
{
    class DatabaseSynchronizer
    {
        public DatabaseSynchronizer()
        {
            synchronize();
        }

        private void synchronize()
        {
            Model.MySql mySql = new Model.MySql();
            Model.MySql mySqlWeb = new Model.MySql(true);
            string appURL = mySql.ApplicantURL().url;

            List<ModelFullApplicant> webList = ModelWebApplicant.getList("SELECT * FROM jeloltek");
            foreach (ModelFullApplicant applicant in webList)
            {
                if(Applicant.isExists(applicant.email) == null)
                {
                    int applicantID = applicant.Insert();
                    if(applicantID != 0)
                    {
                        //TODO: email kiküldése
                        applicant.deleteWeb(applicant.email);
                        List<DocumentModel> docList = DocumentModel.GetDocuments(applicant.email);
                        string path = appURL + applicantID + "\\";
                        foreach (var doc in docList)
                        {
                            Applicant.SaveDocument(path, doc.document_name,doc.document);
                            doc.deleteDocumentWeb(doc.document_name);
                        }
                    }
                }
            }

            mySql.dbClose();
            mySqlWeb.dbClose();
        }
    }
}
