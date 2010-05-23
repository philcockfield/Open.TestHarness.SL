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
using Open.Core.Common;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using Open.TestHarness.Model;

namespace Open.TestHarness.Test.Model
{
    [TestClass]
    public class ViewTestTest
    {
        #region Head
        private ViewTestClassesAssemblyModule moduleModel;
        private ViewTestClass classModel;
        private ViewTest testModel;

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

            classModel.IsCurrent = true;
            testModel = classModel.ViewTests[0];
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldRetrieveMethodsFromClass()
        {
            var sampleType = new SampleViewTestClass1().GetType(); // NB: Instance used to get actual SL type of (instead of 'typeof' which returns native Type).

            var list = ViewTest.GetMethods(sampleType);
            list.Count.ShouldBe(4);

            var method1 = list[0];
            method1.MethodInfo.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldExecute()
        {
            EventArgs args = null;
            testModel.ExecuteRequest += (s, e) => args = e;
            testModel.Execute();
            args.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldExtractAttribute()
        {
            testModel.Attribute.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldThrowOnMethodWithoutAttribute()
        {
            var methodInfo = GetType().GetMethod("ShouldThrowOnMethodWithoutAttribute");
            methodInfo.ShouldNotBe(null);
            Should.Throw<ArgumentOutOfRangeException>(() => new ViewTest(methodInfo));
        }

        [TestMethod]
        public void ShouldExposeParametersCollection()
        {
            testModel.Parameters.Items.Count().ShouldBe(1);
            testModel.Parameters.Items.ElementAt(0).Type.ShouldBe(typeof(Placeholder));
        }

        [TestMethod]
        public void ShouldHaveThreeTagsOneInheristedFromParent()
        {
            testModel.Tags.Count().ShouldBe(3);
            testModel.Tags.ElementAt(0).ShouldBe("One");
            testModel.Tags.ElementAt(1).ShouldBe("Two");
            testModel.Tags.ElementAt(2).ShouldBe("Three");
        }

        [TestMethod]
        public void ShouldHaveNoTagsOnlyFromParentClass()
        {
            classModel.ViewTests[1].Tags.Count().ShouldBe(2);
        }

        [TestMethod]
        public void ShouldHaveNoTags()
        {
            classModel = new ViewTestClass(typeof(SampleViewTestClass2), "File.xap");
            classModel.ViewTests[0].Tags.Count().ShouldBe(0);
        }
        #endregion
    }
}
