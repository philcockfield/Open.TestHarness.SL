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
using System.Collections.Generic;
using Open.Core.Common;
using T = Open.TestHarness.Model.TestHarnessSettings;

namespace Open.TestHarness.Model
{
    /// <summary>Provides client-side persistence functionality for storing settings.</summary>
    public class TestHarnessSettings : IsolatedStorageModelBase
    {
        #region Head
        private const int recentSelectionMax = 8;
        private readonly TestHarnessModel testHarness;

        internal TestHarnessSettings(TestHarnessModel testHarness) : base(IsolatedStorageType.Application, "~~-TestHarness.Settings-~~")
        {
            this.testHarness = testHarness;
            PropertyExplorer = new PropertyExplorerSettings();
            ControlDisplayOptionSettings = new ControlDisplayOptionSettings();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the module's (XAP and Assembly Name) have been loaded.</summary>
        public ModuleSetting[] LoadedModules
        {
            get { return GetPropertyValue<T, ModuleSetting[]>(m => m.LoadedModules, new ModuleSetting[]{}); }
            set { SetPropertyValue<T, ModuleSetting[]>(m => m.LoadedModules, value, new ModuleSetting[]{}); }
        }

        /// <summary>Gets or sets the collection of recent selections.</summary>
        public RecentSelectionSetting[] RecentSelections
        {
            get { return GetPropertyValue<T, RecentSelectionSetting[]>(m => m.RecentSelections, new RecentSelectionSetting[]{}); }
            set { SetPropertyValue<T, RecentSelectionSetting[]>(m => m.RecentSelections, value, new RecentSelectionSetting[]{}); }
        }

        /// <summary>Gets the property explorer settings.</summary>
        public PropertyExplorerSettings PropertyExplorer { get; private set; }

        /// <summary>Gets settings for how the test control(s) are displayed.</summary>
        public ControlDisplayOptionSettings ControlDisplayOptionSettings { get; private set; }
        #endregion

        #region Methods
        /// <summary>Persists the current settings.</summary>
        public override bool Save()
        {
            PropertyExplorer.Save();
            ControlDisplayOptionSettings.Save();
            return base.Save();
        }

        /// <summary>Removes the specified item from the recent selections list.</summary>
        /// <param name="item">The item to remove.</param>
        /// <param name="silent">Flag indicating if the corresponding property-change event should be fired.</param>
        public void RemoveRecentSelection(ViewTestClass item, bool silent)
        {
            var list = new List<RecentSelectionSetting>(RecentSelections);
            if (RemoveItem(item, list)) SetRecentSelections(list.ToArray());
        }

        /// <summary>Syncs the 'Recent Selections' list with the current TestHarness selected class.</summary>
        public void SyncRecentSelection()
        {
            // Setup initial conditions.
            var list = new List<RecentSelectionSetting>(RecentSelections);
            var current = testHarness.CurrentClass;
            if (current == null) return;

            // Remove existing item from list (if it already exists).
            RemoveItem(current, list);

            // Add the current item at the head of the list.
            var setting = new RecentSelectionSetting(current, current.XapFileName);
            list.Insert(0, setting);

            // Don't allow the list to grow beyond the max size.
            if (list.Count > recentSelectionMax)
            {
                list.RemoveRange(recentSelectionMax, list.Count - recentSelectionMax);
            }

            // Finish up.
            RecentSelections = list.ToArray();
            Save();
        }

        /// <summary>Syncs the set of saved loaded-module-names with the current set of modules.</summary>
        public void SyncLoadedModulesWithTestHarness()
        {
            var list = new List<ModuleSetting>();
            foreach (var module in testHarness.Modules)
            {
                var assemblyModule = module as ViewTestClassesAssemblyModule;
                if (assemblyModule != null) list.Add(assemblyModule.ToSetting());
            }

            LoadedModules = list.ToArray();
        }
        #endregion

        #region Internal
        private static bool RemoveItem(ViewTestClass item, ICollection<RecentSelectionSetting> collection)
        {
            var match = collection.FirstOrDefault(
                setting => 
                setting.Module.AssemblyName == item.AssemblyName 
                && setting.ClassName == item.TypeName);
            if (match == null) return false;
            collection.Remove(match);
            return true;
        }

        private void SetRecentSelections(IEnumerable<RecentSelectionSetting> value)
        {
            RecentSelections = value.ToArray();
        }
        #endregion
    }
}
