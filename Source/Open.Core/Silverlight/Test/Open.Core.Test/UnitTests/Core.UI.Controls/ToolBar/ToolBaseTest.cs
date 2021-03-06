﻿using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.ToolBar
{
    [TestClass]
    public class ToolBaseTest : SilverlightUnitTest
    {
        #region Head
        private MockTool tool;


        [TestInitialize]
        public void TestSetup()
        {
            tool = new MockTool();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldBeModel()
        {
            tool.ShouldBeInstanceOfType<ModelBase>();
        }

        [TestMethod]
        public void ShouldHaveDefaultValues()
        {
            tool.HorizontalAlignment.ShouldBe(HorizontalAlignment.Left);
            tool.VerticalAlignment.ShouldBe(VerticalAlignment.Top);
        }

        [TestMethod]
        public void ShouldFireEventFromEventBus()
        {
            EventBus.IsAsynchronous = false;
            EventBus.ShouldFire<IToolEvent>(() => tool.PublishToolEvent());
        }

        [TestMethod]
        public void ShouldHaveToolIdOnFiredEvent()
        {
            EventBus.IsAsynchronous = false;
            tool.Id = "MySampleId";

            EventBus.Subscribe<IToolEvent>(OnToolEvent);

            tool.PublishToolEvent();
            firedArgs.ToolId.ShouldBe("MySampleId");
        }

        IToolEvent firedArgs = null;
        public void OnToolEvent(IToolEvent e)
        {
            firedArgs = e;
        }

        [TestMethod]
        public void ShouldBeEnabledByDefault()
        {
            tool.IsEnabled.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldBeVisibleByDefault()
        {
            tool.IsVisible.ShouldBe(true);
        }
        #endregion
    }
}
