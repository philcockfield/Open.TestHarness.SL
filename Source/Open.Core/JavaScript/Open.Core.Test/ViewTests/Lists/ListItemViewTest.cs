using System;
using System.Collections;
using Open.Core.Lists;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Lists
{
    public class ListItemViewTest
    {
        #region Head

        private SampleListItem model;
        private ListItemView view;

        public void ClassInitialize()
        {
            model = new SampleListItem("My Item");
            view = new ListItemView(Html.CreateDiv(), model);

            view.Width = 300;

            TestHarness.AddControl(view);
        }
        public void ClassCleanup()
        {
            view.Dispose();
        }

        public void TestInitialize() { }
        public void TestCleanup() { }
        #endregion

        #region Tests
        public void Toggle__CanSelect()
        {
            model.CanSelect = !model.CanSelect;
            Log.Info("CanSelect: " + model.CanSelect);
        }

        public void Toggle__IsSelected()
        {
            model.IsSelected = !model.IsSelected;
            Log.Info("IsSelected: " + model.IsSelected);
        }
        #endregion
    }
}
