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

namespace Open.Core.Common.Controls.Editors.PropertyGridStructure.Editors
{
    /// <summary>Base class for view-model's of property-grid editors.</summary>
    public abstract class EditorViewModelBase : ViewModelBase
    {
        #region Head
        public const string PropValue = "Value";
        private ParseValue parseValue;

        protected EditorViewModelBase(PropertyModel model)
        {
            Model = model;
        }
        #endregion

        #region Properties
        /// <summary>Gets the embedded property model.</summary>
        protected PropertyModel Model { get; private set; }

        /// <summary>Gets or sets the delegate to the value parser.</summary>
        public ParseValue ParseValue
        {
            get
            {
                if (parseValue == null) parseValue = ValueParser.ParseValue;
                return parseValue;
            }
            set { parseValue = value; }
        }

        /// <summary>Gets or sets whether the editor resides within a popup.</summary>
        public bool IsPopup { get; set; }
        #endregion
    }
}
