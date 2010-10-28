using System;
using Open.Core;
using Open.Testing.Views;

namespace Open.Testing.Controllers
{
    /// <summary>Constroller for adding new test packages.</summary>
    public class AddPackageController : TestHarnessControllerBase
    {
        #region Head
        private const int MinHeight = 450;

        private readonly TestHarnessEvents events;
        private readonly ShellView shell;
        private readonly IButton addButton;
        private bool isShowing;

        /// <summary>Constructor.</summary>
        public AddPackageController()
        {
            // Setup initial conditions.
            events = Common.Events;
            shell = Common.Shell;
            addButton = Common.Buttons.AddPackage;

            // Wire up events.
            addButton.Click += OnAddPackageClick;
            AddPackageView.Showing += OnViewShowing;
            AddPackageView.Hidden += OnViewHidden;
            events.ClearControls += OnViewHidden;
        }


        /// <summary>Destroy.</summary>
        protected override void OnDisposed()
        {
            addButton.Click -= OnAddPackageClick;
            AddPackageView.Showing -= OnViewShowing;
            AddPackageView.Hidden -= OnViewHidden;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnAddPackageClick(object sender, EventArgs e)
        {
            addButton.IsEnabled = false;
            Show();
        }

        private void OnViewShowing(object sender, EventArgs e)
        {
            isShowing = true;
            SyncButtonState();
        }

        void OnViewHidden(object sender, EventArgs e)
        {
            if (!isShowing) return;
            isShowing = false;
            SyncButtonState();
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

            // Reveal the screen.
            AddPackageView.AddToTestHarness();
        }
        #endregion

        #region Internal
        private void SyncButtonState()
        {
            addButton.IsEnabled = !isShowing;
        }
        #endregion
    }
}
