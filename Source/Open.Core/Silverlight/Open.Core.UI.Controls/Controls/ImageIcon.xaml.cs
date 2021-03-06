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

using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>A simple way of rendering an 'IconImage' in XAML.</summary>
    public partial class ImageIcon : UserControl
    {
        #region Head
        /// <summary>Constructor.</summary>
        public ImageIcon()
        {
            InitializeComponent();
            OnSourceChanged();
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the enum representing the icon image.</summary>
        public IconImage Source
        {
            get { return (IconImage)(GetValue(SourceProperty)); }
            set { SetValue(SourceProperty, value); }
        }
        /// <summary>Gets or sets the enum representing the icon image.</summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ImageIcon>(m => m.Source),
                typeof(IconImage),
                typeof(ImageIcon),
                new PropertyMetadata(IconImage.SilkAccept, (s, e) => ((ImageIcon)s).OnSourceChanged()));
        private void OnSourceChanged()
        {
            iconImage.Source = Source.ToImageSource();
        }
        #endregion
    }
}