using jQueryApi;
using Open.Testing;

namespace Test.Samples
{
    public class ControlsTest
    {
        public void Add_Control__Default()
        {
            jQueryObject div1 = TestHarness.AddControl(SizeMode.Control);
            div1.Append("Control 1");

            jQueryObject div2 = TestHarness.AddControl(SizeMode.Control);
            div2.Append("Control 2");
        }

        public void Clear()
        {
            TestHarness.ClearControls();
        }
    }
}
