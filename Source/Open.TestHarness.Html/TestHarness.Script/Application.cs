using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Controls;
using Open.TestHarness.Controllers;
using Open.TestHarness.Models;
using Open.TestHarness.Views;

namespace Open.TestHarness
{
    public class Application
    {
        #region Head
        private static ShellView shell;
        private static PanelResizeController resizeController;
        private static SidebarController sidebarController;
        #endregion

        #region Properties
        /// <summary>Gets the root view of the application shell.</summary>
        public static ShellView Shell { get { return shell; } }
        #endregion

        #region Methods
        public static void Main(Dictionary args)
        {
            // Setup the output log.
            LogView logView = new LogView(jQuery.Select(CssSelectors.Log).First());
            Log.RegisterView(logView);

            // Create views.
            shell = new ShellView(jQuery.Select(CssSelectors.Root));

            // Create TestHarness controllers.
            resizeController = new PanelResizeController();
            sidebarController = new SidebarController();

            // =================================

            //TEMP : Insert sample TestPackage 
            const string scriptUrl = "/Content/Scripts/Test.debug.js";
            const string initMethod = "Test.Application.main";

            PackageInfo packageDef = PackageInfo.SingletonFromUrl(scriptUrl, initMethod);
            sidebarController.AddPackage(packageDef);
        }
        #endregion
    }
}
