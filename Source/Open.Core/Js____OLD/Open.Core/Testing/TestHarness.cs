using System;
using System.Runtime.CompilerServices;
using jQueryApi;
using Open.Core;
using Open.Testing.Internal;

namespace Open.Testing
{
    /// <summary>Flags representing the various different sizing strategies for a hosted control.</summary>
    public enum ControlDisplayMode
    {
        /// <summary>No sizing or positioning is applied to the control.</summary>
        None = 0,

        /// <summary>The size is determined by the control, which is centered within the canvas.</summary>
        Center = 1,

        /// <summary>The control is sized to fill the host container.</summary>
        Fill = 2,

        /// <summary>The control is sized to fill the host container but is surrounded by some whitespace.</summary>
        FillWithMargin = 3,
    }


    /// <summary>
    ///     Shared functionality for working with the TestHarness 
    ///     (so that test assemblies don't have to reference to TestHarness project [and corresponding dependences]).
    /// </summary>
    public static class TestHarness
    {
        #region Head
        private static ITestHarnessEvents events;
        private static ControlDisplayMode displayMode = ControlDisplayMode.Center;
        private static bool canScroll = true;
        #endregion

        #region Properties
        private static ITestHarnessEvents Events
        {
            get { return events ?? (events = DiContainer.DefaultContainer.GetSingleton(typeof(ITestHarnessEvents)) as ITestHarnessEvents); }
        }

        /// <summary>Gets or sets the size strategy for displaying added controls/HTML.</summary>
        public static ControlDisplayMode DisplayMode
        {
            get { return displayMode; }
            set { displayMode = value; }
        }

        /// <summary>Gets or sets whether the control host canvas can scroll.</summary>
        public static bool CanScroll
        {
            get { return canScroll; }
            set
            {
                canScroll = value;
                Events.FireUpdateLayout();
            }
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
        /// <returns>The added control (fluent).</returns>
        public static IView AddControl(IView control)
        {
            if (Script.IsNullOrUndefined(control)) throw new Exception("A visual control was not specified.");
            FireControlAdded(control, control.Container);
            return control;
        }

        /// <summary>Adds an DIV element to the host canvas.</summary>
        /// <returns>The added DIV element.</returns>
        [AlternateSignature]
        public static extern jQueryObject AddElement();

        /// <summary>Adds an HTML element to the host canvas.</summary>
        /// <param name="element">The HTML content of the control.</param>
        /// <returns>The added element (fluent).</returns>
        public static jQueryObject AddElement(jQueryObject element)
        {
            if (Script.IsNullOrUndefined(element)) element = Html.CreateDiv();
            FireControlAdded(null, element);
            return element;
        }

        /// <summary>Adds the view created from the given model to the host canvas.</summary>
        /// <param name="model">The model to add.</param>
        /// <returns>The view created for the model.</returns>
        public static IView AddModel(IViewFactory model)
        {
            if (Script.IsNullOrUndefined(model)) throw new Exception("A model was not specified.");
            IView control = model.CreateView();
            AddControl(control);
            return control;
        }

        /// <summary>Clears the controls from the host canvas and resets to orginal state.</summary>
        public static void Reset()
        {
            displayMode = ControlDisplayMode.Center;
            Events.FireClearControls();
            CanScroll = true;
        }

        /// <summary>Forces the display canvas to run it's layout routine.</summary>
        public static void UpdateLayout()
        {
            Events.FireUpdateLayout();
        }
        #endregion

        #region Internal
        private static void FireControlAdded(IView control, jQueryObject element)
        {
            TestControlEventArgs e = new TestControlEventArgs();
            e.Control = control;
            e.HtmlElement = element;
            e.ControlDisplayMode = DisplayMode;
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
        public ControlDisplayMode ControlDisplayMode;
        public jQueryObject HtmlElement;
        public IView Control;
    }
}