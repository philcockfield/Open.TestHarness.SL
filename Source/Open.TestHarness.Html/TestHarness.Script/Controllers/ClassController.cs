using System;
using Open.Core;
using Open.Testing.Models;
using Open.Testing.Views;

namespace Open.Testing.Controllers
{
    /// <summary>Controlls the selected test-class.</summary>
    public class ClassController : ControllerBase
    {
        #region Head
        private readonly ClassInfo classInfo;
        private readonly SidebarView sidebarView;

        /// <summary>Constructor.</summary>
        /// <param name="classInfo">The test-class that is under control.</param>
        /// <param name="sidebarView">The Sidebar control.</param>
        public ClassController(ClassInfo classInfo, SidebarView sidebarView)
        {
            // Setup initial conditions.
            this.classInfo = classInfo;
            this.sidebarView = sidebarView;

            // Wire up events.
            sidebarView.MethodList.MethodClicked += OnMethodClicked;

            // Invoke the class-setup method.
            if (classInfo.ClassInitialize != null) classInfo.ClassInitialize.Invoke();
        }

        protected override void OnDisposed()
        {
            // Unwire events.
            sidebarView.MethodList.MethodClicked -= OnMethodClicked;

            // Invoke the class-teardown method.
            if (classInfo.ClassCleanup != null) classInfo.ClassCleanup.Invoke();

            // Clear any visual controls from the screen.
            TestHarness.ClearControls();

            // Finish up.
            base.OnDisposed();
        }
        #endregion

        #region Properties
        private MethodInfo SelectedMethod { get { return sidebarView.MethodList.SelectedMethod; } }
        #endregion

        #region Event Handlers
        private void OnMethodClicked(object sender, EventArgs e)
        {
            InvokeSelectedMethod();
        }
        #endregion

        #region Methods
        /// <summary>Invokes the currently selected method (including pre/post TestInitialize and TestCleanup methods).</summary>
        /// <returns>True if the method was invoked, or False if there was not currently selected method.</returns>
        public bool InvokeSelectedMethod()
        {
            // Setup initial conditions.
            MethodInfo method = SelectedMethod;
            if (method == null) return false;

            // Invoke the method.
            method.Invoke();

            // Finish up.
            return true;
        }
        #endregion
    }
}
