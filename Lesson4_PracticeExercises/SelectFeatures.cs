using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;


namespace Lesson4_PracticeExercises
{
    public class SelectFeatures : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public SelectFeatures()
        {
        }


        public void SelectFeaturesExample()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            IFeatureLayer pFLayer;
            pFLayer = (IFeatureLayer)pMap.Layer[0];  //** Assuming STATES is the first layer

            IQueryFilter pQueryFilter;
            pQueryFilter = new QueryFilter();

            string strState;
            strState = "Alaska";
            pQueryFilter.WhereClause = "NAME = '" + strState + "'";

            IFeatureSelection pFSel;
            pFSel = (IFeatureSelection)pFLayer;  //** QI

            pFSel.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

            IActiveView pActiveView;
            pActiveView = (IActiveView)pMxDoc.FocusMap;
            pActiveView.Refresh();
        }


        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            SelectFeaturesExample();
        }

        protected override void OnUpdate()
        {
        }
    }
}
