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
using System.Linq;
using System.Net;
using System.Reflection;
using Open.Core.Common;
using Open.Core.Common.Network;

using T = Open.TestHarness.Model.ViewTestClassesAssemblyModule;

namespace Open.TestHarness.Model
{
    /// <summary>A version of 'ViewTestClassesModule' that represents as assembly.</summary>
    public class ViewTestClassesAssemblyModule : ViewTestClassesModule
    {
        #region Events
        /// <summary>Fires when immediately before the assembly is loaded.</summary>
        public event EventHandler AssemblyLoadStarted;
        private void FireAssemblyLoadStarted() { if (AssemblyLoadStarted != null) AssemblyLoadStarted(this, new EventArgs()); }

        /// <summary>Fires when the assembly completes loading.</summary>
        public event EventHandler AssemblyLoadComplete;
        private void FireAssemblyLoadComplete() { if (AssemblyLoadComplete != null) AssemblyLoadComplete(this, new EventArgs()); }
        #endregion

        #region Head
        /// <summary>Constructor.</summary>
        /// <param name="moduleSetting">The identifying settings of the module to load.</param>
        public ViewTestClassesAssemblyModule(ModuleSetting moduleSetting) : this(moduleSetting.AssemblyName, moduleSetting.XapFileName)
        {
        }

        /// <summary>Constructor.</summary>
        /// <param name="assemblyName">The name of the entry-point assembly.</param>
        /// <param name="xapFileName">The name of the XAP file that contains the module.</param>
        public ViewTestClassesAssemblyModule(string assemblyName, string xapFileName) 
        {
            // Store values.
            AssemblyName = assemblyName;
            XapFileName = AssemblyLoader.StripExtensions(xapFileName);

            // Derive property values.
            DisplayName = XapFileName;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the name of the XAP file that contains the module.</summary>
        public string XapFileName { get; private set; }

        /// <summary>Gets the assembly name of the module.</summary>
        public string AssemblyName
        {
            get { return GetPropertyValue<T, string>(m => m.AssemblyName); }
            set
            {
                if (value == AssemblyName) return;
                value = value.AsNullWhenEmpty();
                if (value != null) value = ReflectionUtil.GetAssemblyName(value);
                SetPropertyValue<T, string>(m => m.AssemblyName, value);
            }
        }

        /// <summary>Gets the assembly (or null if the assembly has not been loaded).</summary>
        public Assembly Assembly
        {
            get { return GetPropertyValue<T, Assembly>(m => m.Assembly); }
            private set { SetPropertyValue<T, Assembly>(m => m.Assembly, value); }
        }

        /// <summary>Gets whether the assembly has been loaded.</summary>
        public bool IsLoaded { get { return Assembly != null; } }

        /// <summary>Gets whether the assembly is currently being downloaded.</summary>
        public bool IsLoading
        {
            get { return GetPropertyValue<T, bool>(m => m.IsLoading); }
            private set { SetPropertyValue<T, bool>(m => m.IsLoading, value); }
        }

        /// <summary>Gets or sets the package-download service.</summary>
        /// <remarks>
        ///    NB: This is set in App.Startup() routine.
        ///    It would be best not to strongly couple this with a static member, ideally you'd use
        ///    MEF to import the service, however re-composition errors seems to occur in some
        ///    situations when this downloader is also involved in a MEF import itself.
        /// </remarks>
        public static IPackageDownloadService PackageDownloader { get; set; }
        #endregion

        #region Properties - Internal
        private bool CanLoad
        {
            get
            {
                if (IsLoaded) return false;
                if (IsLoading) return false;
                if (TestHarness.LoadedAssemblies.Contains(Assembly)) return false;
                return true;
            }
        }
        #endregion

        #region Methods
        /// <summary>Loads the specified assembly from the server.</summary>
        /// <param name="callback">Callback to invoke when the assembly is loaded.</param>
        public void LoadAssembly(Action callback)
        {
            // Setup initial conditions.
            if (!CanLoad) return;
            IsLoading = true;
            FireAssemblyLoadStarted();

            // Start the downloading of the XAP.
            Debug.WriteLine("Test Harness - Loading with 'AssemblyLoader': " + XapFileName);
            PackageDownloader.DownloadAsync(XapFileName, response =>
                                {
                                    // Check for failure.
                                    IsLoading = false;
                                    if (!response.IsSuccessful)
                                    {
                                        // This module cannot be loaded - remove it from the list.
                                        TestHarness.Modules.Remove(this);
                                        TestHarness.Settings.SyncLoadedModulesWithTestHarness();
                                        TestHarness.Settings.Save();
                                    }
                                    else
                                    {
                                        // Load the entry-point assembly.
                                        LoadAssemblyInternal(response.Result.EntryPointAssembly);
                                    }
                                        

                                    // Finish up.
                                    if (callback != null) callback();
                                });
        }

        /// <summary>Loads the specified assembly into the module.</summary>
        /// <param name="assembly">The assembly to load.</param>
        public void LoadAssembly(Assembly assembly)
        {
            if (!CanLoad) return;
            FireAssemblyLoadStarted();
            LoadAssemblyInternal(assembly);
        }

        private void LoadAssemblyInternal(Assembly assembly)
        {
            // Setup initial conditions.
            if (assembly == null)
            {
                Unload();
                return;
            }

            // Store values.
            Assembly = assembly;
            AssemblyName = ReflectionUtil.GetAssemblyName(assembly.FullName);
            TestHarness.LoadedAssemblies.Add(assembly);

            // Add children to the module.
            AddFromAssembly(assembly, XapFileName);

            // Finish up.
            OnPropertyChanged<T>(m => m.IsLoaded);
            FireAssemblyLoadComplete();
        }

        /// <summary>Removes the module.</summary>
        public void Unload()
        {
            // Deselect class if this module contains the current class.
            var currentClass = CurrentClass;
            if (currentClass != null && currentClass == TestHarness.CurrentClass) currentClass.IsCurrent = false;

            // Clear settings.
            Assembly = null;
            Classes.RemoveAll();

            // Remove from parent TestHarness.
            TestHarness.Modules.Remove(this);

            // Remove from stored settings.
            RemoveModuleSetting();
            RemoveFromRecentSelections();

            // Finish up.
            OnPropertyChanged<T>(m => m.IsLoaded);
        }

        /// <summary>Returns the identifying values of the module (AssemblyName and XAP file) as a setting value.</summary>
        public ModuleSetting ToSetting()
        {
            return new ModuleSetting(AssemblyName, XapFileName);
        }
        #endregion

        #region Internal
        private void RemoveModuleSetting( )
        {
            var settings = TestHarnessModel.Instance.Settings;
            var settingNames = new List<ModuleSetting>(settings.LoadedModules);

            var removeItem = settingNames.FirstOrDefault(item => item.XapFileName == XapFileName);
            settingNames.Remove(removeItem);

            settings.LoadedModules = settingNames.ToArray();
            settings.Save();
        }

        private void RemoveFromRecentSelections()
        {
            var settings = TestHarnessModel.Instance.Settings;
            var recentSelections = new List<RecentSelectionSetting>(settings.RecentSelections);

            foreach (var item in recentSelections.ToList())
            {
                if (item.Module.XapFileName == XapFileName)
                {
                    recentSelections.Remove(item);
                }
            }

            settings.RecentSelections = recentSelections.ToArray();
            settings.Save();
        }
        #endregion
    }
}
