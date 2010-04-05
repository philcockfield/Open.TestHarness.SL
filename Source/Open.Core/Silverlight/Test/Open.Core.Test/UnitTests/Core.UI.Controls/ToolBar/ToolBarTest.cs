using System;
using System.Linq;
using System.ComponentModel.Composition;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.ToolBar
{
    [TestClass]
    public class ToolBarTest
    {
        #region Head
        private IToolBar toolbar;

        [TestInitialize]
        public void TestSetup()
        {
            toolbar = new Open.Core.UI.Controls.ToolBar();
        }
        #endregion
        
        #region Tests
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        public IToolBar ImportedToolBar1 { get; set; }

        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        public IToolBar ImportedToolBar2 { get; set; }

        [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
        public IToolBar ImportedToolBar3 { get; set; }

        [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
        public IToolBar ImportedToolBar4 { get; set; }

        [TestMethod]
        public void ShouldImportUniqueInstances()
        {
            CompositionInitializer.SatisfyImports(this);

            ImportedToolBar1.ShouldBeInstanceOfType<Open.Core.UI.Controls.ToolBar>();
            ImportedToolBar2.ShouldBeInstanceOfType<Open.Core.UI.Controls.ToolBar>();

            ImportedToolBar1.ShouldNotBe(ImportedToolBar2);
        }

        [TestMethod]
        public void ShouldImportSingletonInstanceOfToolBar()
        {
            CompositionInitializer.SatisfyImports(this);
            ImportedToolBar3.ShouldBe(ImportedToolBar4);
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
        public void ShouldAssignToolBarAsParentWhenAdded()
        {
            var tool = new MockTool();
            tool.Parent.ShouldBe(null);

            toolbar.Add(tool);
            tool.Parent.ShouldBe(toolbar);
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

        [TestMethod]
        public void ShouldFireUpdateLayoutRequest()
        {
            FireUpdateLayoutCount(() => toolbar.UpdateLayout()).ShouldBe(1);
        }

        [TestMethod]
        public void ShouldClear()
        {
            toolbar.Add(new MockTool());
            toolbar.Add(new MockTool());
            toolbar.Add(new MockTool());
            toolbar.Tools.Count().ShouldBe(3);
            
            toolbar.Clear();
            toolbar.Tools.Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldFireUpdateLayoutOnClear()
        {
            toolbar.Add(new MockTool());
            FireUpdateLayoutCount(() => toolbar.Clear()).ShouldBe(1);
            FireUpdateLayoutCount(() => toolbar.Clear()).ShouldBe(0);
            FireUpdateLayoutCount(() => toolbar.Clear()).ShouldBe(0);
        }
        #endregion

        #region Internal
        private int FireUpdateLayoutCount(Action action)
        {
            var fireCount = 0;
            toolbar.UpdateLayoutRequest += delegate { fireCount++; };
            action();
            return fireCount;
        }
        #endregion
    }
}
