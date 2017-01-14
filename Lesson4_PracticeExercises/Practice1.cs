using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;


namespace Lesson4_PracticeExercises
{
    public class Practice1 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Practice1()
        {
        }

        public void Practice1Exercise()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            IWorkspaceFactory pWFactory;
            pWFactory = new ShapefileWorkspaceFactory();

            IFeatureWorkspace pFWorkspace;
            pFWorkspace = (IFeatureWorkspace)pWFactory.OpenFromFile("c:/temp",ArcMap.Application.hWnd);

            IFieldsEdit pFieldsEdit;
            pFieldsEdit = (IFieldsEdit)new Fields();

            IFieldEdit pIDField;
            pIDField = (IFieldEdit)new Field();

            pIDField.Name_2 = "OID";
            pIDField.Type_2 = esriFieldType.esriFieldTypeOID;
            pIDField.Length_2 = 3;

            IFieldEdit pCodeField;
            pCodeField = (IFieldEdit)new Field();

            pCodeField.Name_2 = "LU_Code";
            pCodeField.Type_2 = esriFieldType.esriFieldTypeString;
            pCodeField.Length_2 = 4;

            IFieldEdit pDescField;
            pDescField = (IFieldEdit)new Field();

            pDescField.Name_2 = "LU_Desc";
            pDescField.Type_2 = esriFieldType.esriFieldTypeString;
            pDescField.Length_2 = 25;

            pFieldsEdit.AddField(pIDField);
            pFieldsEdit.AddField(pCodeField);
            pFieldsEdit.AddField(pDescField);

            ITable pTable;
            pTable = pFWorkspace.CreateTable("lu_codes.dbf", pFieldsEdit, null, null, "");

            ITableCollection pTableCollection;
            pTableCollection = (ITableCollection)pMap; //QI
           
            pTableCollection.AddTable(pTable);
            pMxDoc.UpdateContents();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            Practice1Exercise();
        }

        protected override void OnUpdate()
        {
        }
    }
}
