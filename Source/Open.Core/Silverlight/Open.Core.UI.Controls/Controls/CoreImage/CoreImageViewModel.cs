﻿//------------------------------------------------------
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
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.UI.Model;

using T = Open.Core.UI.Controls.CoreImageViewModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof(IImage))]
    public class CoreImageViewModel : ViewModelBase, IImage
    {
        #region Head
        private IDropShadowEffect shadow;
        #endregion

        #region Properties - IImage
        public ImageSource Source
        {
            get { return GetPropertyValue<T, ImageSource>(m => m.Source); }
            set { SetPropertyValue<T, ImageSource>(m => m.Source, value); }
        }

        public Stretch Stretch
        {
            get { return GetPropertyValue<T, Stretch>(m => m.Stretch, Stretch.None); }
            set { SetPropertyValue<T, Stretch>(m => m.Stretch, value, Stretch.None); }
        }

        public IDropShadowEffect DropShadow
        {
            get { return shadow ?? (shadow = new DropShadowEffect { Opacity = 0 }); }
        }

        public Thickness Margin
        {
            get { return GetPropertyValue<T, Thickness>(m => m.Margin, new Thickness(0)); }
            set { SetPropertyValue<T, Thickness>(m => m.Margin, value, new Thickness(0)); }
        }

        public bool IsVisible
        {
            get { return GetPropertyValue<T, bool>(m => m.IsVisible, true); }
            set { SetPropertyValue<T, bool>(m => m.IsVisible, value, true, m => m.Visibility); }
        }

        public string Tooltip
        {
            get { return Property.GetValue<T, string>(m => m.Tooltip); }
            set { Property.SetValue<T, string>(m => m.Tooltip, value); }
        }
        #endregion

        #region Properties - ViewModel
        public Visibility Visibility { get { return IsVisible ? Visibility.Visible : Visibility.Collapsed; } }
        #endregion

        #region Methods
        public FrameworkElement CreateView()
        {
            return new CoreImage { ViewModel = this };
        }
        #endregion
    }
}
