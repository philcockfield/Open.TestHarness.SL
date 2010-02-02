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

namespace Open.Core.Common.Controls.Editors
{
    /// <summary>A string formatter for converting a property value to a display string.</summary>
    /// <param name="property">The property to format.</param>
    /// <returns>A string representation of the property's current value.</returns>
    public delegate string FormatValueToString(PropertyModel property);

    /// <summary>Parses a string representation of a value converting it to it's native type.</summary>
    /// <param name="textValue">The text display version of the value to parse.</param>
    /// <param name="property">The definition of the property the value pertains to.</param>
    /// <param name="error">An error to return if parsing was not successful (null if parsed successfully).</param>
    /// <returns>The parsed value in it's native type/</returns>
    public delegate object ParseValue(string textValue, PropertyModel property, out Exception error);
}
