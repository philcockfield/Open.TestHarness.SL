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
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace Open.Core.Common
{
    /// <summary>Base class for a reference index of resources within a dictionary.</summary>
    public abstract class ResourcesBase
    {
        #region Head
        private Dictionary<string, ResourceDictionary> resourceDictionaries;
        #endregion

        #region Properties
        /// <summary>Gets the resource dictionary containing the templates.</summary>
        public abstract ResourceDictionary Dictionary { get; }
        #endregion

        #region Methods
        /// <summary>Retrieves the specified ControlTemplate.</summary>
        /// <param name="key">The key of the template.</param>
        public ControlTemplate GetControlTemplate(string key)
        {
            return GetItem("ControlTemplate", key) as ControlTemplate;
        }

        /// <summary>Retrieves the specified DataTemplate.</summary>
        /// <param name="key">The key of the template.</param>
        public DataTemplate GetDataTemplate(string key)
        {
            return GetItem("DataTemplate", key) as DataTemplate;
        }

        /// <summary>Loads the specified resource dictionary once.  Repeat calls return the same dictionary.</summary>
        /// <param name="path">The path to the resource dictionary.</param>
        protected ResourceDictionary GetResourceDictionarySingleton(string path)
        {
            return GetResourceDictionarySingleton(GetType().Assembly, path);
        }

        /// <summary>Loads the specified resource dictionary once.  Repeat calls return the same dictionary.</summary>
        /// <param name="assembly">The assembly that the dictionary is contained within.</param>
        /// <param name="path">The path to the resource dictionary.</param>
        protected ResourceDictionary GetResourceDictionarySingleton(Assembly assembly, string path)
        {
            // Setup initial conditions.
            if (resourceDictionaries == null) resourceDictionaries = new Dictionary<string, ResourceDictionary>();

            // Check if an instance already exists.
            var key = assembly.FullName + path;
            if (resourceDictionaries.ContainsKey(key)) return resourceDictionaries[key];

            // Load the resource dictionary and store it.
            var dictionary = assembly.GetResourceDictionary(path);
            resourceDictionaries[key] = dictionary;

            // Finish up.
            return dictionary;
        }
        #endregion

        #region Method - ApplyTemplate
        /// <summary>
        ///    Looks up a control-template with a name corresponding to the class-name of the 
        ///    given control and applies it to the control.
        /// </summary>
        /// <param name="control">The control to apply the template to.</param>
        public void ApplyTemplate(Control control)
        {
            ApplyTemplate(control.GetType(), control);
        }

        /// <summary>
        ///    Looks up a control-template with a name corresponding to the class-name of the 
        ///    given control and applies it to the control.
        /// </summary>
        public void ApplyTemplate<T>(Control control)
        {
            ApplyTemplate(typeof(T), control);
        }

        private void ApplyTemplate(Type type, Control control)
        {
            control.Template = GetControlTemplate(type.Name);
        }
        #endregion

        #region Internal
        private NotFoundException NotFoundError(string resourceName, string key)
        {
            return new NotFoundException(
                string.Format("A {0} with the key '{1}' was not found within the resource dictionary '{2}'.",
                              resourceName,
                              key,
                              Dictionary.Source.OriginalString));
        }

        private object GetItem(string resourceName, string key)
        {
            var item = Dictionary[key];
            if (item == null) throw NotFoundError(resourceName, key);
            return item;
        }
        #endregion
    }
}
