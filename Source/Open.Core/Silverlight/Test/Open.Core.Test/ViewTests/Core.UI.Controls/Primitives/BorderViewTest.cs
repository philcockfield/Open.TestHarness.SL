using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.UI;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.UI.Controls.Primitives
{
    [ViewTestClass(DisplayName = "Border : IBorder")]
    public class BorderViewTest 
    {
        #region Head
        [Import] public IBorder ViewModel { get; set; }


        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ViewFactoryContent control)
        {
            CompositionInitializer.SatisfyImports(this);
            control.Width = 300;
            control.Height = 250;
            control.ViewFactory = ViewModel;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Set__Color(ViewFactoryContent control)
        {
            ViewModel.Color = new SolidColorBrush(Colors.Orange);
        }

        [ViewTest]
        public void Toggle__Opacity(ViewFactoryContent control)
        {
            ViewModel.Opacity = ViewModel.Opacity == 1 ? 0.3 : 1;
        }

        [ViewTest]
        public void Toggle__IsVisible(ViewFactoryContent control)
        {
            ViewModel.IsVisible = !ViewModel.IsVisible;
        }

        [ViewTest]
        public void Toggle__Thickness(ViewFactoryContent control)
        {
            ViewModel.Thickness = ViewModel.Thickness.Left == 1 ? new Thickness(5) : new Thickness(1);
        }

        [ViewTest]
        public void Toggle__CorderRadius(ViewFactoryContent control)
        {
            ViewModel.CornerRadius = ViewModel.CornerRadius.TopLeft == 0 
                            ? new CornerRadius(15) 
                            : new CornerRadius(0);
        }
        #endregion
    }
}
