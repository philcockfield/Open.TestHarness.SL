using jQueryApi;
using Open.Core;

namespace Open.Testing.Views
{
    /// <summary>The root view of the application shell.</summary>
    public class ShellView : TestHarnessViewBase
    {
        #region Head
        private readonly SidebarView sidebar;

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing DIV.</param>
        public ShellView(jQueryObject container) : base(container)
        {
            // Create child views.
            sidebar = new SidebarView(jQuery.Select(CssSelectors.Sidebar));
        }
        #endregion

        #region Properties
        /// <summary>Gets the view for the SideBar.</summary>
        public SidebarView Sidebar { get { return sidebar; } }
        #endregion
    }
}
