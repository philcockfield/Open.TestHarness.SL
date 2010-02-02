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
    /// <summary>Utility functions for working with the Reflection system.</summary>
    /// <remarks>
    ///    Most reflection utilities are created uing extension-methods.  
    ///    This class is for funtions where you don't already have an instance of Reflection object you want to operate on.
    /// </remarks>
    public static class ReflectionUtil
    {
        #region Assembly
        /// <summary>Extracts the name part from the given fully-qualified assembly name.</summary>
        /// <param name="assemblyName">The full name of the assembly.</param>
        /// <returns>The name part of the assembly-name.</returns>
        /// <remarks>
        ///    For example:<BR/>
        ///    TestHarness.Silverlight.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null<BR/>
        ///       would return:<BR/>
        ///    TestHarness.Silverlight.Test<BR/>
        /// </remarks>
        public static string GetAssemblyName(string assemblyName)
        {
            assemblyName = assemblyName.AsNullWhenEmpty();
            return assemblyName == null ? null : assemblyName.Split(",".ToCharArray())[0];
        }
        #endregion

        #region Methods - Enum
        /// <summary>Gets the collection of values from the given enum type.</summary>
        /// <param name="enumType">The type of the enum to examine.</param>
        /// <returns>A list of enum values.</returns>
        /// <exception cref="ArgumentException">Is thrown if a non-Enum type is passed.</exception>
        public static List<object> GetEnumValues(Type enumType)
        {
            // Setup initial conditions.
            if (!enumType.IsEnum) throw new ArgumentException(string.Format("The specified type '{0}' is not an enum.", enumType.Name));

            // Filter for literal fields.
            var fields = from field in enumType.GetFields()
                                     where field.IsLiteral
                                     select field.GetValue(enumType);

            // Finish up.
            return fields.Cast<object>().ToList();
        }
        #endregion
    }
}
