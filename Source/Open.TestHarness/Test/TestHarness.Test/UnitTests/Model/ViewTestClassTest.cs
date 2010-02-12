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
using System.ComponentModel;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using Open.TestHarness.Model;
using ReflectionUtil=Open.Core.Common.ReflectionUtil;
using ViewTestClassAttribute=Open.Core.Common.ViewTestClassAttribute;

namespace Open.TestHarness.Test.Model
{
    [TestClass]
    public class ViewTestClassTest
    {
        #region Test
        [TestMethod]
        public void ShouldConstructFromType()
        {
            var sampleType = new SampleViewTestClass1().GetType(); // NB: Instance used to get actual SL type of (instead of 'typeof' which returns native Type).
            var model = new ViewTestClass(sampleType, "File.xap");

            model.TypeName.ShouldBe(sampleType.FullName);
            model.AssemblyName.ShouldBe(ReflectionUtil.GetAssemblyName(sampleType.Assembly.FullName));
            model.IsActivated.ShouldBe(true);

            Assert.AreEqual(sampleType, model.Type);
            Assert.IsNotNull(model.Attribute);
        }

        [TestMethod]
        public void ShouldThowErrorWhenClassDoesNotHaveAttribute()
        {
            // NB: Instance used to get actual SL type of (instead of 'typeof' which returns native Type).
            // This is an arbitrary SL class that is not decorated with the [ViewTestClass] attribute.
            var sampleType = new ViewTestClassAttribute().GetType();
            Should.Throw<ArgumentException>(() => new ViewTestClass(sampleType, "File.xap"));
        }

        [TestMethod]
        public void ShouldHaveCollectionOfViewTests()
        {
            var sampleType = new SampleViewTestClass1().GetType(); // NB: Instance used to get actual SL type of (instead of 'typeof' which returns native Type).
            var model = new ViewTestClass(sampleType, "File.xap");
            model.ViewTests.Count.ShouldBe(4);
        }

        [TestMethod]
        public void ShouldHaveEmptyCollectionOfViewTests()
        {
            var model = new ViewTestClass(typeof(SampleViewTestClassEmpty), "File.xap");
            model.ViewTests.Count.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldLazyLoadFromString()
        {
            var sampleType = new SampleViewTestClass1().GetType(); // NB: Instance used to get actual SL type of (instead of 'typeof' which returns native Type).
            var typeName = sampleType.FullName;
            var assemblyName = sampleType.Assembly.FullName;
            var model = new ViewTestClass(typeName, null, assemblyName, "File.xap");

            model.TypeName.ShouldBe(typeName);
            model.IsActivated.ShouldBe(false);

            // Activate from Type.
            model.Type.ShouldNotBe(null);
            model.Type.FullName.ShouldBe(typeName);
            model.IsActivated.ShouldBe(true);

            // Activate from Attribute.
            model = new ViewTestClass(typeName, null, assemblyName, "File.xap");
            model.IsActivated.ShouldBe(false);
            model.Attribute.ShouldNotBe(null);
            model.IsActivated.ShouldBe(true);

            // Activate from ViewTests.
            model = new ViewTestClass(typeName, null, assemblyName, "File.xap");
            model.IsActivated.ShouldBe(false);
            model.ViewTests.Count.ShouldNotBe(0);
            model.IsActivated.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldDetermineIfAssemblyIsLoaded()
        {
            TestHarnessModel.ResetSingleton();
            var testHarness = TestHarnessModel.Instance;
            testHarness.Modules.Clear();

            var sampleType = new SampleViewTestClass1().GetType(); // NB: Instance used to get actual SL type of (instead of 'typeof' which returns native Type).
            var typeName = sampleType.FullName;
            var assemblyName = sampleType.Assembly.FullName;
            var model = new ViewTestClass(typeName, null, assemblyName, "File.xap");

            var module = new ViewTestClassesAssemblyModule(new ModuleSetting(assemblyName, "File.xap"));
            testHarness.Modules.Add(module);

            model.IsAssemblyLoaded.ShouldBe(false);
            module.LoadAssembly(sampleType.Assembly);
            model.IsAssemblyLoaded.ShouldBe(true);

            TestHarnessModel.ResetSingleton();
        }

        [TestMethod]
        public void ShouldSetIsCurrent()
        {
            TestHarnessModel.ResetSingleton();
            var testHarness = TestHarnessModel.Instance;
            var sampleType = new SampleViewTestClass1().GetType(); // NB: Instance used to get actual SL type of (instead of 'typeof' which returns native Type).

            var model1 = new ViewTestClass(sampleType.FullName, null, sampleType.Assembly.FullName, "File.xap");
            var model2 = new ViewTestClass(typeof(SampleViewTestClass2), "File.xap");

            var module = new ViewTestClassesAssemblyModule(new ModuleSetting(sampleType.Assembly.FullName, "File.xap"));
            module.Classes.AddRange(new []{model1, model2});
            testHarness.Modules.Add(module);
            module.CurrentClass.ShouldBe(null);

            PropertyChangedEventArgs argsModel1 = null;
            model1.PropertyChanged += (sender, e) => argsModel1 = e;

            // ----------

            model1.IsActivated.ShouldBe(false);
            model1.IsCurrent.ShouldBe(false);
            model1.IsCurrent = true;
            model1.IsCurrent.ShouldBe(true);
            testHarness.CurrentClass.ShouldBe(model1);

            argsModel1.PropertyName.ShouldBe(LinqExtensions.GetPropertyName<ViewTestClass>(m => m.IsCurrent));

            module.CurrentClass.ShouldBe(model1);

            model2.IsCurrent = true;
            model2.IsCurrent.ShouldBe(true);
            testHarness.CurrentClass.ShouldBe(model2);
            model1.IsCurrent.ShouldBe(false); // Deselected.
            module.CurrentClass.ShouldBe(model2);

            model2.IsCurrent = false;
            module.CurrentClass.ShouldBe(null);
            testHarness.CurrentClass.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldReload()
        {
            TestHarnessModel.ResetSingleton();
            var testHarness = TestHarnessModel.Instance;
            var sampleType = new SampleViewTestClass1().GetType(); // NB: Instance used to get actual SL type of (instead of 'typeof' which returns native Type).

            var model1 = new ViewTestClass(sampleType.FullName, null, sampleType.Assembly.FullName, "File.xap");

            var module = new ViewTestClassesAssemblyModule(new ModuleSetting(sampleType.Assembly.FullName, "File.xap"));
            module.Classes.AddRange(new[] { model1 });
            testHarness.Modules.Add(module);
            module.CurrentClass.ShouldBe(null);

            model1.IsCurrent = true;
            testHarness.CurrentClass.ShouldBe(model1);

            var instance = (SampleViewTestClass1)model1.Instance;
            instance.MyProperty.ShouldBe(null);
            instance.MyProperty = "My Custom Value";

            model1.Reload();
            instance = (SampleViewTestClass1)model1.Instance;
            instance.MyProperty.ShouldBe(null);
        }


        [TestMethod]
        public void ShouldReturnParentModule()
        {
            TestHarnessModel.ResetSingleton();
            var testHarness = TestHarnessModel.Instance;
            testHarness.Modules.RemoveAll();

            var classModel = new ViewTestClass(typeof(SampleViewTestClass2), "File.xap");
            var moduleModel = new ViewTestClassesAssemblyModule(new ModuleSetting(typeof(SampleViewTestClass2).Assembly.FullName, "File.xap"));

            moduleModel.Classes.Add(classModel);
            testHarness.Modules.Add(moduleModel);
            testHarness.Modules.Count.ShouldBe(1);

            var retrievedModule = classModel.ParentModule;
            retrievedModule.ShouldBe(moduleModel);
        }

        [TestMethod]
        public void ShouldAlertWhenChildMethodExecuteRequestIsMade()
        {
            var sampleType = new SampleViewTestClass1().GetType(); // NB: Instance used to get actual SL type of (instead of 'typeof' which returns native Type).
            var classModel = new ViewTestClass(sampleType, "File.xap");
            var testModel = classModel.ViewTests[2];
            testModel.ShouldNotBe(classModel.DefaultViewTest);

            classModel.CurrentViewTest.ShouldBe(classModel.DefaultViewTest);

            var argsProp = new List<string>();
            classModel.PropertyChanged += (sender, e) => argsProp.Add(e.PropertyName);

            TestExecuteEventArgs argsExecuteRequest = null;
            classModel.ExecuteRequest += (sender, e) => argsExecuteRequest = e;

            testModel.Execute();
            argsExecuteRequest.ShouldNotBe(null);
            argsProp.ShouldContain(LinqExtensions.GetPropertyName<ViewTestClass>(m => m.CurrentViewTest));

            classModel.CurrentViewTest.ShouldBe(testModel);
        }

        [TestMethod]
        public void ShouldReturnFirstTestIfNotDefaultTestsSpecified()
        {
            var model = new ViewTestClass(typeof(SampleViewTestClass2), "File.xap");
            model.DefaultViewTest.MethodInfo.Name.ShouldBe(SampleViewTestClass2.PropMyMethod);
        }

        [TestMethod]
        public void ShouldReturnFirstTestMarkedAsDefault()
        {
            var model = new ViewTestClass(typeof(SampleViewTestClass1), "File.xap");
            model.DefaultViewTest.MethodInfo.Name.ShouldBe(SampleViewTestClass1.PropMethod2);
        }

        [TestMethod]
        public void ShouldHaveNullAsDefaultTestIfNoTestsExist()
        {
            var model = new ViewTestClass(typeof(SampleViewTestClassEmpty), "File.xap");
            model.DefaultViewTest.ShouldBe(null);
        }
        #endregion

        #region Test - Current Controls
        [TestMethod]
        public void ShouldExposeCurrentControls()
        {
            var classModel = new ViewTestClass(typeof(SampleViewTestClass1), "File.xap");

            classModel.CurrentViewTest.ShouldBe(classModel.DefaultViewTest);
            classModel.CurrentViewTest.MethodInfo.Name.ShouldBe(SampleViewTestClass1.PropMethod2);

            classModel.CurrentControls.Count.ShouldBe(3);
            classModel.CurrentControls[0].GetType().ShouldBe(typeof(Placeholder));
            classModel.CurrentControls[1].GetType().ShouldBe(typeof(Border));
            classModel.CurrentControls[2].GetType().ShouldBe(typeof(Placeholder));

            classModel.CurrentControls[0].ShouldNotBe(classModel.CurrentControls[2]);
        }

        [TestMethod]
        public void ShouldChangeCurrentControls()
        {
            var classModel = new ViewTestClass(typeof(SampleViewTestClass1), "File.xap");
            
            var args = new List<string>();
            classModel.PropertyChanged += (sender, e) => args.Add(e.PropertyName);

            classModel.CurrentControls.Count.ShouldBe(3);
            classModel.ViewTests[0].Execute();

            args.ShouldContain(LinqExtensions.GetPropertyName<ViewTestClass>(m => m.CurrentControls));

            classModel.CurrentControls.Count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldReuseExistingControlInstances()
        {
            var classModel = new ViewTestClass(typeof(SampleViewTestClass1), "File.xap");

            var firstInstance = classModel.CurrentControls[0];
            classModel.ViewTests[0].Execute();
            classModel.CurrentControls[0].ShouldBe(firstInstance);
        }

        [TestMethod]
        public void ShouldReportEquivalent()
        {
            var type1 = typeof(SampleViewTestClass1);

            var model1A = new ViewTestClass(type1, "File.xap");
            var model1B = new ViewTestClass(type1.FullName, null, type1.Assembly.GetAssemblyName(), "File.xap");

            model1A.IsEquivalent(type1).ShouldBe(true);
            model1B.IsEquivalent(type1).ShouldBe(true);

            var model2 = new ViewTestClass(typeof(SampleViewTestClass2), "File.xap");
            model2.IsEquivalent(type1).ShouldBe(false);
        }
        #endregion

        #region Sample Data
        [ViewTestClass]
        private class Sample
        {
            [Core.Common.ViewTest]
            public void Method1()
            {
            }

            [ViewTest]
            public void Method2(Border border)
            {
            }
        }
        #endregion
    }
}
