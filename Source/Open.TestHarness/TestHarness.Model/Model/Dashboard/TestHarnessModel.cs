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
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Open.Core.Common;
using Open.Core.Common.Network;

[assembly: InternalsVisibleTo("Open.TestHarness.Test")]

namespace Open.TestHarness.Model
{
    /// <summary>The model representation of the TestHarness.</summary>
    public class TestHarnessModel : ModelBase
    {
        #region Head
        public const string PropCurrentClass = "CurrentClass";
        public const string PropRecentSelectionsModule = "RecentSelectionsModule";

        private readonly ObservableCollection<Assembly> loadedAssemblies = new ObservableCollection<Assembly>();
        private readonly ModuleLoader moduleLoader;
        private ViewTestClass currentClass;

        static TestHarnessModel() { ResetSingleton(); }
        private TestHarnessModel()
        {
            // Setup initial conditions.
            Settings = new TestHarnessSettings(this);
            moduleLoader = new ModuleLoader(this);

            // Finish up.
            Refresh();
       }
        #endregion

        #region Properties
        /// <summary>Gets the singleton instance of the TestHarness.</summary>
        public static TestHarnessModel Instance { get; private set; }

        /// <summary>Gets the TestHarness settings.</summary>
        public TestHarnessSettings Settings { get; private set; }

        /// <summary>Gets the collection of modules.</summary>
        public ObservableCollection<ViewTestClassesModule> Modules { get { return moduleLoader.Modules; } }

        /// <summary>Gets the recent selections module (or null if the modules have not been built).</summary>
        public ViewTestClassesModule RecentSelectionsModule { get { return moduleLoader.RecentSelectionsModule; } }

        /// <summary>Gets the collection of loaded assemblies.</summary>
        public ObservableCollection<Assembly> LoadedAssemblies{get { return loadedAssemblies; }}

        /// <summary>Gets the currently selected [TestClass].</summary>
        public ViewTestClass CurrentClass
        {
            get { return currentClass; }
            internal set
            {
                // Setup initial conditions.
                if (value == CurrentClass) return;

                // Store values.
                var oldClass = currentClass;
                currentClass = value;

                // Alert listeners that the existing class is no longer current.
                if (oldClass != null) oldClass.RaiseIsCurrentChanged(); 

                // Initialize the selected [ViewTestClass] object.
                if (currentClass != null)  currentClass.ResetTests(true);

                // Finish up.
                OnPropertyChanged(PropCurrentClass);
            }
        }
        #endregion

        #region Methods
        /// <summary>Creates a new instance of the singleton (see the 'Instance' property).</summary>
        internal static void ResetSingleton()
        {
            Instance = new TestHarnessModel();
        }

        /// <summary>Refreshes the TestHarness from persisted settings.</summary>
        public void Refresh()
        {
            moduleLoader.AddModules();
        }

        /// <summary>Gets whether the specified assembly is currently loaded.</summary>
        /// <param name="xapFileName">The name of the XAP file for the module.</param>
        /// <returns>True if the assembly is loaded, otherwise False.</returns>
        public bool IsLoaded(string xapFileName)
        {
            var module = GetModule(xapFileName);
            return module == null ? false : module.IsLoaded;
        }

        /// <summary>Gets the module corresponding to the given assembly name.</summary>
        /// <param name="xapFileName">The name of the XAP file for the module.</param>
        /// <returns>The corresponding module, or null if a corresponding module does not exist.</returns>
        public ViewTestClassesAssemblyModule GetModule(string xapFileName)
        {
            xapFileName = AssemblyLoader.StripExtensions(xapFileName);
            var module = Modules.FirstOrDefault(item =>
                                                    {
                                                        var assModule = item as ViewTestClassesAssemblyModule;
                                                        return assModule == null
                                                                   ? false
                                                                   : assModule.XapFileName == xapFileName;
                                                    }) as ViewTestClassesAssemblyModule;
            return module;
        }

        /// <summary>Loads the module with the specified name.</summary>
        /// <param name="moduleSetting">The identifying settings of the module to load.</param>
        public void AddModule(ModuleSetting moduleSetting)
        {
            moduleLoader.AddModule(moduleSetting);
        }
        #endregion
    }
}
