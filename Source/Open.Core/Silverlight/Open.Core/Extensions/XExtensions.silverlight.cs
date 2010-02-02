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
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace Open.Core.Common
{
    public static partial class XExtensions
    {
        /// <summary>Gets the element value of the specified child element and converts it to a Thickness.</summary>
        /// <param name="self">The containing element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <returns>A Thickness.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the string does not contain four sides of the Thickness value.</exception>
        public static Thickness GetChildValueAsThickness(this XContainer self, string childElementName)
        {
            if (self == null) return default(Thickness);
            var xElement = self.Element(childElementName);
            return xElement == null ? default(Thickness) : xElement.Value.FromThicknessString();
        }

        /// <summary>Gets the element value of the specified child element and converts it to a color.</summary>
        /// <param name="self">The containing element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <returns>A color.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the string does not contain all ARGB values.</exception>
        public static Color GetChildValueAsColor(this XContainer self, string childElementName)
        {
            if (self == null) return default(Color);
            var xElement = self.Element(childElementName);
            return xElement == null ? default(Color) : xElement.Value.FromColorString();
        }

        /// <summary>Gets the element value of the specified child element and converts it to a memory stream.</summary>
        /// <param name="self">The containing element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <returns>A memory stream, or null if the child element does not exist.</returns>
        /// <exception cref="FormatException">Is thrown if the specified element's value cannot be converted to a stream.</exception>
        public static Stream GetChildValueAsStream(this XContainer self, string childElementName)
        {
            if (self == null) return null;
            var xElement = self.Element(childElementName);
            return xElement == null ? null : xElement.Value.FromBase64ToStream();
        }
    }
}

