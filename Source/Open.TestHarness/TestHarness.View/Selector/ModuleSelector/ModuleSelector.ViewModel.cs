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

using Open.Core.Common;
using Open.Core.Common.Collection;
using Open.TestHarness.Model;

namespace Open.TestHarness.View.Selector
{
    /// <summary>Logical representation the module-selector.</summary>
    public class ModuleSelectorViewModel : ViewModelBase
    {
        #region Head
        private readonly ObservableCollectionWrapper<ViewTestClassesModule, ModuleNodeViewModel> modules;

        public ModuleSelectorViewModel()
        {
            var model = TestHarnessModel.Instance;
            modules = new ObservableCollectionWrapper<ViewTestClassesModule, ModuleNodeViewModel>(model.Modules, item => new ModuleNodeViewModel(item));
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection of modules.</summary>
        public ObservableCollectionWrapper<ViewTestClassesModule, ModuleNodeViewModel> Modules { get { return modules; } }
        #endregion
    }
}
