using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace Lesson2_PracticeExercises
{
    public class ReportTemperature : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ReportTemperature()
        {
        }

        public void Report_Temp()
        {
            double dblInput = 0;
            double dblOutput = 0;

            string strChoice = null;

            do
            {
                strChoice = Interaction.InputBox("1 = Fahrenheit to Celsius" + System.Environment.NewLine + "2 = Celsius to Fahrenheit" + System.Environment.NewLine + "Any other key to quit", "Temp Converter");

                if (strChoice == "1")
                {
                    dblInput = double.Parse(Interaction.InputBox("What's the temp in Fahrenheit?", "Temp in F?"));
                    dblOutput = Temp_Convert(dblInput, "F");
                }
                else if (strChoice == "2")
                {
                    dblInput = double.Parse(Interaction.InputBox("What's the temp in Celsius?", "Temp in C?"));
                    dblOutput = Temp_Convert(dblInput, "C");
                }
                else
                {
                    return;
                }

                MessageBox.Show("The converted temp is " + dblOutput, "Converted Temp", MessageBoxButtons.OK);
                
            } while (true);

        }

        public double Temp_Convert(double dblInTemp, string strInScale)
        {
            double functionReturnValue = 0;

            if (strInScale == "F")
            {
                functionReturnValue = (dblInTemp - 32) * 5 / 9;
            }
            else if (strInScale == "C")
            {
                functionReturnValue = (dblInTemp * 9 / 5) + 32;
            }
            else
            {
                MessageBox.Show("Input temp must be in F or C","Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                functionReturnValue = -9999;
            }
            return functionReturnValue;

        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            Report_Temp();
        }

        protected override void OnUpdate()
        {
        }
    }
}
