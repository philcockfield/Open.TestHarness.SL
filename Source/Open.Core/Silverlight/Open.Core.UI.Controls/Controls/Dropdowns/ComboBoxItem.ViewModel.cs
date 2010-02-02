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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Open.Core.Common;

using T = Open.Core.UI.Controls.ComboBoxItemViewModel;

namespace Open.Core.UI.Controls
{
    /// <summary>Represents a single item within the ComboBox.</summary>
    public class ComboBoxItemViewModel : ViewModelBase
    {
        #region Head
        public ComboBoxItemViewModel()
        {
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the display text of the item.</summary>
        public string Text
        {
            get { return GetPropertyValue<T, string>(m => m.Text); }
            set { SetPropertyValue<T, string>(m => m.Text, value); }
        }

        /// <summary>Gets or sets the value associated with the item.</summary>
        public object Value
        {
            get { return GetPropertyValue<T, object>(m => m.Value); }
            set { SetPropertyValue<T, object>(m => m.Value, value); }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            if (Text.AsNullWhenEmpty() != null) return Text;
            return Value != null ? Value.ToString() : null;
        }
        #endregion
    }
}
