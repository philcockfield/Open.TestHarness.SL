using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Lists;
using Open.Testing.Controllers;

namespace Open.Testing.Views
{
    /// <summary>The view for the side-bar.</summary>
    public class SidebarView : TestHarnessViewBase
    {
        #region Head
        public const double SlideDuration = 0.2; // secs.
        public const string PropIsTestListVisible = "IsTestListVisible";

        private readonly ListTreeView rootList;
        private readonly ListTreeBackController backController;
        private readonly MethodListView methodList;
        private readonly MethodListHeightController methodListHeightController;

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing DIV.</param>
        public SidebarView(jQueryObject container)
        {
            // Setup initial conditions.
            Initialize(container);

            // Create the list-tree.
            rootList = new ListTreeView(jQuery.Select(CssSelectors.SidebarRootList));
            rootList.SlideDuration = SlideDuration;

            // Create the test-list.
            methodList = new MethodListView(jQuery.Select(CssSelectors.MethodList));

            // Create controllers.
            backController = new ListTreeBackController(
                    rootList,
                    jQuery.Select(CssSelectors.SidebarToolbar),
                    jQuery.Select(CssSelectors.BackMask));
            methodListHeightController = new MethodListHeightController(this);

            // Wire up events.
            GlobalEvents.WindowResizeComplete += OnSizeChanged;
            GlobalEvents.PanelResizeComplete += OnSizeChanged;

            // Finish up.
            UpdateLayout();
        }

        protected override void OnDisposed()
        {
            // Unwire events.
            GlobalEvents.WindowResizeComplete -= OnSizeChanged;
            GlobalEvents.PanelResizeComplete -= OnSizeChanged;

            // Dispose of views.
            rootList.Dispose();

            // Dispose of controllers.
            backController.Dispose();
            methodListHeightController.Dispose();

            // Finish up.
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnSizeChanged(object sender, EventArgs e)
        {
            UpdateLayout();
        }
        #endregion

        #region Properties
        /// <summary>Gets the main List-Tree view.</summary>
        public ListTreeView RootList { get { return rootList; } }

        /// <summary>Gets the Test-List view.</summary>
        public MethodListView MethodList{get { return methodList; }}

        /// <summary>Gets or sets whether the TestList panel is visible.</summary>
        public bool IsMethodListVisible
        {
            get { return (bool) Get(PropIsTestListVisible, false); }
            set { Set(PropIsTestListVisible, value, false); } 
        }
        #endregion

        #region Methods
        /// <summary>Refreshes the visual state.</summary>
        public void UpdateLayout()
        {
            methodListHeightController.UpdateLayout();
            SyncRootListHeight();
        }
        #endregion

        #region Internal
        private void SyncRootListHeight()
        {
            RootList.Container.CSS(Css.Bottom, MethodList.Container.GetHeight() + Css.Px);
        }
        #endregion
    }
}
