using System.ComponentModel.Composition;
using System.Windows;
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
        public IButton Button { get; set; }

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(IButtonTestControl control)
        {
            // Setup initial conditions.
            CompositionInitializer.SatisfyImports(this);
            control.content.ViewFactory = Button;

            // Wire up events.
            Button.Click += delegate { Output.Write("!! Click"); };

            // Finish up.
            Change__Label(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Toggle__IsEnabled(IButtonTestControl control)
        {
            Button.IsEnabled = !Button.IsEnabled;
        }

        [ViewTest]
        public void Toggle__IsVisible(IButtonTestControl control)
        {
            Button.IsVisible = !Button.IsVisible;
        }

        [ViewTest]
        public void Change__Label(IButtonTestControl control)
        {
            Button.Text = RandomData.LoremIpsum(1, 2);
        }

        [ViewTest]
        public void Set__Margin(IButtonTestControl control)
        {
            Button.Margin = new Thickness(50);
        }

        [ViewTest]
        public void Set__ToolTip(IButtonTestControl control)
        {
            Button.ToolTip = RandomData.LoremIpsum(5, 20);
        }
        #endregion
    }
}
