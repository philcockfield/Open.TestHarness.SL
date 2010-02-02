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
using System.Reflection;
using System.Windows;

namespace Open.Core.Common
{
    public static partial class ReflectionExtensions
    {
        /// <summary>Gets the specified resource-dictionary from within the assembly.</summary>
        /// <param name="assembly">The assembly to look within.</param>
        /// <param name="path">The absolute path to the XAML file from the root of the assembly.</param>
        /// <returns>The specified resource dictionary.</returns>
        /// <remarks>Make sure the XAML file's build-action is set to 'Page'.</remarks>
        public static ResourceDictionary GetResourceDictionary(this Assembly assembly, string path)
        {
            path = path.TrimStart("/".ToCharArray());
            path = string.Format("/{0};component/{1}", assembly.GetAssemblyName(), path);
            try
            {
                return new ResourceDictionary { Source = new Uri(path, UriKind.Relative) };
            }
            catch (Exception e)
            {
                // Not found.
                throw new NotFoundException(string.Format("A resource dictionary at the location '{0}' was not found.", path), e);
            }
        }
    }
}
