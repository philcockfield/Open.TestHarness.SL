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
using System.Diagnostics;

namespace Open.Core.Common.Controls.Editors.PropertyGridStructure.Editors
{
    public class StringEditorViewModel : EditorViewModelBase
    {
        #region Head
        public const string PropUpdateOnKeyPress = "UpdateOnKeyPress";

        private bool updateOnKeyPress;

        public StringEditorViewModel(PropertyModel model) : base(model)
        {
            UpdateOnKeyPress = false;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the value.</summary>
        public string Value
        {
            get { return Model.ToValueString(false); }
            set
            {
                if (value == Value) return;
                UpdateModelValue(value);
            }
        }

        /// <summary>Gets or sets whether updates are automatically sent to the model on each keypress.</summary>
        public bool UpdateOnKeyPress
        {
            get { return updateOnKeyPress; }
            set { updateOnKeyPress = value; OnPropertyChanged(PropUpdateOnKeyPress); }
        }
        #endregion

        #region Internal
        private void UpdateModelValue(string textValue)
        {
            // Invoke the value parser.
            Exception error;
            var value = ParseValue(textValue, Model, out error);

            // Check for error.
            //TODO - error checking on string editor
            if (error != null)
            {
                Debug.WriteLine("error.Message: " + error.Message);
            }

            // Update the model.
            if (error == null) Model.Value = value;

            // Finish up.
            OnPropertyChanged(PropValue);
        }
        #endregion
    }
}
