using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.GeoDatabaseUI;

namespace Lesson2_PracticeExercises
{

    static class Utilities
    {

        public static void Util_Extract(IFeatureLayer pInFLayer, string strValue, string strField)
        {
            //****** Author:  Jim Detwiler
            //******** Date:  11/17/2001
            //* Description:  Sub procedure that accepts a feature layer, attribute
            //*               value, and field name as parameters.  Using these
            //*               parameters, it will select out all features having
            //*               the attribute value in the specified field, the
            //*               write them to a new shapefile.
            //**** Calls to:
            //**** Calls by:  Rivers_Subset
            //***** Globals:
            //****** Locals:  pInFLayer, strValue, strField, pInFClass, pInDataset,
            //******          pFeatureWorkspace, pInFClassName, strShpName, pFields,
            //******          lngGeomIndex, pField, pGeomDef, pQFilter,
            //******          pWorkspaceFactory, pOutWorkspaceName, pOutFClassName,
            //******          pOutDatasetName, pSelSet, pExportOp
            //**** Location:
            //****** Source:
            //******* Notes:
            //****************************************
            //* Revision Author:  Andrew Murdoch
            //** Revision Date:  5/28/2011
            //** Revision Notes:  Updated for ArcGIS 10 and VB.NET
            //** Revision Date:  11/8/2014
            //** Revision Notes:  Updated for C#


            // Getting the input feature class info
            IFeatureClass pInFClass;
            pInFClass = pInFLayer.FeatureClass;

            IDataset pInDataset;
            pInDataset = (IDataset)pInFLayer;
            // QI to get workspace

            IFeatureWorkspace pFeatureWorkspace;
            pFeatureWorkspace = (IFeatureWorkspace)pInDataset.Workspace;

            IFeatureClassName pInFClassName;
            pInFClassName = (IFeatureClassName)pInDataset.FullName;

            //Get geometry definition from input featureclass.
            string strShpName;
            strShpName = pInFClass.ShapeFieldName;
            IFields pFields;
            pFields = pInFClass.Fields;
            int lngGeomIndex = 0;
            lngGeomIndex = pFields.FindField(strShpName);
            IField pField;
            pField = pFields.get_Field(lngGeomIndex);
            IGeometryDef pGeomDef;
            pGeomDef = pField.GeometryDef;

            // Setting up the query filter based on the supplied field and value
            IQueryFilter pQFilter;
            pQFilter = new QueryFilter();
            pQFilter.WhereClause = strField + " = '" + strValue + "'";

            IWorkspaceFactory pWorkspaceFactory;
            pWorkspaceFactory = new ShapefileWorkspaceFactory();

            //Set up outworkspacename for new shape file.
            IWorkspaceName pOutWorkspaceName;
            pOutWorkspaceName = (IWorkspaceName)new WorkspaceName();
            pOutWorkspaceName.WorkspaceFactoryProgID = "esricore.shapefileworkspacefactory.1";
            pOutWorkspaceName.PathName = "c:\\temp";
            //path to where I want the shapefile.
            IFeatureClassName pOutFClassName;
            pOutFClassName = (IFeatureClassName)new FeatureClassName();
            IDatasetName pOutDatasetName;
            pOutDatasetName = (IDatasetName)pOutFClassName;
            pOutDatasetName.Name = strValue;
            //Output shapefile name.
            pOutDatasetName.WorkspaceName = pOutWorkspaceName;

            // Performing the selection
            ISelectionSet pSelSet;
            pSelSet = pInFClass.Select(pQFilter, esriSelectionType.esriSelectionTypeIDSet, esriSelectionOption.esriSelectionOptionNormal, pInDataset.Workspace);

            // Exporting the selection
            IExportOperation pExportOp;
            pExportOp = new ExportOperation();
            pExportOp.ExportFeatureClass((IDatasetName)pInFClassName, pQFilter, pSelSet, pGeomDef, (IFeatureClassName)pOutDatasetName, 0);

        }

    }
}