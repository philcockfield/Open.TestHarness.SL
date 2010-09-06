using System;
using jQueryApi;

namespace Open.Testing.Internal
{
    public static class TestHarnessEvents
    {
        /// <summary>Fires when a test class is registered.</summary>
        public static event TestClassHandler TestClassRegistered;
        internal static void FireTestClassRegistered(TestClassEventArgs e) { if (TestClassRegistered != null) TestClassRegistered(typeof(TestHarnessEvents), e); }

        /// <summary>Fires when a control is added to the host canvas.</summary>
        public static event TestControlHandler ControlAdded;
        internal static void FireControlAdded(TestControlEventArgs e) { if (ControlAdded != null) ControlAdded(typeof(TestHarnessEvents), e); }

        /// <summary>Fires when the host canvas is to be cleared of controls.</summary>
        public static event EventHandler ClearControls;
        internal static void FireClearControls() { if (ClearControls != null) ClearControls(typeof(TestHarnessEvents), new EventArgs()); }
    }

    public delegate void TestClassHandler(object sender, TestClassEventArgs e);
    public class TestClassEventArgs
    {
        public Type TestClass;
    }

    public delegate void TestControlHandler(object sender, TestControlEventArgs e);
    public class TestControlEventArgs
    {
        public SizeMode SizeMode;
        public jQueryObject ControlContainer;
    }
}