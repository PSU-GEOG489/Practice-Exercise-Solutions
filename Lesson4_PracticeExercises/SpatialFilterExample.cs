using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;


namespace Lesson4_PracticeExercises
{
    public class SpatialFilterExample : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public SpatialFilterExample()
        {
        }

        public void CreateSpatialFilter()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            IFeatureLayer pCityLayer;
            pCityLayer = (IFeatureLayer)pMap.Layer[0];  //** Assuming first layer is us_cities

            IFeatureLayer pStateLayer;
            pStateLayer = (IFeatureLayer)pMap.Layer[1];  //** Assuming second layer is STATES

            IFeatureClass pStateFClass;
            pStateFClass = pStateLayer.FeatureClass;

            IQueryFilter pQueryFilter;
            pQueryFilter = new QueryFilter();
            pQueryFilter.WhereClause = "NAME = 'Pennsylvania'";

            IFeatureCursor pStateFCursor;
            pStateFCursor = pStateFClass.Search(pQueryFilter, true);

            IFeature pStateFeature;
            pStateFeature = pStateFCursor.NextFeature();  //** Moving to the PA feature

            IGeometry pGeom;
            pGeom = pStateFeature.Shape;  //** Getting the PA polygon geometry

            ISpatialFilter pSpatialFilter;
            pSpatialFilter = new SpatialFilter();

            pSpatialFilter.Geometry = pGeom;  //** Setting equal to PA shape
            pSpatialFilter.GeometryField = "SHAPE";
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;  //** Getting features contained by PA shape

            IFeatureClass pCityFClass;
            pCityFClass = pCityLayer.FeatureClass;

            IFeatureCursor pCityFCursor;
            pCityFCursor = pCityFClass.Search(pSpatialFilter, true);

            int intPopclassIndex = 0;  //** Field index for population class
            int intCityNameIndex = 0;  //** Field index for the name of the city

            intPopclassIndex = pCityFClass.Fields.FindField("POPCLASS");
            intCityNameIndex = pCityFClass.Fields.FindField("NAME");

            IFeature pCityFeature;
            pCityFeature = pCityFCursor.NextFeature();

            while (pCityFeature != null)
            {
                if ((double)pCityFeature.Value[intPopclassIndex] >= 3)
                {
                    //** If a city is big, report that
                    MessageBox.Show((string)pCityFeature.Value[intCityNameIndex] + " is a big city.");
                }
                pCityFeature = pCityFCursor.NextFeature();
            }
        }

        protected override void OnClick()
        {
            CreateSpatialFilter();
        }

        protected override void OnUpdate()
        {
        }
    }
}
