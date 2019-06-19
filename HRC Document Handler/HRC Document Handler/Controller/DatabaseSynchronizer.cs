using HRC_Document_Handler.Model;
using HRC_Document_Handler.Model.StatisticModel;
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
        private MySql mySql;
        private MySql mySqlWeb;
        public DatabaseSynchronizer()
        {
            //try
            //{
                mySql = new MySql();
                mySqlWeb = new MySql(true);
                appURL = mySql.ApplicantURL().url;
                if (Utility.hasWriteAccessToFolder(appURL))
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
                if (Applicant.isExists(applicant.email) == 0 || applicant.kategoria == 3)
                {
                    int applicantID = applicant.Insert();
                    if (applicantID != 0)
                    {

                        if (applicant.kategoria == 3)
                        {
                            ProjectConnectionModel.insertDb_kulsos(applicant, applicantID);
                        }
                        else
                        {
                            List<ProjectConnectionModel> connectedProjects = ProjectConnectionModel.getListWeb("SELECT * FROM projekt_jelolt_kapcs WHERE jelolt_id = " + applicant.id + "");
                            foreach (var projects in connectedProjects)
                            {
                                ProjectConnectionModel.insertDb(projects, applicantID);

                            }
                        }
                        applicant.deleteWeb(applicant.id);
                        List<DocumentModel> docList = DocumentModel.GetDocuments(applicant.id);
                        string path = appURL + applicantID.ToString() + "\\";
                        foreach (var doc in docList)
                        {
                            Applicant.SaveDocument(path, doc.document_name, doc.document);
                            doc.deleteDocumentWeb(applicant.id);
                        }
                        //TODO: email kiküldése

                    }
                }
                else
                {
                    int applicantID = applicant.Update(applicant.email);

                    List<ProjectConnectionModel> connectedProjects = ProjectConnectionModel.getListWeb("SELECT * FROM projekt_jelolt_kapcs WHERE jelolt_id = " + applicant.id + "");
                    foreach (var projects in connectedProjects)
                    {
                        ProjectConnectionModel.insertDb(projects, applicantID);
                    }
                    //TODO: email kiküldése
                    applicant.deleteWeb(applicant.id);
                    List<DocumentModel> docList = DocumentModel.GetDocuments(applicant.id);
                    string path = appURL + applicantID.ToString() + "\\";
                    foreach (var doc in docList)
                    {
                        Applicant.SaveDocument(path, doc.document_name, doc.document);
                        doc.deleteDocumentWeb(applicant.id);
                    }
                }
            }
        }

        private void synchronizeResources()
        {
            List<ModelProjektek> projektList = ModelProjektek.getProjektek("SELECT id, megnevezes_projekt, statusz, fel_datum FROM projektek WHERE statusz=1");
            Utility.deleteWebTable("projektek");
            foreach (var item in projektList)
            {
                item.insertWeb(mySqlWeb);
            }

            List<ModelErtesulesek> ertesulesekList = ModelErtesulesek.getErtesulesek("SELECT id, ertesules_megnevezes FROM ertesulesek");
            Utility.deleteWebTable("ertesulesek");
            foreach (var item in ertesulesekList)
            {
                item.insertWeb(mySqlWeb);
            }

            List<ModelNyelv> nyelvList = ModelNyelv.getNyelv("SELECT id, megnevezes_nyelv FROM nyelv");
            Utility.deleteWebTable("nyelv");
            foreach (var item in nyelvList)
            {
                item.insertWeb(mySqlWeb);
            }

            List<ModelVegzettseg> vegzettsegList = ModelVegzettseg.getVegzettsegek("SELECT id, megnevezes_vegzettseg FROM vegzettsegek");
            Utility.deleteWebTable("vegzettsegek");
            foreach (var item in vegzettsegList)
            {
                item.insertWeb(mySqlWeb);
            }

            List<ModelFreelancerList> freelancerList = ModelFreelancerList.getFreelancerList("SELECT * FROM freelancer_list");
            Utility.deleteWebTable("freelancer_list");
            foreach (var item in freelancerList)
            {
                item.insertWeb(mySqlWeb);
            }

            List<ModelJelentkezesek> jelentkezesekList = ModelJelentkezesek.getJelentkezesekWeb("SELECT * FROM jelentkezesek");
            Utility.deleteTable("jelentkezesek");
            foreach (var item in jelentkezesekList)
            {
                item.Insert(mySql);
            }
        }
    }
}
