using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using Open.Core.UI.Controls.Controls;

namespace Open.Core.Test.ViewTests.Core.UI.Controls
{
    [ViewTestClass]
    public class PostItViewTest
    {
        #region Head

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(PostIt control)
        {
            control.Width = 160;
            control.Height = 150;

            Set__Text(control);
        }
        #endregion

        #region Tests

        [ViewTest]
        public void Set__Text(PostIt control)
        {
            control.Text = RandomData.LoremIpsum(5, 20);
        }

        [ViewTest]
        public void Change__Angle(PostIt control)
        {
            control.Angle = control.Angle == 0 ? 30 : 0;
        }
        #endregion
    }
}
