using jQueryApi;
using Open.Core;
using Open.Core.Lists;
using Open.TestHarness.Models;

namespace Open.TestHarness.Views
{
    /// <summary>The list of tests.</summary>
    public class TestListView : ViewBase
    {
        #region Head
        public const string PropTestClass = "TestClass";

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
            rootNode.ClearChildren();
            if (testClass == null) return;
            foreach (TestMethodInfo method in testClass)
            {
                rootNode.AddChild(new TestMethodListItem(method));
            }
        }
        #endregion
    }
}
