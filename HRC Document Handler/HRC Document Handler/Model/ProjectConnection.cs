using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Model
{
    class ProjectConnection
    {
        protected int project_id { get; set; }
        public string project_name { get; set; }
        public string email { get; set; }
        public string date { get; set; }

        public ProjectConnection(string project_name)
        {
            this.project_name = project_name;
            this.project_id = getProjectId(project_name);
        }

        public void Insert(int applicantID)
        {
            MySql mySql = new MySql();
            string command = "SELECT count(`projekt_jelolt_kapcs`.`id`) as count FROM `pmkcvtest`.`projekt_jelolt_kapcs` WHERE `projekt_id` = " + project_id + " AND `jelolt_id` = " + applicantID + "";
            string result = mySql.SqlSingleQuery(command, "count");
            if (result == "0")
            {
                mySql.execute(@"INSERT INTO `projekt_jelolt_kapcs` (`projekt_id`, `jelolt_id`,`hr_id`, `allapota`, `datum`) VALUES (" + project_id + "," + applicantID + ",default,default,'" + date + "') ");
            }
            mySql.execute(command);
            mySql.dbClose();
        }

        protected int getProjectId(string project_name)
        {
            MySql mySql = new MySql();
            string value = mySql.SqlSingleQuery("SELECT id FROM projektek WHERE megnevezes_projekt LIKE '%"+ project_name + "%' AND statusz = 1","id");
            mySql.dbClose();
            return Convert.ToInt32(value);
        }
    }
}
