using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace Lesson2_PracticeExercises
{
    public class Practice14 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice14()
        {
        }

        public double CToF(double dblCel)
        {
            return (9 / 5 * dblCel) + 32;
        }

        public void PracticeExercise14()
        {
            double dblC1 = 0;
            dblC1 = double.Parse(Interaction.InputBox("Please enter the 3AM temp in Celsius."));

            double dblF1 = 0;
            dblF1 = CToF(dblC1);

            double dblC2 = 0;
            dblC2 = double.Parse(Interaction.InputBox("Please enter the 9AM temp in Celsius."));

            double dblF2 = 0;
            dblF2 = CToF(dblC2);

            double dblC3 = 0;
            dblC3 = double.Parse(Interaction.InputBox("Please enter the 3PM temp in Celsius."));

            double dblF3 = 0;
            dblF3 = CToF(dblC3);

            double dblC4 = 0;
            dblC4 = double.Parse(Interaction.InputBox("Please enter the 9PM temp in Celsius."));

            double dblF4 = 0;
            dblF4 = CToF(dblC4);

            double dblAvg = 0;
            dblAvg = (dblF1 + dblF2 + dblF3 + dblF4) / 4;

            MessageBox.Show(dblAvg.ToString());
        }

        protected override void OnClick()
        {
            PracticeExercise14();
        }

        protected override void OnUpdate()
        {
        }
    }
}
