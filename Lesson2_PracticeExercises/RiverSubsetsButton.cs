using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;

namespace Lesson2_PracticeExercises
{
    public class RiverSubsetsButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public RiverSubsetsButton()
        {
        }

        public void River_Subsets()
        {
            //****** Author:  Jim Detwiler, edited by Frank Hardisty
            //******** Date:  6/Nov/2009
            //* Description:  Searches the active data frame for a layer called
            //*               "U.S. Rivers" or "us_hydro".  When found, creates a new
            //*               collection of river names to pass to another sub
            //*               that creates a new shapefile for each individual
            //*               river.
            //**** Calls to:  Util_Extract
            //**** Calls by:
            //***** Globals:
            //****** Locals:  pMxDoc, pMap, pLayers, pLayer, colRivers,
            //******          strQueryField, i
            //**** Location:
            //****** Source:
            //******* Notes:
            //****************************************
            //* Revision Author:  Andrew Murdoch
            //*** Revision Date:  5/28/2011
            //** Revision Notes:  Updated for ArcGIS 10 and VB.NET
            //*** Revision Date:  11/8/2014
            //** Revision Notes:  Updated for C#

            IMxDocument pMxDoc = default(IMxDocument);
            pMxDoc = (IMxDocument)ArcMap.Application.Document;
            //* Getting the mxd

            IMap pMap;
            pMap = pMxDoc.FocusMap;
            //* Getting the focus map from the mxd

            IEnumLayer pLayers;
            pLayers = pMap.Layers;
            //* Getting an enumeration of layers from the map

            ILayer pLayer;
            pLayer = pLayers.Next();
            //* Moving the pointer to the first layer

            //* Looping thru all layers
            while (!(pLayer == null))
            {
                if (pLayer.Name == "U.S. Rivers" | pLayer.Name == "us_hydro")
                {
                    break; 
                    //* Found the layer we want
                }
                pLayer = pLayers.Next();
                //* If not correct layer, go to next layer
            }

            if (pLayer == null)
            {
                //* Couldn't find the layer.  Tell user, then quit.
                MessageBox.Show("Sorry, can't find U.S. Rivers layer", "Warning", MessageBoxButtons.OK);
                return;
            }

            //* Creating a new rivers collection
            var colRivers = new List<string>();

            //* Adding items to the collection
            colRivers.Add("Colorado River");
            colRivers.Add("Columbia River");
            colRivers.Add("Mississippi River");
            colRivers.Add("Rio Grande");

            string strQueryField;
            strQueryField = "NAMEEN";
            //* This is the field to query on

            int i;
            //* For each item in the collection
            for (i = 0; i < colRivers.Count; i++)
            {
                //* Make call to Util_Extract passing the layer, current item in
                //* collection, and the query field
                Utilities.Util_Extract((IFeatureLayer)pLayer, colRivers[i], strQueryField);
            }

            MessageBox.Show("Finished creating river subsets.","Done",MessageBoxButtons.OK);
        }

        protected override void OnClick()
        {
            River_Subsets();
        }

        protected override void OnUpdate()
        {
        }
    }
}
