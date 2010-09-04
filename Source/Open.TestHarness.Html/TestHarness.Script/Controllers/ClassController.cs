using System;
using Open.Core;
using Open.TestHarness.Models;
using Open.TestHarness.Views;

namespace Open.TestHarness.Controllers
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
        }

        protected override void OnDisposed()
        {
            sidebarView.MethodList.MethodClicked -= OnMethodClicked;
            base.OnDisposed();
        }
        #endregion

        #region Properties
        private MethodInfo SelectedMethod { get { return sidebarView.MethodList.SelectedMethod; } }
        #endregion

        #region Event Handlers
        private void OnMethodClicked(object sender, EventArgs e)
        {
            MethodInfo method = SelectedMethod;
            if (method != null) method.Invoke();
        }
        #endregion

    }
}
