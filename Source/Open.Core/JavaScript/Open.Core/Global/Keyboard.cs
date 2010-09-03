using jQueryApi;
using Open.Core.Helpers;

namespace Open.Core
{
    /// <summary>Global monitoring of keyboard state.</summary>
    public static class Keyboard
    {
        #region Head
        private const int keyShift = 16;
        private const int keyCtrl = 17;
        private const int keyAlt = 18;

        private static bool isShiftPressed;
        private static bool isCtrlPressed;
        private static bool isAltPressed;

        /// <summary>Constructor.</summary>
        static Keyboard()
        {
            JQueryHelper helper = Helper.JQuery;
            jQuery.Document.Keydown(delegate(jQueryEvent e)
                                        {
                                            if (helper.IsKey(e, keyShift)) isShiftPressed = true;
                                            if (helper.IsKey(e, keyCtrl)) isCtrlPressed = true;
                                            if (helper.IsKey(e, keyAlt)) isAltPressed = true;
                                        });
            jQuery.Document.Keyup(delegate(jQueryEvent e)
                                      {
                                          if (helper.IsKey(e, keyShift)) isShiftPressed = false;
                                          if (helper.IsKey(e, keyCtrl)) isCtrlPressed = false;
                                          if (helper.IsKey(e, keyAlt)) isAltPressed = false;
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
