using jQueryApi;
using Open.Core;
using Open.Testing;

namespace Test.Samples
{
    public class Control_Insertion_Test
    {
        #region Methods : Tests
        public void Add_Control__None() { AddControl(ControlDisplayMode.None); }
        public void Add_Control__Center() { AddControl(ControlDisplayMode.Center); }
        public void Add_Control__Fill() { AddControl(ControlDisplayMode.Fill); }
        public void Add_Control__FillWithMargin() { AddControl(ControlDisplayMode.FillWithMargin); }

        public void Reset()
        {
            TestHarness.Reset();
            Log.Title("Reset");
            Log.Info("SizeMode: " + TestHarness.DisplayMode.ToString());
        }
        #endregion

        #region Internal
        private static void AddControl(ControlDisplayMode displayMode)
        {
            IView view = new MyView(Html.CreateDiv(), displayMode);
            TestHarness.DisplayMode = displayMode;
            TestHarness.AddControl(view);
            Log.Info("Control added. DisplayMode: " + displayMode.ToString());
        }
        #endregion
    }

    public class MyView : ViewBase
    {
        public MyView(jQueryObject container, ControlDisplayMode controlDisplayMode)
        {
            Initialize(container);
            container.Append(string.Format("Control [sizeMode:{0}]", controlDisplayMode.ToString()));
            container.CSS(Css.Background, "#f0ebc5");
            container.CSS(Css.Width, "300px");
            container.CSS(Css.Height, "200px");

            container.CSS("-webkit-border-radius", "10px");
            container.CSS("-moz-border-radius", "10px");
            container.CSS("border-radius", "10px");
        }
    }
}
