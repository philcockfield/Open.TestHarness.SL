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

using System.Diagnostics;
using System.Windows;
namespace Open.Core.Common.Controls.Editors.PropertyGridStructure.Editors
{
    public class BooleanEditorViewModel : EditorViewModelBase
    {
        #region Head
        public const string PropTrueIsChecked = "TrueIsChecked";
        public const string PropFalseIsChecked = "FalseIsChecked";
        public const string PropNullIsChecked = "NullIsChecked";
        public const string PropNullVisibility = "NullVisibility";

        public BooleanEditorViewModel(PropertyModel model) : base(model)
        {
            IsNullBool = model.Definition.PropertyType.IsAssignableFrom(typeof(bool?));
        }
        #endregion

        #region Properties
        /// <summary>Gets whether the Boolean is a nullable.</summary>
        public bool IsNullBool{ get; private set;}

        /// <summary>Gets or sets the boolean value.</summary>
        public bool? Value
        {
            get
            {
                if (!IsNullBool) return (bool) Model.Value;
                return Model.Value == null ? null : (bool?) Model.Value;
            }
            set
            {
                if (value == Value) return;
                Model.Value = value;
                OnPropertyChanged(PropValue, PropTrueIsChecked, PropFalseIsChecked, PropNullIsChecked);
            }
        }

        public bool TrueIsChecked
        {
            get
            {
                return Value == null ? false : Value.Value;
            }
            set { if (value) Value = true; }
        }
        public bool FalseIsChecked
        {
            get { return Value == null ? false : !Value.Value; }
            set { if (value) Value = false; }
        }

        public bool NullIsChecked
        {
            get { return Value == null; }
            set { if (value) Value = null; }
        }
        #endregion
    }
}
