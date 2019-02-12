using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Model
{
    class DocumentModel
    {
        public string email { get; set; }
        public string document_name { get; set; }
        public byte[] document { get; set; }

        public static List<DocumentModel> GetDocuments(string email)
        {
            List<DocumentModel> list = new List<DocumentModel>();
            MySql mySql = new MySql(true);
            if (mySql.dbOpen() == true)
            {
                string command = "SELECT * FROM csatolmanyok" + (email == "" ? "":" WHERE email ='"+email+"'");
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();

                while (mySql.sdr.Read())
                {
                    list.Add(new DocumentModel
                    {
                        email = mySql.sdr["email"].ToString(),
                        document_name = mySql.sdr["document_name"].ToString(),
                        document = Convert.FromBase64String(Encoding.ASCII.GetString((byte[])mySql.sdr["document"]))
                });
                }
                mySql.sdr.Close();
                mySql.dbClose();
            }
            return list;
        }

        public void deleteDocumentWeb(string documentName)
        {
            MySql mySql = new MySql(true);
            try
            {
                string command = "DELETE FROM csatolmanyok WHERE csatolmanyok.document_name = '" + documentName + "';";
                mySql.execute(command);
            }
            catch (Exception)
            {
                throw;
            }
            mySql.dbClose();
        }
    }
}
