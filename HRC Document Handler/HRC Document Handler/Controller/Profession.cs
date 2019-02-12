using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Controller
{
    public class Profession
    {
        string name;
        string email;
        string telephone;
        string project;

        public Profession(string rawEmail)
        {
            if (!rawEmail.Equals(null))
            {
                name = Regex.Split(Regex.Split(rawEmail, "Név: <b>")[1], "</b>")[0]; 
                email = Regex.Split(Regex.Split(rawEmail, "<a href=\"mailto:")[1], "\" style")[0];
                telephone = Regex.Split(Regex.Split(rawEmail, "Telefonszám: ")[1], "<br")[0];
                project = Regex.Split(Regex.Split(rawEmail, "Pozíció/cég: <b>")[1], "</b>")[0];
            }
        }

        protected void Out()
        {
            Console.WriteLine(name);
            Console.WriteLine(email);
            Console.WriteLine(telephone);
            Console.WriteLine(project);
        }

        public string Insert()  //javított
        {
            string id = null;
            Model.MySql mySql = new Model.MySql();
            if (!mySql.bind("SELECT count(id) FROM jeloltek WHERE email='" + email + "'") && !mySql.bind("SELECT count(id) FROM profession_jeloltek WHERE email='" + email + "'"))
            {
                DateTime localDate = DateTime.Now;
                string command = "INSERT INTO profession_jeloltek (`id`, `nev`, `email`, `telefon`, `projekt`, `reg_date`) " +
                "VALUES(NULL, '" + name + "',  '" + email + "', '" + telephone + "', '"+project+"', '"+ localDate.ToString("yyyy.MM.dd") + "');";
                mySql.execute(command);
                id = mySql.SqlSingleQuery("SELECT id FROM profession_jeloltek WHERE email='" + email + "'", "id");
                mySql.dbClose();
            }
            else
            {
                id = mySql.SqlSingleQuery("SELECT id FROM profession_jeloltek WHERE email = '" + email + "'", "id");
                mySql.dbClose();
            }
            return id;
            //command = "SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + items[0].email + "' AND jeloltek.nev = '" + items[0].nev + "'";
        }
        public void SaveDocuments(string path, string fileName, byte[] file)
        {
            Directory.CreateDirectory(path);
            File.WriteAllBytes(path + fileName, file);
        }
    }
}
