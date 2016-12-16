using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Windows.Forms;

namespace Lesson1_PracticeExercises
{
    public class Practice2 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice2()
        {
        }

        private void PracticeExercise2()
        {
            int x;
            x = 9;
            int y;
            y = 7;
            int z;
            z = x + y;

            MessageBox.Show(z.ToString(), "Practice 2", MessageBoxButtons.OK);
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
