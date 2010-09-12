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
            WriteProperties();
        }

        public void Toggle__IsSelected()
        {
            bool newValue = !model.IsSelected;
            if (!model.CanSelect && newValue == true) Log.Info("New value is true.  Because 'CanSelect' == false, the new value will not stick.");
            model.IsSelected = newValue;
            WriteProperties();
        }
        #endregion

        #region Internal
        private void WriteProperties()
        {
            Log.Info("IsSelected: " + model.IsSelected);
            Log.Info("CanSelect: " + model.CanSelect);
        }
        #endregion
    }
}
