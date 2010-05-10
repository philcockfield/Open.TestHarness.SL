using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;
using Open.Core.Common.AttachedBehavior;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Behavior
{
    [ViewTestClass]
    public class Behavior__UpdateTextBoxOnKeyPressViewTest : UpdateOnKeyPressViewTestBase<TextBox>
    {
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(UpdateOnKeyPressTestControl control)
        {
            var behavior = new UpdateTextBoxOnKeyPress();
            var textBox = control.textbox;
            Behaviors.SetUpdateTextBoxOnKeyPress(textBox, behavior);
            textBox.Visibility = Visibility.Visible;

            base.Initialize(control, behavior);
        }
    }
}
