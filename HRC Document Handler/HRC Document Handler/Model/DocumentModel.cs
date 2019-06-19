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
        public int jelolt_id { get; set; }
        public string document_name { get; set; }
        public byte[] document { get; set; }

        public static List<DocumentModel> GetDocuments(int id)
        {
            List<DocumentModel> list = new List<DocumentModel>();
            MySql mySql = new MySql(true);
            if (mySql.dbOpen() == true)
            {
                string command = "SELECT * FROM csatolmanyok" + (id == null ? "":" WHERE jelolt_id ="+id+"");
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();

                while (mySql.sdr.Read())
                {
                    list.Add(new DocumentModel
                    {
                        jelolt_id = Convert.ToInt32(mySql.sdr["jelolt_id"]),
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

        public void deleteDocumentWeb(int webid)
        {
            MySql mySql = new MySql(true);
            try
            {
                string command = "DELETE FROM csatolmanyok WHERE csatolmanyok.jelolt_id = " + webid + ";";
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
