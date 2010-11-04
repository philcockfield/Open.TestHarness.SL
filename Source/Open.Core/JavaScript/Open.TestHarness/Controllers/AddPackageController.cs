using System;
using Open.Core;
using Open.Testing.Models;
using Open.Testing.Views;

namespace Open.Testing.Controllers
{
    /// <summary>Constroller for adding new test packages.</summary>
    public class AddPackageController : TestHarnessControllerBase
    {
        #region Head
        private const int MinHeight = 450;

        private AddPackageView view;
        private readonly TestHarnessEvents events;
        private readonly ShellView shell;
        private readonly IButton showButton;
        private bool isShowing;

        /// <summary>Constructor.</summary>
        public AddPackageController()
        {
            // Setup initial conditions.
            events = Common.Events;
            shell = Common.Shell;
            showButton = Common.Buttons.AddPackage;

            // Wire up events.
            showButton.Click += OnShowClick;
            AddPackageView.Showing += OnViewShowing;
            AddPackageView.Hidden += OnViewHidden;
            events.ClearControls += OnViewHidden;
        }

        /// <summary>Destroy.</summary>
        protected override void OnDisposed()
        {
            showButton.Click -= OnShowClick;
            AddPackageView.Showing -= OnViewShowing;
            AddPackageView.Hidden -= OnViewHidden;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnShowClick(object sender, EventArgs e)
        {
            showButton.IsEnabled = false;
            Show();
        }

        private void OnViewShowing(object sender, EventArgs e)
        {
            isShowing = true;
            SyncButtonState();
        }

        private void OnViewHidden(object sender, EventArgs e)
        {
            if (!isShowing) return;
            isShowing = false;
            DestroyView();
            SyncButtonState();
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            if (!isShowing || view == null) return;
            if (!view.IsPopulated) return;
            events.FireAddPackage(view.GetPackageInfo());
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            if (!isShowing || view == null) return;
            view.SlideOff(delegate { DestroyView(); });
        }
        #endregion

        #region Methods
        /// <summary>Shows the 'Add Package' screen.</summary>
        public void Show()
        {
            // Change the log height (if required).
            if (shell.ControlHost.Height < MinHeight)
            {
                events.FireChangeLogHeight(shell.ControlHost.DivMain.GetHeight() - MinHeight);
            }

            // Insert a new view.
            view = AddPackageView.AddToTestHarness();
            view.AddButton.Click += OnAddClick;
            view.CancelButton.Click += OnCancelClick;
        }
        #endregion

        #region Internal
        private void SyncButtonState()
        {
            showButton.IsEnabled = !isShowing;
        }

        private void DestroyView()
        {
            if (view != null) view.Dispose();
            view = null;
            TestHarness.Reset();
        }
        #endregion
    }
}
