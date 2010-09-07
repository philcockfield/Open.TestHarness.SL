﻿using jQueryApi;
using Open.Core;
using Open.Testing;

namespace Test.Samples
{
    public class ControlsTest
    {
        #region Methods : Tests
        public void Add_Control__ControlsSize() { AddControl(SizeMode.ControlsSize); }
        public void Add_Control__Fill() { AddControl(SizeMode.Fill); }
        public void Add_Control__FillWithMargin() { AddControl(SizeMode.FillWithMargin); }

        public void Clear()
        {
            TestHarness.ClearControls();
        }
        #endregion

        #region Internal
        private static void AddControl(SizeMode sizeMode)
        {
            IView view = new MyView(Html.CreateDiv(), sizeMode);
            TestHarness.AddControl(view, sizeMode);
        }
        #endregion
    }

    public class MyView : ViewBase
    {
        public MyView(jQueryObject container, SizeMode sizeMode)
        {
            Initialize(container);
            container.Append(string.Format("Control [{0}]", sizeMode.ToString()));
            container.CSS(Css.Background, "#f0ebc5");
            container.CSS(Css.Width, "300px");
            container.CSS(Css.Height, "200px");
        }
    }
}
