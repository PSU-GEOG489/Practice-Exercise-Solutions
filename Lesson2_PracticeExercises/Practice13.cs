using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;

namespace Lesson2_PracticeExercises
{
    public class Practice13 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice13()
        {
        }

        public void SetColor(ILayer pLayer, string strColor)
        {
            IRgbColor pRGBColor;
            pRGBColor = new RgbColor();

            if (strColor == "red")
            {
                pRGBColor.Red = 255;
                pRGBColor.Green = 0;
                pRGBColor.Blue = 0;
            }
            else if (strColor == "green")
            {
                pRGBColor.Red = 0;
                pRGBColor.Green = 255;
                pRGBColor.Blue = 0;
            }
            else if (strColor == "blue")
            {
                pRGBColor.Red = 0;
                pRGBColor.Green = 0;
                pRGBColor.Blue = 255;
            }

            IFeatureLayer2 pFLayer;
            pFLayer = (IFeatureLayer2)pLayer;
            IGeoFeatureLayer pGeoFLayer;
            pGeoFLayer = (IGeoFeatureLayer)pFLayer;
            ISimpleRenderer pRenderer;
            pRenderer = (ISimpleRenderer)pGeoFLayer.Renderer;

            if (pFLayer.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
            {
                ISimpleMarkerSymbol pMarkerSym;
                pMarkerSym = new SimpleMarkerSymbol();
                pMarkerSym.Color = pRGBColor;
                pRenderer.Symbol = (ISymbol)pMarkerSym;
            }
            else if (pFLayer.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline)
            {
                ISimpleLineSymbol pLineSym;
                pLineSym = new SimpleLineSymbol();
                pLineSym.Color = pRGBColor;
                pRenderer.Symbol = (ISymbol)pLineSym;
            }
            else if (pFLayer.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
            {
                ISimpleFillSymbol pFillSym;
                pFillSym = new SimpleFillSymbol();
                pFillSym.Color = pRGBColor;
                pRenderer.Symbol = (ISymbol)pFillSym;
            }

            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;
            pMxDoc.UpdateContents();
            pMxDoc.ActiveView.Refresh();
        }


        public void PracticeExercise13()
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
                if (pLayer.Name == "us_cities")
                {
                    SetColor(pLayer, "red");
                }
                else if (pLayer.Name == "us_roads")
                {
                    SetColor(pLayer, "green");
                }
                else if (pLayer.Name == "us_boundaries")
                {
                    SetColor(pLayer, "blue");
                }
                pLayer = pLayers.Next();
            }

        }

        protected override void OnClick()
        {
            PracticeExercise13();
        }

        protected override void OnUpdate()
        {
        }
    }
}
