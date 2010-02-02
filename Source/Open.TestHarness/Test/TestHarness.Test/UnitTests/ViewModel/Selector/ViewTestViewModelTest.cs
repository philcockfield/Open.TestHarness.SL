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
    public class ViewTestViewModelTest : SilverlightTest
    {
        #region Head
        private SelectorPanelViewModel selectorPanelVm;
        private ViewTestClassesAssemblyModule moduleModel;
        private ViewTestClass classModel;

        [TestInitialize]
        public void TestInitialize()
        {
            TestHarnessModel.ResetSingleton();
            var testHarness = TestHarnessModel.Instance;
            testHarness.Modules.RemoveAll();

            classModel = new ViewTestClass(typeof(SampleViewTestClass1), "File.xap");

            moduleModel = new ViewTestClassesAssemblyModule(new ModuleSetting(GetType().Assembly.FullName, "File.xap"));
            moduleModel.Classes.Add(classModel);
            testHarness.Modules.Add(moduleModel);

            selectorPanelVm = new SelectorPanelViewModel(new RootViewModel());

            classModel.IsCurrent = true;
            selectorPanelVm.TestSelector.ShouldNotBe(null);
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldConvertUnderscoresToSpaces()
        {
            var testView = selectorPanelVm.TestSelector.ViewTests[0];
            testView.DisplayName.Contains(" ").ShouldBe(true);
        }

        [TestMethod]
        public void ShouldExecuteFromClick()
        {
            var testModel = classModel.ViewTests[0];
            var testVM = selectorPanelVm.TestSelector.ViewTests[0];

            EventArgs args= null;
            testModel.ExecuteRequest += (s, e) => args = e;

            testVM.Click.Execute(null);

            args.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldNotBeVisible()
        {
            var method1 =
                new ViewTestButtonViewModel(
                    classModel.ViewTests.FirstOrDefault(
                        item => item.MethodInfo.Name == SampleViewTestClass1.PropMethod_1));
            method1.ShouldNotBe(null);
            method1.Visibility.ShouldBe(Visibility.Visible);

            var methodInvisible =
                new ViewTestButtonViewModel(
                    classModel.ViewTests.FirstOrDefault(
                        item => item.MethodInfo.Name == SampleViewTestClass1.PropMethod_Invisible));
            methodInvisible.ShouldNotBe(null);
            methodInvisible.Visibility.ShouldBe(Visibility.Collapsed);
        }
        #endregion
    }
}
