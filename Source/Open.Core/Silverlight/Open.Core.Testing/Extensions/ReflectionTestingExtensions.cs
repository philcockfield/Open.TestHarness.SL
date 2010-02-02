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

namespace Open.Core.Common.Testing
{
    /// <summary>Specialized unit-test extensions for making assertions that involve reflection.</summary>
    public static class ReflectionTestingExtensions
    {
        #region Methods - Reflection

        public static void ShouldHaveValuesForAllProperties(this object self)
        {
            self.ShouldHaveValuesForAllProperties<object>();
        }

        public static void ShouldHaveValuesForAllProperties<TPropertyType>(this object self)
        {
            self.ShouldHaveValuesForAllProperties<TPropertyType>(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
        }

        public static void ShouldHaveValuesForAllProperties(this object self, BindingFlags bindingFlags)
        {
            self.ShouldHaveValuesForAllProperties<object>(bindingFlags);
        }

        public static void ShouldHaveValuesForAllProperties<TPropertyType>(this object self, BindingFlags bindingFlags)
        {
            // Setup initial conditions.
            if (self == null) throw new ArgumentNullException("self", "Cannot check for property values on a null reference.");

            // Retrieve the set of specified properties to examine.
            var properties =
                        from n in self.GetType().GetProperties(bindingFlags)
                        where n.PropertyType.IsA(typeof(TPropertyType))
                        select n;

            // Examine each property.
            var errors = new List<PropertyInfo>();
            foreach (var propertyInfo in properties)
            {
                if (!propertyInfo.CanRead) continue;
                var instance = propertyInfo.IsStatic() ? null : self;
                if (propertyInfo.GetValue(instance, new object[]{}) == null) errors.Add(propertyInfo);
            }

            // Check if there were any errors.
            if (errors.Count == 0) return;
            var msg = "";
            foreach (var propertyInfo in errors)
            {
                msg += string.Format(" - {0}\n", propertyInfo.Name);
            }
            throw new AssertionException(string.Format("The following properties were did not return values:\n{0}", msg));
        }
        #endregion

    }
}
