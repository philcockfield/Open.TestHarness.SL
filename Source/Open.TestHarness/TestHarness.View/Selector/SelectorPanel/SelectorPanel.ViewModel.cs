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
using Open.Core.Common;
using Open.Core.Common.Collection;
using Open.Core.Composite.Command;
using Open.TestHarness.Model;

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
        private DelegateCommand<string> addAssemblyClick;

        public SelectorPanelViewModel(RootViewModel parent)
        {
            // Setup initial conditions.
            this.parent = parent;
            model = TestHarnessModel.Instance;
            modules = new ObservableCollectionWrapper<ViewTestClassesModule, ModuleNodeViewModel>(model.Modules, item => new ModuleNodeViewModel(item));

            // Wire up events.
            model.PropertyChanged += (sender, e) =>
                                         {
                                             if (e.PropertyName == TestHarnessModel.PropCurrentClass) UpdateTestSelectorModel();
                                         };
        }
        #endregion

        #region Event Handlers
        private void HandleAddAssemblyClick()
        {
            parent.ClientBinGrid.IsShowing = true;
            parent.ClientBinGrid.LoadAsync(true);
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection of modules.</summary>
        public ObservableCollectionWrapper<ViewTestClassesModule, ModuleNodeViewModel> Modules { get { return modules; } }

        /// <summary>Gets the currently selected [TestClass].</summary>
        public TestSelectorViewModel TestSelector { get { return testSelector; } }
        
        /// <summary>Gets the command for reacing to click's of the 'Add Assembly' option.</summary>
        public DelegateCommand<string> AddAssemblyClick
        {
            get
            {
                if (addAssemblyClick == null) addAssemblyClick = new DelegateCommand<string>(param => HandleAddAssemblyClick(), param => true);
                return addAssemblyClick;
            }
        }
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
