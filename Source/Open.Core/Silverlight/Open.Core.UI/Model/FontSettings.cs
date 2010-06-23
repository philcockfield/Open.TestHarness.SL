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
using T = Open.Core.UI.FontSettings;

namespace Open.Core.UI
{
    [Export(typeof(IFontSettings))]
    public class FontSettings : NotifyPropertyChangedBase, IFontSettings
    {
        #region Head
        private static TextBlock defaultValue;
        #endregion

        #region Properties
        public FontFamily FontFamily
        {
            get { return GetPropertyValue<T, FontFamily>(m => m.FontFamily, GetDefault().FontFamily); }
            set { SetPropertyValue<T, FontFamily>(m => m.FontFamily, value, GetDefault().FontFamily); }
        }

        public double FontSize
        {
            get { return GetPropertyValue<T, double>(m => m.FontSize, GetDefault().FontSize); }
            set { SetPropertyValue<T, double>(m => m.FontSize, value.WithinBounds(0, double.MaxValue), GetDefault().FontSize); }
        }

        public FontStretch FontStretch
        {
            get { return GetPropertyValue<T, FontStretch>(m => m.FontStretch, GetDefault().FontStretch); }
            set { SetPropertyValue<T, FontStretch>(m => m.FontStretch, value, GetDefault().FontStretch); }
        }

        public FontStyle FontStyle
        {
            get { return GetPropertyValue<T, FontStyle>(m => m.FontStyle, GetDefault().FontStyle); }
            set { SetPropertyValue<T, FontStyle>(m => m.FontStyle, value, GetDefault().FontStyle); }
        }

        public FontWeight FontWeight
        {
            get { return GetPropertyValue<T, FontWeight>(m => m.FontWeight, GetDefault().FontWeight); }
            set { SetPropertyValue<T, FontWeight>(m => m.FontWeight, value, GetDefault().FontWeight); }
        }

        public Brush Color
        {
            get { return GetPropertyValue<T, Brush>(m => m.Color, GetDefault().Foreground); }
            set { SetPropertyValue<T, Brush>(m => m.Color, value, GetDefault().Foreground); }
        }

        public double Opacity
        {
            get { return GetPropertyValue<T, double>(m => m.Opacity, 1); }
            set { SetPropertyValue<T, double>(m => m.Opacity, value.WithinBounds(0, 1), 1); }
        }

        public TextWrapping TextWrapping
        {
            get { return GetPropertyValue<T, TextWrapping>(m => m.TextWrapping, GetDefault().TextWrapping); }
            set { SetPropertyValue<T, TextWrapping>(m => m.TextWrapping, value, GetDefault().TextWrapping); }
        }

        public TextTrimming TextTrimming
        {
            get { return GetPropertyValue<T, TextTrimming>(m => m.TextTrimming, GetDefault().TextTrimming); }
            set { SetPropertyValue<T, TextTrimming>(m => m.TextTrimming, value, GetDefault().TextTrimming); }
        }
        #endregion

        #region Internal
        private static TextBlock GetDefault() { return defaultValue ?? (defaultValue = new TextBlock()); }
        #endregion
    }
}
