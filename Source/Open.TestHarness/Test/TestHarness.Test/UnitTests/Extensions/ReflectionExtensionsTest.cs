using System.Linq;
using System.Reflection;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.TestHarness.Test.Model;
using T1 = Open.TestHarness.Test.Model.Namespace1.MyReflectionSampleViewTest;

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

        [TestMethod]
        public void ShouldContainSampleViewTestClass()
        {
            var types = assembly.GetViewTestClasses();
            types.Count(m => m == typeof(SampleViewTestClass1)).ShouldBe(1);
        }


        [TestMethod]
        public void ShouldRetrieveNoMethods()
        {
            ((Assembly)null).GetViewTestMethods("MyReflectionSampleMethod").Count().ShouldBe(0);
            assembly.GetViewTestMethods("NotAMethodThatExists").Count().ShouldBe(0);
            assembly.GetViewTestMethods("Open.TestHarness.Test.Model.Namespace1.NotAMethodThatExists").Count().ShouldBe(0);
            assembly.GetViewTestMethods("MethodWithOutAttribute").Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldRetreiveAllViewTestsInAssembly()
        {
            var methods = from c in assembly.GetViewTestClasses()
                        from m in c.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                        where m.HasAttribute<ViewTestAttribute>()
                        select m;

            assembly.GetViewTestMethods().Count().ShouldBe(methods.Count());
            assembly.GetViewTestMethods(null).Count().ShouldBe(methods.Count());
            assembly.GetViewTestMethods("").Count().ShouldBe(methods.Count());
            assembly.GetViewTestMethods("  ").Count().ShouldBe(methods.Count());
        }

        [TestMethod]
        public void ShouldContainSampleMethod()
        {
            var method = typeof(SampleViewTestClass1).GetMethod("Method_1");
            assembly.GetViewTestMethods().ShouldContain(method);
        }

        [TestMethod]
        public void ShouldRetrieveTwoMethods()
        {
            assembly.GetViewTestMethods("MyReflectionSampleMethod").Count().ShouldBe(2);
            assembly.GetViewTestMethods("myReflectionSampleMethod").Count().ShouldBe(2); // Different casing.
        }

        [TestMethod]
        public void ShouldRetrieveOneMethod()
        {
            assembly.GetViewTestMethods("open.TestHarness.test.model.namespace1.MyReflectionSampleViewTest.MyReflectionSampleMethod").Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotHaveViewTestAttribute()
        {
            var method = typeof (T1).GetMethod("MethodWithOutAttribute");
            method.HasAttribute<ViewTestAttribute>().ShouldBe(false);
        }
        #endregion
    }
}

namespace Open.TestHarness.Test.Model.Namespace1
{
    [ViewTestClass]
    public class MyReflectionSampleViewTest
    {
        [ViewTest]
        public void MyReflectionSampleMethod(){}

        public void MethodWithOutAttribute(){}
    }
}

namespace Open.TestHarness.Test.Model.Namespace2
{
    [ViewTestClass]
    public class MyReflectionSampleViewTest
    {
        [ViewTest]
        public void MyReflectionSampleMethod() { }
    }
}


