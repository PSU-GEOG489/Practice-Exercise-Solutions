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
    public class SelectionSet : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public SelectionSet()
        {
        }

        public void SelectionSetExample()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            IFeatureLayer pFLayer;
            pFLayer = (IFeatureLayer)pMap.Layer[0];  //** Assuming STATES is the first layer

            IFeatureSelection pFSel;
            pFSel = (IFeatureSelection)pFLayer;

            ISelectionSet pSelSet;
            pSelSet = pFSel.SelectionSet;

            ICursor pCursor;
            pSelSet.Search(null, true, out pCursor);

            IFeatureCursor pFCursor;
            pFCursor = (IFeatureCursor)pCursor;

            IFeature pFeature;
            pFeature = pFCursor.NextFeature();

            int i = 0;
            double lngTotalArea = 0;

            i = 0;
            lngTotalArea = 0;

            IFeatureClass pFClass;
            pFClass = pFLayer.FeatureClass;

            int intAreaIndex = 0;
            intAreaIndex = pFClass.Fields.FindField("Shape_Area");

            while (!(pFeature == null))
            {
                i = i + 1;
                lngTotalArea = lngTotalArea + (double)pFeature.Value[intAreaIndex];

                pFeature = pFCursor.NextFeature();
            }

            MessageBox.Show("There are " + i.ToString() + " selected states." + Environment.NewLine +
                "The total area (decimal degrees) of the states is " + lngTotalArea.ToString() + 
                Environment.NewLine + "and the average area per state is " + (lngTotalArea / i).ToString());
        }

        protected override void OnClick()
        {
            SelectionSetExample();
        }

        protected override void OnUpdate()
        {
        }
    }
}
