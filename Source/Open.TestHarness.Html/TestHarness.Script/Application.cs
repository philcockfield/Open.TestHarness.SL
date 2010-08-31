using System.Collections;
using jQueryApi;
using Open.TestHarness.Log;
using Open.TestHarness.Shell;

namespace Open.TestHarness
{
    public class Application
    {
        #region Head
        private static LogView log;
        private static PanelResizeController resizeController;
        #endregion

        #region Methods
        public static void Main(Dictionary args)
        {
            // Create controllers.
            resizeController = new PanelResizeController();

            // Create views.
            log = new LogView(jQuery.Select(CssSelectors.LogList).First());

            // TEMP

            for (int i = 0; i < 3; i++)
            {
                log.Write("Hello " + i);
            }

        }
        #endregion
    }
}
