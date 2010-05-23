using System;
using System.ComponentModel.Composition;
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
using Open.Core.UI;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.UI.Controls.Buttons
{
    [ViewTestClass]
    public class Button__IButtonViewTest
    {
        #region Head
        [Import]
        public IButton button;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ViewFactoryContent control)
        {
            CompositionInitializer.SatisfyImports(this);
            control.ViewFactory = button;

            button.Click += delegate { Output.Write("Click"); };

        }
        #endregion

        #region Tests

        [ViewTest]
        public void Toggle__IsEnabled(ViewFactoryContent control)
        {
            button.IsEnabled = !button.IsEnabled;
        }

        [ViewTest]
        public void Change__Label(ViewFactoryContent control)
        {
            button.Label = RandomData.LoremIpsum(1, 2);
        }
        #endregion
    }
}
