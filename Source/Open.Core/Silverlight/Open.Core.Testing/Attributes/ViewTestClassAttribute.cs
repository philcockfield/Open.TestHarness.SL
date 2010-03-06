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

namespace Open.Core.Common
{
    /// <summary>Declares a class as a container of [ViewTest]'s.</summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ViewTestClassAttribute : Attribute
    {
        #region Head
        public ViewTestClassAttribute()
        {
            // Set default values.
            IsPropertyExplorerVisible = true;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the display name of the test class.</summary>
        public string DisplayName { get; set; }

        /// <summary>Gets or sets whether the property editor pane is visible (default is True).</summary>
        public bool IsPropertyExplorerVisible { get; set; }
        #endregion

        #region Methods - Static
        /// <summary>Gets the display name of the test class from the given object instance.</summary>
        /// <param name="instance">The instance to examine.</param>
        /// <returns>The display name.</returns>
        /// <exception cref="ArgumentException">Thrown if instance is not decorated with the [ViewTestClass] attribute.</exception>
        public static string GetDisplayName(object instance)
        {
            // Setup initial conditions.
            var type = instance.GetType();

            // Retrieve the attribute from the object.
            var attribute = type.GetCustomAttributes(typeof(ViewTestClassAttribute), false).FirstOrDefault() as ViewTestClassAttribute;
            if (attribute == null) throw new ArgumentException(string.Format("Instance must have the [{0}] attribute.", typeof(ViewTestClassAttribute).Name));

            // Determine if an explicit display name exists on the attribute, otherwise return the formatted class name.
            return attribute.DisplayName.AsNullWhenEmpty() != null ? attribute.DisplayName : type.Name.FormatUnderscores();
        }
        #endregion
    }
}
