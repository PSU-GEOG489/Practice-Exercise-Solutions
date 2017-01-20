﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ESRI.ArcGIS.ArcMapUI;
using System.Windows.Forms;


namespace Lesson1_PracticeExercises
{
    public class Practice4 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice4()
        {
        }

        private void PracticeExercise4()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;
            int intSearchTol;
            intSearchTol = pMxDoc.SearchTolerancePixels;

            MessageBox.Show(intSearchTol.ToString());
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            PracticeExercise4();
        }

        protected override void OnUpdate()
        {
        }
    }
}
