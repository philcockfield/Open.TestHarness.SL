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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>Renders a list of controls with a display name (or title) above each one.</summary>
    public interface INamedControlList : INotifyPropertyChanged, IViewFactory
    {
        /// <summary>Gets the collection of items.</summary>
        ObservableCollection<INamedControlListItem> Items { get; }

        /// <summary>Gets the font settings for the title.</summary>
        IFontSettings TitleFont { get; }

        /// <summary>Gets or sets the spacing to insert between items.</summary>
        double ItemSpacing { get; set; }

        /// <summary>Gets or sets the margin to apply to the control of each item.</summary>
        Thickness ControlMargin { get; set; }

        /// <summary>Gets or sets the visibility of the horizontal scrollbar.</summary>
        ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }

        /// <summary>Gets or sets the visibility of the vertical scrollbar.</summary>
        ScrollBarVisibility VerticalScrollBarVisibility { get; set; }

        /// <summary>Adds an item to the bottom of the list.</summary>
        /// <param name="title">The display name of the item.</param>
        /// <param name="control">The logical representation of the control.</param>
        /// <returns>The new item reference.</returns>
        INamedControlListItem Add(string title, IViewFactory control);

        /// <summary>Adds an item to given location within the list.</summary>
        /// <param name="index">The index to insert at.</param>
        /// <param name="title">The display name of the item.</param>
        /// <param name="control">The logical representation of the control.</param>
        /// <returns>The new item reference.</returns>
        INamedControlListItem Insert(int index, string title, IViewFactory control);

        /// <summary>Removes the specified item from the list.</summary>
        /// <param name="item">The named item.</param>
        void Remove(INamedControlListItem item);

        /// <summary>Removes the specified item from the list.</summary>
        /// <param name="control">The logical representation of the control.</param>
        void Remove(IViewFactory control);

        /// <summary>Retrieves the item with the specified control.</summary>
        /// <param name="control">The logical representation of the control.</param>
        INamedControlListItem GetItem(IViewFactory control);
    }



    /// <summary>A single item within a INamedControlList.</summary>
    public interface INamedControlListItem : INotifyPropertyChanged, IDisposable
    {
        /// <summary>Gets or sets the title of the control in the list.</summary>
        string Title { get; set; }

        /// <summary>Gets the logical representation of the control.</summary>
        IViewFactory Control { get; }

        /// <summary>Gets or sets the specific margin for the control.</summary>
        Thickness Margin { get; set; }
    }
}
