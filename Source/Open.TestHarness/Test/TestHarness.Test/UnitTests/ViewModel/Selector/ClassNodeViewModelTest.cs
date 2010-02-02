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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;

using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.TestHarness.Model;
using Open.TestHarness.Test.Model;
using Open.TestHarness.View.Selector;

namespace Open.TestHarness.Test.ViewModel
{
    [TestClass]
    public class ClassNodeViewModelTest : SilverlightTest
    {
        #region Head
        private ModuleNodeViewModel moduleNode;
        private ClassNodeViewModel classNode;

        private ViewTestClassesModule moduleModel;
        private ViewTestClass classModel;

        [TestInitialize]
        public void Initialize()
        {
            TestHarnessModel.ResetSingleton();
            TestHarnessModel.Instance.Modules.RemoveAll();

            moduleModel = new ViewTestClassesAssemblyModule(new ModuleSetting(GetType().Assembly.FullName, "File.xap"));
            var type = typeof (SampleViewTestClass1);
            classModel = new ViewTestClass(type.FullName, null, type.Assembly.FullName, "File.xap");
            TestHarnessModel.Instance.Modules.Add(moduleModel);

            moduleNode = new ModuleNodeViewModel(moduleModel);
            classNode = new ClassNodeViewModel(classModel);
            moduleNode.Classes.Add(classNode);
        }

        [TestCleanup]
        public void Cleanup()
        {
            TestHarnessModel.ResetSingleton();
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldBeSelectable()
        {
            var testHarness = TestHarnessModel.Instance;
            classNode.TestAssembly = GetType().Assembly; // Set test assembly.

            testHarness.CurrentClass.ShouldBe(null);
            classNode.IsAssemblyLoaded.ShouldBe(false);

            // ---------
            EventArgs args = null;
            classNode.Selected += (s, e) => args = e;

            classNode.OnSelected();

            testHarness.CurrentClass.ShouldBe(classModel);
            classNode.IsCurrent.ShouldBe(true);
            classModel.IsCurrent.ShouldBe(true);

            classNode.IsAssemblyLoaded.ShouldBe(true);
            args.ShouldNotBe(null);
        }
        #endregion
    }
}
