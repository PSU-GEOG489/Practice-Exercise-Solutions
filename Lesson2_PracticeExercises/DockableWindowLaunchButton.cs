using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;

namespace Lesson2_PracticeExercises
{
    public class DockableWindowLaunchButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public DockableWindowLaunchButton()
        {
        }

        protected override void OnClick()
        {
            UID dockWinID = new UIDClass();
            dockWinID.Value = ThisAddIn.IDs.DockableWindowTest;

            // Use GetDockableWindow directly as we want the client IDockableWindow not the internal class  
            IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
            dockWindow.Show(true);
        }

        protected override void OnUpdate()
        {
        }
    }
}
