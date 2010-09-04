using jQueryApi;
using Open.Core.Helpers;

namespace Open.Core
{
    /// <summary>Keyboard key codes.</summary>
    public enum Key
    {
        Shift = 16,
        Ctrl = 17,
        Alt = 18,
    }

    /// <summary>Global monitoring of keyboard state.</summary>
    public static class Keyboard
    {
        #region Head
        private static bool isShiftPressed;
        private static bool isCtrlPressed;
        private static bool isAltPressed;

        /// <summary>Constructor.</summary>
        static Keyboard()
        {
            JQueryHelper h = Helper.JQuery;
            jQuery.Document.Keydown(delegate(jQueryEvent e)
                                        {
                                            if (h.IsKey(e, Key.Shift)) isShiftPressed = true;
                                            if (h.IsKey(e, Key.Ctrl)) isCtrlPressed = true;
                                            if (h.IsKey(e, Key.Alt)) isAltPressed = true;
                                        });
            jQuery.Document.Keyup(delegate(jQueryEvent e)
                                      {
                                          if (h.IsKey(e, Key.Shift)) isShiftPressed = false;
                                          if (h.IsKey(e, Key.Ctrl)) isCtrlPressed = false;
                                          if (h.IsKey(e, Key.Alt)) isAltPressed = false;
                                      });
        }
        #endregion

        #region Properties
        /// <summary>Gets whether the SHIFT key is currently pressed.</summary>
        public static bool IsShiftPressed { get { return isShiftPressed; } }

        /// <summary>Gets whether the CTRL key is currently pressed.</summary>
        public static bool IsCtrlPressed { get { return isCtrlPressed; } }

        /// <summary>Gets whether the ALT key is currently pressed.</summary>
        public static bool IsAltPressed { get { return isAltPressed; } }
        #endregion
    }
}
