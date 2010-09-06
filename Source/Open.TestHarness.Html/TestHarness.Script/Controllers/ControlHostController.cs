using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Testing.Internal;
using Open.Testing.Views;

namespace Open.Testing.Controllers
{
    /// <summary>Controls the 'Control Host' panel where test-controls are displayed.</summary>
    public class ControlHostController : TestHarnessControllerBase
    {
        #region Head
        private readonly jQueryObject divControlHost;
        private readonly ArrayList views = new ArrayList();
        private readonly TestHarnessEvents events;

        /// <summary>Constructor.</summary>
        public ControlHostController()
        {
            // Setup initial conditions.
            divControlHost = jQuery.Select(CssSelectors.ControlHost);
            events = Common.Events;

            // Wire up events.
            events.ControlAdded += OnControlAdded;
            events.ClearControls += OnClearControls;
        }

        protected override void OnDisposed()
        {
            // Dispose of views.
            Clear();

            // Unwire events.
            events.ControlAdded -= OnControlAdded;
            events.ClearControls -= OnClearControls;

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
