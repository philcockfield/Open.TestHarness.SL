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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Open.Core.Common.AttachedBehavior;

namespace Open.Core.UI.Controls
{
    /// <summary>The core-extension wrapper for the TextBox.</summary>
    public class CoreTextBox : TextBox
    {
        #region Head
        /// <summary>Fires when the 'Enter' key is pressed.</summary>
        public event KeyEventHandler EnterPress;
        protected void OnEnterPress(KeyEventArgs e){if (EnterPress != null) EnterPress(this, e);}

        public const string PropSelectAllOnFocus = "SelectAllOnFocus";
        public const string PropUpdateDataSourceOnEnterPress = "UpdateDataSourceOnEnterPress";

        public CoreTextBox()
        {
            // Wire up events.
            GotFocus += delegate { if (SelectAllOnFocus) SelectAll(); };
            KeyDown += HandleKeyDown;
        }
        #endregion

        #region Event Handlers
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OnEnterPress(e);
                if (UpdateDataSourceOnEnterPress) UpdateTextBoxOnKeyPress.UpdateDataSource(this);
            }
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets whether the Text value is selected when the TextBox recieves focus.</summary>
        public bool SelectAllOnFocus
        {
            get { return (bool) (GetValue(SelectAllOnFocusProperty)); }
            set { SetValue(SelectAllOnFocusProperty, value); }
        }
        /// <summary>Gets or sets whether the Text value is selected when the TextBox recieves focus.</summary>
        public static readonly DependencyProperty SelectAllOnFocusProperty =
            DependencyProperty.Register(
                PropSelectAllOnFocus,
                typeof (bool),
                typeof (CoreTextBox),
                new PropertyMetadata(true));


        /// <summary>Gets or sets whether the data-source bound to the 'Text' property is updated when the Enter key is pressed.</summary>
        public bool UpdateDataSourceOnEnterPress
        {
            get { return (bool) (GetValue(UpdateDataSourceOnEnterPressProperty)); }
            set { SetValue(UpdateDataSourceOnEnterPressProperty, value); }
        }
        /// <summary>Gets or sets whether the data-source bound to the 'Text' property is updated when the Enter key is pressed.</summary>
        public static readonly DependencyProperty UpdateDataSourceOnEnterPressProperty =
            DependencyProperty.Register(
                PropUpdateDataSourceOnEnterPress,
                typeof (bool),
                typeof (CoreTextBox),
                new PropertyMetadata(false));
        
        #endregion
    }
}
