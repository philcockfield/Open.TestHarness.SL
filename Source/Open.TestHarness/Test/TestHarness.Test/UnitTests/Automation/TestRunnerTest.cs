using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using Open.TestHarness.Automation;
using Open.TestHarness.Model;

namespace Open.TestHarness.Test.UnitTests.Automation
{
    [TestClass]
    public class TestRunnerTest : SilverlightUnitTest
    {
        #region Head
        private TestRunner testRunner;
        private ViewTestClassesAssemblyModule module;
        private ViewTestClass testClass;
        private MethodInfo testMethodInfo;

        [TestInitialize]
        public void TestSetup()
        {
            TestHarnessModel.ResetSingleton();
            TestRunnerMockViewTest.InvokeCount = 0;

            testRunner = new TestRunner { Interval = 0.01 };

            module = new ViewTestClassesAssemblyModule(new ModuleSetting());
            module.LoadAssembly(GetType().Assembly);

            testClass = module.GetTestClasses("TestRunnerMockViewTest").FirstOrDefault();
            testMethodInfo = testClass.GetTestMethod("SampleTestRunnerMethod").MethodInfo;
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldAddOneTestMethod()
        {
            testRunner.Add(testClass, testMethodInfo);
            testRunner.GetMethods().Count(m => m.MethodInfo == testMethodInfo).ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotAddTheSameMethodTwice()
        {
            testRunner.Add(testClass, testMethodInfo);
            testRunner.Add(testClass, testMethodInfo);
            testRunner.Add(testClass, testMethodInfo);
            testRunner.GetMethods().Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldAddAllTestMethodsInClass()
        {
            testRunner.Add(testClass);
            testRunner.GetMethods().Count().ShouldBe(testClass.ViewTests.Count);
            foreach (var method in testRunner.GetMethods())
            {
                testClass.ViewTests.ShouldContain(method);
            }
        }

        [TestMethod]
        public void ShouldAddAllTestMethodsInAssembly()
        {
            var types = module.Assembly.GetViewTestMethods();
            testRunner.Add(module);

            testRunner.GetMethods().Count().ShouldBe(types.Count());
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
            testRunner.Add(testClass);
            testRunner.Start(() =>
                         {
                             TestRunnerMockViewTest.InvokeCount.ShouldBe(4); // NB: default test executed for each test.
                             var instance = testClass.Instance as TestRunnerMockViewTest;
                             instance.Control.ShouldBeInstanceOfType<Placeholder>();
                             EnqueueTestComplete();
                         });
        }

        [TestMethod]
        [Asynchronous]
        public void ShouldSetTestClassAsCurrentOnRootModel()
        {
            testRunner.Add(testClass);
            TestHarnessModel.Instance.CurrentClass.ShouldBe(null);
            testRunner.Start(() =>
                            {
                                TestHarnessModel.Instance.CurrentClass.ShouldBe(testClass);
                                EnqueueTestComplete();
                            });
        }

        [TestMethod]
        [Asynchronous]
        public void ShouldReportWhenRunning()
        {
            testRunner.Add(testClass);
            testRunner.IsRunning.ShouldBe(false);
            testRunner.Start(() =>
                        {
                            testRunner.IsRunning.ShouldBe(false);
                            EnqueueTestComplete();
                        });
            testRunner.IsRunning.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldThrowIfRunnerStartedWhileAlreadyRunning()
        {
            testRunner.Add(testClass);
            testRunner.Start(null);
            Should.Throw<Exception>(() => testRunner.Start(null));
        }

        [TestMethod]
        [Asynchronous]
        public void ShouldReportSuccessesAndFailures()
        {
            var method1 = testClass.GetTestMethod("SampleTestRunnerMethod").MethodInfo;
            var method2 = testClass.GetTestMethod("AnotherTestRunnerMethod").MethodInfo;
            var method3 = testClass.GetTestMethod("ThrowError").MethodInfo;

            testRunner.Add(testClass);
            testRunner.Start(() =>
                                {
                                    testRunner.Passed.Count.ShouldBe(2);
                                    testRunner.Failed.Count.ShouldBe(1);

                                    testRunner.Passed.ShouldContain(method1);
                                    testRunner.Passed.ShouldContain(method2);
                                    testRunner.Failed.ShouldContain(method3);

                                    EnqueueTestComplete();
                                });
        }

        [TestMethod]
        [Asynchronous]
        public void ShouldResetPassAndFailCollectionUponStart()
        {
            testRunner.Add(testClass);
            testRunner.Start(() =>
                        {
                            testRunner.Passed.Count.ShouldBe(2);
                            testRunner.Failed.Count.ShouldBe(1);

                            testRunner.Start(() => EnqueueTestComplete());

                            testRunner.Passed.Count.ShouldBe(0);
                            testRunner.Failed.Count.ShouldBe(0);
                        });
        }
        #endregion
    }

    [ViewTestClass]
    public class TestRunnerMockViewTest
    {
        public Placeholder Control { get; private set; }
        public static int InvokeCount { get; internal set; }

        [ViewTest(Default = true)]
        public void SampleTestRunnerMethod(Placeholder control)
        {
            Control = control;
            InvokeCount++;
            Output.Write(Colors.Orange, "EXECUTED: SampleTestRunnerMethod (Default)");
        }

        [ViewTest]
        public void AnotherTestRunnerMethod(Placeholder control)
        {
            Control = control;
            InvokeCount++;
            Output.Write(Colors.Green, "EXECUTED: AnotherTestRunnerMethod");
        }

        [ViewTest]
        public void ThrowError(Placeholder control)
        {
            throw new Exception("Sample exception thrown in ViewTest.");
        }
    }

    [ViewTestClass]
    public class TestRunnerMockViewTestNoErrors
    {
        [ViewTest]
        public void Method1(Placeholder control)
        {
            Output.Write("Executed: Method1");
        }

        [ViewTest]
        public void Method2(Placeholder control)
        {
            Output.Write("Executed: Method2");
        }
    }

}
