using System;
using System.ComponentModel.Composition;
using System.Net;
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
using Open.Core.Composite;
using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.ToolBar
{
    [Tag("current")]
    [TestClass]
    public class ToolBaseTest
    {
        #region Head
        private MockTool mockTool;


        [TestInitialize]
        public void TestSetup()
        {
            mockTool = new MockTool();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldBeModel()
        {
            mockTool.ShouldBeInstanceOfType<ModelBase>();
        }

        [TestMethod]
        public void ShouldFireEventFromEventBus()
        {
            var eventBus = new Importer().EventBus;
            eventBus.IsAsynchronous = false;
            eventBus.ShouldFire<IToolEvent>(() => mockTool.FireExecutedEventPublic());
        }
        #endregion

        public class Importer : ImporterBase
        {
            [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
            public IEventBus EventBus { get; set; }
        }

    }
}
