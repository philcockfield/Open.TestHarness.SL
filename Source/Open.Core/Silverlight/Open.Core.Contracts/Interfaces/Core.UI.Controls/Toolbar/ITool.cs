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
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>A single tool in a toolbar.</summary>
    public interface ITool : IViewFactory, IEnableable, IVisibility, INotifyPropertyChanged
    {
        /// <summary>Gets or sets the unique identifier of the tool.</summary>
        object Id { get; set; }

        /// <summary>Gets the toolbar that this tool resides within (null if not added to a toolbar, or is a root element).</summary>
        IToolBar Parent { get; set; }

        /// <summary>Gets or sets the minimum width the tool can be.</summary>
        double MinWidth { get; set; }

        /// <summary>Gets or sets the horizontal-alignment of the tool.</summary>
        HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>Gets or sets the vertical-alignment of the tool.</summary>
        VerticalAlignment VerticalAlignment { get; set; }
    }
}
