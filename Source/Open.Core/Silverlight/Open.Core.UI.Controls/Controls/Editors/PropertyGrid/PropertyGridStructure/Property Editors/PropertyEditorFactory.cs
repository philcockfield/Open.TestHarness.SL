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
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common.Controls.Editors.PropertyGridStructure.Editors;

namespace Open.Core.Common.Controls.Editors.PropertyGridStructure
{
    internal static class PropertyEditorFactory
    {
        #region Head
        private readonly static List<Type> stringEditorTypes =new List<Type>
                                                                    {
	                                                                    typeof(string),
                                                                        typeof(int),
	                                                                    typeof(short),
	                                                                    typeof(long),
	                                                                    typeof(double),
	                                                                    typeof(float),
	                                                                    typeof(byte),
	                                                                    typeof(sbyte),
	                                                                    typeof(char),
	                                                                    typeof(decimal),
	                                                                    typeof(Thickness),
                                                                    };

        private readonly static List<Type> booleanEditorTypes = new List<Type>
                                                                    {
	                                                                    typeof(bool),
	                                                                    typeof(bool?),
                                                                    };
        #endregion

        #region Methods
        /// <summary>Gets an editor control corresponding to the specified property.</summary>
        /// <param name="property">The property to get an editor for.</param>
        /// <returns>A property editor control, or null if the property cannot be edited, or does not have and editor.</returns>
        public static Control GetEditor(PropertyModel property)
        {
            // Setup initial conditions.
            if (!property.Definition.CanWrite) return null;
            var type = property.Definition.PropertyType;
            Control editor;

            // Look for a suitable editor.
            if (typeof(Enum).IsAssignableFrom(type)) return new EnumEditor { ViewModel = new EnumEditorViewModel(property) };
            if (IsColor(type)) return new ColorEditor { ViewModel = new ColorEditorViewModel(property) { IsPopup = true } };
            if (typeof(Stream).IsAssignableFrom(type)) return new StreamEditor { ViewModel = new StreamEditorViewModel(property) };
            if (typeof(string).IsAssignableFrom(type)) return new StringEditor { ViewModel = new StringEditorViewModel(property){UpdateOnKeyPress = true} };

            // Look for editors that support multiple types.
            if (CreateCorrespondingEditor(property, stringEditorTypes, out editor, () => new StringEditor { ViewModel = new StringEditorViewModel(property) })) return editor;
            if (CreateCorrespondingEditor(property, booleanEditorTypes, out editor, () => new BooleanEditor { ViewModel = new BooleanEditorViewModel(property) })) return editor;

            // Finish up.
            return null;
        }
        #endregion

        #region Internal
        private static bool CreateCorrespondingEditor(PropertyModel property, IEnumerable<Type> types, out Control editor, Func<Control> createEditor)
        {
            editor = null;
            var type = property.Definition.PropertyType;
            foreach (var editorType in types)
            {
                if (editorType.IsAssignableFrom(type))
                {
                    editor = createEditor();
                    return true;
                }
            }
            return false;
        }

        private static bool IsColor(Type type)
        {
            return (typeof (Brush).IsAssignableFrom(type) || typeof (Color).IsAssignableFrom(type));
        }
        #endregion
    }
}
