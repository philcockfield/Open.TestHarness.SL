using System.ComponentModel.Composition;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using Open.Core.UI.Controls.Controls.ToolBar.Events;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.ToolBar.Events
{
    [TestClass]
    public class ToolStateEventTest : SilverlightUnitTest
    {
        #region Head
        public enum MyTool { One, Two }

        [Import(typeof(IButtonTool))]
        public ExportFactory<IButtonTool> ToolCreator { get; set; }

        private ITool tool1;
        private ITool tool2;


        [TestInitialize]
        public void TestSetup()
        {
            CompositionInitializer.SatisfyImports(this);

            tool1 = ToolCreator.CreateExport().Value;
            tool1.Id = MyTool.One;

            tool2 = ToolCreator.CreateExport().Value;
            tool2.Id = MyTool.Two;
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldChangeToDisabled()
        {
            tool2.IsEnabled.ShouldBe(true);
            EventBus.Publish<IToolStateEvent>(new ToolStateEvent { ToolId = MyTool.Two, IsEnabled = false }, isAsynchronous:false);
            tool2.IsEnabled.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldChangeToEnabled()
        {
            tool2.IsEnabled = false;
            EventBus.Publish<IToolStateEvent>(new ToolStateEvent { ToolId = MyTool.Two, IsEnabled = true }, isAsynchronous: false);
            tool2.IsEnabled.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldNotChangeEnabledStateWhenEventArgsLeftNull()
        {
            tool2.IsEnabled = false;
            tool2.IsEnabled.ShouldBe(false);
            EventBus.Publish<IToolStateEvent>(new ToolStateEvent { ToolId = MyTool.Two }, isAsynchronous: false);
            tool2.IsEnabled.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldNotChangeStateOfOtherTools()
        {
            tool1.IsEnabled.ShouldBe(true);
            tool2.IsEnabled.ShouldBe(true);

            EventBus.Publish<IToolStateEvent>(new ToolStateEvent { ToolId = MyTool.Two, IsEnabled = false }, isAsynchronous: false);

            tool1.IsEnabled.ShouldBe(true);
            tool2.IsEnabled.ShouldBe(false);
        }
        #endregion

        #region Tests - Via Static Helper Class
        [TestMethod]
        public void ShouldChangeStateViaHelperMethod()
        {
            tool2.IsEnabled.ShouldBe(true);
            ToolState.Change(MyTool.Two, false);
            tool2.IsEnabled.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldNotChangeStateViaHelperMethod()
        {
            tool2.IsEnabled = false;
            tool2.IsEnabled.ShouldBe(false);
            ToolState.Change(MyTool.Two);
            tool2.IsEnabled.ShouldBe(false);
        }
        #endregion
    }
}
