//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;
using T = Open.Core.UI.Controls.BorderModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof(IBorder))]
    public class BorderModel : ModelBase, IBorder
    {
        #region Head
        private static readonly Brush defaultColor = new SolidColorBrush(Colors.Black);
        private static readonly Thickness defaultThickness = new Thickness(1);
        #endregion

        #region Properties : IBorder
        public double Opacity
        {
            get { return GetPropertyValue<T, double>(m => m.Opacity, 1); }
            set { SetPropertyValue<T, double>(m => m.Opacity, value, 1); }
        }

        public bool IsVisible
        {
            get { return GetPropertyValue<BorderModel, bool>(m => m.IsVisible, true); }
            set { SetPropertyValue<BorderModel, bool>(m => m.IsVisible, value, true, m => m.Visibility); }
        }

        public Brush Color
        {
            get { return GetPropertyValue<T, Brush>(m => m.Color, defaultColor); }
            set { SetPropertyValue<T, Brush>(m => m.Color, value, defaultColor); }
        }

        public Thickness Thickness
        {
            get { return GetPropertyValue<T, Thickness>(m => m.Thickness, defaultThickness); }
            set { SetPropertyValue<T, Thickness>(m => m.Thickness, value, defaultThickness); }
        }

        public CornerRadius CornerRadius
        {
            get { return GetPropertyValue<T, CornerRadius>(m => m.CornerRadius); }
            set { SetPropertyValue<T, CornerRadius>(m => m.CornerRadius, value); }
        }
        #endregion

        #region Properties : ViewModel
        public Visibility Visibility { get { return IsVisible ? Visibility.Visible : Visibility.Collapsed; } }
        #endregion

        #region Methods
        public void SetColor(Color color)
        {
            Color = new SolidColorBrush(color);
        }

        public FrameworkElement CreateView()
        {
            var border = new Border{DataContext = this};
            border.SetBindings<IBorder>();
            return border;
        }
        #endregion
    }
}
