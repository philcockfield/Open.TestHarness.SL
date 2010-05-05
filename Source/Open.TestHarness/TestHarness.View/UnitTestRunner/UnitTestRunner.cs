using System.Windows;
using System.Windows.Controls;
using Microsoft.Silverlight.Testing;
using Open.Core.Common;

namespace Open.TestHarness.View
{
    /// <summary>Renders the UnitTest runner in the main window.</summary>
    [ViewTestClass]
    public class UnitTestRunner
    {
        [ViewTest(Default = true, IsVisible = false, SizeMode = TestControlSize.Fill)]
        public void Initialize(ContentControl control)
        {
            control.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            control.VerticalContentAlignment = VerticalAlignment.Stretch;
            control.Content = UnitTestSystem.CreateTestPage();
        }

    }
}
