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
using System.Reflection;
using System.ComponentModel;
using Open.Core.Common.Controls.Editors.PropertyGridStructure;
using Open.Core.Common.Controls.Editors.PropertyGridStructure.Converters;

namespace Open.Core.Common.Controls.Editors
{
    /// <summary>Represents a property.  Used by the PropertyGrid.</summary>
    public class PropertyModel : ModelBase
    {
        #region Head
        public const string PropDefinition = "Definition";
        public const string PropParentInstance = "ParentInstance";
        public const string PropCategory = "Category";
        public const string PropValue = "Value";

        private readonly PropertyInfo definition;
        private readonly object parentInstance;
        private bool isSettingValue;
        private FormatValueToString formatValueToString;

        public PropertyModel(object parentInstance, PropertyInfo definition)
        {
            // Store values.
            this.definition = definition;
            this.parentInstance = parentInstance;
        }
        #endregion

        #region Properties
        /// <summary>Gets the meta-data definition of the property.</summary>
        public PropertyInfo Definition
        {
            get { return definition; }
        }

        /// <summary>Gets the object instance that the property is exposed on.</summary>
        public object ParentInstance
        {
            get { return parentInstance; }
        }

        /// <summary>Gets the display name of the property.</summary>
        /// <remarks>This is either the actual name of the property, or the overriden name declared within the [PropertyGrid] attribute.</remarks>
        public string DisplayName
        {
            get
            {
                var attr = PropertyGridAttribute;
                var name = attr == null ? null : attr.Name.AsNullWhenEmpty();
                return name ?? Definition.Name;
            }
        }

        /// <summary>Gets the current value of the property.</summary>
        public object Value
        {
            get { return Definition.GetValue(ParentInstance, null); }
            set
            {
                if (isSettingValue) return; // Avoid circular callback loops with NotifyPropertyChanged.
                isSettingValue = true;

                Definition.SetValue(ParentInstance, value, null);

                OnPropertyChanged(PropValue);
                isSettingValue = false;
            }
        }

        /// <summary>Gets the category attribute decorating the property (or null if the attribute is not declared).</summary>
        public CategoryAttribute CategoryAttribute
        {
            get
            {
                var attributes = Definition.GetCustomAttributes(typeof(CategoryAttribute), true);
                return attributes.Length == 0 ? null : ((CategoryAttribute)attributes[0]);
            }
        }

        /// <summary>Gets the property-grid attribute which provides additional display instructions (or null if the attribute is not declared).</summary>
        public PropertyGridAttribute PropertyGridAttribute
        {
            get
            {
                var attributes = Definition.GetCustomAttributes(typeof(PropertyGridAttribute), true);
                return attributes.Length == 0 ? null : ((PropertyGridAttribute)attributes[0]);
            }
        }

        /// <summary>Gets or sets the delegate to use to format the property value to a display string.</summary>
        public FormatValueToString FormatValueToString
        {
            get
            {
                if (formatValueToString == null) formatValueToString = ValueParser.FormatValueToString;
                return formatValueToString;
            }
            set { formatValueToString = value; }
        }
        #endregion

        #region Methods
        /// <summary>Converts the property's current value to a string.</summary>
        /// <param name="displayNull">Flag indicating if a display version of 'Null' should be returned (true) or an actual Null value (false) if the value is null.</param>
        /// <returns>The value as a string, or null if there is no current value.</returns>
        /// <remarks>To override the formatting for this property see the 'FormatValueToString' delegate property.</remarks>
        public string ToValueString(bool displayNull)
        {
            if (Value == null && !displayNull) return null;
            return FormatValueToString(this);
        }
        #endregion

        #region Methods - Static
        /// <summary>Gets models for each property on the specified object.</summary>
        /// <param name="instance">The instance to read properties from.</param>
        /// <param name="includeHierarchy">Flag indicating if properties from all levels of the inheritance tree should be included.</param>
        /// <returns>A collection of property models.</returns>
        public static ObservableCollection<PropertyModel> GetProperties(object instance, bool includeHierarchy)
        {
            // Setup initial conditions.
            var list = new ObservableCollection<PropertyModel>();

            // Retrieve the properties.
            var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
            if (!includeHierarchy) flags = flags | BindingFlags.DeclaredOnly;
            var properties = instance.GetType().GetProperties(flags);

            // Build return list of models.
            foreach (var property in properties)
            {
                list.Add(new PropertyModel(instance, property));
            }

            // Finish up.
            return list;
        }
        #endregion
    }
}
