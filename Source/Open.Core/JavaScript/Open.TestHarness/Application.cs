using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Helpers;
using Open.Testing.Controllers;
using Open.Testing.Internal;
using Open.Testing.Models;
using Open.Testing.Views;

namespace Open.Testing
{
    public class Application
    {
        #region Head
        public const string PublicDomain = "http://TestHarness.org";

        // View.
        private static ShellView shell;

        // Controllers.
        private static DiContainer container;
        private static SidebarController sidebarController;
        private static ControlHostController controlHostController;
        private static LogController logController;
        private static AddPackageController addPackageController;
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

            // Create shared models.
            Container.RegisterSingleton(typeof (CommonButtons), new CommonButtons());

            // Create views.
            shell = new ShellView(jQuery.Select(CssSelectors.Root));
            Container.RegisterSingleton(typeof(ShellView), shell);
            
            // Create controllers.
            Container.RegisterSingleton(typeof(IPanelResizeController), new PanelResizeController());
            sidebarController = new SidebarController();
            controlHostController = new ControlHostController();
            logController = new LogController();
            addPackageController = new AddPackageController();

            // Preload images.
            PreloadImages();

            //TEMP : Insert sample packages.
            AddPackage("/Content/Scripts/Open.Core.Test.debug.js", "Open.Core.Test.Application.main");
            AddPackage("/Content/Scripts/Quest.Rogue.Test.debug.js", "Quest.Rogue.Test.Application.main");
            AddPackage("/Content/Scripts/Quest.OnDemand.Test.debug.js", "Quest.OnDemand.Test.Application.main");
            AddPackage("/Content/Scripts/Quest.Insandra.Test.debug.js", "Quest.Insandra.Test.Application.main");
        }
        #endregion

        #region Internal
        private static void PreloadImages()
        {
            IconHelper icon = Helper.Icon;
            ImagePreloader.Preload("/Open.Assets/Icons/Api/Property.png");
            ImagePreloader.Preload("/Open.Assets/Icons/Api/Class.png");
            ImagePreloader.Preload(icon.Path(Icons.SilkAccept));
            ImagePreloader.Preload(icon.Path(Icons.SilkExclamation));
            ImagePreloader.Preload(icon.Path(Icons.SilkError));
        }

        private static void AddPackage(string scriptUrl, string initMethod)
        {
            PackageInfo testHarnessPackage = PackageInfo.SingletonFromUrl(scriptUrl, initMethod);
            sidebarController.AddPackage(testHarnessPackage);
        }
        #endregion
    }
}
