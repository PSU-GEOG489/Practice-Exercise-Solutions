using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;


namespace Lesson2_PracticeExercises
{
    public class Practice7 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice7()
        {
        }

        public void PracticeExercise7()
        {
            IMxDocument pMxDoc;
            pMxDoc= (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            ILayer pLayer;
            pLayer = pMap.get_Layer(0);

            MessageBox.Show(pLayer.ShowTips.ToString());
        }

        protected override void OnClick()
        {
            PracticeExercise7();
        }

        protected override void OnUpdate()
        {
        }
    }
}
