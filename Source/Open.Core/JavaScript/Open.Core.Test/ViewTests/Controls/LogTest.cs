using System;
using System.Collections;

namespace Open.Core.Test.ViewTests.Controls
{
    public class LogTest
    {
        #region Head
        public void ClassInitialize() { }
        public void ClassCleanup() { }

        public void TestInitialize() { }
        public void TestCleanup() { }
        #endregion

        #region Tests
        public void Log__Info() { Log.Info("Info"); }
        public void Log__Debug() { Log.Debug("Debug"); }
        public void Log__Warning() { Log.Warning("Warning"); }
        public void Log__Error() { Log.Error("Error"); }
        public void Log__Null() { Log.Info(null); }
        public void Log__Empty_String() { Log.Info(""); }

        public void LineBreak(){ Log.LineBreak();}
        public void NewSection() { Log.NewSection(); }

        public void Log_After_Delay()
        {
            DelayedAction.Invoke(0.5, delegate { Log.Info("Logged after delay"); });
        }

        public void Clear() { Log.Clear(); }

        public void Sample1()
        {
            Log.Info("This is a log entry");
            Log.Info("Another entry within the same test.");
        }

        public void Sample2()
        {
            Log.Info("This entry came from another test (Sample2).");
            Log.Info("Note the section divider visually showing that these entries were emitted from different tests.");
        }

        #endregion
    }
}
