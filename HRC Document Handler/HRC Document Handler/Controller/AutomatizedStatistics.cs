using HRC_Document_Handler.Enum;
using HRC_Document_Handler.Model.StatisticModel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRC_Document_Handler.Controller
{
    class AutomatizedStatistics
    {
        private Workbook wb;
        private Worksheet ws;
        private Application excel;

        private string statUrl;
        private MySql mySql;
        public AutomatizedStatistics()
        {
            mySql = new MySql();
            statUrl = mySql.StatisticURL().url;
            if (Utility.hasWriteAccessToFolder(statUrl))
            {
                 if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && DateTime.Now.Hour >= 22)
                 {
                 JelentkezokEloszlasaGenerate();
                }
            }
            
            mySql.dbClose();

        }


        public void JelentkezokEloszlasaGenerate()
        {
            DateTime to = DateTime.Today;
            if (StatisticStampModel.AlreadyTaken(to,(int)StatType.JeloltEloszlas))
            {
                return;
            }
            excel = new Application();
            wb = (excel.Workbooks.Add());
            ws = (Worksheet)wb.ActiveSheet;


            DateTime from = to.AddDays(-7);

            string toStr = to.Year + "." + Utility.DateCorrect(to.Month) + "." + Utility.DateCorrect(to.Day) + ".";
            string fromStr = from.Year + "." + Utility.DateCorrect(from.Month) + "." + Utility.DateCorrect(from.Day) + ".";

            List<JelentkezoEloszlasModel> list = JelentkezoEloszlasModel.GetByProjekt(from, to);
            //Jelentkezések eloszlása
            ws.Cells[1, 1].Value = "Időszak";
            ws.Cells[2, 1].Value = fromStr + " - " + toStr;
            int summed = 0;
            int actualColumn = 1;
            foreach (var item in list)
            {
                if (item.projekt_megnevezes != "")
                {
                    ws.Cells[1, actualColumn + 1] = item.projekt_megnevezes;
                    ws.Cells[2, actualColumn + 1] = item.darab;
                    summed += item.darab;
                    actualColumn++;
                }
            }
            //Összegzés
            ws.Cells[1, actualColumn + 1] = "Összesen";
            ws.Cells[2, actualColumn + 1] = summed;
            actualColumn += 2;

            //Jelentkezés típusok meghatározása (Profession vagy weboldal)
            List<ModelJelentkezesek> listForType = ModelJelentkezesek.getJelentkezesekInner(from, to, "SELECT * FROM jelentkezesek");
            
            int type_profession = 0;
            int type_webform = 0;
            foreach (var item in listForType)
            {
                if (item.profession_type == 1)
                {
                    type_profession++;
                }
                else if(item.profession_type == 0)
                {
                    type_webform++;
                }
            }

            ws.Cells[1, actualColumn + 1] = "Profession";
            ws.Cells[2, actualColumn + 1] = type_profession;
            actualColumn++;
            ws.Cells[1, actualColumn + 1] = "Weblap";
            ws.Cells[2, actualColumn + 1] = type_webform;
            actualColumn++;

            ws.Columns.AutoFit();

            ws.SaveAs(statUrl + "Systematic\\JeloltEloszlas\\JeloltekStatisztika " + fromStr + " -" + toStr + ".xlsx");
            wb.Close();
            excel.Quit();
            excel.CheckAbort(excel);
            Utility.AbortExcel();

            mySql.execute("INSERT INTO statistic_stamp (name, type, date) VALUES('JeloltekStatisztika " + fromStr + "-" + toStr + ".xlsx',"+(int)StatType.JeloltEloszlas+",'"+ to.Year + "." + Utility.DateCorrect(to.Month) + "." + Utility.DateCorrect(to.Day) + "')");
            mySql.dbClose();
        }

    }
}
