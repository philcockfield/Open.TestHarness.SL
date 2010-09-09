using jQueryApi;
using Open.Core;
using Open.Testing;

namespace Test.Samples
{
    public class CssTest
    {
        #region Head
       private MyCssView view;

        public void TestInitialize()
        {
            TestHarness.ClearControls();
            view = AddControl(SizeMode.ControlsSize);
        }
        #endregion

        #region Methods : Tests
        public void Perspective()
        {
            view.Apply("transform", "perspective(500px);");
        }
        #endregion

        #region Internal
        private static MyCssView AddControl(SizeMode sizeMode)
        {
            MyCssView view = new MyCssView(Html.CreateDiv(), sizeMode);
            TestHarness.SizeMode = sizeMode;
            TestHarness.AddControl(view);
            return view;
        }
        #endregion
    }

    public class MyCssView : ViewBase
    {
        public MyCssView(jQueryObject container, SizeMode sizeMode)
        {
            Initialize(container);
            container.Append(string.Format("Control [sizeMode:{0}]", sizeMode.ToString()));
            container.CSS(Css.Background, "#f0ebc5");
            container.CSS(Css.Width, "300px");
            container.CSS(Css.Height, "200px");

            container.CSS("-webkit-border-radius", "10px");
            container.CSS("-moz-border-radius", "10px");
            container.CSS("border-radius", "10px");
        }

        public void Apply(string prop, string value)
        {
            Container.CSS(prop, value);
            Log.Info(string.Format("{0}: {1}", prop, value));
        }
    }
}
