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
using Open.Core.Common;
using Open.TestHarness.Model.Assets;

namespace Open.TestHarness.Model
{
    /// <summary>Encapsulates the logic for loading modules in the TestHarness.</summary>
    internal class ModuleLoader
    {
        #region Head
        private readonly ObservableCollection<ViewTestClassesModule> modules = new ObservableCollection<ViewTestClassesModule>();
        private readonly TestHarnessModel testHarness;

        public ModuleLoader(TestHarnessModel testHarness)
        {
            // Store values.
            this.testHarness = testHarness;

            // Wire up events.
            testHarness.Settings.Cleared += delegate { AddModules(); };
            testHarness.Settings.PropertyChanged += (s, e) =>
                                                             {
                                                                if (e.PropertyName == TestHarnessSettings.PropRecentSelections) LoadRecentSelectionsModule(RecentSelectionsModule);
                                                             };
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection of modules.</summary>
        public ObservableCollection<ViewTestClassesModule> Modules { get { return modules; } }

        /// <summary>Gets the recent selections module (or null if the modules have not been built.</summary>
        public ViewTestClassesModule RecentSelectionsModule { get; private set; }
        #endregion

        #region Methods
        public void ClearModules()
        {
            Modules.RemoveAll();
            RecentSelectionsModule = null;
        }

        public void AddModules()
        {
            // Setup initial conditions.
            ClearModules();

            // Load the modules.
            LoadRecentSelectionsModule();
            LoadAssemblyModules();
        }

        /// <summary>Loads the module with the specified name.</summary>
        /// <param name="moduleSetting">The identifying settings of the module to load.</param>
        public void AddModule(ModuleSetting moduleSetting)
        {
            var module = new ViewTestClassesAssemblyModule(moduleSetting);
            Modules.Add(module);
        }
        #endregion

        #region Internal
        private void LoadAssemblyModules()
        {
            foreach (var assemblyName in testHarness.Settings.LoadedModules)
            {
                AddModule(assemblyName);
            }
        }

        private void LoadRecentSelectionsModule()
        {
            // Setup initial conditions.
            RecentSelectionsModule = null;
            if (testHarness.Settings.RecentSelections.Length == 0) return;

            // Create the module.
            var module = new ViewTestClassesModule
                             {
                                 DisplayName = StringLibrary.RecentSelections_Title
                             };

            // Populate with recent selections.
            LoadRecentSelectionsModule(module);

            // Finish up.
            Modules.Insert(0, module);
            RecentSelectionsModule = module;
        }

        private void LoadRecentSelectionsModule(ViewTestClassesModule module)
        {
            // Setup initial conditions.
            if (module == null) return;

            // Build the new list.
            var list = new List<ViewTestClass>();
            foreach (var setting in testHarness.Settings.RecentSelections)
            {
                list.Add(ViewTestClass.GetSingleton(
                                                    setting.ClassName, 
                                                    setting.CustomName, 
                                                    setting.Module.AssemblyName, 
                                                    setting.Module.XapFileName));
            }

            // Re-populate the collection.
            module.Classes.RemoveAll();
            module.Classes.AddRange(list);
        }
        #endregion
    }
}
