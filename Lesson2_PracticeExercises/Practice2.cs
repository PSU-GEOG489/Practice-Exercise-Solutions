using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Carto;

namespace Lesson2_PracticeExercises
{
    public class Practice2 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice2()
        {
        }

        public void PracticeExercise2()
        {
            IDocumentInfo pDocumentInfo;
            pDocumentInfo = (IDocumentInfo)ArcMap.Application.Document;
            pDocumentInfo.Author = "Andrew Murdoch";
        }

        protected override void OnClick()
        {
            PracticeExercise2();
        }

        protected override void OnUpdate()
        {
        }
    }
}
