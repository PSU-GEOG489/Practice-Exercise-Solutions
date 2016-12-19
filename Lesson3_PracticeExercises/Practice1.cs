using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.VisualBasic;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;


namespace Lesson3_PracticeExercises
{
    public class Practice1 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice1()
        {
        }

        public void PracticeExercise1()
        {
            string strUserInput = null;
            strUserInput = Interaction.InputBox("Enter the data frame to activate");

            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMaps pMaps;
            pMaps = pMxDoc.Maps;

            IMap pMap;
            int i = 0;

            for (i = 0; i <= pMaps.Count - 1; i++)
            {
                pMap = pMaps.Item[i];
                if (pMap.Name == strUserInput)
                {
                    pMxDoc.ActiveView =(IActiveView)pMap;
                    break; 
                }
            }
            pMxDoc.UpdateContents();
        }

        protected override void OnClick()
        {
            PracticeExercise1();
        }

        protected override void OnUpdate()
        {
        }
    }
}
