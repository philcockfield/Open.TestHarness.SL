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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Open.Core.Common;
using Open.Core.Common.Collection;
using Open.Core.Composite.Command;
using Open.TestHarness.Model;

using T = Open.TestHarness.View.Selector.ModuleNodeViewModel;

namespace Open.TestHarness.View.Selector
{
    /// <summary>Logical model of a root node within the class selector.</summary>
    public class ModuleNodeViewModel : NodeViewModelBase
    {
        #region Head
        private readonly ViewTestClassesModule model;
        private readonly ObservableCollectionWrapper<ViewTestClass, ClassNodeViewModel> classes;
        private bool isLoadingFromClick;

        public ModuleNodeViewModel(ViewTestClassesModule model)
        {
            // Setup initial conditions.
            this.model = model;
            classes = new ObservableCollectionWrapper<ViewTestClass, ClassNodeViewModel>(model.Classes, item => new ClassNodeViewModel(item));
            if (IsRecentSelections) IsOpen = true;

            // Create commands.
            RemoveCommand = new DelegateCommand<Button>(button => RemoveModule());
            ClickCommand = new DelegateCommand<Button>(button => { if (!IsLoading) IsOpen = !IsOpen; });

            // Wire up events.
            if (AssemblyModel != null)
            {
                AssemblyModel.AssemblyLoadStarted += delegate { LoadStart(); };
                AssemblyModel.AssemblyLoadComplete += delegate { LoadComplete(); };
            }
            model.PropertyChanged += (s, e) =>
                                         {
                                             if (e.PropertyName == ViewTestClassesModule.PropDisplayName)
                                             {
                                                 UpdateDisplayName();
                                                 OnPropertyChanged<T>(m => m.DisplayName);
                                             }
                                         };

            // Finish up.
            UpdateDisplayName();
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection of classes.</summary>
        public ObservableCollectionWrapper<ViewTestClass, ClassNodeViewModel> Classes { get { return classes; } }

        /// <summary>Retrieves the display name of the node.</summary>
        public string DisplayName
        {
            get { return GetPropertyValue<T, string>(m => m.DisplayName); }
            set { SetPropertyValue<T, string>(m => m.DisplayName, value); }
        }

        /// <summary>Gets whether the module is loaded.</summary>
        /// <remarks>Modules remain in unloaded state until selected.</remarks>
        public bool IsLoaded
        {
            get { return GetPropertyValue<T, bool>(m => m.IsLoaded); }
            set { SetPropertyValue<T, bool>(m => m.IsLoaded, value, m => m.IsRemoveButtonVisible); }
        }

        /// <summary>Gets or sets whether the module is currently being loaded.</summary>
        public bool IsLoading
        {
            get { return GetPropertyValue<T, bool>(m => m.IsLoading); }
            set { SetPropertyValue<T, bool>(m => m.IsLoading, value); }
        }

        /// <summary>Gets whether the button that removes the modeule from the list is visible.</summary>
        public bool IsRemoveButtonVisible
        {
            get { return IsLoaded && model is ViewTestClassesAssemblyModule; }
        }

        /// <summary>Gets the command associated with the 'Remove' button.</summary>
        public ICommand RemoveCommand { get; private set; }

        /// <summary>Gets the command for handling general selection of the button.</summary>
        public DelegateCommand<Button> ClickCommand { get; private set; }

        /// <summary>Gets the currently selected class within the module (null if none are selected).</summary>
        public ClassNodeViewModel CurrentClass
        {
            get { return Classes.FirstOrDefault(c => c.IsCurrent); }
        }

        /// <summary>Gets or sets whether the module node is currently open.</summary>
        public bool IsOpen
        {
            get { return GetPropertyValue<T, bool>(m => m.IsOpen); }
            set
            {
                // Store value.
                SetPropertyValue<T, bool>(m => m.IsOpen, value);

                // Load the assembly (if it's not already loaded).
                if (value && !IsLoading && !IsLoaded)
                {
                    isLoadingFromClick = true;
                    OnSelected();
                }
            }
        }
        #endregion

        #region Properties - Private
        private ViewTestClassesAssemblyModule AssemblyModel { get { return model as ViewTestClassesAssemblyModule; } }
        private bool IsRecentSelections { get { return model == TestHarnessModel.Instance.RecentSelectionsModule; } }
        #endregion

        #region Methods
        public override void OnSelected()
        {
            if (IsLoaded || IsLoading) return;
            Load();
        }
        #endregion

        #region Internal
        private void Load()
        {
            if (IsLoaded || IsLoading || AssemblyModel == null) return;
            AssemblyModel.LoadAssembly((Action)null);
        }

        private void LoadStart()
        {
            IsLoading = true;
        }

        private void LoadComplete()
        {
            IsLoading = false;
            IsLoaded = true;
            UpdateDisplayName();
            OnPropertyChanged<T>(m => m.IsRemoveButtonVisible);
            if (!IsOpen && isLoadingFromClick) IsOpen = true;
        }

        private void UpdateDisplayName()
        {
            var name = model.DisplayName;
            if (IsLoaded) name = string.Format("{0} ({1})", name, model.Classes.Count);
            DisplayName = name;
        }

        private void RemoveModule()
        {
            if (!(model is ViewTestClassesAssemblyModule)) return;
            ((ViewTestClassesAssemblyModule)model).Unload();
        }
        #endregion
    }
}
