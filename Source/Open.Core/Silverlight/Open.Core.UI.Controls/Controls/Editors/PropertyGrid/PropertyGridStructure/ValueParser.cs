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
using System.IO;
using System.Windows.Media;
using System.Diagnostics;
using Open.Core.Common.Controls.Editors.PropertyGridStructure.Converters;

namespace Open.Core.Common.Controls.Editors.PropertyGridStructure
{
    /// <summary>Contains parsing routines for converting values.</summary>
    public static class ValueParser
    {
        #region Head
        public const string NullLabel = "<Null>";
        #endregion

        #region Methods
        /// <summary>The default string formatter for converting a property value to a display string.</summary>
        /// <param name="property">The property to format.</param>
        /// <returns>A string representation of the property's current value.</returns>
        public static string FormatValueToString(PropertyModel property)
        {
            // Setup initial conditions.
            var value = property.Value;
            var type = property.Definition.PropertyType;
            var typeFullName = type.FullName;
            var typeName = type.Name;

            // Retrieve the value, and check for Null.
            if (value == null) return NullLabel;

            // Check if an explicit text-value has been defined on the attribute.
            var attr = property.PropertyGridAttribute;
            if (attr != null && attr.Value.AsNullWhenEmpty() != null) return attr.Value;

            // Check if the object is a color.
            if (IsColor(property.Definition.PropertyType))
            {
                var color = ToColor(value);
                return string.Format("R:{0}, G:{1}, B:{2}, A:{3}", color.R, color.G, color.B, color.A);
            }

            // Convert the value to a string.
            var textValue = value.ToString();
            if (textValue == typeFullName || property.Definition.PropertyType.IsAssignableFrom(typeof(Stream)))
            {
                // If the value emitted a full type-name, shorten it.
                textValue = string.Format("[{0}]", typeName);
            }
            return textValue;
        }
        #endregion

        #region Methods - Parse Value
        /// <summary>Parses a string representation of a value converting it to it's native type.</summary>
        /// <param name="textValue">The text display version of the value to parse.</param>
        /// <param name="property">The definition of the property the value pertains to.</param>
        /// <param name="error">An error to return if parsing was not successful (null if parsed successfully).</param>
        /// <returns>The parsed value in it's native type/</returns>
        public static object ParseValue(string textValue, PropertyModel property, out Exception error)
        {
            // Setup initial conditions.
            var propType = property.Definition.PropertyType;
            error = null;

            // Retrieve the appropriate value converter.
            var converter = TypeConverterHelper.GetConverter(propType);

            // Attempt to perform the value conversion.
            try
            {
                return converter != null ? converter.ConvertFrom(textValue) : null;
            }
            catch (Exception e)
            {
                error = e;
                return null;
            }
        }
        #endregion

        #region Internal
        internal static Color ToColor(object value)
        {
            if (value == null) return default(Color);
            var type = value.GetType();
            if (type.IsAssignableFrom(typeof(Color))) return (Color)value;
            if (type.IsAssignableFrom(typeof(SolidColorBrush))) return ((SolidColorBrush)value).Color;
            return default(Color);
        }

        private static bool IsColor(Type type)
        {
            return (typeof(SolidColorBrush).IsAssignableFrom(type) || typeof(Color).IsAssignableFrom(type));
        }
        #endregion
    }
}
