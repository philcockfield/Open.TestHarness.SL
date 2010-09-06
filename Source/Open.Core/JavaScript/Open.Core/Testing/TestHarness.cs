using System;
using jQueryApi;
using Open.Core;
using Open.Testing.Internal;

namespace Open.Testing
{
    /// <summary>Flags representing the various different sizing strategies for a hosted control.</summary>
    public enum SizeMode
    {
        /// <summary>The size is determined by the control.</summary>
        Control = 0,

        /// <summary>The control is sized to fill the host container.</summary>
        Fill = 1,

        /// <summary>The control is sized to fill the host container but is surrounded by some whitespace.</summary>
        FillWithMargin = 2,
    }


    /// <summary>
    ///     Shared functionality for working with the TestHarness 
    ///     (so that test assemblies don't have to reference to TestHarness project [and corresponding dependences]).
    /// </summary>
    public static class TestHarness
    {
        #region Head
        private static ITestHarnessEvents events;
        #endregion

        #region Properties
        private static ITestHarnessEvents Events
        {
            get { return events ?? (events = DiContainer.DefaultContainer.GetSingleton(typeof(ITestHarnessEvents)) as ITestHarnessEvents); }
        }
        #endregion

        #region Methods
        /// <summary>Registers a test-class with the harness.</summary>
        /// <param name="testClass">The type of the test class.</param>
        public static void RegisterClass(Type testClass)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(testClass)) return;
            if (Events == null) return;

            // Alert the test-harness via an event.
            TestClassEventArgs e = new TestClassEventArgs();
            e.TestClass = testClass;
            Events.FireTestClassRegistered(e);
        }

        /// <summary>Adds a control to the host canvas.</summary>
        /// <param name="sizeMode">The strategy used to size the control.</param>
        /// <returns>A DIV element to contain the control.</returns>
        public static jQueryObject AddControl(SizeMode sizeMode)
        {
            // Alert the test-harness via an event.
            TestControlEventArgs e = new TestControlEventArgs();
            e.SizeMode = sizeMode;
            e.ControlContainer = Html.CreateDiv();
            Events.FireControlAdded(e);

            // Finish up.
            return e.ControlContainer;
        }

        /// <summary>Clears all added controls from the host canvas.</summary>
        public static void ClearControls()
        {
            Events.FireClearControls();
        }
        #endregion
    }
}

namespace Open.Testing.Internal
{
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