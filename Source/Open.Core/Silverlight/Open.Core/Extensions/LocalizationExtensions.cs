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
using System.Diagnostics;
using System.Reflection;
using System.Resources;

namespace Open.Core.Common
{
    /// <summary>Extensions for performing localization on strings.</summary>
    public static partial class LocalizationExtensions
    {
        #region Head
        private static readonly Dictionary<Assembly, ResourceManager> resourceManagers = new Dictionary<Assembly, ResourceManager>();
        #endregion

        #region Methods
        /// <summary>Converts the given key to it's localized form.</summary>
        /// <param name="key">The key to lookup in the asset library.</param>
        /// <returns>A localized string object</returns>
        /// <remarks>
        ///    This method assumes that the resource file is named 'StringLibrary.resx' 
        ///    (or standard regional variant, such as 'StringLibrary.de.resx' or 'StringLibrary.de-DE.resx')
        ///    and placed within the project.<BR/>
        ///    This can be placed anywhere within the project, but only one file with this name should be present.
        /// </remarks>
        public static string ToLocalString(this string key)
        {
            return ToLocalString(key, Assembly.GetCallingAssembly());
        }

        /// <summary>Converts the given key to it's localized form.</summary>
        /// <param name="key">The key to lookup in the asset library.</param>
        /// <param name="assembly">The assembly to use as a content lookup.</param>
        /// <returns>A localized string object</returns>
        /// <remarks>
        ///    This method assumes that the resource file is named 'StringLibrary.resx' 
        ///    (or standard regional variant, such as 'StringLibrary.de.resx' or 'StringLibrary.de-DE.resx')
        ///    and placed within the project.<BR/>
        ///    This can be placed anywhere within the project, but only one file with this name should be present.
        /// </remarks>
        public static string ToLocalString(this string key, Assembly assembly)
        {
            // Look for the string in the given assembly.
            var value = GetLocalStringValue(key, assembly);

            // If not found look in this 'Common' assembly for a matching string.
            if (value == null) value = GetLocalStringValue(key, typeof(LocalizationExtensions).Assembly);

            // Format string if not found.
            if (value == null) value = string.Format("['{0}' NOT FOUND]", key);

            // Finish up.
            return value;
        }

        private static string GetLocalStringValue(string key, Assembly assembly)
        {
            var library = GetStringLibrary(assembly);
            return library == null ? null : library.GetString(key);
        }

        /// <summary>Gets the 'StringLibrary' resource for the given assembly.</summary>
        /// <param name="assembly">The assembly containing the resource.</param>
        /// <returns>A resource-manager.</returns>
        /// <remarks>
        ///    This method assumes that the resource file is named 'StringLibrary.resx' 
        ///    (or standard regional variant, such as 'StringLibrary.de.resx' or 'StringLibrary.de-DE.resx')
        ///    and placed within the project.<BR/>
        ///    This can be placed anywhere within the project, but only one file with this name should be present.
        /// </remarks>
        public static ResourceManager GetStringLibrary(this Assembly assembly)
        {
            // Setup initial conditions.
            if (resourceManagers.ContainsKey(assembly)) return resourceManagers[assembly];

            // Find the matching name.
            string name = null;
            foreach (var fileName in assembly.GetManifestResourceNames())
            {
                if (fileName.EndsWith("StringLibrary.resources"))
                {
                    name = fileName;
                    break;
                }
            }
            if (name == null) return null;
            name = name.TrimEnd(".resources".ToCharArray());

            // Construct the resource manager and cache it.
            var resourceManager = new ResourceManager(name, assembly);
            resourceManagers.Add(assembly, resourceManager);

            // Finish up.
            return resourceManager;
        }
        #endregion
    }
} 
