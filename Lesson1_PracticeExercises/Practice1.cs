using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Windows.Forms;

namespace Lesson1_PracticeExercises
{
    public class Practice1 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice1()
        {
        }

        private void PracticeExercise1()
        {
            string x;
            x = "Hello";
            MessageBox.Show(x, "Practice 1", MessageBoxButtons.OK);
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
