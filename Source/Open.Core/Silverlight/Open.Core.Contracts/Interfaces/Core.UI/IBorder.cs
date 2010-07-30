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

using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common;

namespace Open.Core.UI
{
    /// <summary>Defines an abstract border.</summary>
    public interface IBorder : IOpacity, IVisibility, IViewFactory, INotifyPropertyChanged
    {
        /// <summary>Gets or sets the color of the border.</summary>
        Brush Color { get; set; }

        /// <summary>Gets or sets the thickness of the border.</summary>
        Thickness Thickness { get; set; }

        /// <summary>Gets or sets the rounded corder radius.</summary>
        CornerRadius CornerRadius { get; set; }

        /// <summary>Converts the specified color to a Brush, and sets it as the Color property.</summary>
        /// <param name="color">The color to apply.</param>
        void SetColor(Color color);
    }
}