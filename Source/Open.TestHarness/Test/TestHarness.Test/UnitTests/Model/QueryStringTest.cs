using System;
using System.Linq;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
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
        public void ShouldContainClassNamesFromQueryString()
        {
            var uri = new Uri("/TestHarness.htm?xap=One&Class=NS.Class&class=  Class2.&CLASS=  &class=Class3  ", UriKind.Relative);
            var model = new QueryString(uri.GetQueryString());
            model.ClassNames.Count().ShouldBe(3);

            model.ClassNames.ElementAt(0).ShouldBe("NS.Class");
            model.ClassNames.ElementAt(1).ShouldBe("Class2.");
            model.ClassNames.ElementAt(2).ShouldBe("Class3");
        }

        [TestMethod]
        public void ShouldGetRunTestsFromQueryString()
        {
            new QueryString(new Uri("/TestHarness.htm?RunTests=truE", UriKind.Relative).GetQueryString()).RunTests.ShouldBe(true);
            new QueryString(new Uri("/TestHarness.htm?RUNTESTS=FALSE", UriKind.Relative).GetQueryString()).RunTests.ShouldBe(false);

            new QueryString(new Uri("/TestHarness.htm?", UriKind.Relative).GetQueryString()).RunTests.ShouldBe(false);
            new QueryString(new Uri("/TestHarness.htm?runTests=", UriKind.Relative).GetQueryString()).RunTests.ShouldBe(false);

            new QueryString(new Uri("/TestHarness.htm?runTests=  TRUE  ", UriKind.Relative).GetQueryString()).RunTests.ShouldBe(true);
            new QueryString(new Uri("/TestHarness.htm?runtests=  false  ", UriKind.Relative).GetQueryString()).RunTests.ShouldBe(false);
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

        [TestMethod]
        public void ShouldHaveDefaultTestTypeWhenNotSpecified()
        {
            var uri = new Uri("/TestHarness.htm", UriKind.Relative);
            var model = new QueryString(uri.GetQueryString());
            model.TestType.ShouldBe(TestType.ViewTest);
        }

        [TestMethod]
        public void ShouldHaveDefaultTestTypeWhenUnknownValueSpecified()
        {
            var uri = new Uri("/TestHarness.htm?testType=UNKNOWN", UriKind.Relative);
            var model = new QueryString(uri.GetQueryString());
            model.TestType.ShouldBe(TestType.ViewTest);
        }

        [TestMethod]
        public void ShouldReadViewTestType()
        {
            var uri = new Uri("/TestHarness.htm?testType=  viewtest ", UriKind.Relative);
            var model = new QueryString(uri.GetQueryString());
            model.TestType.ShouldBe(TestType.ViewTest);
        }

        [TestMethod]
        public void ShouldReadUnitTestType()
        {
            var uri = new Uri("/TestHarness.htm?testType=unitTest", UriKind.Relative);
            var model = new QueryString(uri.GetQueryString());
            model.TestType.ShouldBe(TestType.UnitTest);
        }

        [TestMethod]
        public void ShouldHaveThreeTags()
        {
            var uri = new Uri("/TestHarness.htm?tag=one&tag=   two   &tag= three&", UriKind.Relative);
            var model = new QueryString(uri.GetQueryString());

            model.Tags.Count().ShouldBe(3);
            model.Tags.ElementAt(0).ShouldBe("one");
            model.Tags.ElementAt(1).ShouldBe("two");
            model.Tags.ElementAt(2).ShouldBe("three");
        }

        [TestMethod]
        public void ShouldNotHaveTags()
        {
            var uri = new Uri("/TestHarness.htm?tag=   ", UriKind.Relative);
            var model = new QueryString(uri.GetQueryString());
            model.Tags.Count().ShouldBe(0);

            uri = new Uri("/TestHarness.htm", UriKind.Relative);
            model = new QueryString(uri.GetQueryString());
            model.Tags.Count().ShouldBe(0);
        }
        #endregion
    }
}
