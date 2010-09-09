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
        #endregion
    }
}
