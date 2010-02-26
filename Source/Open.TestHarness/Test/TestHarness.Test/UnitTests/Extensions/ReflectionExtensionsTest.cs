using System;
using System.Linq;
using System.Net;
using System.Reflection;
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
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.TestHarness.Test.Model;

using T1 = Open.TestHarness.Test.Model.Namespace1.MyReflectionSampleViewTest;
using T2 = Open.TestHarness.Test.Model.Namespace1.MyReflectionSampleViewTest;

namespace Open.TestHarness.Test.UnitTests.Extensions
{
    [TestClass]
    public class ReflectionExtensionsTest
    {
        #region Head
        private Assembly assembly;

        [TestInitialize]
        public void TestSetup()
        {
            assembly = GetType().Assembly;
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldGetViewTestClasses()
        {
            assembly.GetViewTestClasses().ShouldContain(typeof(T1));
        }

        [TestMethod]
        public void ShouldRetrieveMultipleViewTestClasses()
        {
            var types = assembly.GetViewTestClasses("MyReflectionSampleViewTest");
            types.Count().ShouldBe(2);
        }

        [TestMethod]
        public void ShouldRetrieveSingleViewTestClass()
        {
            var types = assembly.GetViewTestClasses("open.TestHarness.test.model.namespace1.MyReflectionSampleViewTest");
            types.Count().ShouldBe(1);
        }
        #endregion
    }
}

namespace Open.TestHarness.Test.Model.Namespace1
{
    [ViewTestClass]
    public class MyReflectionSampleViewTest { }
}

namespace Open.TestHarness.Test.Model.Namespace2
{
    [ViewTestClass]
    public class MyReflectionSampleViewTest { }
}

