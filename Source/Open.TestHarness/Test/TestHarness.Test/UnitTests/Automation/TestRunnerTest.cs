using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.TestHarness.Automation;
using Open.TestHarness.Model;

namespace Open.TestHarness.Test.UnitTests.Automation
{
    [TestClass]
    public class TestRunnerTest
    {
        #region Head
        private TestRunner testRunner;
        private ViewTestClassesAssemblyModule module;
        private ViewTestClass testClass;
        private ViewTest testMethod;

        [TestInitialize]
        public void TestSetup()
        {
            testRunner = new TestRunner(TestHarnessModel.Instance);

            module = new ViewTestClassesAssemblyModule(new ModuleSetting());
            module.LoadAssembly(GetType().Assembly);

            testClass = module.GetTestClasses("TestRunnerMockViewTest").FirstOrDefault();
            testMethod = testClass.GetTestMethod("SampleTestRunnerMethod");
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldConstructWithDefaultValues()
        {
            testRunner.TestHarness.ShouldBeInstanceOfType<TestHarnessModel>();
        }

        [TestMethod]
        public void ShouldAddOneTestMethod()
        {
            testRunner.Add(testClass, testMethod);
            testRunner.Methods.ShouldContain(testMethod);
        }

        [TestMethod]
        public void ShouldNotAddTheSameMethodTwice()
        {
            testRunner.Add(testClass, testMethod);
            testRunner.Add(testClass, testMethod);
            testRunner.Add(testClass, testMethod);
            testRunner.Methods.Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldAddAllTestMethodsInClass()
        {
            testRunner.Add(testClass);
            testRunner.Methods.Count().ShouldBe(testClass.ViewTests.Count);
            foreach (var method in testRunner.Methods)
            {
                testClass.ViewTests.ShouldContain(method);
            }
        }

        [TestMethod]
        public void ShouldAddAllTestMethodsInAssembly()
        {
            var types = module.Assembly.GetViewTestMethods();
            testRunner.Add(module);

            testRunner.Methods.Count().ShouldBe(types.Count());
        }

        [TestMethod]
        public void ShouldThrowIfAddingUnloadedModule()
        {
            module.Unload();
            Should.Throw<ArgumentOutOfRangeException>(() => testRunner.Add(module));
        }
        #endregion
    }

    [ViewTestClass]
    public class TestRunnerMockViewTest
    {
        [ViewTest]
        public void SampleTestRunnerMethod()
        {
        }

    }


    //TEMP 
    //namespace Open.TestHarness.Test.Model.Namespace1
    //{
    //    [ViewTestClass]
    //    public class MyReflectionSampleViewTest
    //    {
    //        [ViewTest]
    //        public void MyReflectionSampleMethod() { }

    //        public void MethodWithOutAttribute() { }
    //    }
    //}


}
