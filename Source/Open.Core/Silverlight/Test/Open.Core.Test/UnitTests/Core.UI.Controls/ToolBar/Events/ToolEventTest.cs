using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.ToolBar.Events
{
    [TestClass]
    public class ToolEventTest : SilverlightUnitTest
    {
        #region Head
        public enum MyEnum
        {
            One,
            Two,
            Three
        } 

        private ToolEvent toolEvent;

        [TestInitialize]
        public void TestSetup()
        {
            toolEvent = new ToolEvent();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldBeIdMatch()
        {
            toolEvent.ToolId = MyEnum.Two;
            toolEvent.IsMatch(MyEnum.Two).ShouldBe(true);
        }

        [TestMethod]
        public void ShouldBeNullMatch()
        {
            toolEvent.ToolId = null;
            toolEvent.IsMatch(null).ShouldBe(true);
        }

        [TestMethod]
        public void ShouldNotBeIdMatch()
        {
            toolEvent.ToolId = null;
            toolEvent.IsMatch(MyEnum.Two).ShouldBe(false);

            toolEvent.ToolId = MyEnum.Three;
            toolEvent.IsMatch(MyEnum.Two).ShouldBe(false);

            toolEvent.ToolId = MyEnum.Three;
            toolEvent.IsMatch(null).ShouldBe(false);
        }
        #endregion
    }
}
