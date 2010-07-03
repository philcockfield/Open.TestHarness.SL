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
using Open.Core.UI;
using Open.Core.UI.Controls;
using Open.Core.UI.Silverlight.Test;

namespace Open.Core.Test.ViewTests.Core.UI.Controls.Primitives
{
    [ViewTestClass(DisplayName = "Background : IBackground")]
    public class BackgroundViewTest
    {
        #region Head
        [Import]
        public IBackground ViewModel { get; set; }


        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ViewFactoryContent control)
        {
            CompositionInitializer.SatisfyImports(this);
            control.Width = 300;
            control.Height = 250;
            control.ViewFactory = ViewModel;
            
            ViewModel.CornerRadius = new CornerRadius(15);
            ViewModel.Opacity = 0.1;
            ViewModel.SetColor(Colors.Black);
            ViewModel.Border.SetColor(Colors.Black);
            ViewModel.Border.Opacity = 0.2;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Set__Color(ViewFactoryContent control)
        {
            ViewModel.Color = new SolidColorBrush(Colors.Orange);
        }

        [ViewTest]
        public void Set__Template(ViewFactoryContent control)
        {
            ViewModel.Template = SampleTemplates.Ellipse;
        }

        [ViewTest]
        public void Toggle__Opacity(ViewFactoryContent control)
        {
            ViewModel.Opacity = ViewModel.Opacity == 0.1 ? 0.03 : 0.1;
        }

        [ViewTest]
        public void Toggle__Border_Thickness(ViewFactoryContent control)
        {
            ViewModel.Border.Thickness = ViewModel.Border.Thickness.Left == 1 ? new Thickness(5) : new Thickness(1);
        }

        [ViewTest]
        public void Toggle__IsVisible(ViewFactoryContent control)
        {
            ViewModel.IsVisible = !ViewModel.IsVisible;
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
