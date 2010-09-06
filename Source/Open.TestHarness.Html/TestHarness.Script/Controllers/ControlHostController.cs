using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Testing.Internal;
using Open.Testing.Views;

namespace Open.Testing.Controllers
{
    /// <summary>Controls the 'Control Host' panel where test-controls are displayed.</summary>
    public class ControlHostController : ControllerBase
    {
        #region Head
        private readonly jQueryObject divControlHost;
        private readonly ArrayList views = new ArrayList();


        /// <summary>Constructor.</summary>
        public ControlHostController()
        {
            // Setup initial conditions.
            divControlHost = jQuery.Select(CssSelectors.ControlHost);

            // Wire up events.
            TestHarnessEvents.ControlAdded += OnControlAdded;
            TestHarnessEvents.ClearControls += OnClearControls;
        }

        protected override void OnDisposed()
        {
            // Dispose of views.
            Clear();

            // Unwire events.
            TestHarnessEvents.ControlAdded -= OnControlAdded;
            TestHarnessEvents.ClearControls -= OnClearControls;

            // Finish up.
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnControlAdded(object sender, TestControlEventArgs e) { AddView(e.ControlContainer); }
        private void OnClearControls(object sender, EventArgs e) { Clear(); }
        #endregion

        #region Methods
        /// <summary>Clears all views.</summary>
        public void Clear()
        {
            Helper.Collection.DisposeAndClear(views);
        }
        #endregion

        #region Internal
        private void AddView(jQueryObject controlContainer)
        {
            ControlWrapperView view = new ControlWrapperView(divControlHost, controlContainer);
            views.Add(view);
        }
        #endregion
    }
}
