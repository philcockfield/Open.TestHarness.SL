using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Controls;
using Open.Testing.Controllers;
using Open.Testing.Models;
using Open.Testing.Views;

namespace Open.Testing
{
    public class Application
    {
        #region Head
        // View.
        private static ShellView shell;

        // Controllers.
        private static PanelResizeController resizeController;
        private static SidebarController sidebarController;
        private static ControlHostController controlHostController;
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

            // Create controllers.
            resizeController = new PanelResizeController();
            sidebarController = new SidebarController();
            controlHostController = new ControlHostController();

            // =================================

            //TEMP : Insert sample packages.
            AddTestHarnessPackage();
            AddCorePackage();
        }

        private static void AddTestHarnessPackage()
        {
            const string scriptUrl = "/Content/Scripts/TestHarness.Test.debug.js";
            const string initMethod = "Test.Application.main";

            PackageInfo testHarnessPackage = PackageInfo.SingletonFromUrl(scriptUrl, initMethod);
            sidebarController.AddPackage(testHarnessPackage);
        }

        private static void AddCorePackage()
        {
            const string scriptUrl = "/Content/Scripts/Open.Core.Test.debug.js";
            const string initMethod = "Open.Core.Test.Application.main";

            PackageInfo testHarnessPackage = PackageInfo.SingletonFromUrl(scriptUrl, initMethod);
            sidebarController.AddPackage(testHarnessPackage);
        }
        #endregion
    }
}
