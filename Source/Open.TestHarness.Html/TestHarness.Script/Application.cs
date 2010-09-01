using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Controls;
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

            // Setup the output log.
            LogView logView = new LogView(jQuery.Select(CssSelectors.Log).First());
            Log.RegisterView(logView);


//            Log.Write("Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello Hello ", LogSeverity.Info);

            //TEMP );
            for (int i = 0; i < 50; i++)
            {
                Log.Write("Hello " + i, LogSeverity.Debug);
            }

            Log.LineBreak();
            Log.Info("9999");


            //log.Write("Fail", LogSeverity.Error);
            //log.Write("Warn", LogSeverity.Warning);

            //                            Log.Clear();

            //log.Write("New 1", LogSeverity.Debug);
            //log.Write("New 2", LogSeverity.Debug);

            DelayedAction.Invoke(2, delegate
                                        {
                                            Log.Error("Error");
                                            Log.Debug("Foo");
                                            Log.LineBreak();
                                            Log.Info("Yo");
                                        });

            //                        });


            //// TEMP




            // TEST THIS
            // log.Dispose();

        }
        #endregion
    }
}
