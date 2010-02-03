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
using System.Linq;
using System.Reflection;

namespace Open.Core.Common
{
    /// <summary>Silverlight version of 'ReflectionExtensions'.</summary>
    public static partial class ReflectionExtensions
    {
        /// <summary>Retrieves the version of the assembly.</summary>
        /// <param name="assembly">The assembly to examine.</param>
        public static Version Version(this Assembly assembly)
        {
            if (assembly == null) return null;
            var fullName = assembly.FullName.AsNullWhenEmpty();
            if (fullName == null) return null;

            var parts = fullName.Split(',');
            var version = parts[1].Trim().RemoveStart("Version=");

            return new Version(version);
        }

        /// <summary>Retrieves the collection of Enum values for the specified enum type.</summary>
        /// <param name="enumType">The type of enum to retrieve values for.</param>
        /// <remarks>This is necessary because the .NET 'GetValues' method for Enum was omitted from Silverlight.</remarks>
        public static object[] GetEnumValues(this Type enumType)
        {
            // Setup initial conditions.
            if (!enumType.IsEnum) throw new ArgumentException(string.Format("The type '{0}' is not an enum.", enumType.Name));

            // Retrieve the liberal fields (these are the enum values).
            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field.GetValue(enumType);


            // Finish up.
            return fields.ToArray();
        }
    }
}
