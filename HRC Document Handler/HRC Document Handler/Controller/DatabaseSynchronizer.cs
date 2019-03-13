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
        private string appURL;
        private Model.MySql mySql;
        private Model.MySql mySqlWeb;
        public DatabaseSynchronizer()
        {
            //try
            //{
                mySql = new Model.MySql();
                mySqlWeb = new Model.MySql(true);
                appURL = mySql.ApplicantURL().url;
                if (Utils.Utils.hasWriteAccessToFolder(appURL))
                {
                    synchronizeApplicants();
                }
                else
                {
                    Console.WriteLine("- Applicants mappa elérése sikertelen!");
                }

                synchronizeResources();
                mySql.dbClose();
                mySqlWeb.dbClose();
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("Szál futása sikertelen!");
            //}

        }

        private void synchronizeApplicants()
        {
            List<ModelFullApplicant> webList = ModelWebApplicant.getList("SELECT * FROM jeloltek");
            foreach (ModelFullApplicant applicant in webList)
            {
                if (Applicant.isExists(applicant.email) == 0)
                {
                    int applicantID = applicant.Insert();
                    if (applicantID != 0)
                    {
                        List<ProjectConnectionModel> connectedProjects = ProjectConnectionModel.getListWeb("SELECT * FROM projekt_jelolt_kapcs WHERE email = '" + applicant.email + "'");
                        foreach (var projects in connectedProjects)
                        {
                            ProjectConnectionModel.insertDb(projects, applicantID);
                        }
                        //TODO: email kiküldése
                        applicant.deleteWeb(applicant.email);
                        List<DocumentModel> docList = DocumentModel.GetDocuments(applicant.email);
                        string path = appURL + applicantID.ToString() + "\\";
                        foreach (var doc in docList)
                        {
                            Applicant.SaveDocument(path, doc.document_name, doc.document);
                            doc.deleteDocumentWeb(applicant.email);
                        }
                    }
                }
                else
                {
                    int applicantID = applicant.Update(applicant.email);

                    List<ProjectConnectionModel> connectedProjects = ProjectConnectionModel.getListWeb("SELECT * FROM projekt_jelolt_kapcs WHERE email = '" + applicant.email + "'");
                    foreach (var projects in connectedProjects)
                    {
                        ProjectConnectionModel.insertDb(projects, applicantID);
                    }
                    //TODO: email kiküldése
                    applicant.deleteWeb(applicant.email);
                    List<DocumentModel> docList = DocumentModel.GetDocuments(applicant.email);
                    string path = appURL + applicantID.ToString() + "\\";
                    foreach (var doc in docList)
                    {
                        Applicant.SaveDocument(path, doc.document_name, doc.document);
                        doc.deleteDocumentWeb(applicant.email);
                    }
                }
            }
        }

        private void synchronizeResources()
        {
            List<ModelProjektek> projektList = ModelProjektek.getProjektek("SELECT id, megnevezes_projekt, statusz, fel_datum FROM projektek WHERE statusz=1");
            Utils.Utils.deleteWebTable("projektek");
            foreach (var item in projektList)
            {
                item.insertWeb(mySqlWeb);
            }

            List<ModelErtesulesek> ertesulesekList = ModelErtesulesek.getErtesulesek("SELECT id, ertesules_megnevezes FROM ertesulesek");
            Utils.Utils.deleteWebTable("ertesulesek");
            foreach (var item in ertesulesekList)
            {
                item.insertWeb(mySqlWeb);
            }

            List<ModelNyelv> nyelvList = ModelNyelv.getNyelv("SELECT id, megnevezes_nyelv FROM nyelv");
            Utils.Utils.deleteWebTable("nyelv");
            foreach (var item in nyelvList)
            {
                item.insertWeb(mySqlWeb);
            }

            List<ModelVegzettseg> vegzettsegList = ModelVegzettseg.getVegzettsegek("SELECT id, megnevezes_vegzettseg FROM vegzettsegek");
            Utils.Utils.deleteWebTable("vegzettsegek");
            foreach (var item in vegzettsegList)
            {
                item.insertWeb(mySqlWeb);
            }
        }
    }
}
