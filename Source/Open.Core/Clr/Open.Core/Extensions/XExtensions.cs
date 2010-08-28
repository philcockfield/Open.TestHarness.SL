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
using System.Xml.Linq;

namespace Open.Core.Common
{
    public static partial class XExtensions
    {
        /// <summary>Retrieves the value of the attribute, if it exists.</summary>
        /// <param name="self">The attribute to examine.</param>
        /// <returns>The attribute value, or null if there was not attribute.</returns>
        public static string ValueOrNull(this XAttribute self)
        {
            return self == null ? null : self.Value;
        }

        /// <summary>
        ///    Retrieves the value of the value of the given attribute, or null if the 
        ///    attribute either does not exist or contained no value (empty string).
        /// </summary>
        /// <param name="self">The element decorated with the attribute.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        public static string GetAttributeValue(this XElement self, string attributeName)
        {
            return self.GetAttributeValue(attributeName, null);
        }

        /// <summary>
        ///    Retrieves the value of the value of the given attribute, or a default value if the 
        ///    attribute either does not exist or contained no value (empty string).
        /// </summary>
        /// <param name="self">The element decorated with the attribute.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="defaultValue">The default value to use if the attribute either does not exist or contained no value (empty string).</param>
        public static string GetAttributeValue(this XElement self, string attributeName, string defaultValue)
        {
            // Setup initial conditions.
            if (self == null) return defaultValue;

            // Retrieve the attribute.
            var attribute = self.Attribute(attributeName);
            if (attribute == null) return defaultValue;

            // Retreive the value.
            var value = attribute.Value.AsNullWhenEmpty();
            return value ?? defaultValue;
        }

        /// <summary>Gets the element value of the specified child element.</summary>
        /// <param name="self">The containing element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <returns>The element value or null if not found.</returns>
        public static string GetChildValue(this XContainer self, string childElementName)
        {
            return self == null ? null : GetValue(self.Element(childElementName));
        }

        /// <summary>Gets the element value of the specified child element.</summary>
        /// <param name="self">The containing element.</param>
        /// <param name="childPrefix">The prefix of the child node.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <returns>The element value or null if not found.</returns>
        public static string GetChildValue(this XElement self, string childPrefix, string childElementName)
        {
            if (self == null) return null;
            var name = self.GetNamespaceOfPrefix(childPrefix) + childElementName;
            return GetValue(self.Element(name));
        }

        private static string GetValue(this XElement xElement)
        {
            return xElement == null ? null : xElement.Value.AsNullWhenEmpty();
        }


        /// <summary>Gets the element value of the specified child element and converts it to a double.</summary>
        /// <param name="self">The containing element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <param name="defaultValue">The default value to return if the child element does not exist.</param>
        /// <returns>The element value (as a double).</returns>
        /// <exception cref="FormatException">Is thrown if the specified element's value cannot be cast to a double.</exception>
        public static double GetChildValueAsDouble(this XContainer self, string childElementName, double defaultValue)
        {
            if (self == null) return defaultValue;
            var xElement = self.Element(childElementName);
            return xElement == null ? defaultValue : Convert.ToDouble(xElement.Value);
        }

        /// <summary>Gets the element value of the specified child element and converts it to a Boolean.</summary>
        /// <param name="self">The containing element.</param>
        /// <param name="childElementName">The name of the child element.</param>
        /// <param name="defaultValue">The default value to return if the child element does not exist.</param>
        /// <returns>A boolean value (False if the ).</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the string does not contain four sides of the Thickness value.</exception>
        public static bool GetChildValueAsBool(this XContainer self, string childElementName, bool defaultValue)
        {
            if (self == null) return defaultValue;
            var xElement = self.Element(childElementName);
            if (xElement == null) return defaultValue;
            var value = xElement.Value.AsNullWhenEmpty();
            return value == null ? defaultValue : Convert.ToBoolean(value);
        }
    }
}

