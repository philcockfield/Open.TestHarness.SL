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
using System.Windows.Browser;
using System.Windows.Controls;
using Open.Core.Common;
using Open.Core.Common.Collection;
using Open.Core.Composite.Command;
using Open.TestHarness.Model;
using Open.TestHarness.View.Assets;
using T = Open.TestHarness.View.Selector.SelectorPanelViewModel;

namespace Open.TestHarness.View.Selector
{
    /// <summary>Logical representation the module-selector.</summary>
    public class SelectorPanelViewModel : ViewModelBase
    {
        #region Head
        private readonly TestHarnessModel model;
        private readonly RootViewModel parent;
        private readonly ObservableCollectionWrapper<ViewTestClassesModule, ModuleNodeViewModel> modules;
        private readonly TestSelectorViewModel testSelector = new TestSelectorViewModel();

        public SelectorPanelViewModel(RootViewModel parent)
        {
            // Setup initial conditions.
            this.parent = parent;
            model = TestHarnessModel.Instance;
            modules = new ObservableCollectionWrapper<ViewTestClassesModule, ModuleNodeViewModel>(model.Modules, item => new ModuleNodeViewModel(item));
            Strings = new StringLibrary();

            // Create commands.
            AddAssemblyCommand = new DelegateCommand<Button>(param => OnAddAssemblyClick());
            AutoRunTestsCommand = new DelegateCommand<Button>(param => OnAutoRunTestsClick());
            RunUnitTests = new DelegateCommand<Button>(param => OnRunUnitTests());

            // Wire up events.
            model.PropertyChanged += (sender, e) =>
                                         {
                                             if (e.PropertyName == TestHarnessModel.PropCurrentClass) UpdateTestSelectorModel();
                                         };
        }
        #endregion

        #region Event Handlers
        private void OnAddAssemblyClick()
        {
            parent.ClientBinGrid.IsShowing = true;
            parent.ClientBinGrid.LoadAsync(true);
        }

        private static void OnAutoRunTestsClick()
        {
            // Setup initial conditions.
            var modules = TestHarnessModel.Instance.Settings.LoadedModules;
            if (modules.IsEmpty()) return;

            // Build query string.
            var xapQuery = string.Empty;
            foreach (var assembly in modules)
            {
                xapQuery += string.Format("xap={0}&", assembly.XapFileName);
            }
            var query = string.Format("?runTests=true&{0}", xapQuery.RemoveEnd("&"));
            var url = HtmlPage.Document.DocumentUri.ToString().SubstringBeforeLast("?") + query;

            // Navigate to the auto-run URL.
            HtmlPage.Window.Navigate(new Uri(url));
        }

        private void OnRunUnitTests()
        {
            UnitTestRunner.Run(UnitTestTag);
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection of modules.</summary>
        public ObservableCollectionWrapper<ViewTestClassesModule, ModuleNodeViewModel> Modules { get { return modules; } }

        /// <summary>Gets the currently selected [TestClass].</summary>
        public TestSelectorViewModel TestSelector { get { return testSelector; } }

        /// <summary>Get the localized string resources.</summary>
        public StringLibrary Strings { get; private set; }

        /// <summary>Gets the command for the 'Add Assembly' button.</summary>
        public DelegateCommand<Button> AddAssemblyCommand { get; private set; }

        /// <summary>Gets the command for the 'Auto Run Tests' button.</summary>
        public DelegateCommand<Button> AutoRunTestsCommand { get; private set; }

        /// <summary>Gets the command for that runs unit tests.</summary>
        public DelegateCommand<Button> RunUnitTests { get; private set; }

        /// <summary>Gets or sets the tag to limit the unit-test run to.</summary>
        public string UnitTestTag
        {
            get { return TestHarnessModel.Instance.Settings.ControlDisplayOptionSettings.UnitTestTag; }
            set { TestHarnessModel.Instance.Settings.ControlDisplayOptionSettings.UnitTestTag = value; }
        }

        /// <summary>Gets whether the 'Run Tests' button is visible (only available when running within the browser).</summary>
        public bool IsRunTestsButtonVisible { get { return !Application.Current.IsRunningOutOfBrowser; } }
        #endregion

        #region Internal
        private void UpdateTestSelectorModel()
        {
            // Update the test selector with the current class.
            var module = Modules.FirstOrDefault(m => m.CurrentClass != null);
            ViewTestClass value = null;
            if (module != null && module.CurrentClass != null) value = module.CurrentClass.Model;
            TestSelector.Model = value;
        }
        #endregion
    }
}
