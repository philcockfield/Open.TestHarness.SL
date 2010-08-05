using System.Collections;
using Open.TestHarness.Shell;

namespace Open.TestHarness
{
    public class Application
    {
        #region Head
        private static PanelResizeController resizeController;
        #endregion

        #region Methods
        public static void Main(Dictionary args)
        {
            // Create controllers.
            resizeController = new PanelResizeController();
        }
        #endregion
    }
}
