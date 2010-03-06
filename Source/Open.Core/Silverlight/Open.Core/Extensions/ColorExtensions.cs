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

using System.Windows;
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
                                    ToByte(self.R, 1),
                                    ToByte(self.G, 1),
                                    ToByte(self.B, 1));
        }

        /// <summary>Converts the given color to a solid color brush at the given opacity.</summary>
        /// <param name="self">The color to build the brush from.</param>
        /// <param name="opacity">The oapcity percentage (value from 0 to 1).</param>
        public static SolidColorBrush ToBrush(this Color self, double opacity)
        {
            return new SolidColorBrush(self.ToAlpha(opacity));
        }

        /// <summary>Creates a gradient brush with the given colors flowing in the specified direction.</summary>
        /// <param name="direction">The direction of the gradient.</param>
        /// <param name="start">The starting color.</param>
        /// <param name="end">The ending color.</param>
        public static LinearGradientBrush ToGradient(this Direction direction, Color start, Color end)
        {
            // Create the brush.
            var brush = new LinearGradientBrush();
            switch (direction)
            {
                case Direction.Down:
                case Direction.Up:
                    brush.StartPoint = new Point(0.5, 0);
                    brush.EndPoint = new Point(0.5, 1);
                    break;
                case Direction.Right:
                case Direction.Left:
                    brush.StartPoint = new Point(0, 0.5);
                    brush.EndPoint = new Point(1, 0.5);
                    break;
            }

            // Configure colors.
            var gradientStart = new GradientStop { Color = start };
            var gradientEnd = new GradientStop { Color = end };

            gradientStart.Offset = direction == Direction.Up || direction == Direction.Left ? 1 : 0;
            gradientEnd.Offset = direction == Direction.Down || direction == Direction.Right ? 1 : 0;

            // Insert colors.
            brush.GradientStops.Add(gradientStart);
            brush.GradientStops.Add(gradientEnd);

            // Finish up.
            return brush;
            
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
