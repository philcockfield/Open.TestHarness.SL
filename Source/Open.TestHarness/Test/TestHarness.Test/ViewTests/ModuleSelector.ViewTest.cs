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

using System.Collections.ObjectModel;
using Open.Core.Common;
using Open.TestHarness.Model;
using Open.TestHarness.Test.Model;
using Open.TestHarness.View.Selector;

namespace Open.TestHarness.Test
{
    [ViewTestClass]
    public class ModuleSelectorViewTest
    {
        #region Head
        private readonly ObservableCollection<ModuleNodeViewModel> modules = new ObservableCollection<ModuleNodeViewModel>();

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ModuleSelector control)
        {
            // Setup initial conditions.
            control.Width = 320;
            control.Height = 400;

             // Setup sample data.
            var model1 = new ViewTestClassesModule { DisplayName = "Recent Selections" };
            model1.Classes.Add(new ViewTestClass(typeof(ModuleSelectorViewTest), "File.xap"));
            model1.Classes.Add(new ViewTestClass(typeof(SampleViewTestClass1), "File.xap"));
            var viewModel1 = new ModuleNodeViewModel(model1);

            var model2 = new ViewTestClassesAssemblyModule(new ModuleSetting("Assembly.Name.xap"));
            model2.Classes.Add(new ViewTestClass(typeof(SampleViewTestClass1), "File.xap"));
            model2.Classes.Add(new ViewTestClass(typeof(SampleViewTestClass2), "File.xap"));
            model2.Classes.Add(new ViewTestClass(typeof(ModuleSelectorViewTest), "File.xap"));
            var viewModel2 = new ModuleNodeViewModel(model2);

            modules.Add(viewModel1);
            modules.Add(viewModel2);

            // Assign sample data to control.
            control.DataContext = modules;
        }
        #endregion

        #region Tests
        #endregion
    }
}
