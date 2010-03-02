using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using Open.TestHarness.Automation;
using Open.TestHarness.Model;

namespace Open.TestHarness.Test.UnitTests.Automation
{
    [Tag("current")]
    [TestClass]
    public class TestRunnerTest : SilverlightUnitTest
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

        [TestMethod]
        [Asynchronous]
        public void ShouldRunTests()
        {
            testRunner.Interval = 0.2;
            testRunner.Add(testClass);
            testRunner.Start(() =>
                                 {
                                     var instance = testClass.Instance as TestRunnerMockViewTest;
                                     instance.InvokeCount.ShouldBe(2);
                                     instance.Control.ShouldBeInstanceOfType<Placeholder>();
                                     EnqueueTestComplete();
                                 });
        }
        #endregion
    }

    [ViewTestClass]
    public class TestRunnerMockViewTest
    {
        public Placeholder Control { get; private set; }
        public int InvokeCount { get; private set; }

        [ViewTest]
        public void SampleTestRunnerMethod(Placeholder control)
        {
            Control = control;
            InvokeCount++;
        }

        [ViewTest]
        public void AnotherTestRunnerMethod(Placeholder control)
        {
            Control = control;
            InvokeCount++;
        }

    }
}
