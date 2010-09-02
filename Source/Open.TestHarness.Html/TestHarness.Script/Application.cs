using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Controls;
using Open.TestHarness.Shell;
using Open.TestHarness.Sidebar;

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
            // Create controllers.
            resizeController = new PanelResizeController();
            sidebarController = new SidebarController();

            // Setup the output log.
            LogView logView = new LogView(jQuery.Select(CssSelectors.Log).First());
            Log.RegisterView(logView);
        }
        #endregion
    }
}
