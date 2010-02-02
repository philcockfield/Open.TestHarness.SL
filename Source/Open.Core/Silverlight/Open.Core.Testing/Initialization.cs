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

using System.Linq;
using System.Reflection;
using System.Windows;
using Microsoft.Silverlight.Testing;

namespace Open.Core.Common.Testing
{
    public static class Initialization
    {
        #region Head
        private const string TagCurrent = "current";
        #endregion

        #region Methods
        /// <summary>Looks for tags within the assembly, and applies them if necessary.</summary>
        /// <param name="settings">The test settings.</param>
        /// <param name="applicationArgs">The args passed to the 'Application_Startup' method of the Silverlight application.</param>
        /// <remarks>If the 'current' tag is present anywhere within the assembly, this is the only tag which is applied.</remarks>
        public static void AssignTags(UnitTestSettings settings, StartupEventArgs applicationArgs)
        {
            // Setup initial conditions.
            var callingAssembly = Assembly.GetCallingAssembly();

            // Add the 'current' tag if one exists within the assembly.
            if (HasCurrentTag(callingAssembly))
            {
                settings.TagExpression = TagCurrent;
                return;
            }
        }
        #endregion

        #region Internal
        private static bool HasCurrentTag(Assembly assembly)
        {
            // Check for classes that are tagged as "current".
            var taggedClasses = 
                from t in assembly.GetTypes()
                where t.GetCustomAttributes(typeof(TagAttribute), true).Count(attr => ((TagAttribute)attr).Tag == TagCurrent) > 0 
                select t;
            if (taggedClasses.Count() > 0) return true;

            var testMethods =
                from t in assembly.GetTypes()
                where t.GetMethods().Where(item => item.GetCustomAttributes(typeof(TagAttribute), true).Count(attr => ((TagAttribute)attr).Tag == TagCurrent) > 0).Count() > 0
                select t;
            return testMethods.Count() > 0;
        }
        #endregion
    }
}
