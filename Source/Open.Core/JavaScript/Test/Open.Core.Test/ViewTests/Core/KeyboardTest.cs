using System;

namespace Open.Core.Test.ViewTests.Core
{
    public class KeyboardTest
    {
        #region Head
        public void ClassInitialize()
        {
            // Wire up events.
            Keyboard.Keydown += delegate(object sender, KeyEventArgs args) { LogEvent("Keydown", args); };
            Keyboard.Keyup += delegate(object sender, KeyEventArgs args) { LogEvent("Keyup", args); };
        }
        #endregion

        #region Internal
        private static void LogEvent(string title, KeyEventArgs args)
        {
            Log.Event(title + " | KeyCode: " + args.Code + " | Key: " + args.Key.ToString());
        }
        #endregion
    }
}
