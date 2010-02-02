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

// Original source from: http://slg30.codeplex.com/
// Licence: Ms-PL ( http://slg30.codeplex.com/license )
// NB: No inclusion of licence, copyright data within source files
// Included: May 25, 2009
// This source has been refactored and modified.

using System;
using System.ComponentModel;
using System.Windows;

namespace Open.Core.Common.Controls.Editors.PropertyGridStructure.Converters
{
	internal class TypeConverterHelper
	{
        /// <summary>Gets a type converter for the specified type.</summary>
        /// <param name="type">The type of the value to convert.</param>
        /// <returns>A type converter, or null if not corresponding converter exists.</returns>
        public static TypeConverter GetConverter(Type type)
        {
            return GetCoreConverterFromCoreType(type) ?? GetCoreConverterFromCustomType(type);
        }

	    private static TypeConverter GetCoreConverterFromCoreType(Type type)
	    {
	        if (type == typeof(int)) return new Int32Converter();
	        if (type == typeof(short)) return new Int16Converter();
	        if (type == typeof(long)) return new Int64Converter();
	        if (type == typeof(bool?)) return new NullableBoolConverter();
	        if (type == typeof(double)) return new DoubleConverter();
	        if (type == typeof(float)) return new SingleConverter();
	        if (type == typeof(byte)) return new ByteConverter();
	        if (type == typeof(sbyte)) return new SByteConverter();
	        if (type == typeof(char)) return new CharConverter();
	        if (type == typeof(decimal)) return new DecimalConverter();
            if (type == typeof(string)) return new StringConverter();
            if (type == typeof(Thickness)) return new ThicknessConverter();
            return null;
	    }

	    private static TypeConverter GetCoreConverterFromCustomType(Type type)
	    {
	        if (typeof(int).IsAssignableFrom(type)) return new Int32Converter();
	        if (typeof(short).IsAssignableFrom(type)) return new Int16Converter();
	        if (typeof(long).IsAssignableFrom(type)) return new Int64Converter();
	        if (typeof(double).IsAssignableFrom(type)) return new DoubleConverter();
	        if (typeof(float).IsAssignableFrom(type)) return new SingleConverter();
	        if (typeof(byte).IsAssignableFrom(type)) return new ByteConverter();
	        if (typeof(sbyte).IsAssignableFrom(type)) return new SByteConverter();
	        if (typeof(char).IsAssignableFrom(type)) return new CharConverter();
	        if (typeof(decimal).IsAssignableFrom(type)) return new DecimalConverter();
	        if (typeof(string).IsAssignableFrom(type)) return new StringConverter();
            if (type == typeof(Thickness)) return new ThicknessConverter();
            return null;
	    }
	}
}
