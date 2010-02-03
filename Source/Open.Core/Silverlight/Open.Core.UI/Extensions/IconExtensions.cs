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
    /// <summary>Extensions for accessing icon images.</summary>
    public static partial class IconExtensions
    {
        /// <summary>Determines whether the given icon flag is from the Silk icon set.</summary>
        /// <param name="icon">The flag of the icon to examine.</param>
        public static bool IsSilk(this Icons icon)
        {
            return icon.ToString().StartsWith("Silk");
        }


        /// <summary>Retrieves the path to the corresponding image file.</summary>
        /// <param name="icon">The flag of the icon to convert.</param>
        public static Uri ToUri(this Icons icon)
        {
            var path = string.Format("/Images/Icons/Silk/{0}.png", icon);
            return new Uri(path, UriKind.Relative);
        }
    }
}
