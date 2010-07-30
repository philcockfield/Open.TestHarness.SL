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
using System.Windows;

namespace Open.Core.Common
{
    public static class EnumExtensions
    {
        /// <summary>Converts the unit to it's display name.</summary>
        /// <param name="unit">The unit to convert.</param>
        /// <param name="abbreviate">Flag indicating if the name should be abbreivated (eg. "KB") or not (eg. "Kilobyte").</param>
        public static string ToString(this FileSizeUnit unit, bool abbreviate)
        {
            if (abbreviate)
            {
                if (unit == FileSizeUnit.Byte) return "B";
                if (unit == FileSizeUnit.Kilobyte) return "KB";
                if (unit == FileSizeUnit.Megabyte) return "MB";
                if (unit == FileSizeUnit.Gigabyte) return "GB";
                if (unit == FileSizeUnit.Terabyte) return "TB";
            }
            else
            {
                if (unit == FileSizeUnit.Byte) return "Byte";
                if (unit == FileSizeUnit.Kilobyte) return "Kilobyte";
                if (unit == FileSizeUnit.Megabyte) return "Megabyte";
                if (unit == FileSizeUnit.Gigabyte) return "Gigabyte";
                if (unit == FileSizeUnit.Terabyte) return "Terabyte";
            }
            throw new NotSupportedException(unit.ToString());
        }

        /// <summary>Converts the unit to it's pluralized display name (the display name is long form, not abbreviated).</summary>
        /// <param name="unit">The unit to convert.</param>
        /// <param name="value">The file size value.</param>
        public static string ToString(this FileSizeUnit unit, double value)
        {
            var name = unit.ToString(false);
            var isSingular = (value == 1 || value == -1);
            return isSingular ? name : name + "s";

        }

        /// <summary>Converts a file-size in the give unit to a different unit.</summary>
        /// <param name="sourceUnit">The source unit the 'value' is currently in.</param>
        /// <param name="targetUnit">The unit to convert to.</param>
        /// <param name="value">The file-size value to convert.</param>
        public static double ConvertTo(this FileSizeUnit sourceUnit, FileSizeUnit targetUnit, double value)
        {
            // Setup initial conditions.
            var bytes = sourceUnit.ToBytes(value);

            // Perform conversion.
            const int multiplier = 1024;
            if (targetUnit == FileSizeUnit.Byte) return bytes;
            if (targetUnit == FileSizeUnit.Kilobyte) return bytes / multiplier;
            if (targetUnit == FileSizeUnit.Megabyte) return bytes / multiplier / multiplier;
            if (targetUnit == FileSizeUnit.Gigabyte) return bytes / multiplier / multiplier / multiplier;
            if (targetUnit == FileSizeUnit.Terabyte) return bytes / multiplier / multiplier / multiplier / multiplier;

            throw new NotSupportedException(targetUnit.ToString());
        }

        /// <summary>Converts the given value to types.</summary>
        /// <param name="unit">The unit the value is currently in.</param>
        /// <param name="value">The value to convert.</param>
        public static double ToBytes(this FileSizeUnit unit, double value)
        {
            const int multiplier = 1024;

            if (unit == FileSizeUnit.Byte) return value;
            if (unit == FileSizeUnit.Kilobyte) return value * multiplier;
            if (unit == FileSizeUnit.Megabyte) return value * multiplier * multiplier;
            if (unit == FileSizeUnit.Gigabyte) return value * multiplier * multiplier * multiplier;
            if (unit == FileSizeUnit.Terabyte) return value * multiplier * multiplier * multiplier * multiplier;

            throw new NotSupportedException(unit.ToString());
        }

        /// <summary>Converts the given collection of edges to a thickness.</summary>
        /// <param name="edges">The collection of edges to include in the return Thickness.</param>
        /// <param name="thickness">The pixel width/height of the edges.</param>
        public static Thickness ToThickness(this RectEdgeFlag edges, int thickness = 1)
        {
            Func<RectEdgeFlag, int> getThickness = edge =>  (edges & edge) == edge ? thickness : 0; 
            return new Thickness(
                                getThickness(RectEdgeFlag.Left),
                                getThickness(RectEdgeFlag.Top),
                                getThickness(RectEdgeFlag.Right), 
                                getThickness(RectEdgeFlag.Bottom));
        }

        /// <summary>Gets whether the direction is vertical.</summary>
        /// <param name="direction">The value to examine.</param>
        public static bool IsVertical(this Direction direction) { return direction == Direction.Up || direction == Direction.Down; }

        /// <summary>Gets whether the direction is horizontal.</summary>
        /// <param name="direction">The value to examine.</param>
        public static bool IsHorizontal(this Direction direction) { return direction == Direction.Left || direction == Direction.Right; }

        /// <summary>Converts a boolean into a corresponding Visible or Collapsed value.</summary>
        /// <param name="isVisible">The boolean to convert.</param>
        public static Visibility ToVisibility(this bool isVisible) { return isVisible ? Visibility.Visible : Visibility.Collapsed; }
    }
}
