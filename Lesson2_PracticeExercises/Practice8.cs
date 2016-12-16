using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;


namespace Lesson2_PracticeExercises
{
    public class Practice8 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice8()
        {
        }

        public void PracticeExercise8()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            ILayer pLayer;
            pLayer = pMap.get_Layer(0);

            pLayer.ShowTips = true;
        }

        protected override void OnClick()
        {
            PracticeExercise8();
        }

        protected override void OnUpdate()
        {
        }
    }
}
