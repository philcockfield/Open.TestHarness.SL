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
using System.Windows.Media;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>
    ///     An extended ListBox for displaying selections with the ability to easily control individual item templates
    ///     and display options for when the list is empty, and paging etc.
    /// </summary>
    public interface ISelectionList : INotifyPropertyChanged, IViewFactory
    {
        /// <summary>Fires when the SelectedItem changes.</summary>
        event EventHandler SelectionChanged;

        /// <summary>Gets the collection of items.</summary>
        ObservableCollection<object> Items { get; }

        /// <summary>Gets or sets the currently selected item.</summary>
        object SelectedItem { get; set; }

        /// <summary>Gets or sets the View shown when the list is empty (null to use default).</summary>
        object EmptyMessage { get; set; }

        /// <summary>Gets or sets the selector for the DataTemplate which renders each item (passes the item value to the Func).</summary>
        Func<object, DataTemplate> ItemTemplateSelector { get; set; }

        /// <summary>Gets or sets the color of the item divider.</summary>
        Brush ItemDividerColor { get; set; }

        /// <summary>Gets or sets the padding to apply to the item.</summary>
        Thickness ItemPadding { get; set; }

        /// <summary>Selects the first item.</summary>
        void SelectFirst();

        /// <summary>Selects the last item.</summary>
        void SelectLast();
    }
}