using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.Composite;

namespace Open.Core.Test.UnitTests.Core.Model
{
    [TestClass]
    public class NotifyPropertyChangedBaseSilverlightTest : SilverlightUnitTest
    {
        #region Head
        private Mock mock;

        [TestInitialize]
        public void TestSetup()
        {
            mock = new Mock();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldHaveSingletonEventBus()
        {
            mock.EventBus.ShouldNotBe(null);
            mock.EventBus.ShouldBe(EventBus);
        }
        #endregion

        public class Mock : NotifyPropertyChangedBase
        {
            public new IEventBus EventBus { get { return base.EventBus; } }
        }

    }
}
