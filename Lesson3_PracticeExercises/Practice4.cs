using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;


namespace Lesson3_PracticeExercises
{
    public class Practice4 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice4()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
        }

        protected override void OnUpdate()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            Enabled = false;

            if (pMap.LayerCount > 0)
            {
                IEnumLayer pLayers;
                pLayers = pMap.Layers;

                ILayer pLayer;
                pLayer = pLayers.Next();

                while (pLayer != null)
                {
                    if (pLayer is IFeatureLayer)
                    {
                        Enabled = true;
                        break;
                    }
                    pLayer = pLayers.Next();
                }
            }
        }
    }
}
