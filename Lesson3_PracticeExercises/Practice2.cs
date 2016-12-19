using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;


namespace Lesson3_PracticeExercises
{
    public class Practice2 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice2()
        {
        }

        public void PracticeExercise2()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            IWorkspaceFactory pWSFactory;
            pWSFactory = new ShapefileWorkspaceFactory();

            IWorkspace pWorkspace;
            pWorkspace = pWSFactory.OpenFromFile("c:\\WCGIS\\G5224P\\Lesson1_2_3", ArcMap.Application.hWnd);

            //QI
            IFeatureWorkspace pFWorkspace;
            pFWorkspace = (IFeatureWorkspace)pWorkspace;


            IFeatureClass pStatesFClass;
            pStatesFClass = pFWorkspace.OpenFeatureClass("us_boundaries");

            IFeatureLayer pStatesLayer;
            pStatesLayer = new FeatureLayer();
            pStatesLayer.FeatureClass = pStatesFClass;
            pStatesLayer.Name = "U.S. States";

            pMap.AddLayer(pStatesLayer);

            IFeatureClass pCitiesFClass;
            pCitiesFClass = pFWorkspace.OpenFeatureClass("us_cities");

            IFeatureLayer pCitiesLayer;

            pCitiesLayer = new FeatureLayer();
            pCitiesLayer.FeatureClass = pCitiesFClass;
            pCitiesLayer.Name = "U.S. Cities";

            pMap.AddLayer(pCitiesLayer);

            IActiveView pActiveView;
            pActiveView = (IActiveView)pMap;
            pActiveView.Refresh();

            pMxDoc.UpdateContents();
        }

        protected override void OnClick()
        {
            PracticeExercise2();
        }

        protected override void OnUpdate()
        {
        }
    }
}
