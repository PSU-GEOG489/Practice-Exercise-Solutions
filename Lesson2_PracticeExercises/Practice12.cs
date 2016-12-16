using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.VisualBasic;
using System.Windows.Forms;


namespace Lesson2_PracticeExercises
{
    public class Practice12 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice12()
        {
        }

        public int Square(int intNum)
        {
            //return intNum ^ 2;
            return intNum * intNum;
        }

        public void PracticeExercise12()
        {
            int intUserNum;
            intUserNum = int.Parse(Interaction.InputBox("Enter a number:"));
            int intUserNumSquared;
            intUserNumSquared = Square(intUserNum);

            MessageBox.Show("That number squared is " + intUserNumSquared.ToString());           
        }

        protected override void OnClick()
        {
            PracticeExercise12();
        }

        protected override void OnUpdate()
        {
        }
    }
}
