using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;

namespace Lesson2_PracticeExercises
{
    public class Practice3 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice3()
        {
        }

        public void PracticeExercise3()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            string strName;
            long lngNumLayers;

            strName = pMap.Name;
            lngNumLayers = pMap.LayerCount;

            MessageBox.Show("The name of the active data frame is " + strName + System.Environment.NewLine + "It has " + lngNumLayers.ToString() + " layers");
        }

        protected override void OnClick()
        {
            PracticeExercise3();
        }

        protected override void OnUpdate()
        {
        }
    }
}
