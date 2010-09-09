using System;
using jQueryApi;
using Open.Core;
using Open.Core.Controls;
using Open.Core.Lists;
using Open.Testing.Models;

namespace Open.Testing.Views
{
    /// <summary>The list of tests.</summary>
    public class MethodListView : TestHarnessViewBase
    {
        #region Head
        public const string PropClassInfo = "ClassInfo";
        public const string PropSelectedMethod = "SelectedMethod";

        private readonly ListTreeView listView;
        private readonly ListItem rootNode;
        private readonly TestHarnessEvents events;
        private readonly SystemButton btnRun;

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing div.</param>
        public MethodListView(jQueryObject container)
        {
            // Setup initial conditions.
            Initialize(container);
            events = Common.Events;

            // Create the list-tree.
            listView = new ListTreeView(jQuery.Select(CssSelectors.MethodListContent));
            listView.SlideDuration = SidebarView.SlideDuration;

            // Construct the data-model root.
            rootNode = new ListItem();
            listView.RootNode = rootNode;

            // Construct buttons.
            btnRun = new SystemButton();
            btnRun.Html = "Run"; // TODO - Get from string resources.
            btnRun.FontSize = "8pt";
            btnRun.Padding = "3px 8px";
            btnRun.Insert(CssSelectors.MethodListRunButton, InsertMode.Replace);

            // Wire up events.
            btnRun.Click += delegate { Log.Info("!! Run clicked"); }; // TODO - Run all tests in method list.
        }
        #endregion

        #region Event Handlers
        private void OnItemClick(object sender, EventArgs e)
        {
            SelectedMethod = ((MethodListItem)sender).Method;
            events.FireMethodClicked(SelectedMethod);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the test class the view is listing methods for.</summary>
        public ClassInfo ClassInfo
        {
            get { return (ClassInfo) Get(PropClassInfo, null); }
            set
            {
                if (Set(PropClassInfo, value, null))
                {
                    PopulateList(value);
                }
            }
        }

        /// <summary>Gets or sets the currently selected method..</summary>
        public MethodInfo SelectedMethod
        {
            get { return (MethodInfo) Get(PropSelectedMethod, null); }
            set { Set(PropSelectedMethod, value, null); } 
        }

        /// <summary>Gets the offset height of the items within the list and the title bar.</summary>
        public int OffsetHeight { get { return listView.ContentHeight + DivTitleBar.GetHeight() + 1; } }

        private jQueryObject DivTitleBar { get { return Container.Children(CssSelectors.MethodListTitlebar); } }
        #endregion

        #region Methods
        /// <summary>Updates the visual state of the control.</summary>
        public void UpdateLayout()
        {
            listView.UpdateLayout();
        }
        #endregion

        #region Internal
        private void PopulateList(ClassInfo classInfo)
        {
            ClearChildren();
            if (classInfo == null) return;
            foreach (MethodInfo method in classInfo)
            {
                rootNode.AddChild(CreateListItem(method));
            }
        }

        private MethodListItem CreateListItem(MethodInfo methodInfo)
        {
            MethodListItem item = new MethodListItem(methodInfo);
            item.Click += OnItemClick;
            return item;
        }

        private void ClearChildren()
        {
            foreach (MethodListItem child in rootNode.Children)
            {
                child.Click -= OnItemClick;
            }
            rootNode.ClearChildren();
            SelectedMethod = null;
        }
        #endregion
    }
}
