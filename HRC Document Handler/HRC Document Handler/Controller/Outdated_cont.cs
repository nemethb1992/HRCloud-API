using HRC_Document_Handler.Model;
using System;
using System.Collections.Generic;

namespace HRC_Document_Handler.Controller
{
    class Outdated_cont
    {

        DateTime dateTime = DateTime.Now;
        Model.MySql dbEMy = new Model.MySql();
        public List<ModelFullApplicant> JeloltFullDataSource()
        {

            dateTime.AddYears(1);
            string query = "SELECT jeloltek.id,nev,email,telefon,lakhely,pmk_ismerte,szuldatum,neme,tapasztalat_ev, reg_date,felvett,jeloltek.megjegyzes,folderUrl,hirlevel," +
                "coalesce((SELECT nem FROM nemek WHERE nemek.id = jeloltek.neme),'') AS neme," +
                "(SELECT nemek.id FROM nemek WHERE nemek.id = jeloltek.neme) AS id_neme," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor),'') AS munkakor," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor2),'') AS munkakor2," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor3),'') AS munkakor3," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor),0) AS id_munkakor," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor2),0) AS id_munkakor2," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor3),0) AS id_munkakor3," +
                "coalesce((SELECT megnevezes_nyelv FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas),'') AS nyelvtudas," +
                "coalesce((SELECT megnevezes_nyelv FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas2),'') AS nyelvtudas2," +
                "coalesce((SELECT nyelv.id FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas),0) AS id_nyelvtudas," +
                "coalesce((SELECT nyelv.id FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas2),0) AS id_nyelvtudas2," +
                "coalesce((SELECT ertesules_megnevezes FROM ertesulesek WHERE ertesulesek.id = jeloltek.ertesult),'') AS ertesules_megnevezes, " +
                "coalesce((SELECT ertesulesek.id FROM ertesulesek WHERE ertesulesek.id = jeloltek.ertesult),0) AS id_ertesult, " +
                "coalesce((SELECT megnevezes_vegzettseg FROM vegzettsegek WHERE vegzettsegek.id = jeloltek.vegz_terulet),'') AS vegz_terulet, " +
                "coalesce((SELECT vegzettsegek.id FROM vegzettsegek WHERE vegzettsegek.id = jeloltek.vegz_terulet),0) AS id_vegz_terulet " +
                "FROM jeloltek WHERE reg_date ";
            //string query = "SELECT * FROM jeloltek INNER JOIN nyelv on jeloltek.nyelvtudas = nyelv.id INNER JOIN munkakor on jeloltek.munkakor = munkakor.id INNER JOIN ertesulesek ON jeloltek.ertesult = ertesulesek.id WHERE jeloltek.id = " + ApplicantID + "";
            List<ModelFullApplicant> data = dbEMy.JeloltExtended_MySql_listQuery(query);
            dbEMy.dbClose();
            return data;
        }
    }
}
