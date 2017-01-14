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
    public class NewTable : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public NewTable()
        {
        }

        public void CreateNewTable()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;

            IMap pMap;
            pMap = pMxDoc.FocusMap;

            IWorkspaceFactory pWSFactory;
            pWSFactory = new ShapefileWorkspaceFactory();

            IFeatureWorkspace pFWorkspace;
            pFWorkspace = (IFeatureWorkspace)pWSFactory.OpenFromFile("c:/temp", ArcMap.Application.hWnd);

            IFieldsEdit pFieldsEdit;
            pFieldsEdit = (IFieldsEdit)new Fields();

            IFieldEdit pIDField;
            pIDField = (IFieldEdit)new Field();
            pIDField.Name_2 = "OID";
            pIDField.Type_2 = esriFieldType.esriFieldTypeOID;
            pIDField.Length_2 = 8;

            IFieldEdit pNameField;
            pNameField = (IFieldEdit)new Field();
            pNameField.Name_2 = "Site_Name";
            pNameField.Type_2 = esriFieldType.esriFieldTypeString;
            pNameField.Length_2 = 20;

            IFieldEdit pReadingField;
            pReadingField = (IFieldEdit)new Field();
            pReadingField.Name_2 = "Reading";
            pReadingField.Type_2 = esriFieldType.esriFieldTypeInteger;
            pReadingField.Length_2 = 8;

            IFieldEdit pDateField;
            pDateField = (IFieldEdit)new Field();
            pDateField.Name_2 = "Visit_Date";
            pDateField.Type_2 = esriFieldType.esriFieldTypeDate;

            pFieldsEdit.AddField(pIDField);
            pFieldsEdit.AddField(pNameField);
            pFieldsEdit.AddField(pReadingField);
            pFieldsEdit.AddField(pDateField);

            ITable pTable = default(ITable);
            pTable = pFWorkspace.CreateTable("measurements.dbf", pFieldsEdit, null, null, "");

            ITableCollection pTableCollection = default(ITableCollection);
            pTableCollection = (ITableCollection)pMap;

            pTableCollection.AddTable(pTable);
            pMxDoc.UpdateContents();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            CreateNewTable();
        }

        protected override void OnUpdate()
        {
        }
    }
}
