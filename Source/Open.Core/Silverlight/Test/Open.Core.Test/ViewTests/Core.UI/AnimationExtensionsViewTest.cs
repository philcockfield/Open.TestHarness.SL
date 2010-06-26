using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Open.Core.Common;
using Open.Core.UI;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.UI
{
    [ViewTestClass]
    public class AnimationExtensionsViewTest
    {
        #region Head
        private MyViewModel viewModel;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(Placeholder control)
        {
            control.Width = 150;
            control.Height = 150;
            viewModel = new MyViewModel();

            control.DataContext = viewModel;
            control.SetBinding(UIElement.OpacityProperty, new Binding("Opacity"));
        }
        #endregion

        #region Tests
        [ViewTest]
        public void FadeOut(Placeholder control) { viewModel.FadeOut(0.5, onComplete: () => Output.Write("FadeOut Complete")); }

        [ViewTest]
        public void FadeIn(Placeholder control) { viewModel.FadeIn(0.5, onComplete: () => Output.Write("FadeIn Complete")); }

        [ViewTest]
        public void Fade(Placeholder control) { viewModel.Fade(1, 0.3, 1, onComplete:()=>Output.Write("Fade Complete")); }
        #endregion

        public class MyViewModel : ViewModelBase, IOpacity
        {
            public double Opacity
            {
                get { return GetPropertyValue<MyViewModel, double>(m => m.Opacity, 1); }
                set { SetPropertyValue<MyViewModel, double>(m => m.Opacity, value.WithinBounds(0, 1), 1); }
            }
        }
    }
}
