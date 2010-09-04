using System;
using jQueryApi;
using Open.Core;
using Open.Core.Lists;
using Open.TestHarness.Models;

namespace Open.TestHarness.Views
{
    /// <summary>The list of tests.</summary>
    public class TestListView : ViewBase
    {
        #region Events
        /// <summary>Fires when each time a method in the list is clicked (see the 'SelectedMethod' property).</summary>
        public event EventHandler MethodClicked;
        private void FireMethodClicked(){if (MethodClicked != null) MethodClicked(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropTestClass = "TestClass";
        public const string PropSelectedMethod = "SelectedMethod";

        private readonly ListTreeView listView;
        private readonly ListItem rootNode;

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing div.</param>
        public TestListView(jQueryObject container)
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
            SelectedMethod = ((TestMethodListItem)sender).TestMethod;
            FireMethodClicked();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the test class the view is listing methods for.</summary>
        public TestClassInfo TestClass
        {
            get { return (TestClassInfo) Get(PropTestClass, null); }
            set
            {
                if (Set(PropTestClass, value, null))
                {
                    PopulateList(value);
                }
            }
        }

        /// <summary>Gets or sets the currently selected method..</summary>
        public TestMethodInfo SelectedMethod
        {
            get { return (TestMethodInfo) Get(PropSelectedMethod, null); }
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
        private void PopulateList(TestClassInfo testClass)
        {
            ClearChildren();
            if (testClass == null) return;
            foreach (TestMethodInfo method in testClass)
            {
                rootNode.AddChild(CreateListItem(method));
            }
        }

        private TestMethodListItem CreateListItem(TestMethodInfo method)
        {
            TestMethodListItem item = new TestMethodListItem(method);
            item.Click += OnItemClick;
            return item;
        }


        private void ClearChildren()
        {
            foreach (TestMethodListItem child in rootNode.Children)
            {
                child.Click -= OnItemClick;
            }
            rootNode.ClearChildren();
            SelectedMethod = null;
        }
        #endregion
    }
}
