using System.ComponentModel.Composition;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Controls
{
    [Tag("foo")]
    [TestClass]
    public class PagerTest
    {
        #region Head
        [Import]
        public ExportFactory<IPager> PagerFactory { get; set; }
        private IPager pager;

        public PagerTest()
        {
            CompositionInitializer.SatisfyImports(this);
        }

        [TestInitialize]
        public void TestSetup()
        {
            pager = PagerFactory.CreateExport().Value;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }
        #endregion
        
        #region Tests
        #endregion
    }
}
