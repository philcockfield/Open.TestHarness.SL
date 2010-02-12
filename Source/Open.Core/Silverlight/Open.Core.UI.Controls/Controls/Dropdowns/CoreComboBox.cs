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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>A simple combobox optimized to work with the ComboBox view-model.</summary>
    public class CoreComboBox : ComboBox
    {
        #region Head
        private readonly DataContextObserver dataContextObserver;

        public CoreComboBox()
        {
            dataContextObserver = new DataContextObserver(this, OnDataContextChanged);
        }
        #endregion

        #region Event Handlers
        private void OnDataContextChanged()
        {
            if (ViewModel == null) return;
            InitializeBindings();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public ComboBoxViewModel ViewModel
        {
            get { return DataContext as ComboBoxViewModel; }
            set { DataContext = value; }
        }
        #endregion

        #region Internal
        private void InitializeBindings()
        {
            // Setup the collection binding.
            ItemsSource = ViewModel.Items;

            // Bind to the 'SelectedItem' property.
            var selectedItemBinding = new Binding(LinqExtensions.GetPropertyName<ComboBoxViewModel>(m => m.SelectedItem))
                              {
                                  Mode = BindingMode.TwoWay
                              };
            SetBinding(SelectedItemProperty, selectedItemBinding);

            // Bind the tooltip.
            var tooltipBinding = new Binding(LinqExtensions.GetPropertyName<ComboBoxViewModel>(m => m.ToolTip));
            SetBinding(ToolTipService.ToolTipProperty, tooltipBinding);
        }
        #endregion
    }
}
