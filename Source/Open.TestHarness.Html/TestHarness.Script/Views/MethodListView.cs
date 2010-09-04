using System;
using jQueryApi;
using Open.Core;
using Open.Core.Lists;
using Open.TestHarness.Models;

namespace Open.TestHarness.Views
{
    /// <summary>The list of tests.</summary>
    public class MethodListView : ViewBase
    {
        #region Events
        /// <summary>Fires when each time a method in the list is clicked (see the 'SelectedMethod' property).</summary>
        public event EventHandler MethodClicked;
        private void FireMethodClicked(){if (MethodClicked != null) MethodClicked(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropClassInfo = "ClassInfo";
        public const string PropSelectedMethod = "SelectedMethod";

        private readonly ListTreeView listView;
        private readonly ListItem rootNode;

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing div.</param>
        public MethodListView(jQueryObject container)
        {
            // Setup initial conditions.
            Initialize(container);

            // Create the list-tree.
            listView = new ListTreeView(jQuery.Select(CssSelectors.TestListContent));
            listView.SlideDuration = SidebarView.SlideDuration;

            // Construct the data-model root.
            rootNode = new ListItem();
            listView.RootNode = rootNode;

            //TEMP 
            MethodClicked += delegate
                                 {
                                     Log.Debug("!! Method Clicked: " + SelectedMethod.DisplayName);
                                 };
        }
        #endregion

        #region Event Handlers
        private void OnItemClick(object sender, EventArgs e)
        {
            SelectedMethod = ((MethodListItem)sender).Method;
            FireMethodClicked();
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

        #endregion

        #region Methods
        /// <summary>Updates the visual state of the control.</summary>
        public void UpdateLayout()
        {
            listView.UpdateLayout();
        }
        #endregion

        #region Internal
        private void PopulateList(ClassInfo @class)
        {
            ClearChildren();
            if (@class == null) return;
            foreach (MethodInfo method in @class)
            {
                rootNode.AddChild(CreateListItem(method));
            }
        }

        private MethodListItem CreateListItem(MethodInfo method)
        {
            MethodListItem item = new MethodListItem(method);
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
