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
using System.ComponentModel;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>A data-pager for navigating a large collection of items.</summary>
    public interface IPager : IViewFactory, IEnableable, INotifyPropertyChanged
    {
        /// <summary>Fires when the 'CurrentIndex' property changes.</summary>
        event EventHandler CurrentIndexChanged;

        /// <summary>Gets of sets the current page (1-based).</summary>
        int CurrentPage { get; set; }

        /// <summary>Gets of sets the index of the current page (0-based).</summary>
        int CurrentPageIndex { get; set; }

        /// <summary>Gets the total number of pages (1-based).</summary>
        int TotalPages { get; set; }

        /// <summary>Gets or sets the number of buttons to render before/after current selection.</summary>
        /// <remarks>
        ///      For example, if = 2, buttons appear for a control with 100 pages
        ///          Button 5 selected:
        ///          Previous 1 2 3 4 (5) 6 7 ... 99 100 Next
        ///      Button 6 selected:
        ///          Previous 1 2 ... 4 5 (6) 7 8 ... 99 100 Next
        ///      Button 95 selected
        ///          Previous 1 2 ... 93 94 (95) 96 97 ... 99 100 Next
        ///      Button 96 selected
        ///          Previous 1 2 ... 94 95 (96) 97 98 99 100 Next
        /// </remarks>
        int TotalPageButtons { get; set; }

        /// <summary>Gets or sets whether the current page is loading.</summary>
        bool IsLoading { get; set; }
    }
}