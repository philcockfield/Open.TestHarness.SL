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
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Open.Core.Common;

namespace Open.TestHarness.Model
{
    /// <summary>Provides client-side persistence functionality for storing settings.</summary>
    public class TestHarnessSettings : NotifyPropertyChangedBase
    {
        #region Events
        /// <summary>Fires when the TestHarness settings are cleared.</summary>
        public event EventHandler Cleared;
        protected void OnCleared() { if (Cleared != null) Cleared(this, new EventArgs()); }
        #endregion

        #region Head
        public const string PropLoadedModules = "LoadedModules";
        public const string PropRecentSelections = "RecentSelections";

        private const int RecentSelectionMax = 8;

        private const string KeyLoadedApplicationNames = "TestHarness.Setting.LoadedModules";
        private const string KeyRecentSelections = "TestHarness.Setting.RecentSelections";

        private readonly IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        private readonly TestHarnessModel testHarness;

        internal TestHarnessSettings(TestHarnessModel testHarness)
        {
            // Setup initial conditions.
            this.testHarness = testHarness;
            PropertyExplorer = new PropertyExplorerSettings();
            ControlDisplayOptionSettings = new ControlDisplayOptionSettings();

            // Wire up events.
            testHarness.PropertyChanged += (sender, e) =>
                                                          {
                                                              if (e.PropertyName == TestHarnessModel.PropCurrentClass) SyncRecentSelection();
                                                          };
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the module's (XAP and Assembly Name) have been loaded.</summary>
        public ModuleSetting[] LoadedModules
        {
            get
            {
                return settings.Contains(KeyLoadedApplicationNames)
                                        ? settings[KeyLoadedApplicationNames] as ModuleSetting[]
                                        : new ModuleSetting[] { };
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    Remove(KeyLoadedApplicationNames);
                }
                else
                {
                    settings[KeyLoadedApplicationNames] = value;
                }
                OnPropertyChanged(PropLoadedModules);
            }
        }

        /// <summary>Gets or sets the collection of recent selections.</summary>
        public RecentSelectionSetting[] RecentSelections
        {
            get
            {
                return settings.Contains(KeyRecentSelections) 
                                        ? settings[KeyRecentSelections] as RecentSelectionSetting[] 
                                        : new RecentSelectionSetting[] { };
            }
            set { SetRecentSelections(value, false); }
        }

        /// <summary>Gets the property explorer settings.</summary>
        public PropertyExplorerSettings PropertyExplorer { get; private set; }

        /// <summary>Gets settings for how the test control(s) are displayed.</summary>
        public ControlDisplayOptionSettings ControlDisplayOptionSettings { get; private set; }
        #endregion

        #region Methods
        /// <summary>Clears the settings from isloated storage.</summary>
        public void Clear()
        {
            Remove(KeyLoadedApplicationNames);
            Remove(KeyRecentSelections);
            OnCleared();
        }

        /// <summary>Persists the current settings.</summary>
        public void Save()
        {
            settings.Save();
            PropertyExplorer.Save();
            ControlDisplayOptionSettings.Save();
        }

        /// <summary>Removes the specified item from the recent selections list.</summary>
        /// <param name="item">The item to remove.</param>
        /// <param name="silent">Flag indicating if the corresponding property-change event should be fired.</param>
        public void RemoveRecentSelection(ViewTestClass item, bool silent)
        {
            var list = new List<RecentSelectionSetting>(RecentSelections);
            if (RemoveItem(item, list)) SetRecentSelections(list.ToArray(), true);
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
        private void Remove(string key)
        {
            if (settings.Contains(key)) settings.Remove(key);
        }

        private void SyncRecentSelection()
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
            if (list.Count > RecentSelectionMax)
            {
                list.RemoveRange(RecentSelectionMax, list.Count - RecentSelectionMax);
            }

            // Finish up.
            RecentSelections = list.ToArray();
            Save();
        }

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

        private void SetRecentSelections(ICollection<RecentSelectionSetting> value, bool silent)
        {
            if (value == null || value.Count == 0)
            {
                Remove(KeyRecentSelections);
            }
            else
            {
                settings[KeyRecentSelections] = value;
            }
            if (!silent) OnPropertyChanged(PropRecentSelections);
        }
        #endregion
    }
}
