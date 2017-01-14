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
    public class NewShapefile : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public NewShapefile()
        {
        }

        public void CreateNewShapefile()
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
            pIDField.Name_2 = "FID";
            pIDField.Type_2 = esriFieldType.esriFieldTypeOID;
            pIDField.Length_2 = 8;

            IFeatureLayer pStatesLayer;
            pStatesLayer = (IFeatureLayer)pMap.Layer[0];

            IFields pStatesFields;
            pStatesFields = pStatesLayer.FeatureClass.Fields;

            IField pStatesShapeField;
            pStatesShapeField = pStatesFields.Field[pStatesFields.FindField("Shape")];

            IGeometryDef pStatesGeomDef;
            pStatesGeomDef = pStatesShapeField.GeometryDef;

            IFieldEdit pShapeField;
            pShapeField = (IFieldEdit)new Field();
            pShapeField.Name_2 = "Shape";
            pShapeField.Type_2 = esriFieldType.esriFieldTypeGeometry;
            pShapeField.GeometryDef_2 = pStatesGeomDef;

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

            IFieldEdit pDateField = default(IFieldEdit);
            pDateField = (IFieldEdit)new Field();
            pDateField.Name_2 = "Visit_Date";
            pDateField.Type_2 = esriFieldType.esriFieldTypeDate;

            pFieldsEdit.AddField(pIDField);
            pFieldsEdit.AddField(pShapeField);
            pFieldsEdit.AddField(pNameField);
            pFieldsEdit.AddField(pReadingField);
            pFieldsEdit.AddField(pDateField);

            IFeatureClass pFClass;
            pFClass = pFWorkspace.CreateFeatureClass("measurements.shp", pFieldsEdit, null, null, esriFeatureType.esriFTSimple, "Shape", "");

            IFeatureLayer pFLayer;
            pFLayer = new FeatureLayer();

            pFLayer.FeatureClass = pFClass;
            pFLayer.Name = "Field Measurements";

            pMap.AddLayer(pFLayer);
            pMxDoc.UpdateContents();

            IActiveView pActiveView;
            pActiveView = (IActiveView)pMap;

            pActiveView.Refresh();
        }


        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            CreateNewShapefile();
        }

        protected override void OnUpdate()
        {
        }
    }
}
