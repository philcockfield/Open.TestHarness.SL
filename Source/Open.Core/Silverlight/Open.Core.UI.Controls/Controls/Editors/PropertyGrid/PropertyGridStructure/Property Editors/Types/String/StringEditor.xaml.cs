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

using System.Windows.Controls;
using Open.Core.Common.AttachedBehavior;

namespace Open.Core.Common.Controls.Editors.PropertyGridStructure.Editors
{
    /// <summary>A property-grid editor for String values.</summary>
    public partial class StringEditor : UserControl
    {
        #region Head
        private UpdateTextBoxOnKeyPress updateOnKeyPress;

        public StringEditor()
        {
            InitializeComponent();
            GotFocus += delegate { textbox.Focus(); };
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public StringEditorViewModel ViewModel
        {
            get { return DataContext as StringEditorViewModel; }
            set
            {
                DataContext = value;
                if (value.UpdateOnKeyPress) CreateUpdateOnKeyPress();
            }
        }
        #endregion

        #region Internal
        private void CreateUpdateOnKeyPress()
        {
            updateOnKeyPress = new UpdateTextBoxOnKeyPress();
            Behaviors.SetUpdateTextBoxOnKeyPress(textbox, updateOnKeyPress);
        }
        #endregion
    }
}
