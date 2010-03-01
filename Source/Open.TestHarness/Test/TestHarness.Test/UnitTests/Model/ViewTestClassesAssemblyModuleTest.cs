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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.TestHarness.Model;

using T = Open.TestHarness.Model.ViewTestClassesAssemblyModule;

namespace Open.TestHarness.Test.Model
{
    [TestClass]
    public class ViewTestClassesAssemblyModuleTest : SilverlightTest
    {
        #region Head
        private readonly Assembly sampleAssembly = new SampleViewTestClass1().GetType().Assembly;
        private ViewTestClassesAssemblyModule moduleModel;
        private ViewTestClass classModel;

        [TestInitialize]
        public void Initialize()
        {
            TestHarnessModel.ResetSingleton();
            var testHarness = TestHarnessModel.Instance;
            testHarness.Modules.RemoveAll();

            classModel = new ViewTestClass(typeof(SampleViewTestClass1), "File.xap");

            moduleModel = new ViewTestClassesAssemblyModule(new ModuleSetting(sampleAssembly.FullName, "File.xap"));
            moduleModel.LoadAssembly(sampleAssembly);
            moduleModel.Classes.Add(classModel);

            testHarness.Modules.Add(moduleModel);
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldLoadAssembly()
        {
            TestHarnessModel.ResetSingleton();
            var testHarness = TestHarnessModel.Instance;
            testHarness.LoadedAssemblies.ShouldBeEmpty();

            moduleModel = new ViewTestClassesAssemblyModule(new ModuleSetting("Some Name"));

            var propArgs = new List<string>();
            moduleModel.PropertyChanged += (sender, e) => propArgs.Add(e.PropertyName);

            EventArgs startArgs = null;
            moduleModel.AssemblyLoadStarted += (sender, e) => startArgs = e;

            EventArgs completeArgs = null;
            moduleModel.AssemblyLoadComplete += (sender, e) => completeArgs = e;

            // --------------
            moduleModel.IsLoaded.ShouldBe(false);
            moduleModel.LoadAssembly(sampleAssembly);

            moduleModel.AssemblyName.ShouldBe(ReflectionUtil.GetAssemblyName(sampleAssembly.FullName));
            moduleModel.Assembly.ShouldBe(sampleAssembly);
            moduleModel.IsLoaded.ShouldBe(true);
            moduleModel.Classes.ShouldNotBeEmpty();
            testHarness.LoadedAssemblies.ShouldContain(sampleAssembly);

            propArgs.ShouldContain(LinqExtensions.GetPropertyName<T>(m => m.AssemblyName));
            propArgs.ShouldContain(LinqExtensions.GetPropertyName<T>(m => m.Assembly));
            propArgs.ShouldContain(LinqExtensions.GetPropertyName<T>(m => m.IsLoaded));
            propArgs.Clear();

            startArgs.ShouldNotBe(null);
            completeArgs.ShouldNotBe(null);

            moduleModel.LoadAssembly(sampleAssembly);
            propArgs.ShouldBeEmpty();
        }

        [TestMethod]
        public void ShouldUnloadModule()
        {
            var testHarness = TestHarnessModel.Instance;

            moduleModel.Assembly.ShouldNotBe(null);
            moduleModel.Classes.ShouldNotBeEmpty();

            classModel.IsCurrent = true;
            moduleModel.CurrentClass.ShouldBe(classModel);
            testHarness.CurrentClass.ShouldBe(classModel);

            var settings = testHarness.Settings;
            settings.Clear();
            var assemblyName = ReflectionUtil.GetAssemblyName(typeof (SampleViewTestClass1).Assembly.FullName);
            settings.RecentSelections = new[] 
                            {
                                new RecentSelectionSetting(
                                                            typeof(SampleViewTestClass1).FullName, 
                                                            assemblyName, 
                                                            "File.xap")
                            };

            // ---------

            moduleModel.Unload();

            moduleModel.IsLoaded.ShouldBe(false);
            moduleModel.Assembly.ShouldBe(null);
            moduleModel.AssemblyName.ShouldNotBe(null); // Retained for identification and future reloading.
            moduleModel.Classes.ShouldBeEmpty();

            testHarness.Modules.ShouldNotContain(moduleModel);
            testHarness.Settings.LoadedModules.ShouldNotContain(moduleModel.AssemblyName);

            testHarness.CurrentClass.ShouldBe(null);

            settings.RecentSelections.FirstOrDefault(item => item.Module.AssemblyName == assemblyName).ShouldBe(null);
        }

        [TestMethod]
        public void ShouldExtractAssemblyNameInConstructor()
        {
            moduleModel = new ViewTestClassesAssemblyModule(new ModuleSetting(sampleAssembly.FullName, "File.xap"));
            moduleModel.AssemblyName.ShouldBe(ReflectionUtil.GetAssemblyName(sampleAssembly.FullName));
        }

        [TestMethod]
        public void ShouldRemoveXapExtensionInConstructor()
        {
            moduleModel = new ViewTestClassesAssemblyModule(new ModuleSetting(sampleAssembly.FullName, "File.xap"));
            moduleModel.XapFileName.ShouldBe("File");
        }
        #endregion

        #region Tests - Query Methods
        private const string sampleClassName = "MySampleQueryViewTest";
        private const string sampleMethodName = "MySampleQueryMethod";


        [TestMethod]
        public void ShouldGetAllMatchingClasses()
        {
            moduleModel.LoadAssembly(sampleAssembly);
            moduleModel.GetTestClasses(sampleClassName).Count().ShouldBe(2);
        }

        [TestMethod]
        public void ShouldGetAllSpecificFullyQualifiedClass()
        {
            moduleModel.LoadAssembly(sampleAssembly);
            moduleModel.GetTestClasses("Open.TestHarness.Test.Model.Namespace1." + sampleClassName).Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotFindClass()
        {
            moduleModel.Unload();
            moduleModel.IsLoaded.ShouldBe(false);
            moduleModel.GetTestClasses(sampleClassName).Count().ShouldBe(0);

            moduleModel.LoadAssembly(sampleAssembly);
            moduleModel.IsLoaded.ShouldBe(true);
            moduleModel.GetTestClasses("NotAClassThatExistsAnywhere").Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldReturnAllClasses()
        {
            moduleModel.LoadAssembly(sampleAssembly);
            moduleModel.GetTestClasses(null).ShouldBe(moduleModel.Classes);
            moduleModel.GetTestClasses("").ShouldBe(moduleModel.Classes);
            moduleModel.GetTestClasses("  ").ShouldBe(moduleModel.Classes);
        }

        [TestMethod]
        public void ShouldReturnNoMethods()
        {
            moduleModel.Unload();
            moduleModel.IsLoaded.ShouldBe(false);
            moduleModel.GetTestMethods(sampleMethodName).Count().ShouldBe(0);

            moduleModel.LoadAssembly(sampleAssembly);
            moduleModel.GetTestMethods("NotAMethodName").Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldGetTwoMethods()
        {
            moduleModel
                .GetTestMethods(sampleMethodName)
                .Count().ShouldBe(2);
        }

        [TestMethod]
        public void ShouldGetOneFullyQualifiedMethod()
        {
            moduleModel
                .GetTestMethods("open.testHarness.Test.Model.namespace1.MySampleQueryViewTest.MySampleQueryMethod")
                .Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldGetAllMethodsInAssembly()
        {
            var methods = GetType().Assembly.GetViewTestMethods();
            moduleModel.GetTestMethods().Count().ShouldBe(methods.Count());
        }
        #endregion
    }
}

namespace Open.TestHarness.Test.Model.Namespace1
{
    [ViewTestClass]
    public class MySampleQueryViewTest
    {
        [ViewTest]
        public void MySampleQueryMethod() { }
    }
}

namespace Open.TestHarness.Test.Model.Namespace2
{
    [ViewTestClass]
    public class MySampleQueryViewTest
    {
        [ViewTest]
        public void MySampleQueryMethod() { }
    }
}




