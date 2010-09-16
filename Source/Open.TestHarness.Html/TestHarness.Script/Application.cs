using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Controls;
using Open.Testing.Controllers;
using Open.Testing.Internal;
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
        private static DiContainer container;
        private static PanelResizeController resizeController;
        private static SidebarController sidebarController;
        private static HostCanvasController hostCanvasController;
        #endregion

        #region Properties
        /// <summary>Gets the DI container.</summary>
        public static DiContainer Container { get { return container ?? (container = DiContainer.DefaultContainer); } }
        #endregion

        #region Methods
        /// <summary>Application entry point.</summary>
        /// <param name="args">Init parameters.</param>
        public static void Main(Dictionary args)
        {
            // Setup the DI container.
            Container.RegisterSingleton(typeof(ITestHarnessEvents), new TestHarnessEvents());
            Container.RegisterSingleton(typeof(Common), new Common());

            // Setup the output log.
            Log.View = new LogView(jQuery.Select(CssSelectors.Log).First());

            // Create views.
            shell = new ShellView(jQuery.Select(CssSelectors.Root));
            Container.RegisterSingleton(typeof(ShellView), shell);

            // Create controllers.
            resizeController = new PanelResizeController();
            sidebarController = new SidebarController();
            hostCanvasController = new HostCanvasController();

            // =================================

            //TEMP : Insert sample packages.
            AddPackage("/Content/Scripts/TestHarness.Test.debug.js", "Test.Application.main");
            AddPackage("/Content/Scripts/Open.Core.Test.debug.js", "Open.Core.Test.Application.main");
            AddPackage("/Content/Scripts/Quest.Rogue.Test.debug.js", "Quest.Rogue.Test.Application.main");
            AddPackage("/Content/Scripts/Quest.OnDemand.Test.debug.js", "Quest.OnDemand.Test.Application.main");
        }

        private static void AddPackage(string scriptUrl, string initMethod)
        {
            PackageInfo testHarnessPackage = PackageInfo.SingletonFromUrl(scriptUrl, initMethod);
            sidebarController.AddPackage(testHarnessPackage);
        }
        #endregion
    }
}
