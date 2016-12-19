using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;


namespace Lesson3_PracticeExercises
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

            IEnumLayer pLayers;
            pLayers = pMap.Layers;

            ILayer pLayer;
            pLayer = pLayers.Next();

            while (pLayer != null)
            {
                if (pLayer.Name == "U.S. Cities")
                {
                    break; 
                }
                pLayer = pLayers.Next();
            }

            //QI
            IGeoFeatureLayer pGeoFLayer;
            pGeoFLayer = (IGeoFeatureLayer)pLayer;

            ISimpleMarkerSymbol pSym;
            pSym = new SimpleMarkerSymbol();
            pSym.Style = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSX;

            ISimpleRenderer pRenderer;
            pRenderer = new SimpleRenderer();
            pRenderer.Symbol = (ISymbol)pSym;
            pGeoFLayer.Renderer = (IFeatureRenderer)pRenderer;

            IActiveView pActiveView;
            pActiveView = (IActiveView)pMap;
            pActiveView.Refresh();
            pMxDoc.UpdateContents();
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
