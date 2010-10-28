using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Testing.Internal;
using Open.Testing.Views;

namespace Open.Testing.Controllers
{
    /// <summary>Controls the 'Host Canvas' panel where controls under test are displayed.</summary>
    public class ControlHostController : TestHarnessControllerBase
    {
        #region Head
        private readonly jQueryObject divControlHost;
        private readonly ArrayList views = new ArrayList();
        private readonly TestHarnessEvents events;
        private bool canScroll = true;

        /// <summary>Constructor.</summary>
        public ControlHostController()
        {
            // Setup initial conditions.
            divControlHost = jQuery.Select(CssSelectors.ControlHost);
            events = Common.Events;

            // Wire up events.
            events.ControlAdded += OnControlAdded;
            events.ClearControls += OnClearControls;
            events.UpdateLayout += OnUpdateLayout;

            // Finish up.
            UpdateLayout();
        }

        protected override void OnDisposed()
        {
            // Dispose of views.
            Clear();

            // Unwire events.
            events.ControlAdded -= OnControlAdded;
            events.ClearControls -= OnClearControls;
            events.UpdateLayout -= OnUpdateLayout;

            // Finish up.
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnControlAdded(object sender, TestControlEventArgs e) { AddView(e.Control, e.HtmlElement, e.ControlDisplayMode); }
        private void OnClearControls(object sender, EventArgs e) { Clear(); }
        private void OnControlSizeChanged(object sender, EventArgs e) { UpdateLayout(); }
        private void OnUpdateLayout(object sender, EventArgs e) { UpdateLayout(); }
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the control host canvas can scroll.</summary>
        public bool CanScroll
        {
            get { return canScroll; }
            set
            {
                if (value == CanScroll) return;
                canScroll = value;
                Css.SetOverflow(divControlHost, value ? CssOverflow.Auto : CssOverflow.Hidden);
            }
        }
        #endregion

        #region Methods
        /// <summary>Clears all views.</summary>
        public void Clear()
        {
            // Unwire events.
            foreach (ControlWrapperView view in views)
            {
                if (view.Control != null) view.Control.SizeChanged -= OnControlSizeChanged;
            }

            // Dispose of all views.
            Helper.Collection.DisposeAndClear(views);
        }
        #endregion

        #region Internal
        private void AddView(IView control, jQueryObject htmlElement, ControlDisplayMode controlDisplayMode)
        {
            // Create and add the view.
            ControlWrapperView view = new ControlWrapperView(divControlHost, control, htmlElement, controlDisplayMode, views);
            views.Add(view);

            // Wire up events.
            if (control != null) control.SizeChanged += OnControlSizeChanged;

            // If there is more than one view, set all existing 'Fill' modes to 'FillWithMargin' (to avoid scrollbar issue).
            if (views.Count > 1)
            {
                foreach (ControlWrapperView item in views)
                {
                    if (item.DisplayMode == ControlDisplayMode.Fill) item.DisplayMode = ControlDisplayMode.FillWithMargin;
                }
            }

            // Finish up.
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            CanScroll = TestHarness.CanScroll;
            foreach (ControlWrapperView item in views)
            {
                item.UpdateLayout();
            }
        }
        #endregion
    }
}
