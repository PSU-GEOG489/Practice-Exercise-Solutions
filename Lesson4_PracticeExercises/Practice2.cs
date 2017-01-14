using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;


namespace Lesson4_PracticeExercises
{
    public class Practice2 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice2()
        {
        }

        public void Practice2Exercise()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            ITableCollection pTableCollection;
            pTableCollection = (ITableCollection)pMap; //** QI
           
            int i = 0;
            ITable pTable = null;
            IDataset pDataset;  //** IDataset needed for Name property
          
            bool blnFoundTable = false;
            blnFoundTable = false;

            for (i = 0; i <= pTableCollection.TableCount - 1; i++)
            {
                pTable = pTableCollection.Table[i];
                pDataset = (IDataset)pTable; //** QI
               
                if (pDataset.Name == "lu_codes")
                {
                    blnFoundTable = true;
                    break; 
                }
            }

            if (blnFoundTable == false)
            {
                MessageBox.Show("Table not loaded in data frame");
                return;
            }

            int intCodeField = 0;
            int intDescField = 0;

            intCodeField = pTable.FindField("LU_Code");
            intDescField = pTable.FindField("LU_Desc");

            IRow pRow;
            pRow = pTable.CreateRow();
            pRow.Value[intCodeField] = "RES";
            pRow.Value[intDescField] = "Residential";
            pRow.Store();

            pRow = pTable.CreateRow();
            pRow.Value[intCodeField] = "COM";
            pRow.Value[intDescField] = "Commercial";
            pRow.Store();

            pRow = pTable.CreateRow();
            pRow.Value[intCodeField] = "IND";
            pRow.Value[intDescField] = "Industrial";
            pRow.Store();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            Practice2Exercise();
        }

        protected override void OnUpdate()
        {
        }
    }
}
