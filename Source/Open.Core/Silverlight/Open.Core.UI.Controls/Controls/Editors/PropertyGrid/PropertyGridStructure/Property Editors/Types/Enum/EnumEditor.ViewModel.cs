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
using System.Linq;

namespace Open.Core.Common.Controls.Editors.PropertyGridStructure.Editors
{
    public class EnumEditorViewModel : EditorViewModelBase
    {
        #region Head
        public const string PropSelectedValue = "SelectedValue";

        private int selectedIndex;

        public EnumEditorViewModel(PropertyModel model) : base(model)
        {
            EnumValues = EnumValue.GetValues(model.Definition.PropertyType);

            var currentValue = Value;
            var selectedValue = EnumValues.FirstOrDefault(item => Equals(currentValue, item.Value));
            SelectedIndex = EnumValues.IndexOf(selectedValue);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the boolean value.</summary>
        public object Value
        {
            get { return Model.Value; }
            set
            {
                if (value == Value) return;
                Model.Value = value;
                OnPropertyChanged(PropValue, PropSelectedValue);
            }
        }

        /// <summary>Gets the collection of enumeration values to display in the drop-down list.</summary>
        public ObservableCollection<EnumValue> EnumValues { get; private set; }

        /// <summary>Gets or sets the currently selected value.</summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (value == SelectedIndex) return;
                selectedIndex = value;
                Value = value < 0 ? null : EnumValues[value].Value;
            }
        }
        #endregion
    }

    /// <summary>Represents an Enumeration value.</summary>
    /// <remarks>Used to bind to a drop-down.</remarks>
    public class EnumValue
    {
        public string Name{ get; set;}
        public object Value { get; set; }
        public override string ToString() { return Name; }

        public static ObservableCollection<EnumValue> GetValues(Type enumType)
        {
            var collection = new ObservableCollection<EnumValue>();
            foreach (var item in ReflectionUtil.GetEnumValues(enumType))
            {
                collection.Add(new EnumValue{Name = item.ToString(), Value = item});
            }
            return collection;
        }
    }
}
