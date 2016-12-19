using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;


namespace Lesson4_PracticeExercises
{
    public class Practice4 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice4()
        {
        }

        public void Practice4Exercise()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            ILayer pLayer;
            IEnumLayer pLayers;
            IFeatureLayer pStateLayer = null;
            IFeatureLayer pRoadLayer = null;

            pLayers = pMap.Layers;
            pLayer = pLayers.Next();

            while (pLayer != null)
            {
                if (pLayer.Name == "us_boundaries")
                {
                    pStateLayer = (IFeatureLayer)pLayer;
                }
                else if (pLayer.Name == "us_roads")
                {
                    pRoadLayer = (IFeatureLayer)pLayer;
                }
                pLayer = pLayers.Next();
            }

            IQueryFilter pQueryFilter;
            pQueryFilter = new QueryFilter();
            pQueryFilter.WhereClause = "NAME = 'New York'";

            IFeatureClass pStateFClass;
            pStateFClass = pStateLayer.FeatureClass;

            IFeatureCursor pStateFCursor;
            pStateFCursor = pStateFClass.Search(pQueryFilter, true);

            IFeature pStateFeature;
            pStateFeature = pStateFCursor.NextFeature(); //** Moving to the NY feature
           
            IGeometry pGeom;
            pGeom = pStateFeature.Shape;   //** Getting the NY polygon geometry
        
            ISpatialFilter pSpatialFilter;
            pSpatialFilter = new SpatialFilter();

            pSpatialFilter.Geometry = pGeom;  //** Setting equal to NY shape
            pSpatialFilter.GeometryField = "SHAPE";
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;  //** Getting roads that intersect NY
          
            IFeatureSelection pFSel;
            pFSel = (IFeatureSelection)pRoadLayer;  //** QI
            pFSel.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

            IActiveView pActiveView;
            pActiveView = (IActiveView)pMap;
            pActiveView.Refresh();
        }

        protected override void OnClick()
        {
            Practice4Exercise();
        }

        protected override void OnUpdate()
        {
        }
    }
}
