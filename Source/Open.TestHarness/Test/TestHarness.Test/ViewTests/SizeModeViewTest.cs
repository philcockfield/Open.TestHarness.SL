using Open.Core.Common;
using Open.Core.UI.Controls;

namespace Open.TestHarness.Test.ViewTests
{
    [ViewTestClass]
    public class SizeModeViewTest
    {
        #region Head
        [ViewTest(Default = true, IsVisible = false, SizeMode = TestControlSize.Fill)]
        public void Initialize(Placeholder control)
        {
        }
        #endregion

        #region Tests
        [ViewTest(SizeMode = TestControlSize.Manual)]
        public void SizeMode__Manual(Placeholder control)
        {
        }

        [ViewTest(SizeMode = TestControlSize.Fill)]
        public void SizeMode__Fill(Placeholder control)
        {
        }
        #endregion
    }
}
