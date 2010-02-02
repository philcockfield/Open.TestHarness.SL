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
    // TODO - Convert to Extensions
    public static class MathUtil
    {
        /// <summary>Expontial falloff using the equation: e^(-decayConstant * value).</summary>
        /// <param name="value">The x value (for example time)</param>
        /// <param name="decayConstant">A positive value. The higher the value, the sharper the falloff.</param>
        /// <remarks>A constant decay value of 7 will yield a falloff  [value=0, 1] [value=1, 0.0009]</remarks>
        public static double ExponentialDecay(double value, double decayConstant)
        {
            decayConstant.WithinBounds(0, double.MaxValue);
            return Math.Pow(Math.E, -decayConstant * value);
        }

        /// <summary>Ensures a value is within the given bounds.</summary>
        /// <param name="value">The value to examine.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The given value unchanged (if within bounds) otherwise the min or max value.</returns>
        public static double ToWithinBounds(double value, double min, double max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
    }
}
