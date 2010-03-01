using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.TestHarness.Automation;
using Open.TestHarness.Model;

namespace Open.TestHarness.Test.UnitTests.Automation
{
//    [Tag("current")]
    [TestClass]
    public class TestRunnerTest
    {
        #region Head
        private TestRunner testRunner;
        private ViewTestClassesAssemblyModule module;

        [TestInitialize]
        public void TestSetup()
        {
            testRunner = new TestRunner(TestHarnessModel.Instance);

            module = new ViewTestClassesAssemblyModule(new ModuleSetting());
            module.LoadAssembly(GetType().Assembly);
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldConstructWithDefaultValues()
        {
            testRunner.TestHarness.ShouldBeInstanceOfType<TestHarnessModel>();
        }

        [TestMethod]
        public void ShouldAddTestMethods()
        {
            // TODO
        }

        [TestMethod]
        public void ShouldAddAllTestMethodsInClass()
        {
            // TODO
        }

        [TestMethod]
        public void ShouldNotAddTheSameMethodTwice()
        {
            // TODO
        }
        #endregion
    }
}
