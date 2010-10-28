using System;
using jQueryApi;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.HtmlPrimitive
{
    public class SpacingTest
    {
        #region Head
        private Spacing spacing;
        private int adjustBy;
        private jQueryObject outer;
        private jQueryObject inner;

        public void ClassInitialize()
        {
            outer = CreateDiv(Color.Red(0.2));
            Css.SetSize(outer, 100, 100);

            inner = CreateDiv("red");
            inner.AppendTo(outer);

            spacing = new Spacing().Sync(inner, delegate(Edge edge, int value) { return value += adjustBy; });
            //            spacing = Spacing.Sync(inner);
            Uniform_Padding();

            TestHarness.AddElement(outer);
        }
        #endregion

        #region Tests
        public void No_Padding() { spacing.Change(0); Write_Properties(); }
        public void Reduce_Padding() { spacing.Change(1, 2, 3, 4); Write_Properties(); }
        public void Increase_Padding() { spacing.Change(10, 20, 30, 40); Write_Properties(); }
        public void Uniform_Padding() { spacing.Change(20); Write_Properties(); }
        public void X__Y_Padding() { spacing.Change(30, 5); Write_Properties(); }

        public void UpdateLayout() { spacing.UpdateLayout(); }

        public void Interceptor__Adjust_by_5() { adjustBy = 5; UpdateLayout(); }
        public void Interceptor__Adjust_by_0() { adjustBy = 0; UpdateLayout(); }

        public void Toggle__Size()
        {
            if (outer.GetWidth() == 100)
            {
                Css.SetSize(outer, 200, 300);                
            }
            else
            {
                Css.SetSize(outer, 100, 100);                
            }
            TestHarness.UpdateLayout();
        }

        public void Write_Properties()
        {
            Log.Info("Spacing: " + spacing.ToString());
            Log.Info("outer.Size: " + outer.GetWidth() + " x " + outer.GetHeight());
            Log.Info("inner.Size: " + inner.GetWidth() + " x " + inner.GetHeight());
            Log.Info("Adjustment value used in 'onBeforeSync': " + adjustBy);
        }
        #endregion

        #region Internal
        private static jQueryObject CreateDiv(string color)
        {
            jQueryObject div = Html.CreateDiv();
            div.CSS(Css.Background, color);
            Css.RoundedCorners(div, 5);
            return div;
        }
        #endregion
    }
}
