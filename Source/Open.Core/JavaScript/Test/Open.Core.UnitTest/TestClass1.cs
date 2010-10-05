// TestClass1.cs
//

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptSharp.Testing;

namespace Open.Core.UnitTest
{

    [TestClass]
    public class TestClass1
    {

        private static WebTest _webTest;

        private TestContext _testContext;

        public TestContext TestContext
        {
            get
            {
                return _testContext;
            }
            set
            {
                _testContext = value;
            }
        }

        [ClassInitialize()]
        public static void OnInitialize(TestContext testContext)
        {
            _webTest = new WebTest();
            _webTest.Start(testContext.TestDeploymentDir, 3976);
        }

        [ClassCleanup()]
        public static void OnCleanup()
        {
            _webTest.Stop();
        }

        #region Additional Test Attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void OnTestInitialize() {
        // }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void OnTestCleanup() {
        // }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            Uri testUri = _webTest.GetTestUri("/Default.htm");

            WebTestResult ieResult = _webTest.RunTest(testUri, WebBrowser.InternetExplorer);
            Assert.IsTrue(ieResult.Succeeded, "Internet Explorer:\r\n" + ieResult.Log);

            WebTestResult chromeResult = _webTest.RunTest(testUri, WebBrowser.Chrome);
            Assert.IsTrue(chromeResult.Succeeded, "Chrome:\r\n" + chromeResult.Log);

            WebTestResult ffResult = _webTest.RunTest(testUri, WebBrowser.Firefox);
            Assert.IsTrue(ffResult.Succeeded, "Firefox:\r\n" + ffResult.Log);
        }
    }
}
