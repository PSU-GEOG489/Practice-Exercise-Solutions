using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;


namespace Lesson2_PracticeExercises
{
    public class Practice5 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice5()
        {
        }

        public void PracticeExercise5()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            MessageBox.Show(pMap.MapUnits.ToString());
        }

        protected override void OnClick()
        {
            PracticeExercise5();
        }

        protected override void OnUpdate()
        {
        }
    }
}
