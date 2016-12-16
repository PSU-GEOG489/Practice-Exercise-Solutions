using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;


namespace Lesson2_PracticeExercises
{
    public class Practice4 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice4()
        {
        }

        public void PracticeExercise4()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            ILayer pLayer;
            pLayer = pMap.get_Layer(0);

            pLayer.Name = "U.S. Boundaries";
            pMxDoc.UpdateContents();
        }

        protected override void OnClick()
        {
            PracticeExercise4();
        }

        protected override void OnUpdate()
        {
        }
    }
}
