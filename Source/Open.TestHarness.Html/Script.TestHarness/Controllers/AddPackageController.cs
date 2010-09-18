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

        /// <summary>Constructor.</summary>
        public AddPackageController()
        {
            // Setup initial conditions.
            events = Common.Events;
            shell = Common.Shell;

            // Wire up events.
            events.AddPackageClick += FireAddPackageClick;
        }

        /// <summary>Destroy.</summary>
        protected override void OnDisposed()
        {
            events.AddPackageClick -= FireAddPackageClick;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void FireAddPackageClick(object sender, EventArgs e)
        {
            Show();
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
    }
}
