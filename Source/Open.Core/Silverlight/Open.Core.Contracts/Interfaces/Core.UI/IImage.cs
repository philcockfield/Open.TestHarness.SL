﻿//------------------------------------------------------
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
    /// <summary>An image.</summary>
    public interface IImage : IViewFactory, IVisiblity, INotifyPropertyChanged
    {
        /// <summary>Gets or sets the source for the image.</summary>
        ImageSource Source { get; set; }

        /// <summary>
        ///     Gets or sets a value that describes how an Image should 
        ///     be stretched to fill the destination rectangle.
        /// </summary>
        Stretch Stretch { get; set; }

        /// <summary>Gets the shadow definition (0 opacity by default, not visible).</summary>
        IDropShadowEffect DropShadow { get; }

        /// <summary>Gets or sets the margin offset to apply to the image.</summary>
        Thickness Margin { get; set; }

        /// <summary>Gets or sets the tooltip to apply to the image.</summary>
        string Tooltip { get; set; }
    }
}