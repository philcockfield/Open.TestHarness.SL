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
    /// <summary>Decorates a property with additional information to craft how the property is displayed within the property-grid.</summary>
    /// <remarks>
    ///    Use in conjuction with the [Category] attribute.<BR/>
    ///    See the 'Editor' assembly for the 'PropertyGrid' and 'PropertyExplorer' controls.
    /// </remarks>
    public class PropertyGridAttribute : Attribute
    {
        /// <summary>Gets or sets an alternative name of the property to display within the property-grid.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the default text value for the property to display if the value is not null.</summary>
        /// <remarks>Use this for complex objects that don't have simple values, like a Stream for example.</remarks>
        public string Value { get; set; }

    }
}
