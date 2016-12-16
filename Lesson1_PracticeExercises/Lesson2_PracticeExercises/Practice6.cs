using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;


namespace Lesson2_PracticeExercises
{
    public class Practice6 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice6()
        {
        }

        public void PracticeExercise6()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            MessageBox.Show(pMap.SelectionCount.ToString());
        }

        protected override void OnClick()
        {
            PracticeExercise6();
        }

        protected override void OnUpdate()
        {
        }
    }
}
