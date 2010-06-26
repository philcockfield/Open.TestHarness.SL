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

namespace Open.Core.Test.ViewTests.Core.UI
{
    [ViewTestClass]
    public class ImageViewTest
    {
        #region Head
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(Image control)
        {
            control.ImageOpened += delegate { Output.Write("!! ImageOpened"); };
            control.ImageFailed += delegate { Output.Write(Colors.Red, "!! ImageFailed"); };
            control.Unloaded += delegate { Output.Write("!! Unloaded"); };
            control.Loaded += delegate { Output.Write("!! Loaded"); };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Load__Dali(Image control)
        {
            control.Source = "/Images/Tests/Dali.png".ToImageSource(ImageLocation.Server);
        }

        [ViewTest]
        public void Load__Fail(Image control)
        {
            control.Source = "/NotAnImage.png".ToImageSource(ImageLocation.Server);
        }

        [ViewTest]
        public void Load__Source_Null(Image control)
        {
            control.Source = null;
        }
        #endregion
    }
}
