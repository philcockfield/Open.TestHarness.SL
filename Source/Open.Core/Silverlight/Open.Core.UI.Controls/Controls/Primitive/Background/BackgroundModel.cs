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

using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common;
using T = Open.Core.UI.Controls.BackgroundModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof(IBackground))]
    public class BackgroundModel : ModelBase, IBackground
    {
        #region Head
        private static readonly Brush defaultColor = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
        #endregion

        #region Properties
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

        public Visibility Visibility { get { return IsVisible ? Visibility.Visible : Visibility.Collapsed; } }

        public Brush Color
        {
            get { return GetPropertyValue<T, Brush>(m => m.Color, defaultColor); }
            set { SetPropertyValue<T, Brush>(m => m.Color, value, defaultColor); }
        }

        public IBorder Border
        {
            get { return GetPropertyValue<T, IBorder>(m => m.Border); }
            set { SetPropertyValue<T, IBorder>(m => m.Border, value); }
        }

        public DataTemplate Template
        {
            get { return GetPropertyValue<T, DataTemplate>(m => m.Template); }
            set { SetPropertyValue<T, DataTemplate>(m => m.Template, value); }
        }
        #endregion


        #region Methods
        public FrameworkElement CreateView()
        {

            return null;
        }
        #endregion

        #region Internal

        //private void IBorder GetDefaultBorder( )
        //{
        //}

        #endregion

    }
}
