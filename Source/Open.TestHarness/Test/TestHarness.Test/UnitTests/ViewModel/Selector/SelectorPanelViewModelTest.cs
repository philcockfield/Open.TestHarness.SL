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
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.TestHarness.Model;
using Open.TestHarness.Test.Model;
using Open.TestHarness.View;
using Open.TestHarness.View.Selector;

namespace Open.TestHarness.Test.ViewModel
{
    [TestClass]
    public class SelectorPanelViewModelTest : SilverlightTest
    {
        #region Head
        private RootViewModel rootVm;
        private SelectorPanelViewModel selectorPanelVm;
        private ViewTestClassesAssemblyModule moduleModel;
        private ViewTestClass classModel;
        
        [TestInitialize]
        public void Initialize()
        {
            TestHarnessModel.ResetSingleton();
            var testHarness = TestHarnessModel.Instance;
            testHarness.Modules.RemoveAll();

            classModel = new ViewTestClass(typeof(SampleViewTestClass1), "File.xap");

            moduleModel = new ViewTestClassesAssemblyModule(new ModuleSetting(GetType().Assembly.FullName, "File.xap"));
            moduleModel.Classes.Add(classModel);
            testHarness.Modules.Add(moduleModel);

            rootVm = new RootViewModel();
            selectorPanelVm = new SelectorPanelViewModel(rootVm);
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldHaveModulesFromParentModel()
        {
            selectorPanelVm.Modules.Count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldReturnCurrentClassVM()
        {
            moduleModel.Classes.ShouldContain(classModel);

            classModel.IsCurrent = true;
            TestHarnessModel.Instance.CurrentClass.ShouldBe(classModel);

            selectorPanelVm.TestSelector.ShouldNotBe(null);
        }
        #endregion
    }
}
