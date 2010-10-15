using System;
using Open.Core;
using Open.Testing.Automation;
using Open.Testing.Models;
using Open.Testing.Views;

namespace Open.Testing.Controllers
{
    /// <summary>Controlls the selected test-class.</summary>
    public class ClassController : TestHarnessControllerBase
    {
        #region Head
        private readonly ClassInfo classInfo;
        private readonly SidebarView sidebarView;
        private readonly TestHarnessEvents events;

        /// <summary>Constructor.</summary>
        /// <param name="classInfo">The test-class that is under control.</param>
        public ClassController(ClassInfo classInfo)
        {
            // Setup initial conditions.
            this.classInfo = classInfo;
            sidebarView = Common.Shell.Sidebar;
            events = Common.Events;

            // Wire up events.
            events.MethodClicked += OnMethodClicked;
            sidebarView.MethodList.RunClick += OnRunClick;
            sidebarView.MethodList.RefreshClick += OnRefreshClick;
            // Finish up.
            Reset();
        }

        protected override void OnDisposed()
        {
            // Unwire events.
            events.MethodClicked -= OnMethodClicked;
            sidebarView.MethodList.RunClick -= OnRunClick;
            sidebarView.MethodList.RefreshClick -= OnRefreshClick;

            // Invoke the class-teardown method.
            if (classInfo.ClassCleanup != null) classInfo.ClassCleanup.Invoke();

            // Clear any visual controls from the screen.
            TestHarness.Reset();

            // Finish up.
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnMethodClicked(object sender, MethodEventArgs e)
        {
            InvokeSelectedMethod();
        }

        private void OnRunClick(object sender, EventArgs e)
        {
            RunAll();
        }

        private void OnRefreshClick(object sender, EventArgs e)
        {
            Log.Success(string.Format("Reload: <b>{0}</b>", classInfo.DisplayName));
            Reset();
        }
        #endregion

        #region Properties
        private MethodInfo SelectedMethod { get { return sidebarView.MethodList.SelectedMethod; } }
        #endregion

        #region Methods
        /// <summary>Initializes the current class.</summary>
        public void Reset()
        {
            // Setup initial conditions.
            TestHarness.Reset();

            // Invoke the class-setup method.
            if (classInfo.ClassInitialize != null) classInfo.ClassInitialize.Invoke();
            Log.NewSection();
        }

        /// <summary>Invokes the currently selected method (including pre/post TestInitialize and TestCleanup methods).</summary>
        /// <returns>True if the method was invoked, or False if there was not currently selected method.</returns>
        public bool InvokeSelectedMethod()
        {
            // Setup initial conditions.
            MethodInfo method = SelectedMethod;
            if (method == null) return false;

            // Invoke the method.
            Log.NewSection();
            method.Invoke();

            // Finish up.
            return true;
        }

        /// <summary>Runs all tests within the class.</summary>
        public void RunAll()
        {
            // Setup initial conditions.
            ClassTestRunner runner = new ClassTestRunner(classInfo);

            // Pause the output log.
            bool originalState = Log.IsActive;
            Log.IsActive = false;

            // Execute the tests.
            runner.Run();

            // Reset to original state.
            TestHarness.Reset();
            Reset(); 
            Log.IsActive = originalState; // Resume the log.
            Log.Clear();

            // Write results.
            runner.WriteResults(Log.Writer);
        }
        #endregion
    }
}
