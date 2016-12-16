using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace Lesson1_PracticeExercises
{
    public class Practice3 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice3()
        {
        }

        private void PracticeExercise3()
        {
            string x;
            x = Interaction.InputBox("Please enter your name");
            MessageBox.Show("Hi " + x + "!", "Practice 3", MessageBoxButtons.OK);
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

