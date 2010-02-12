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
using System.Linq;
using System.Collections.ObjectModel;
using Open.Core.Common;

using T = Open.Core.UI.Controls.ComboBoxViewModel;

namespace Open.Core.UI.Controls
{
    /// <summary>Provides a simple logical representation of a ComboBox.</summary>
    /// <remarks>For simple binding, set this as the DataContext of a CoreComboBox.</remarks>
    public class ComboBoxViewModel : ViewModelBase
    {
        #region Events
        /// <summary>Fires when the SelectedItem has changed.</summary>
        public event EventHandler SelectionChanged;
        private void OnSelectionChanged(){if (SelectionChanged != null) SelectionChanged(this, new EventArgs());}
        #endregion

        #region Head
        public ComboBoxViewModel()
        {
            Items = new ObservableCollection<ComboBoxItemViewModel>();
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection of items.</summary>
        public ObservableCollection<ComboBoxItemViewModel> Items { get; private set; }

        /// <summary>Gets or sets the currently selected item.</summary>
        public ComboBoxItemViewModel SelectedItem
        {
            get { return GetPropertyValue<T, ComboBoxItemViewModel>(m => m.SelectedItem); }
            set
            {
                if (value == SelectedItem) return;
                SetPropertyValue<T, ComboBoxItemViewModel>(m => m.SelectedItem, value);
                OnSelectionChanged();
            }
        }

        /// <summary>Gets the value of the currently selected item (or null if nothing is currently selected).</summary>
        public object SelectedItemValue
        {
            get { return SelectedItem == null ? null : SelectedItem.Value; }
        }

        /// <summary>Gets or sets the tooltip to apply to the ComboBox.</summary>
        public string ToolTip
        {
            get { return GetPropertyValue<T, string>(m => m.ToolTip); }
            set { SetPropertyValue<T, string>(m => m.ToolTip, value); }
        }
        #endregion

        #region Methods
        /// <summary>Sets the first item in the collection as the currently SelectedItem.</summary>
        public void SelectFirst()
        {
            SelectedItem = Items.FirstOrDefault();
        }

        /// <summary>Sets the last item in the collection as the currently SelectedItem.</summary>
        public void SelectLast()
        {
            SelectedItem = Items.LastOrDefault();
        }

        /// <summary>Sets the element at the specified index as the currently SelectedItem.</summary>
        /// <param name="index">The index of the item to select.</param>
        /// <remarks>This method sets the SelectedItem to null if the 'index' is outside the range of 'Items'.</remarks>
        public void Select(int index)
        {
            SelectedItem = Items.ElementAtOrDefault(index);
        }

        /// <summary>
        ///    Selects the item with the specified value (of sets the SelectedItem to null if the value does not exist 
        ///    within the 'Items' collection).
        /// </summary>
        /// <param name="value">The value to select.</param>
        public void SelectValue(object value)
        {
            SelectedItem = Items.FirstOrDefault(m => Equals(m.Value, value));
        }

        /// <summary>Adds a new item to the combo-box.</summary>
        /// <param name="text">The display text of the item.</param>
        /// <param name="value">The value for the item.</param>
        /// <returns>The newly added item model.</returns>
        public ComboBoxItemViewModel Add(string text, object value)
        {
            var item = new ComboBoxItemViewModel
                           {
                               Text = text,
                               Value = value,
                           };
            Items.Add(item);
            return item;
        }
        #endregion
    }
}
