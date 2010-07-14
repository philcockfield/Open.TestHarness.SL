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
        private ToolBarViewModel viewModel;

        [TestInitialize]
        public void TestSetup()
        {
            viewModel = new ToolBarViewModel(); ;
            toolbar = viewModel;
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

            ImportedToolBar1.ShouldBeInstanceOfType<ToolBarViewModel>();
            ImportedToolBar2.ShouldBeInstanceOfType<ToolBarViewModel>();

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
        public void ShouldNotHaveHeightByDefault()
        {
            toolbar.Height.ShouldBe(double.NaN);
        }

        [TestMethod]
        public void ShouldNotShowDividersByDefault()
        {
            toolbar.Dividers.ShouldBe(RectEdgeFlag.None);
            viewModel.IsLeftDividerVisible.ShouldBe(false);
            viewModel.IsRightDividerVisible.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldShowLeftDivider()
        {
            viewModel.Dividers = RectEdgeFlag.Left;
            viewModel.IsLeftDividerVisible.ShouldBe(true);
            viewModel.IsRightDividerVisible.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldShowRightDivider()
        {
            viewModel.Dividers = RectEdgeFlag.Right;
            viewModel.IsLeftDividerVisible.ShouldBe(false);
            viewModel.IsRightDividerVisible.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldShowBothDividers()
        {
            viewModel.Dividers = RectEdgeFlag.Left | RectEdgeFlag.Right;
            viewModel.IsLeftDividerVisible.ShouldBe(true);
            viewModel.IsRightDividerVisible.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldFireDividerVisibilityProperties()
        {
            viewModel.ShouldFirePropertyChanged<ToolBarViewModel>(
                        () => toolbar.Dividers = RectEdgeFlag.Left, 
                        m => m.IsLeftDividerVisible, 
                        m => m.IsRightDividerVisible);
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


        public enum MyEnum { One, Two }
        [TestMethod]
        public void ShouldGetTool()
        {
            var tool = new MockTool { Id = MyEnum.One };
            toolbar.Add(tool);
            toolbar.GetTool(MyEnum.One).ShouldBe(tool);
            toolbar.GetTool(MyEnum.Two).ShouldBe(null);
        }

        [TestMethod]
        public void ShouldGetDeepChildTool()
        {
            var tool = new MockTool { Id = MyEnum.One };
            var toolGroup = toolbar.AddToolGroup();
            toolGroup.Add(tool);

            toolbar.GetTool(MyEnum.One, includeChildToolbars:true).ShouldBe(tool);
            toolbar.GetTool(MyEnum.Two, includeChildToolbars: true).ShouldBe(null);
        }

        [TestMethod]
        public void ShouldNotGetDeepChildTool()
        {
            var tool = new MockTool { Id = MyEnum.One };
            var toolGroup = toolbar.AddToolGroup();
            toolGroup.Add(tool);

            toolbar.GetTool(MyEnum.One, includeChildToolbars: false).ShouldBe(null);
            toolbar.GetTool(MyEnum.Two, includeChildToolbars: false).ShouldBe(null);
        }

        [TestMethod]
        public void ShouldGetTypedTool()
        {
            var buttonTool = new ButtonTool { Id = "MyButtonTool" };
            toolbar.Add<IButtonTool>(buttonTool);
            toolbar.GetTool<IButtonTool>("MyButtonTool").ShouldBe(buttonTool);
        }

        [TestMethod]
        public void ShouldNotGetTool()
        {
            var tool = new MockTool { Id = "MyId" };
            toolbar.Add(tool);
            toolbar.GetTool("MyId2").ShouldBe(null);

            toolbar.GetTool(null).ShouldBe(null);
            toolbar.GetTool<IButtonTool>(null).ShouldBe(null);
        }

        [TestMethod]
        public void ShouldReturnTitleByDefault()
        {
            var title = toolbar.Title;
            toolbar.Title = null;
            toolbar.Title.ShouldBe(title);

            var newTitle = new ToolBarTitleViewModel();
            toolbar.Title = newTitle;
            toolbar.Title.ShouldBe(newTitle);

            toolbar.Title = null;
            toolbar.Title.ShouldBe(title);
        }

        [TestMethod]
        public void TitleShouldNotBeVisibleByDefault()
        {
            new ToolBarTitleViewModel().IsVisible.ShouldBe(true);
            toolbar.Title.IsVisible.ShouldBe(false);
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
