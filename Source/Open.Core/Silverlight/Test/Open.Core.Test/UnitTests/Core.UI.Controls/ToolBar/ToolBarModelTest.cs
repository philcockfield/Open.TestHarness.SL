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
using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.ToolBar
{
//    [Tag("current")]
    [TestClass]
    public class ToolBarModelTest
    {
        #region Head
        private ToolBarModel toolbar;

        [TestInitialize]
        public void TestSetup()
        {
            toolbar = new ToolBarModel();
        }
        #endregion
        
        #region Tests
        [Import]
        public IToolBar ImportedToolBar1 { get; set; }

        [Import]
        public IToolBar ImportedToolBar2 { get; set; }

        [TestMethod]
        public void ShouldImportUniqueInstances()
        {
            CompositionInitializer.SatisfyImports(this);

            ImportedToolBar1.ShouldBeInstanceOfType<ToolBarModel>();
            ImportedToolBar2.ShouldBeInstanceOfType<ToolBarModel>();

            ImportedToolBar1.ShouldNotBe(ImportedToolBar2);
        }

        [TestMethod]
        public void ShouldNotHaveParentByDefault()
        {
            toolbar.Parent.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldNowHaveToolByDefault()
        {
            toolbar.Tools.IsEmpty().ShouldBe(true);
        }

        [TestMethod]
        public void ShouldThrowIfToolNotSpecified()
        {
            Should.Throw<ArgumentNullException>(() => toolbar.Add((ITool)null));
        }

        [TestMethod]
        public void ShouldAddToolToCollection()
        {
            var tool = new MockTool();
            toolbar.Add(tool);
            toolbar.Tools.ShouldContain(tool);
        }

        [TestMethod]
        public void ShouldAutoIncrementColumnWhenNullPassed()
        {
            var tool1 = new MockTool();
            var tool2 = new MockTool();
            toolbar.Add(tool1);
            toolbar.Add(tool2);

            toolbar.GetColumn(tool1).ShouldBe(0);
            toolbar.GetColumn(tool2).ShouldBe(1);
        }

        [TestMethod]
        public void ShouldAutoSetRowToZero()
        {
            var tool = new MockTool();
            toolbar.Add(tool);
            toolbar.GetRow(tool).ShouldBe(0);
        }

        [TestMethod]
        public void ShouldGetColumnAndRowDetailsForAddedTool()
        {
            var tool = new MockTool();
            toolbar.Add(tool, 1, 2, 3, 4);

            toolbar.GetColumn(tool).ShouldBe(1);
            toolbar.GetRow(tool).ShouldBe(2);
            toolbar.GetColumnSpan(tool).ShouldBe(3);
            toolbar.GetRowSpan(tool).ShouldBe(4);
        }

        [TestMethod]
        public void ShouldNotAllowSpanValuesToBeLessThanOne()
        {
            var tool = new MockTool();
            Should.Throw<ArgumentOutOfRangeException>(() => toolbar.Add(tool, columnSpan: 0));
            Should.Throw<ArgumentOutOfRangeException>(() => toolbar.Add(tool, rowSpan: 0));
        }
        #endregion
    }
}
