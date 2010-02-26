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
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Open.Core.Common
{
    public static partial class ReflectionExtensions
    {
        #region General
        /// <summary>Retrieves the next Enum value relative to the given value (cycles from last item to first item).</summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The enum value.</param>
        /// <returns>The next enum value (or the first enum value if the last value was specified).</returns>
        public static T NextValue<T>(this Enum value)
        {
            var values = typeof (T).GetEnumValues().Cast<object>().ToList();
            var index = values.IndexOf(value) + 1;
            if (index == values.Count) index = 0;
            return (T)values[index];
        }
        #endregion

        #region Type
        /// <summary>Returns true if this type has an IS-A relationship with the given type, including implmenting it's interface</summary>
        /// <remarks>ClassA.IsA(ClassA) == true</remarks>
        public static bool IsA<T>(this Type self)
        {
            return self.IsA(typeof(T));
        }

        /// <summary>Returns true if this type has an IS-A relationship with the given type, including implmenting it's interface</summary>
        /// <remarks>ClassA.IsA(ClassA) == true</remarks>
        public static bool IsA(this Type self, Type baseType)
        {
            return baseType.IsAssignableFrom(self);
        }

        /// <summary>Determines whether the object is a number.</summary>
        /// <param name="self">The type to examine.</param>
        public static bool IsNumeric(this object self)
        {
            return self != null && self.GetType().IsNumeric();
        }


        /// <summary>Determines whether the type represents a number.</summary>
        /// <param name="self">The type to examine.</param>
        public static bool IsNumeric(this Type self)
        {
            if (self == null) return false;
            var typeCode = Type.GetTypeCode(self);

            switch (typeCode)
            {
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
            }

            return false;
        }

        /// <summary>Gets the type from the object if the object is not null.</summary>
        /// <param name="self">The object to get the type from.</param>
        /// <returns>The object's Type, or null if the object is not available.</returns>
        /// <remarks>This exists as an easy way for dealing with potentially null struct/enum values.</remarks>
        public static Type GetTypeOrNull(this object self)
        {
            return self == null ? null : self.GetType();
        }
        #endregion

        #region Assembly
        /// <summary>Returns all of the types that have an IsA() relation to the given baseType</summary>
        public static Type[] GetAllTypesOf(this Assembly self, Type baseType)
        {
            return (from t in self.GetTypes()
                    where t.IsA(baseType)
                    select t).ToArray();
        }

        /// <summary>Retrieves the complete set of attributes of the given type from an assembly.</summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="assembly">The assembly to look within.</param>
        public static IEnumerable<T> GetAttributes<T>(this Assembly assembly) where T : Attribute
        {
            var list = new List<T>();
            foreach (var type in assembly.GetTypes())
            {
                AddAttributes(list, type);
                AddAttributes(list, type.GetMembers());
            }
            return list;
        }

        private static void AddAttributes<T>(List<T> list, params MemberInfo[] members) where T : Attribute
        {
            foreach (var member in members)
            {
                list.AddRange(member.GetCustomAttributes(typeof(T), true).Cast<T>());
            }
        }

        /// <summary>Extracts the name part from the assembly.</summary>
        /// <param name="assembly">The assembly to retrieve the name of.</param>
        /// <returns>The name part of the assembly-name.</returns>
        /// <remarks>
        ///    For example:<BR/>
        ///    Open.TestHarness.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null<BR/>
        ///       would return:<BR/>
        ///    Open.TestHarness.Test<BR/>
        /// </remarks>
        public static string GetAssemblyName(this Assembly assembly)
        {
            if (assembly == null) return null;
            var name = assembly.FullName;
            return name == null ? null : name.Split(",".ToCharArray())[0];
        }
        #endregion

        #region PropertyInfo
        /// <summary>Gets whether the PropertyInfo is static or not.</summary>
        /// <param name="self">The property definition to examine.</param>
        public static bool IsStatic(this PropertyInfo self)
        {
            // Setup initial conditions.
            if (self == null) throw new ArgumentNullException("self");

            // Retrieve the getter/setter for the property.
            MethodInfo getter = null;
            MethodInfo setter = null;
            if (self.CanRead) getter = self.GetGetMethod() ?? self.GetGetMethod(true);
            if (self.CanWrite) setter = self.GetSetMethod() ?? self.GetSetMethod(true);

            // Check whether the getter/setter is static.
            return
                (getter != null && getter.IsStatic) ||
                (setter != null && setter.IsStatic);
        }
        #endregion
    }
}
