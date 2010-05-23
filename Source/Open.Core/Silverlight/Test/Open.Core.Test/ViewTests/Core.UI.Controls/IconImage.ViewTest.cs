using Open.Core.Common;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.Controls
{
    [ViewTestClass]
    public class IconImageViewTest
    {
        #region Head

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ImageIcon control)
        {
            control.Width = 16;
            control.Height = 16;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Icon_Accept(ImageIcon control)
        {
            control.Source = IconImage.SilkAccept;
        }

        [ViewTest]
        public void Icon_Bug(ImageIcon control)
        {
            control.Source = IconImage.SilkBug;
        }
        #endregion
    }
}
