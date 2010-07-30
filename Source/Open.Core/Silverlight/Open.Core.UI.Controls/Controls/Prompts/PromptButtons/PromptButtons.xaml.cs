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

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    public partial class PromptButtons : UserControl
    {
        #region Head
        private DataContextObserver dataContextObserver;
        private List<PropertyObserver<IButton>> buttonObservers;
        private IEnumerable<Button> buttonElements;

        /// <summary>Constructor.</summary>
        public PromptButtons()
        {
            // Setup initial conditions.
            InitializeComponent();

            // Wire up events.
            dataContextObserver = new DataContextObserver(this, OnDataContextChanged);
            Loaded += delegate
                          {
                              UpdateVisualState();
                          };
        }
        #endregion

        #region Event Handlers
        private void OnDataContextChanged()
        {
            DestroyButtonObservers();
            WireUpButtonObservers();
            UpdateButtonWidths();
            UpdateRootVisibility();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public PromptButtonsViewModel ViewModel
        {
            get { return DataContext as PromptButtonsViewModel; }
            set { DataContext = value; }
        }

        private IEnumerable<Button> ButtonElements
        {
            get
            {
                if (buttonElements != null && !buttonElements.IsEmpty()) return buttonElements;
                buttonElements = this.FindChildrenOfType<Button>();
                return buttonElements;
            }
        }
        #endregion

        #region Methods
        /// <summary>Forces an update of the visual state.</summary>
        public void UpdateVisualState()
        {
            UpdateButtonWidths();
            UpdateRootVisibility();
        }
        #endregion

        #region Internal
        private void UpdateRootVisibility()
        {
            root.Visibility = ViewModel == null ? Visibility.Collapsed : Visibility.Visible;
        }

        private void DestroyButtonObservers()
        {
            if (buttonObservers == null) return;
            foreach (var item in buttonObservers)
            {
                item.Dispose();
            }
            buttonObservers.Clear();
        }

        private void WireUpButtonObservers()
        {
            if (ViewModel == null) return;
            buttonObservers = new List<PropertyObserver<IButton>>();
            foreach (var button in ViewModel.Buttons)
            {
                var observer = new PropertyObserver<IButton>(button).RegisterHandler(m => m.Text, () => UpdateButtonWidths());
                buttonObservers.Add(observer);
            }
        }

        private void UpdateButtonWidths()
        {
            // Setup initial conditions.
            if (ViewModel == null || ButtonElements == null || ButtonElements.IsEmpty()) return;

            // Clear all explicit widths.
            foreach (var button in ButtonElements)
            {
                button.Width = double.NaN;
            }
            UpdateLayout();

            // Update the width of all buttons to match the widest button.
            var width = GetWidestWidth();
            foreach (var button in ButtonElements)
            {
                button.Width = width;
            }
        }

        private double GetWidestWidth()
        {
            var widestButton = GetWidestButton();
            var match = ButtonElements.FirstOrDefault(m => m.DataContext == widestButton);
            return match == null ? 50 : match.ActualWidth;
        }

        private IButton GetWidestButton()
        {
            IButton button = null;
            var maxLength = 0;
            foreach (var item in ViewModel.Buttons)
            {
                if (!item.IsVisible) continue;
                if (item.Text.Length > maxLength)
                {
                    maxLength = item.Text.Length;
                    button = item;
                }
            }
            return button;
        }
        #endregion
    }
}