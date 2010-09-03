using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Controls;
using Open.TestHarness.Controllers;
using Open.TestHarness.Models;
using TH = Open.Core.TestHarness;

namespace Open.TestHarness
{
    public class Application
    {
        #region Head
        private static PanelResizeController resizeController;
        private static SidebarController sidebarController;
        #endregion

        #region Methods
        public static void Main(Dictionary args)
        {
            // Setup the output log.
            LogView logView = new LogView(jQuery.Select(CssSelectors.Log).First());
            Log.RegisterView(logView);

            // Create TestHarness controllers.
            resizeController = new PanelResizeController();
            sidebarController = new SidebarController();



            //TEMP --------------

            string scriptUrl = "/Content/Scripts/TestHarness.Script.Test.debug.js";
            string initMethod = "TestHarness.Test.Application.main";

            TestPackageDef packageDef = TestPackageDef.SingletonFromUrl(scriptUrl, initMethod);
            sidebarController.AddPackage(packageDef);
        }
        #endregion
    }
}
