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
using System.Windows;
using System.Windows.Resources;

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

        /// <summary>
        ///     Retrieves the current list of assemblies for the application XAP. 
        ///     Depends on the 'Deployment.Current' property being setup and
        ///     so can only be accessed after the Application object has be completely constructed.
        /// </summary>
        /// <param name="currentDeployment">The current deployment.  Use 'Deployment.Current'.</param>
        /// <remarks>Derived from the MEF source (Preview 9).</remarks>
        public static IEnumerable<Assembly> GetAssemblies(this Deployment currentDeployment)
        {
            // Setup initial conditions.
            if (currentDeployment == null) throw new ArgumentNullException("currentDeployment");
            var assemblies = new List<Assembly>();

            // While this may seem like somewhat of a hack, walking the AssemblyParts in the active 
            // deployment object is the only way to get the list of assemblies loaded by the initial XAP. 
            foreach (AssemblyPart assemblyPart in currentDeployment.Parts)
            {
                var streamResource = Application.GetResourceStream(new Uri(assemblyPart.Source, UriKind.Relative));
                if (streamResource != null)
                {
                    // Keep in mind that calling Load on an assembly that is already loaded will
                    // be a no-op and simply return the already loaded assembly object.
                    var assembly = assemblyPart.Load(streamResource.Stream);
                    assemblies.Add(assembly);
                }
            }

            // Finish up.
            return assemblies;
        }
    }
}
