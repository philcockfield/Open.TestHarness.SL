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

namespace Open.Core.Common
{
    public static partial class NumberExtensions
    {
        #region Methods
        /// <summary>Ensures a value is within the given bounds.</summary>
        /// <param name="self">The number to examine.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The given value unchanged (if within bounds) otherwise the min or max value.</returns>
        public static double WithinBounds(this double self, double min, double max)
        {
            if (self < min) self = min;
            if (self > max) self = max;
            return self;
        }

        /// <summary>Ensures a value is within the given bounds.</summary>
        /// <param name="self">The number to examine.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The given value unchanged (if within bounds) otherwise the min or max value.</returns>
        public static int WithinBounds(this int self, int min, int max)
        {
            return (int) ((double) self).WithinBounds(min, max);
        }

        /// <summary>Converts the value to a number with comma formatting (eg. 1,000).</summary>
        /// <param name="self">The value to convert.</param>
        public static string ToComma(this double self)
        {
            var formatted = self.ToString("n").RemoveEnd(".00");
            if (formatted.Contains(".")) formatted = formatted.TrimEnd("0".ToCharArray());
            return formatted;
        }

        /// <summary>Rounds the number to the given number of decimals.</summary>
        /// <param name="value">The number to round.</param>
        /// <param name="decimals">The number of decimal places.</param>
        public static double Round(this double value, int decimals)
        {
            return Math.Round(value, decimals);
        }

        /// <summary>Determines whether the given value could be cast to an enum.</summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        public static bool CanCastToEnum<TEnum>(this int enumValue)
        {
            if (enumValue < 0) return false;
            var values = typeof (TEnum).GetEnumValues();
            return (enumValue <= values.Length - 1);
        }

        /// <summary>Determines whether the given value could be cast to an enum.</summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <param name="throwError">Flag indicating if an exception should be thrown if the value cannot be cast.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the value cannot be cast (and 'throwError' is true).</exception>
        public static bool CanCastToEnum<TEnum>(this int enumValue, bool throwError)
        {
            if (enumValue.CanCastToEnum<TEnum>()) return true;
            if (!throwError) return false;
            throw new ArgumentOutOfRangeException(string.Format("The value {0} cannot be cast to a '{1}' enum.", enumValue, typeof(TEnum).Name));
        }

        /// <summary>Determines whether the number is odd.</summary>
        /// <param name="value">The value to examine.</param>
        public static bool IsOdd(this int value)
        {
            return !value.IsEven();
        }

        /// <summary>Determines whether the number is odd.</summary>
        /// <param name="value">The value to examine.</param>
        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        /// <summary>Expontial falloff using the equation: e^(-decayConstant * value).</summary>
        /// <param name="value">The x value (for example time)</param>
        /// <param name="decayConstant">A positive value. The higher the value, the sharper the falloff.</param>
        /// <remarks>A constant decay value of 7 will yield a falloff  [value=0, 1] [value=1, 0.0009]</remarks>
        public static double ExponentialDecay(this double value, double decayConstant)
        {
            decayConstant.WithinBounds(0, double.MaxValue);
            return Math.Pow(Math.E, -decayConstant * value);
        }
        #endregion

        #region Methods - File Size
        /// <summary>Gets the display unit corresponding to the given file size.</summary>
        /// <param name="value">The value.</param>
        /// <param name="fromUnit">The unit of size the value is in.</param>
        public static FileSizeUnit GetFileSizeUnit(this long value, FileSizeUnit fromUnit) { return GetFileSizeUnit((double)value, fromUnit); }

        /// <summary>Gets the display unit corresponding to the given file size.</summary>
        /// <param name="value">The value.</param>
        /// <param name="fromUnit">The unit of size the value is in.</param>
        public static FileSizeUnit GetFileSizeUnit(this double value, FileSizeUnit fromUnit)
        {
            // Setup initial conditions.
            if (value < 0) value = 0;
            value = fromUnit.ConvertTo(FileSizeUnit.Kilobyte, value);

            // Kilobytes.
            if (value < 512) return FileSizeUnit.Kilobyte;

            // Megabytes (< 500 MB).
            if (value < 512000) return FileSizeUnit.Megabyte;

            // Gigabytes (< 1TB).
            if (value < 1073741824) return FileSizeUnit.Gigabyte;

            // Terabytes 
            return FileSizeUnit.Terabyte;
        }

        /// <summary>Converts the specified KB value to the appropriate filesize and unit (eg. 0.25 MB).</summary>
        /// <param name="kilobytes">Value in kilobytes.</param>
        public static string ToFileSize(this double kilobytes) { return kilobytes.ToFileSize(FileSizeUnit.Kilobyte); }

        /// <summary>Converts the specified value to the appropriate filesize and unit (eg. 0.25 MB).</summary>
        /// <param name="value">The value.</param>
        /// <param name="fromUnit">The unit of size the value is in.</param>
        public static string ToFileSize(this long value, FileSizeUnit fromUnit) { return ToFileSize((double)value, fromUnit); }

        /// <summary>Converts the specified value to the appropriate filesize and unit (eg. 0.25 MB).</summary>
        /// <param name="value">The value.</param>
        /// <param name="fromUnit">The unit of size the value is in.</param>
        public static string ToFileSize(this double value, FileSizeUnit fromUnit)
        {
            value = fromUnit.ConvertTo(FileSizeUnit.Kilobyte, value);
            var toUnit = value.GetFileSizeUnit(FileSizeUnit.Kilobyte);
            return value.ToFileSize(FileSizeUnit.Kilobyte, toUnit);
        }

        /// <summary>Converts the specified value to the appropriate filesize and unit (eg. 0.25 MB).</summary>
        /// <param name="value">The value.</param>
        /// <param name="fromUnit">The unit of size the value is in.</param>
        /// <param name="toUnit">The unit to convert to.</param>
        public static string ToFileSize(this long value, FileSizeUnit fromUnit, FileSizeUnit toUnit) { return ToFileSize((double)value, fromUnit, toUnit); }

        /// <summary>Converts the specified value to the appropriate filesize and unit (eg. 0.25 MB).</summary>
        /// <param name="value">The value.</param>
        /// <param name="fromUnit">The unit of size the value is in.</param>
        /// <param name="toUnit">The unit to convert to.</param>
        public static string ToFileSize(this double value, FileSizeUnit fromUnit, FileSizeUnit toUnit)
        {
            value = fromUnit.ConvertTo(toUnit, value);
            return string.Format("{0} {1}", value.Round(2).ToComma(), toUnit.ToString(true));
        }
        #endregion
    }
}

