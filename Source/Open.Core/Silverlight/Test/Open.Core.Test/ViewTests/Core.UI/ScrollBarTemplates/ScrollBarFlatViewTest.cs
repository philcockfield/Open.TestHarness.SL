using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Open.Core.Common;

namespace Open.Core.Test.ViewTests.Core.UI
{
    [ViewTestClass]
    public class ScrollBarFlatViewTest
    {
        #region Head
        private Border border;
        private Style scrollbarStyle;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ScrollViewer control)
        {
            scrollbarStyle = StyleResources.ControlStyles["ScrollBar.Flat"] as Style;

            control.Width = 200;
            control.Height = 200;
            control.BorderThickness = new Thickness(0);

            border = new Border { Width = 300, Height = 300, Background = new SolidColorBrush(Colors.Transparent) };
            control.Content = border;

            control.ApplyFlatScrollBarStyle();


            Both(control);
        }
        #endregion

        #region Tests

        [ViewTest]
        public void Vertical(ScrollViewer control)
        {
            control.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            control.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        [ViewTest]
        public void Horizontal(ScrollViewer control)
        {
            control.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            control.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }

        [ViewTest]
        public void Both(ScrollViewer control)
        {
            control.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            control.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        }
        #endregion
    }
}
