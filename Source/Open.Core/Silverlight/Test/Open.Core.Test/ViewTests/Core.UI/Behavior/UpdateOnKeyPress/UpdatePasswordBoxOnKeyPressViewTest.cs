using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;
using Open.Core.Common.AttachedBehavior;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Behavior
{
    [ViewTestClass]
    public class Behavior__UpdatePasswordBoxOnKeyPressViewTest : UpdateOnKeyPressViewTestBase<PasswordBox>
    {
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(UpdateOnKeyPressTestControl control)
        {
            var behavior = new UpdatePasswordBoxOnKeyPress();
            var passwordBox = control.password;
            Behaviors.SetUpdatePasswordBoxOnKeyPress(passwordBox, behavior);
            passwordBox.Visibility = Visibility.Visible;

            base.Initialize(control, behavior);
        }
    }
}
