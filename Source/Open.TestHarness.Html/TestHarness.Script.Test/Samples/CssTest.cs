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
            view = AddControl(ControlDisplayMode.Center);
        }
        #endregion

        #region Methods : Tests
        public void Perspective()
        {
            view.Apply("transform", "perspective(500px);");
        }
        #endregion

        #region Internal
        private static MyCssView AddControl(ControlDisplayMode controlDisplayMode)
        {
            MyCssView view = new MyCssView(Html.CreateDiv(), controlDisplayMode);
            TestHarness.DisplayMode = controlDisplayMode;
            TestHarness.AddControl(view);
            return view;
        }
        #endregion
    }

    public class MyCssView : ViewBase
    {
        public MyCssView(jQueryObject container, ControlDisplayMode controlDisplayMode) : base(container)
        {
            container.Append(string.Format("Control [sizeMode:{0}]", controlDisplayMode.ToString()));
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
