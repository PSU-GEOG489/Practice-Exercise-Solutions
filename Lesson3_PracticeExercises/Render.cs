using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using Microsoft.VisualBasic;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.CartoUI;


namespace Lesson3_PracticeExercises
{
    public class Render : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Render()
        {
        }


        public void Render_Layers()
        {
            IMxDocument pMxDoc;
            pMxDoc = (IMxDocument)ArcMap.Application.Document;
            IMap pMap;
            pMap = pMxDoc.FocusMap;
            string strChoice = null;
            //** Giving the user a choice of 3 predefined maps
            strChoice = Interaction.InputBox("1 - U.S. States, " + Environment.NewLine + "2 - Major Cities, " + Environment.NewLine + "3 - State Polygon Area Rank", "Please choose a map.");
            if (string.IsNullOrEmpty(strChoice))
            {
                return;
            }
            IWorkspaceFactory pWFactory;
            pWFactory = new ShapefileWorkspaceFactory();
            IWorkspace pWorkspace;
            pWorkspace = pWFactory.OpenFromFile("C:\\GEOG489\\Lesson1-2", 0);
            IFeatureWorkspace pFeatureWorkspace;
            pFeatureWorkspace = (IFeatureWorkspace)pWorkspace;
            IFeatureLayer pFLayer;
            pFLayer = new FeatureLayer();
            IGeoFeatureLayer pGeoFLayer;
            pGeoFLayer = (IGeoFeatureLayer)pFLayer;
            IFeatureClass pFClass;

            ITable pTable;
            IClassify pClasses;
            ITableHistogram pTableHist;
            IHistogram pHist;
            IClassBreaksRenderer pCBR;
            IClassBreaksUIProperties pUIProperties;
            ILegendInfo pLegendInfo;

            object Frequencies = null;
            object Values = null;


            if (strChoice == "1")
            {
                //** User gets a Unique Value rendering of U.S. States by sub-region
                pFClass = pFeatureWorkspace.OpenFeatureClass("us_boundaries.shp");

                pFLayer.FeatureClass = pFClass;
                pFLayer.Name = "U.S. States";
                pMap.AddLayer(pFLayer);

                IUniqueValueRenderer pUVRender;
                pUVRender = new UniqueValueRenderer();

                //** Setting default symbol for features that don't have a value in
                //** the classification field
                ISimpleFillSymbol pSymDefault;
                pSymDefault = new SimpleFillSymbol();
                pSymDefault.Style = ESRI.ArcGIS.Display.esriSimpleFillStyle.esriSFSSolid;
                pSymDefault.Outline.Width = 0.4;

                //** These properties should be set prior to adding values
                pUVRender.FieldCount = 1;
                //** Can classify based on up to 3 fields
                pUVRender.Field[0] = "NAME";
                //** Name of the 1st (only) field
                pUVRender.DefaultSymbol = (ISymbol)pSymDefault;
                pUVRender.UseDefaultSymbol = true;
                IQueryFilter pQFilter;
                pQFilter = new QueryFilter();
                //** empty QueryFilter same as selecting all recs
                IFeatureCursor pFCursor;
                pFCursor = pFClass.Search(pQFilter, false);
                //** getting all features

                //** Make the color ramp we will use for the symbols in the renderer.
                //** Colors should be random for Unique Value, as opposed to light-to-dark.
                //** Make settings for hue, saturation, value, and seed to have random
                //** colors generated within certain limits.
                IRandomColorRamp pRamp;
                pRamp = new RandomColorRamp();
                pRamp.MinSaturation = 25;
                pRamp.MaxSaturation = 45;
                pRamp.MinValue = 85;
                pRamp.MaxValue = 100;
                pRamp.StartHue = 205;
                pRamp.EndHue = 320;
                pRamp.UseSeed = true;
                pRamp.Seed = 25;

                IFeature pFeature;
                int n = 0;
                int i = 0;

                n = pFClass.FeatureCount(pQFilter);
                //** Getting total count of state features

                //** Loop through the features
                while (i != n)
                {
                    ISimpleFillSymbol pSymX;
                    pSymX = new SimpleFillSymbol();

                    //** Move to the next feature and assign the value in the Sub_region field to a var
                    pFeature = pFCursor.NextFeature();
                    string strVal = null;
                    strVal = pFeature.Value[2].ToString();
                    //** NAME Field is at this index in fields collection

                    //** Test to see if we've already added this value
                    //** to the renderer, if not, then add it.
                    bool blnAdded = false;
                    blnAdded = false;

                    int x = 0;
                    //** First time through, ValueCount = 0
                    for (x = 0; x <= (pUVRender.ValueCount - 1); x++)
                    {
                        if (pUVRender.Value[x] == strVal)
                        {
                            blnAdded = true;
                            break; 
                        }
                    }

                    //** Value not yet encountered, must add it
                    if (blnAdded == false)
                    {
                        pUVRender.AddValue(strVal, "Predominant Term", (ISymbol)pSymDefault);
                        pUVRender.Label[strVal] = strVal;
                        pUVRender.Symbol[strVal] = (ISymbol)pSymX;
                        //** All values get same symbol at first,
                        //** colors assigned later
                    }
                    i = i + 1;
                }

                //** Can size the color ramp and assign the colors, now that the
                //** number of unique values is known.

                pRamp.Size = pUVRender.ValueCount;
                bool blnRamp;
                pRamp.CreateRamp(out blnRamp);

                //** Create an enum of colors from the color ramp and initialize it
                IEnumColors pColors;
                pColors = pRamp.Colors;
                pColors.Reset();

                int y = 0;

                //** Loop through each unique value, setting its symbol's color
                for (y = 0; y <= (pUVRender.ValueCount - 1); y++)
                {
                    string strRendVal = null;
                    strRendVal = pUVRender.Value[y];
                    if (!string.IsNullOrEmpty(strRendVal))
                    {
                        ISimpleFillSymbol pSym;
                        pSym = (ISimpleFillSymbol)pUVRender.Symbol[strRendVal];
                        pSym.Color = pColors.Next();
                        pUVRender.Symbol[strRendVal] = (ISymbol)pSym;
                    }
                }

                //** If you didn't use a color ramp that was predefined
                //** in a style, use "Custom" here, otherwise
                //** use the name of the color ramp you chose.
                pUVRender.ColorScheme = "Custom";
                //pUVRender.FieldType[0] = true;
                pUVRender.set_FieldType(0, true);
                //** Set to True since Sub_region is a string

                pGeoFLayer.Renderer = (IFeatureRenderer)pUVRender;

                //** Make sure the symbology tab shows the correct info.
                IRendererPropertyPage pRendPropPage;
                pRendPropPage = (IRendererPropertyPage)new UniqueValuePropertyPage();
                pGeoFLayer.RendererPropertyPageClassID = pRendPropPage.ClassID;

            }
            else if (strChoice == "2")
            {
                //** User gets a graduated symbol map of U.S. Cities
                pFClass = pFeatureWorkspace.OpenFeatureClass("us_cities.shp");

                pFLayer.FeatureClass = pFClass;
                pFLayer.Name = "Major U.S. Cities";
                pGeoFLayer = (IGeoFeatureLayer)pFLayer;
                pMap.AddLayer(pFLayer);
                //** Switching to the ITable interface; FeatureClass is a type of Table
                pTable = (ITable)pFClass;

                //** Creating a new quantile classification
                pClasses = (IClassify)new Quantile();

                //** Must create a TableHistogram to generate classes
                //** Need both ITableHistogram and IHistogram interfaces
                pTableHist = (ITableHistogram)new TableHistogram();

                pHist = (IHistogram)pTableHist;


                //** Set the table and field for the histogram, then use GetHistogram to
                //** get arrays of values and corresponding frequencies
                pTableHist.Field = "POPCLASS";
                pTableHist.Table = pTable;
                pHist.GetHistogram(out Values, out Frequencies);

                //** Assign the arrays of values and frequencies to the quantile classification
                //** then break the data into 5 classes
                pClasses.SetHistogramData(Values, Frequencies);
                pClasses.Classify(4);

                double[] classBreaks = new double[4];
                classBreaks = (double[])pClasses.ClassBreaks;

                //** Ready to render the data
                pCBR = new ClassBreaksRenderer();

                pCBR.BreakCount = 4;

                pCBR.Field = "POPCLASS";
                pCBR.MinimumBreak = classBreaks[0];
                //** ClassBreak(0) is min value of 1st class

                int j = 0;

                //** Loop thru each class, setting the renderers breaks
                //** The renderer's breaks are 0-based, quantile's breaks are 1-based
                for (j = 0; j <= (pCBR.BreakCount - 1); j++)
                {

                    pCBR.Break[j] = classBreaks[j + 1];
                    pCBR.Label[j] = classBreaks[j] + " - " + classBreaks[j + 1];
                }
                //** Ready for symbols
                ISimpleMarkerSymbol pCitySym;

                //** Setting the smallest symbol size
                double dblFromSize = 0;
                dblFromSize = 4;

                //** Setting the largest symbol size
                double dblToSize = 0;
                dblToSize = 16;

                //** Calculating the change in size between classes based on the min size,
                //** max size, and # of classes
                double dblStep = 0;
                dblStep = (dblToSize - dblFromSize) / (pCBR.BreakCount - 1);

                //** Setting the foreground color of the symbols
                IRgbColor pColor;
                pColor = new RgbColor();

                pColor.Red = 179;
                pColor.Green = 235;
                pColor.Blue = 255;

                //** Setting the outline color of the symbols
                //** No color settings means black outline (r=0, g=0, b=0)
                IRgbColor pOlColor;
                pOlColor = new RgbColor();

                int k = 0;

                //** Setting symbol for each class
                for (k = 0; k <= pCBR.BreakCount - 1; k++)
                {
                    pCitySym = new SimpleMarkerSymbol();
                    pCitySym.Color = pColor;
                    pCitySym.Outline = true;
                    pCitySym.OutlineColor = pOlColor;
                    pCitySym.OutlineSize = 1;
                    pCitySym.Size = dblFromSize + (dblStep * Convert.ToDouble(k));
                    pCBR.Symbol[k] = (ISymbol)pCitySym;
                }
                //** Assigning the ClassBreaksRenderer to the layer
                pGeoFLayer.Renderer = (IFeatureRenderer)pCBR;
                pUIProperties = (IClassBreaksUIProperties)pCBR;
                pUIProperties.LowBreak[0] = pCBR.MinimumBreak;

                int m = 0;
                for (m = 1; m <= pCBR.BreakCount - 1; m++)
                {
                    pUIProperties.LowBreak[m] = classBreaks[m];
                }
                //** Creating a heading for the legend in the Table of Contents
                pLegendInfo = (ILegendInfo)pCBR;
                //qi
                pLegendInfo.LegendGroup[0].Heading = "Population Class";
                pLegendInfo.SymbolsAreGraduated = false;
            }
            else if (strChoice == "3")
            {

                //** User gets a graduated color map of state population normalized by area
                pFClass = pFeatureWorkspace.OpenFeatureClass("us_boundaries.shp");

                pFLayer.FeatureClass = pFClass;
                pFLayer.Name = "State Polygon Area Rank";
                pGeoFLayer = (IGeoFeatureLayer)pFLayer;
                pMap.AddLayer(pFLayer);
                //** Very similar to previous example

                pTable = (ITable)pFClass;
                pClasses = (IClassify)new Quantile();

                pTableHist = (ITableHistogram)new TableHistogram();
                pHist = (IHistogram)pTableHist;

                pTableHist.Field = "Shape_Area";
                pTableHist.Table = pTable;
                //pTableHist.NormField = "Rank" '** Setting the normalization field before getting histogram
                pHist.GetHistogram(out Values, out Frequencies);

                pClasses.SetHistogramData(Values, Frequencies);
                pClasses.Classify(5);

                double[] classBreaks = new double[4];
                classBreaks = (double[])pClasses.ClassBreaks;

                pCBR = new ClassBreaksRenderer();

                //** Making normalization settings for the renderer
                //Dim pNorm As IDataNormalization
                //pNorm = pCBR
                //pNorm.NormalizationField = "Rank"
                //pNorm.NormalizationType = esriDataNormalization.esriNormalizeByField

                pCBR.BreakCount = 5;
                pCBR.Field = "Shape_Area";
                pCBR.MinimumBreak = classBreaks[0];

                int t = 0;

                pUIProperties = (IClassBreaksUIProperties)pCBR;
                pUIProperties.LowBreak[0] = pCBR.MinimumBreak;

                for (t = 0; t <= Information.UBound(classBreaks) - 1; t++)
                {
                    pCBR.Break[t] = classBreaks[t + 1];

                    //** Want to round the class break values in the class labels
                    //** Use the NumericFormat class to set up a 1-decimal format
                    INumericFormat pNumericFormat;
                    pNumericFormat = (INumericFormat)new NumericFormat();
                    pNumericFormat.RoundingOption = ESRI.ArcGIS.esriSystem.esriRoundingOptionEnum.esriRoundNumberOfDecimals;
                    pNumericFormat.RoundingValue = 1;

                    INumberFormat pNumberFormat;
                    pNumberFormat = (INumberFormat)pNumericFormat;

                    string strRndVal1 = null;
                    string strRndVal2 = null;

                    //** Using ValueToString method to convert the numbers into the desired format
                    strRndVal1 = pNumberFormat.ValueToString(classBreaks[t]);
                    strRndVal2 = pNumberFormat.ValueToString(classBreaks[t + 1]);

                    pCBR.Label[t] = strRndVal1 + " - " + strRndVal2;
                }

                pGeoFLayer.Renderer = (IFeatureRenderer)pCBR;

                for (t = 1; t <= 4; t++)
                {
                    pUIProperties.LowBreak[t] = classBreaks[t];
                }
                //** Ready to set colors
                //** Want to use AlgorithmicColorRamp to ramp from one color to another
                IEnumColors pColorEnum;
                IAlgorithmicColorRamp pAColorRamp;
                IRgbColor pFromColor;
                IRgbColor pToColor;

                //** Setting up the algorithmic color ramp
                pFromColor = new RgbColor();
                pFromColor.RGB = Information.RGB(242, 233, 250);
                // lavender
                pToColor = new RgbColor();
                pToColor.RGB = Information.RGB(56, 45, 121);
                // deep purple
                pAColorRamp = new AlgorithmicColorRamp();
                
                pAColorRamp.Algorithm = ESRI.ArcGIS.Display.esriColorRampAlgorithm.esriHSVAlgorithm;
                pAColorRamp.Size = 5;
                //** # of classes
                pAColorRamp.FromColor = pFromColor;
                pAColorRamp.ToColor = pToColor;
                bool blnRamp;
                pAColorRamp.CreateRamp(out blnRamp);
                pColorEnum = pAColorRamp.Colors;
                pColorEnum.Reset();

                int s = 0;

                //** Loop thru the classes, creating a new symbol, assigning the next color
                //** created by the ramp, and assigning the symbol to the renderer.
                for (s = 0; s <= 4; s++)
                {
                    ISimpleFillSymbol pSFSym;
                    pSFSym = new SimpleFillSymbol();
                    pSFSym.Color = pColorEnum.Next();
                    pCBR.Symbol[s] = (ISymbol)pSFSym;
                }

                //** Creating a heading for the legend in the Table of Contents
                pLegendInfo = (ILegendInfo)pCBR;
                //qi
                pLegendInfo.LegendGroup[0].Heading = "Area (Degrees)";
                pLegendInfo.SymbolsAreGraduated = false;
            }
            //** Refreshing the Table of Contents
            pMxDoc.ActiveView.ContentsChanged();
            pMxDoc.UpdateContents();

            //** Re-drawing the map
            pMxDoc.ActiveView.Refresh();
        }

        protected override void OnClick()
        {
            Render_Layers();
        }

        protected override void OnUpdate()
        {
        }
    }
}
