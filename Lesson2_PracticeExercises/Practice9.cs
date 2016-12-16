using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;


namespace Lesson2_PracticeExercises
{
    public class Practice9 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice9()
        {
        }

        public void PracticeExercise9()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            ILayer pLayer;
            pLayer = pMap.get_Layer(0);

            ILayerEffects pLayerEffects;
            pLayerEffects = (ILayerEffects)pLayer;
            pLayerEffects.Transparency = 50;

            IActiveView pActiveView;
            pActiveView = (IActiveView)pMap;
            pActiveView.Refresh();
        }

        protected override void OnClick()
        {
            PracticeExercise9();
        }

        protected override void OnUpdate()
        {
        }
    }
}
