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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Open.Core.Common.Controls.Editors
{
    /// <summary>The logical presentation model for the 'ColorSelector' view.</summary>
    /// <remarks>This V-M is used internally by the control only.</remarks>
    public class ColorSelectorViewModel : ViewModelBase
    {
        #region Head
        public const string PropColor = "Color";
        public const string PropColorBrush = "ColorBrush";
        public const string PropRed = "Red";
        public const string PropGreen = "Green";
        public const string PropBlue = "Blue";
        public const string PropAlpha = "Alpha";

        private int red;
        private int green;
        private int blue;
        private int alpha = 255;
        #endregion

        #region Properties
        /// <summary>Gets or sets the current color value.</summary>
        public Color Color
        {
            get
            {
                return Color.FromArgb(
                                    Convert.ToByte(Alpha),
                                    Convert.ToByte(Red),
                                    Convert.ToByte(Green),
                                    Convert.ToByte(Blue));
            }
            set
            {
                Red = value.R;
                Green= value.G;
                Blue= value.B;
                Alpha = value.A;
                OnPropertyChanged(PropColor);
            }
        }

        /// <summary>Gets the current color setting as a brush.</summary>
        public SolidColorBrush ColorBrush { get { return new SolidColorBrush(Color); } }
        #endregion

        #region Properties - Colors
        /// <summary>Gets or sets the 'Red' value.</summary>
        public int Red
        {
            get { return red; }
            set
            {
                value = FormatValue(value);
                red = value;
                OnPropertyChanged(PropRed, PropColorBrush, PropColor);
            }
        }

        /// <summary>Gets or sets the 'Green' value.</summary>
        public int Green
        {
            get { return green; }
            set
            {
                value = FormatValue(value);
                green = value;
                OnPropertyChanged(PropGreen, PropColorBrush, PropColor);
            }
        }

        /// <summary>Gets or sets the 'Blue' value.</summary>
        public int Blue
        {
            get { return blue; }
            set
            {
                value = FormatValue(value);
                blue = value;
                OnPropertyChanged(PropBlue, PropColorBrush, PropColor);
            }
        }

        /// <summary>Gets or sets the 'Alpha' value.</summary>
        public int Alpha
        {
            get { return alpha; }
            set
            {
                value = FormatValue(value);
                alpha = value;
                OnPropertyChanged(PropAlpha, PropColorBrush, PropColor);
            }
        }
        #endregion

        #region Internal
        private static int FormatValue(int value)
        {
            if (value < 0) value = 0;
            if (value > 255) value = 255;
            return value;
        }
        #endregion
    }
}
