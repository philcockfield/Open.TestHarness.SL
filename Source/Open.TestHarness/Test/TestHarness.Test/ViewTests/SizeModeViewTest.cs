using System.Linq;
using System.Collections.Generic;
using Microsoft.Silverlight.Testing;
using Open.Core.Common;
using Open.Core.UI.Controls;

namespace Open.TestHarness.Test.ViewTests
{
    [ViewTestClass(SizeMode = TestControlSize.FillWithMargin)]
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

        [ViewTest(SizeMode = TestControlSize.FillWithMargin)]
        public void SizeMode__FillWithMargin(Placeholder control)
        {
        }

        [ViewTest]
        public void SizeMode__Default_BubbleToParent(Placeholder control)
        {
        }

        #endregion
    }
}
