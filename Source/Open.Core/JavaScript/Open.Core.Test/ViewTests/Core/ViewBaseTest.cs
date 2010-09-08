using System;
using System.Collections;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Core
{
    public class ViewBaseTest
    {
        #region Head
        private SampleView view;

        public void ClassInitialize()
        {
            view = new SampleView(true);
            view.Background = "orange";

            Css.SetSize(view.Container, 50, 50); //TEMP 

            TestHarness.AddControl(view, SizeMode.ControlsSize);
        }
        public void ClassCleanup() { view.Dispose(); }

        public void TestInitialize() { }
        public void TestCleanup() { }
        #endregion

        #region Tests
        public void Toggle__IsVisible()
        {
            view.IsVisible = !view.IsVisible;
        }

        public void Toggle__Background()
        {
            view.Background = view.Background == "orange" ? "red" : "orange";
        }

        public void Toggle__Opacity()
        {
            view.Opacity = view.Opacity == 1 ? 0.3 : 1;
            Log.Info("Opacity: " + view.Opacity);
        }

        #endregion
    }
}
