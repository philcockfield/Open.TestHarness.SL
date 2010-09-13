using Open.Core;
using Open.Testing;

namespace Test.Samples
{
    public class Canvas_Control_Insertion_Test
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
            IView control = new MyView(displayMode);
            TestHarness.DisplayMode = displayMode;
            TestHarness.AddControl(control);
            Log.Info("Control added. DisplayMode: " + (displayMode.ToString()));
        }
        #endregion
    }

    public class MyView : ViewBase
    {
        public MyView(ControlDisplayMode controlDisplayMode) 
        {
            Container.Append(string.Format("Control [sizeMode:{0}]", controlDisplayMode.ToString()));
            Container.CSS(Css.Background, "#f0ebc5");
            Container.CSS(Css.Width, "300px");
            Container.CSS(Css.Height, "200px");

            Container.CSS("-webkit-border-radius", "10px");
            Container.CSS("-moz-border-radius", "10px");
            Container.CSS("border-radius", "10px");
        }
    }
}
