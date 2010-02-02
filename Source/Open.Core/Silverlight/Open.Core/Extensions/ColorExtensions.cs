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

using System.Windows.Media;

namespace Open.Core.Common
{
    public static class ColorExtensions
    {
        #region Methods
        /// <summary>Converts given color to the specified opacity.</summary>
        /// <param name="self">The color to convert.</param>
        /// <param name="opacity">The opacity percentage (value from 0 to 1).</param>
        public static Color ToAlpha(this Color self, double opacity)
        {
            opacity = opacity.WithinBounds(0, 1);
            return Color.FromArgb(
                                    ToByte(255, opacity),
                                    ToByte(self.R, opacity),
                                    ToByte(self.G, opacity),
                                    ToByte(self.B, opacity));
        }

        /// <summary>Converts the given color to a solid color brush at the given opacity.</summary>
        /// <param name="self">The color to build the brush from.</param>
        /// <param name="opacity">The oapcity percentage (value from 0 to 1).</param>
        public static SolidColorBrush ToBrush(this Color self, double opacity)
        {
            return new SolidColorBrush(self.ToAlpha(opacity));
        }
        #endregion

        #region Internal
        private static byte ToByte(byte colorValue, double opacity)
        {
            return (byte)(colorValue * opacity);
        }
        #endregion
    }
}
