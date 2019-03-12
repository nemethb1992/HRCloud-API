using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Sandbox
{
    class Seged
    {
        public string email { get; set; }
        public int projekt_id { get; set; }
        public string datum { get; set; }
    }

    class TestClass
    {
        public TestClass()
        {
            string sql = "SELECT email, projekt_id, projekt_jelolt_kapcs.datum FROM projekt_jelolt_kapcs LEFT JOIN jeloltek ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id GROUP BY email;";
            List<Seged> list = getList(sql);
            Insert(list);
        }


        public List<Seged> getList(string command)
        {
            List<Seged> list = new List<Seged>();
            Model.MySql mySqlWeb = new Model.MySql();
            if (mySqlWeb.dbOpen() == true)
            {
                mySqlWeb.cmd = new MySqlCommand(command, mySqlWeb.conn);
                mySqlWeb.sdr = mySqlWeb.cmd.ExecuteReader();

                while (mySqlWeb.sdr.Read())
                {
                    list.Add(new Seged
                    {
                        email = mySqlWeb.sdr["email"].ToString(),
                        projekt_id = Convert.ToInt32(mySqlWeb.sdr["projekt_id"]),
                        datum = mySqlWeb.sdr["datum"].ToString()
                    });
                }
                mySqlWeb.sdr.Close();
                mySqlWeb.dbClose();
            }
            return list;
        }

        public void Insert(List<Seged> list)
        {
            Model.MySql mySqlWeb = new Model.MySql(true);
            foreach (var item in list)
            {
                string command = "UPDATE `regisztraltak` SET `projekt_id`="+item.projekt_id+",`reg_date`='"+item.datum+"' WHERE email = '"+item.email+"'";
                mySqlWeb.execute(command);
            }
        }

    }
}
