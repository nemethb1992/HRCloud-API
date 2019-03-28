using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Model
{
    public class ModelFullApplicant : ModelWebApplicant
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string email { get; set; }
        public string telefon { get; set; }
        public string lakhely { get; set; }
        public string ertesult { get; set; }
        public int id_ertesult { get; set; }
        public int szuldatum { get; set; }
        public string neme { get; set; }
        public int id_neme { get; set; }
        public int tapasztalat_ev { get; set; }
        public string munkakor { get; set; }
        public string munkakor2 { get; set; }
        public string munkakor3 { get; set; }
        public int id_munkakor { get; set; }
        public int id_munkakor2 { get; set; }
        public int id_munkakor3 { get; set; }
        public string vegz_terulet { get; set; }
        public int id_vegz_terulet { get; set; }
        public string nyelvtudas { get; set; }
        public string nyelvtudas2 { get; set; }
        public int id_nyelvtudas { get; set; }
        public int id_nyelvtudas2 { get; set; }
        public string reg_date { get; set; }
        public string megjegyzes { get; set; }
        public string folderUrl { get; set; }
        public int statusz { get; set; }
        public int hirlevel { get; set; }
        public int profession_type { get; set; }
        public string project { get; set; }

        public int Insert()  //javított
        {
            MySql mySql = new MySql();
  
            int applicantID = 0;
            try
            {
                string command = "INSERT INTO jeloltek (`id`, `nev`, `email`, `telefon`, `lakhely`, `ertesult`, `szuldatum`, `neme`, `tapasztalat_ev`, `munkakor`, `munkakor2`, `munkakor3`, `vegz_terulet`, `nyelvtudas`,`nyelvtudas2`, `reg_date`, `hirlevel`, `megjegyzes`, `profession_type`) " +
                "VALUES(NULL, '" +
                nev + "',  '" +
                email + "', '" +
                telefon + "', '" +
                lakhely + "', " +
                Utils.Utils.correction(id_ertesult.ToString()) + ", " +
                Utils.Utils.correction(szuldatum.ToString()) + ", " +
                Utils.Utils.correction(id_neme.ToString()) + "," +
                Utils.Utils.correction(tapasztalat_ev.ToString()) + "," +
                Utils.Utils.correction(id_munkakor.ToString()) + "," +
                Utils.Utils.correction(id_munkakor2.ToString()) + "," +
                Utils.Utils.correction(id_munkakor3.ToString()) + "," +
                Utils.Utils.correction(id_vegz_terulet.ToString()) + "," +
                Utils.Utils.correction(id_nyelvtudas.ToString()) + "," +
                Utils.Utils.correction(id_nyelvtudas2.ToString()) + ",'" +
                Utils.Utils.correction(reg_date.ToString(), "") + "'," +
                Utils.Utils.correction(hirlevel.ToString()) + ",'" +
                Utils.Utils.correction(megjegyzes.ToString(), "") + "'," +
                Utils.Utils.correction(profession_type.ToString()) + ");";
                mySql.execute(command);
                command = "SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + email + "' AND jeloltek.nev = '" + nev + "'";
                applicantID = Convert.ToInt16(mySql.uniqueList(command, "jeloltek", 1)[0]);
            }
            catch (Exception)
            {
                throw;
            }
            mySql.dbClose();
            return applicantID;
        }

        public int Update(string email)  //javított
        {
            MySql mySql = new MySql();

            int applicantID = 0;
            try
            {
                string command = "UPDATE `pmkcvtest`.`jeloltek` SET "+
                 "`nev` = '" + nev + "', "+
                 "`email` = '" + email + "', "+
                 "`telefon` = '" + telefon + "', "+ 
                 "`lakhely` = '" + lakhely + @"', "+
                 "`ertesult` = " + Utils.Utils.correction(id_ertesult.ToString()) + ", " +
                 "`szuldatum` = " + Utils.Utils.correction(szuldatum.ToString()) + ", " +
                 "`neme` = " + Utils.Utils.correction(id_neme.ToString()) + ", " +
                 "`tapasztalat_ev` = " + Utils.Utils.correction(tapasztalat_ev.ToString()) + ", " +
                 "`munkakor` = " + Utils.Utils.correction(id_munkakor.ToString()) + ", " +
                 "`munkakor2` = " + Utils.Utils.correction(id_munkakor2.ToString()) + ", " +
                 "`munkakor3` = " + Utils.Utils.correction(id_munkakor3.ToString()) + ", " +
                 "`vegz_terulet` = " + Utils.Utils.correction(id_vegz_terulet.ToString()) + ", " +
                 "`nyelvtudas` = " + Utils.Utils.correction(id_nyelvtudas.ToString()) + ", " +
                 "`nyelvtudas2` = " + Utils.Utils.correction(id_nyelvtudas2.ToString()) + ", " +
                 "`reg_date` = '" + Utils.Utils.correction(reg_date.ToString(), "") + "', " +
                 "`megjegyzes` = '" + Utils.Utils.correction(megjegyzes.ToString(), "") + "', " +
                 "`hirlevel` = " + Utils.Utils.correction(hirlevel.ToString()) + ", " +
                 "`statusz` = 1, "+
                 "`friss` = 1 " +
                 "WHERE `email` = '" + email + "' ;";

                mySql.execute(command);
                command = "SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + email + "'";
                applicantID = Convert.ToInt16(mySql.uniqueList(command, "jeloltek", 1)[0]);

            }
            catch (Exception)
            {
                throw;
            }
            mySql.dbClose();
            return applicantID;
        }

    }

    public class ModelWebApplicant
    {

        public static List<ModelFullApplicant> getList(string command)
        {
            List<ModelFullApplicant> list = new List<ModelFullApplicant>();
            MySql mySqlWeb = new MySql(true);
            if (mySqlWeb.dbOpen() == true)
            {
                mySqlWeb.cmd = new MySqlCommand(command, mySqlWeb.conn);
                mySqlWeb.sdr = mySqlWeb.cmd.ExecuteReader();

                while (mySqlWeb.sdr.Read())
                {
                    list.Add(new ModelFullApplicant
                    {
                        nev = mySqlWeb.sdr["nev"].ToString(),
                        email = mySqlWeb.sdr["email"].ToString(),
                        telefon = mySqlWeb.sdr["telefon"].ToString(),
                        lakhely = mySqlWeb.sdr["lakhely"].ToString(),
                        id_ertesult = Convert.ToInt32(mySqlWeb.sdr["ertesult"]),
                        //pmk_ismerte = Convert.ToInt32(mySql.sdr["pmk_ismerte"]),
                        szuldatum = Convert.ToInt32(mySqlWeb.sdr["szuldatum"]),
                        id_neme = Convert.ToInt32(mySqlWeb.sdr["neme"]),
                        tapasztalat_ev = Convert.ToInt32(mySqlWeb.sdr["tapasztalat_ev"]),
                        id_vegz_terulet = Convert.ToInt32(mySqlWeb.sdr["vegz_terulet"]),
                        id_nyelvtudas = Convert.ToInt32(mySqlWeb.sdr["nyelvtudas"]),
                        reg_date = mySqlWeb.sdr["reg_date"].ToString(),
                        megjegyzes = mySqlWeb.sdr["megjegyzes"].ToString(),
                        hirlevel = Convert.ToInt32(mySqlWeb.sdr["hirlevel"]),
                        profession_type = Convert.ToInt32(mySqlWeb.sdr["profession_type"])
                    });
                }
                mySqlWeb.sdr.Close();
                mySqlWeb.dbClose();
            }
            return list;
        }

        public void deleteWeb(string email)
        {
            MySql mySql = new MySql(true);
            try
            {
                string command = "DELETE FROM jeloltek WHERE jeloltek.email = '" + email + "';";
                mySql.execute(command);
                command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.email = '" + email + "';";
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
