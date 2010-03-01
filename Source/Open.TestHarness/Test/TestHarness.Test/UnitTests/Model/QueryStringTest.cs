using System;
using System.Linq;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.TestHarness.Automation;
using Open.TestHarness.Model;
using Open.Core.Common.Testing;

namespace Open.TestHarness.Test.UnitTests.Model
{
    [TestClass]
    public class QueryStringTest
    {
        #region Head

        [TestInitialize]
        public void TestSetup()
        {
            TestHarnessModel.ResetSingleton();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldHaveNoQueryString()
        {
            var model = new QueryString();
            model.Items.Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldHaveQueryStringItems()
        {
            var uri = new Uri("/TestHarness.htm?xap=MyApp&class=MyClass&method=MyMethod", UriKind.Relative);
            var model = new QueryString(uri.GetQueryString());

            model.Items.ElementAt(0).Key.ShouldBe("xap");
            model.Items.ElementAt(0).Value.ShouldBe("MyApp");

            model.Items.ElementAt(1).Key.ShouldBe("class");
            model.Items.ElementAt(1).Value.ShouldBe("MyClass");

            model.Items.ElementAt(2).Key.ShouldBe("method");
            model.Items.ElementAt(2).Value.ShouldBe("MyMethod");
        }

        [TestMethod]
        public void ShouldContainXapFilesFromQueryString()
        {
            var uri = new Uri("/TestHarness.htm?xap=One&XAP=Two...xap&xap=Three.&xap=Four.XAP&xap=  ", UriKind.Relative);
            var model = new QueryString(uri.GetQueryString());
            model.XapFiles.Count().ShouldBe(4);

            model.XapFiles.ElementAt(0).ShouldBe("One");
            model.XapFiles.ElementAt(1).ShouldBe("Two");
            model.XapFiles.ElementAt(2).ShouldBe("Three");
            model.XapFiles.ElementAt(3).ShouldBe("Four");
        }

        [TestMethod]
        public void ShouldAddXapModulesToRootModel()
        {
            TestHarnessModel.ResetSingleton();
            TestHarnessModel.Instance.Settings.Clear();

            var testHarness = TestHarnessModel.Instance;
            testHarness.Modules.Where(m => m.GetType() == typeof(ViewTestClassesAssemblyModule)).Count().ShouldBe(0);

            var uri = new Uri("/TestHarness.htm?xap=One&xap=Two", UriKind.Relative);
            var model = new QueryString(uri.GetQueryString());

            var modules = testHarness.Modules.Where(m => m.GetType() == typeof (ViewTestClassesAssemblyModule)).Cast<ViewTestClassesAssemblyModule>();
            modules.Count().ShouldBe(2);
            modules.ElementAt(0).XapFileName.ShouldBe("One");
            modules.ElementAt(1).XapFileName.ShouldBe("Two");

            // ---

            uri = new Uri("/TestHarness.htm?xap=One&xap=Three", UriKind.Relative);
            model = new QueryString(uri.GetQueryString());

            modules = testHarness.Modules.Where(m => m.GetType() == typeof(ViewTestClassesAssemblyModule)).Cast<ViewTestClassesAssemblyModule>();
            modules.Count().ShouldBe(3);
            modules.ElementAt(0).XapFileName.ShouldBe("One");
            modules.ElementAt(1).XapFileName.ShouldBe("Two");
            modules.ElementAt(2).XapFileName.ShouldBe("Three");
        }
        #endregion
    }
}
