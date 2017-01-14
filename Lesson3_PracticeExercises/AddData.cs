using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;


namespace Lesson3_PracticeExercises
{
    public class AddData : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public AddData()
        {
        }

        public void Add_AccessData()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            IWorkspaceFactory pWSFactory;
            pWSFactory = new AccessWorkspaceFactory();

            IWorkspace pWorkspace;
            pWorkspace = pWSFactory.OpenFromFile("C:/wcgis/geog484/Lesson1/data/Lesson1.mdb", ArcMap.Application.hWnd);

            IFeatureWorkspace pFWorkspace;
            pFWorkspace = (IFeatureWorkspace)pWorkspace;

            IFeatureClass pFClass;
            pFClass = pFWorkspace.OpenFeatureClass("Roads");

            IFeatureLayer pFLayer;
            pFLayer = new FeatureLayer();
            pFLayer.FeatureClass = pFClass;
            pMap.AddLayer(pFLayer);

            var _with1 = pFLayer;
            _with1.Name = "Roads";
            _with1.ShowTips = true;

            pMxDoc.UpdateContents();

            IActiveView pActiveView;
            pActiveView = (IActiveView)pMap;
            pActiveView.Refresh();

        }


        public void Add_CoverageData()
        {

            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            IWorkspaceFactory pWSFactory;
            pWSFactory = new ArcInfoWorkspaceFactory();

            IWorkspace pWorkspace;
            pWorkspace = pWSFactory.OpenFromFile("C:/wcgis/geog483/Lesson5/data/", ArcMap.Application.hWnd);

            IFeatureWorkspace pFWorkspace;
            pFWorkspace = (IFeatureWorkspace)pWorkspace;

            IFeatureClass pFClass;
            pFClass = pFWorkspace.OpenFeatureClass("wdzoning:polygon");

            IFeatureLayer pFLayer;
            pFLayer = new FeatureLayer();
            pFLayer.FeatureClass = pFClass;
            pMap.AddLayer(pFLayer);

            var _with2 = pFLayer;
            _with2.Name = "Zoning";
            _with2.ShowTips = true;

            pMxDoc.UpdateContents();

            IActiveView pActiveView;
            pActiveView = (IActiveView)pMap;
            pActiveView.Refresh();

        }


        public void Add_Raster()
        {

            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            IRasterLayer pRLayer;
            pRLayer = new RasterLayer();

            // ** Can also go through a process similar to that of a feature layer,
            // ** but this is a nice shortcut!
            pRLayer.CreateFromFilePath("C:/wcgis/geog484/Lesson1/data/cobhamclip.tif");
            pRLayer.Name = "Cobham DRG";
            pMap.AddLayer(pRLayer);

            pMxDoc.UpdateContents();

            IActiveView pActiveView;
            pActiveView = (IActiveView)pMap;
            pActiveView.Refresh();

        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            Add_AccessData();
            Add_CoverageData();
            Add_Raster();
        }

        protected override void OnUpdate()
        {
        }
    }
}
