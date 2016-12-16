using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.VisualBasic;
using System.Windows.Forms;


namespace Lesson2_PracticeExercises
{
    public class Practice10 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice10()
        {
        }

        public void PracticeExercise10()
        {
            int intScore = 0;
            intScore = int.Parse(Interaction.InputBox("Please enter the student's score out of 100"));

            string strGrade = null;

            if (intScore > 89)
            {
                strGrade = "A";
            }
            else if (intScore > 79)
            {
                strGrade = "B";
            }
            else if (intScore > 69)
            {
                strGrade = "C";
            }
            else if (intScore > 59)
            {
                strGrade = "D";
            }
            else
            {
                strGrade = "F";
            }

            MessageBox.Show("The student's letter grade is " + strGrade);
        }

        protected override void OnClick()
        {
            PracticeExercise10();
        }

        protected override void OnUpdate()
        {
        }
    }
}
