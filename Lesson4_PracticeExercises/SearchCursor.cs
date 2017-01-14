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
    public class SearchCursor : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public SearchCursor()
        {
        }

        public void CreateSearchCursor()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            IFeatureLayer pFLayer;
            pFLayer = (IFeatureLayer)pMap.Layer[0]; //** Assuming that us_hydro is the first layer

            IFeatureClass pFClass;
            pFClass = pFLayer.FeatureClass;

            IQueryFilter pQueryFilter; //** Creating a new QueryFilter
            pQueryFilter = new QueryFilter();

            string strBasin;
            strBasin = "Mississippi River";

            //** Defining the WhereClause
            pQueryFilter.WhereClause = "NAMEEN = '" + strBasin + "'";

            IFeatureCursor pFCursor;
            pFCursor = pFClass.Search(pQueryFilter, true);

            IFeature pFeature;
            pFeature = pFCursor.NextFeature();  //** Getting the first Feature

            int i = 0; //** will use to count number of line segments in the cursor
            double lngTotalLength = 0;  //** will use to sum the river lengths in the cursor
            i = 0;
            lngTotalLength = 0;

            //** Getting the index pos of the Shape_Leng field
            int intLengthIndex = 0;
            intLengthIndex = pFClass.Fields.FindField("Shape_Leng");

            while (pFeature != null)
            {
                i = i + 1;  //** Incrementing the line segment counter by 1
                //** Getting the river length and adding to total
                lngTotalLength = lngTotalLength + (double)pFeature.Value[intLengthIndex];
                pFeature = pFCursor.NextFeature();
            }

            MessageBox.Show("There are " + i.ToString() + " line segments in the " + strBasin + 
                " river." + Environment.NewLine + "The total length of the river is " + 
                lngTotalLength.ToString() + " meters." + Environment.NewLine + "and the average length per segment is " + 
                (lngTotalLength / i).ToString() + " meters.");
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            CreateSearchCursor();
        }

        protected override void OnUpdate()
        {
        }
    }
}
