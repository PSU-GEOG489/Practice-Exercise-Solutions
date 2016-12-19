using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;


namespace Lesson4_PracticeExercises
{
    public class Practice3 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice3()
        {
        }

        public void Practice3Exercise()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            IEnumLayer pLayers;
            pLayers = pMap.Layers;

            ILayer pLayer;
            pLayer = pLayers.Next();

            IFeatureLayer pCityLayer = null;

            while (pLayer != null)
            {
                if (pLayer.Name == "us_cities")
                {
                    pCityLayer = (IFeatureLayer)pLayer;
                    break; 
                }
                pLayer = pLayers.Next();
            }

            IQueryFilter pQueryFilter;
            pQueryFilter = new QueryFilter();
            pQueryFilter.WhereClause = "POPCLASS >= 4";

            IFeatureClass pCityFClass;
            pCityFClass = pCityLayer.FeatureClass;

            int intNameField = 0;
            intNameField = pCityFClass.FindField("NAME");

            IFeatureCursor pFCursor;
            pFCursor = pCityFClass.Search(pQueryFilter, true);

            IFeature pFeature;
            pFeature = pFCursor.NextFeature();

            string strList = null;
            strList = "";

            while (pFeature != null)
            {
                strList = strList + pFeature.Value[intNameField] + Environment.NewLine;
                pFeature = pFCursor.NextFeature();
            }

            MessageBox.Show("The following cities are in population class 4 or 5:" + Environment.NewLine + strList);
        }

        protected override void OnClick()
        {
            Practice3Exercise();
        }

        protected override void OnUpdate()
        {
        }
    }
}
