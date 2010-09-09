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
        ControlsSize = 0,

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
        private static SizeMode sizeMode = SizeMode.ControlsSize;
        #endregion

        #region Properties
        private static ITestHarnessEvents Events
        {
            get { return events ?? (events = DiContainer.DefaultContainer.GetSingleton(typeof(ITestHarnessEvents)) as ITestHarnessEvents); }
        }

        /// <summary>Gets or sets the size strategy for displaying added controls/HTML.</summary>
        public static SizeMode SizeMode
        {
            get { return sizeMode; }
            set { sizeMode = value; }
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

        /// <summary>Adds a visual control to the host canvas.</summary>
        /// <param name="control">The control to add.</param>
        /// <returns>A DIV element to contain the control.</returns>
        public static void AddControl(IView control)
        {
            if (control == null) throw new Exception("A visual control was not specified.");
            FireControlAdded(control, control.Container);
        }

        /// <summary>Adds an HTML element to the host canvas.</summary>
        /// <param name="element">The HTML content of the control.</param>
        /// <returns>A DIV element to contain the control.</returns>
        public static void  AddHtml(jQueryObject element)
        {
            if (element == null) throw new Exception("An HTML element was not specified.");
            FireControlAdded(null, element);
        }

        /// <summary>Clears the controls from the host canvas and resets to orginal state.</summary>
        public static void Reset()
        {
            ClearControls();
            sizeMode = SizeMode.ControlsSize;
        }

        /// <summary>Clears all added controls from the host canvas.</summary>
        public static void ClearControls()
        {
            Events.FireClearControls();
        }
        #endregion

        #region Internal
        private static void FireControlAdded(IView control, jQueryObject element)
        {
            TestControlEventArgs e = new TestControlEventArgs();
            e.Control = control;
            e.SizeMode = SizeMode;
            e.HtmlElement = element;
            Events.FireControlAdded(e);
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
        public jQueryObject HtmlElement;
        public IView Control;
    }
}