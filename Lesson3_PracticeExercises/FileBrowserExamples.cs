using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson3_PracticeExercises
{
    class FileBrowserExamples
    {
        // altered slightly from code found here: 
        //  https://saeidp.wordpress.com/2010/11/10/arcobjects-add-shapefile-using-openfiledialog/
        //
        #region"Add Shapefile Using OpenFileDialog"
        // ArcGIS Snippet Title:
        // Add Shapefile Using OpenFileDialog
        // 
        // Long Description:
        // Add a shapefile to the ActiveView using the Windows.Forms.OpenFileDialog control.
        // 
        // Add the following references to the project:
        // ESRI.ArcGIS.Carto
        // ESRI.ArcGIS.DataSourcesFile
        // ESRI.ArcGIS.Display
        // ESRI.ArcGIS.Geodatabase
        // ESRI.ArcGIS.Geometry
        // ESRI.ArcGIS.System
        // System.Windows.Forms
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop (ArcEditor, ArcInfo, ArcView)
        // ArcGIS Engine
        // 
        // Applicable ArcGIS Product Versions:
        // 9.2
        // 9.3
        // 
        // Required ArcGIS Extensions:
        // (NONE)
        // 
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        // 

        ///<summary>Add a shapefile to the ActiveView using the Windows.Forms.OpenFileDialog control.</summary>
        ///
        ///<param name="activeView">An IActiveView interface</param>
        /// 
        ///<remarks></remarks>
        public void AddShapefileUsingOpenFileDialog(ESRI.ArcGIS.Carto.IActiveView activeView)
        {
            //parameter check
            if (activeView == null)
            {
                return;
            }

            // Use the OpenFileDialog Class to choose which shapefile to load.
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Shapefiles (*.shp)|*.shp";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;


            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // The user chose a particular shapefile.

                // The returned string will be the full path, filename and file-extension for the chosen shapefile. Example: "C:\test\cities.shp"
                string shapefileLocation = openFileDialog.FileName;

                if (shapefileLocation != "")
                {
                    // Ensure the user chooses a shapefile

                    // Create a new ShapefileWorkspaceFactory CoClass to create a new workspace
                    ESRI.ArcGIS.Geodatabase.IWorkspaceFactory workspaceFactory = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactory();

                    // System.IO.Path.GetDirectoryName(shapefileLocation) returns the directory part of the string. Example: "C:\test\"
                    ESRI.ArcGIS.Geodatabase.IFeatureWorkspace featureWorkspace = (ESRI.ArcGIS.Geodatabase.IFeatureWorkspace)workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(shapefileLocation), 0); // Explicit Cast

                    // System.IO.Path.GetFileNameWithoutExtension(shapefileLocation) returns the base filename (without extension). Example: "cities"
                    ESRI.ArcGIS.Geodatabase.IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileNameWithoutExtension(shapefileLocation));

                    ESRI.ArcGIS.Carto.IFeatureLayer featureLayer = new ESRI.ArcGIS.Carto.FeatureLayer();
                    featureLayer.FeatureClass = featureClass;
                    featureLayer.Name = featureClass.AliasName;
                    featureLayer.Visible = true;
                    activeView.FocusMap.AddLayer(featureLayer);

                    // Zoom the display to the full extent of all layers in the map
                    activeView.Extent = activeView.FullExtent;
                    activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeography, null, null);
                }
                else
                {
                    // The user did not choose a shapefile.
                    // Do whatever remedial actions as necessary
                    // System.Windows.Forms.MessageBox.Show("Noefile chosen", "No Choice #1",
                    //                                     System.Windows.Forms.MessageBoxButtons.OK,
                    //                                     System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                // The user did not choose a shapefile. They clicked Cancel or closed the dialog by the "X" button.
                // Do whatever remedial actions as necessary.
                // System.Windows.Forms.MessageBox.Show("Noefile chosen", "No Choice #2",
                //                                      System.Windows.Forms.MessageBoxButtons.OK,
                //                                      System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        // taken from code found here:
        //   http://gisinformon.blogspot.com/2011/09/using-igxdialog-to-open-arcgis-feature.html
        //
        public IFeatureClass OpenArcGisFeatureClassFromDialog(int hwnd, string dialogTitle, esriGeometryType possibleInputFeatureClassGeometry)
        {
            IFeatureClass result = null;
            IGxDialog gxDialog = new GxDialog();
            IEnumGxObject gxEnum;

            switch (possibleInputFeatureClassGeometry)
            {
                case esriGeometryType.esriGeometryPoint:
                    gxDialog.ObjectFilter = new GxFilterPointFeatureClasses();
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    gxDialog.ObjectFilter = new GxFilterPolylineFeatureClasses();
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    gxDialog.ObjectFilter = new GxFilterPolygonFeatureClasses();
                    break;
                default:
                    gxDialog.ObjectFilter = new GxFilterFeatureClasses();
                    break;
            }
            gxDialog.AllowMultiSelect = false;
            gxDialog.Title = dialogTitle;

            if (gxDialog.DoModalOpen(hwnd, out gxEnum) && gxEnum != null)
            {
                IGxObject gxObject = gxEnum.Next();
                if (gxObject is IGxDataset)
                {
                    IGxDataset gxDataset = (IGxDataset)gxObject;
                    if (gxDataset.Dataset is IFeatureClass)
                        result = (IFeatureClass)gxDataset.Dataset;
                }
            }

            gxDialog.InternalCatalog.Close();
            Marshal.FinalReleaseComObject(gxDialog);

            return result;
        }
    }
}
