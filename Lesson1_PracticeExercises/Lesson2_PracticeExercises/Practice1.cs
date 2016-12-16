using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lesson2_PracticeExercises
{
    public class Practice1 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice1()
        {
        }

        public void PracticeExercise1()
        {
            ArcMap.Application.Caption = "We Are Penn State!";
        }

        protected override void OnClick()
        {
            PracticeExercise1();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
