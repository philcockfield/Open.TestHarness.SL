using jQueryApi;

namespace Open.Testing.Views
{
    /// <summary>The root view of the application shell.</summary>
    public class ShellView : TestHarnessViewBase
    {
        #region Head
        private readonly SidebarView sidebar;
        private readonly ControlHostView controlHost;
        private readonly LogContainerView logContainer;

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing DIV.</param>
        public ShellView(jQueryObject container) : base(container)
        {
            // Create child views.
            sidebar = new SidebarView();
            controlHost = new ControlHostView();
            logContainer = new LogContainerView();
        }

        /// <summary>Destroy.</summary>
       protected override void OnDisposed()
        {
            sidebar.Dispose();
            controlHost.Dispose();
            logContainer.Dispose();
            base.OnDisposed();
        }
        #endregion

        #region Properties
        /// <summary>Gets the view for the SideBar.</summary>
        public SidebarView Sidebar { get { return sidebar; } }

        /// <summary>Gets the view for the ControlHost.</summary>
        public ControlHostView ControlHost{get { return controlHost; }}

        /// <summary>Gets the view for the Log container.</summary>
        public LogContainerView LogContainer { get { return logContainer; } }
        #endregion
    }
}
