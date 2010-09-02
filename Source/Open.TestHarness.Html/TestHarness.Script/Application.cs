using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Controls;
using Open.TestHarness.Shell;
using Open.TestHarness.Sidebar;
using TH = Open.Core.TestHarness;

namespace Open.TestHarness
{
    public class Application
    {
        #region Head
        private static PanelResizeController resizeController;
        private static SidebarController sidebarController;
        private static GlobalEventController eventController;
        #endregion

        #region Methods
        public static void Main(Dictionary args)
        {
            // Create controllers.
            resizeController = new PanelResizeController();
            sidebarController = new SidebarController();
            eventController = new GlobalEventController();

            // Setup the output log.
            LogView logView = new LogView(jQuery.Select(CssSelectors.Log).First());
            Log.RegisterView(logView);

            //TEMP 
            DelayedAction.Invoke(1, delegate
                                        {
                                            Log.Info("Starting download...");

                                            TestPackageLoader loader = new TestPackageLoader(
                                                                    "/Content/Scripts/TestHarness.Script.Test.debug.js", 
                                                                    "TestHarness.Test.Application.main");
                                            loader.Load(delegate
                                                            {
                                                                Log.Info("Complete.  Succeeded: " + loader.Succeeded);
                                                                Log.LineBreak();
                                                            });
                                        });

            //Type type = typeof (Application);
            //TestPackageDef def1 = TestPackageDef.GetSingleton(type);
            //TestPackageDef def2 = TestPackageDef.GetSingleton(type);
            //Log.Info("def1 == def2: " + (def1 == def2));
        }
        #endregion
    }
}
