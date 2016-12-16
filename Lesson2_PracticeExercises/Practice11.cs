using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using System.Windows.Forms;


namespace Lesson2_PracticeExercises
{
    public class Practice11 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice11()
        {
        }

        public void PracticeExercise11()
        {
            IEnumPrinterNames pPrinterNames;
            pPrinterNames = (IEnumPrinterNames)ArcMap.Application;
            pPrinterNames.Reset();

            string strName;
            strName = pPrinterNames.Next();

            while (!(string.IsNullOrEmpty(strName)))
            {
                MessageBox.Show(strName);
                strName = pPrinterNames.Next();
            }

        }

        protected override void OnClick()
        {
            PracticeExercise11();
        }

        protected override void OnUpdate()
        {
        }
    }
}
