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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Open.Core.Common;

namespace Open.TestHarness.Model
{
    /// <summary>Represents a set of classes decorated with the [ViewTestClass] attribute.</summary>
    public class ViewTestClassesModule : ModelBase
    {
        #region Head
        public const string PropClasses = "Classes";
        public const string PropDisplayName = "DisplayName";

        private string displayName = "Loading...";
        private readonly ObservableCollection<ViewTestClass> classes = new ObservableCollection<ViewTestClass>();
        #endregion

        #region Properties
        /// <summary>Gets or sets the name that describes the set of classes.</summary>
        public string DisplayName
        {
            get { return displayName; }
            set
            {
                if (value == DisplayName) return;
                displayName = value;
                OnPropertyChanged(PropDisplayName);
            }
        }

        /// <summary>Gets the list of classes.</summary>
        public ObservableCollection<ViewTestClass> Classes{get { return classes; }}

        /// <summary>Returns the current class (the 'ViewTestClass' marked as IsCurrent).</summary>
        public ViewTestClass CurrentClass
        {
            get { return Classes.FirstOrDefault(item => item.IsCurrent); }
        }

        /// <summary>The parent model.</summary>
        protected static TestHarnessModel TestHarness { get { return TestHarnessModel.Instance; } }
        #endregion

        #region Methods
        /// <summary>Adds references to [ViewTestClass]'s from the given assembly.</summary>
        /// <param name="assembly">The assembly to load from.</param>
        /// <param name="xapFileName">The name of the XAP file containing the class.</param>
        /// <returns>The number of classes found within the assembly.</returns>
        public int AddFromAssembly(Assembly assembly, string xapFileName)
        {
            // Retrieve the set of classes.
            var viewTestClasses = 
                        from type in assembly.GetTypes()
                        where type.IsPublic && type.GetCustomAttributes(typeof(ViewTestClassAttribute), false).FirstOrDefault() != null
                        orderby type.Name
                        select type;

            // Create corresponding set of models and add to 'Classes' property.
            var list = new List<ViewTestClass>();
            foreach (var type in viewTestClasses)
            {
                list.Add(ViewTestClass.GetSingleton(type, xapFileName));
            }
            Classes.AddRange(list);

            // Finish up.
            var count = list.Count;
            if (count > 0) OnPropertyChanged(PropClasses);
            return count;
        }
        #endregion
    }
}
