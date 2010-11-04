using System;
using jQueryApi;

namespace Open.Core
{
    /// <summary>Keyboard key codes.</summary>
    public enum Key
    {
        Unknown = 0,
        Enter = 13,
        Shift = 16,
        Ctrl = 17,
        Alt = 18,
        Esc = 27,
        Space = 32,
    }

    /// <summary>An event handler related to the keyboard.</summary>
    /// <param name="sender">The object that fired the event.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);

    /// <summary>Event arguments for the keyboard.</summary>
    public class KeyEventArgs
    {
        public KeyEventArgs(jQueryEvent e)
        {
            jQueryEvent = e;
            Code = int.Parse(e.Which);
            Key = Keyboard.ToKey(e);
        }

        public readonly jQueryEvent jQueryEvent;
        public readonly int Code;
        public readonly Key Key;
    }

    /// <summary>Global monitoring of keyboard state.</summary>
    public static class Keyboard
    {
        #region Event Handlers
        /// <summary>Fires when a key is depressed.</summary>
        public static event KeyEventHandler Keydown;
        private static void FireKeydown(jQueryEvent e) { if (Keydown != null) Keydown(typeof(Keyboard), new KeyEventArgs(e)); }

        /// <summary>Fires when a key is released.</summary>
        public static event KeyEventHandler Keyup;
        private static void FireKeyup(jQueryEvent e) { if (Keyup != null) Keyup(typeof(Keyboard), new KeyEventArgs(e)); }
        #endregion

        #region Head
        private static bool isShiftPressed;
        private static bool isCtrlPressed;
        private static bool isAltPressed;

        /// <summary>Constructor.</summary>
        static Keyboard()
        {
            jQuery.Document.Keydown(delegate(jQueryEvent e)
                                        {
                                            if (IsKey(e, Key.Shift)) isShiftPressed = true;
                                            if (IsKey(e, Key.Ctrl)) isCtrlPressed = true;
                                            if (IsKey(e, Key.Alt)) isAltPressed = true;
                                            FireKeydown(e);
                                        });
            jQuery.Document.Keyup(delegate(jQueryEvent e)
                                      {
                                          if (IsKey(e, Key.Shift)) isShiftPressed = false;
                                          if (IsKey(e, Key.Ctrl)) isCtrlPressed = false;
                                          if (IsKey(e, Key.Alt)) isAltPressed = false;
                                          FireKeyup(e);
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

        #region Methods
        /// <summary>Determines whether the specified code matches the given event.</summary>
        /// <param name="e">The jQuery keyboard event.</param>
        /// <param name="keyCode">The code to match.</param>
        public static bool IsKey(jQueryEvent e, Key keyCode)
        {
            return (bool)Script.Literal("e.which === {0}", (int)keyCode);
        }

        /// <summary>Converts the given key event to the corresponding enum value (if represented in the enum).</summary>
        /// <param name="e">The jQuery keyboard event.</param>
        /// <returns>The mapped keycode ('Unknown' if there is no mapping in the enum).</returns>
        public static Key ToKey(jQueryEvent e)
        {
            if (IsKey(e, Key.Enter)) return Key.Enter;
            if (IsKey(e, Key.Shift)) return Key.Shift;
            if (IsKey(e, Key.Ctrl)) return Key.Ctrl;
            if (IsKey(e, Key.Alt)) return Key.Alt;
            if (IsKey(e, Key.Esc)) return Key.Esc;
            if (IsKey(e, Key.Space)) return Key.Space;

            return Key.Unknown;
        }
        #endregion
    }
}
