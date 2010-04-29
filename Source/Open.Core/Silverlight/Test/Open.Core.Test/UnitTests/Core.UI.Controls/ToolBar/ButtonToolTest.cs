using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.Composition;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.UI.Controls;
using Open.Core.Common.Testing;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.ToolBar
{
    [Tag("current")]
    [TestClass]
    public class ButtonToolTest : SilverlightUnitTest
    {
        #region Head
        [Import(typeof(IButtonTool))]
        public ExportFactory<IButtonTool> ToolCreator { get; set; }

        private IButtonTool tool;
        private ButtonToolViewModel viewModel;


        [TestInitialize]
        public void TestSetup()
        {
            CompositionInitializer.SatisfyImports(this);
            tool = ToolCreator.CreateExport().Value;
            viewModel = tool.CreateView().DataContext as ButtonToolViewModel;
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldCreateToolFromExportFactory()
        {
            ToolCreator.CreateExport().Value.ShouldBeInstanceOfType<ButtonTool>();
        }

        [TestMethod]
        public void ShouldHaveDefaultValues()
        {
            tool.Orientation.ShouldBe(Orientation.Horizontal);
            tool.Styles.ShouldBeInstanceOfType<ButtonToolStyles>();
            tool.ButtonType.ShouldBe(ButtonToolType.Default);
            tool.IsDefaultBackgroundVisible.ShouldBe(false);
            tool.MouseState.ShouldBe(ButtonMouseState.Default);
        }

        [TestMethod]
        public void StylesShouldHaveDataTemplateValues()
        {
            tool.Styles.ShouldHaveValuesForAllProperties<DataTemplate>();
        }

        [TestMethod]
        public void ShouldHaveButtonToolTemplatesRegistered()
        {
            Templates.Instance.Dictionary["ButtonTool.Background.Default"].ShouldBeInstanceOfType<DataTemplate>();
        }

        [TestMethod]
        public void ShouldReturnDefaultStylesIfNullSet()
        {
            tool.Styles = null;
            tool.Styles.ShouldBeInstanceOfType<ButtonToolStyles>();
        }

        [TestMethod]
        public void ShouldCreateView()
        {
            var view = tool.CreateView();
            view.ShouldBeInstanceOfType<ButtonToolView>();
            view.DataContext.ShouldBeInstanceOfType<ButtonToolViewModel>();
        }

        [TestMethod]
        public void ShouldUpdateMouseOverFlags()
        {
            // Default.
            tool.MouseState.ShouldBe(ButtonMouseState.Default);
            tool.IsMouseOver.ShouldBe(false);
            tool.IsMouseDown.ShouldBe(false);

            // Over
            tool.MouseState = ButtonMouseState.MouseOver;
            tool.IsMouseOver.ShouldBe(true);
            tool.IsMouseDown.ShouldBe(false);

            // Press
            tool.MouseState = ButtonMouseState.Pressed;
            tool.IsMouseOver.ShouldBe(true);
            tool.IsMouseDown.ShouldBe(true);

            // Release
            tool.MouseState = ButtonMouseState.MouseOver;
            tool.IsMouseOver.ShouldBe(true);
            tool.IsMouseDown.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldFireMouseOverFlagChangeEvents()
        {
            tool.ShouldFirePropertyChanged<IButtonTool>(
                            () => tool.MouseState = ButtonMouseState.MouseOver, 
                            m => m.IsMouseOver,
                            m => m.IsMouseDown);
        }

        [TestMethod]
        public void ShouldNotHaveBackgroundStyleWhenIsDefaultBackgroundIsVisibleEqualsFalse()
        {
            tool.MouseState.ShouldBe(ButtonMouseState.Default);

            tool.IsDefaultBackgroundVisible.ShouldBe(false);
            viewModel.BackgroundTemplate.ShouldBe(null);

            tool.IsDefaultBackgroundVisible = true;
            viewModel.BackgroundTemplate.ShouldBeInstanceOfType<DataTemplate>();
        }

        [TestMethod]
        public void ShouldFireBackgroundTemplatedChangedEvent()
        {
            viewModel.ShouldFirePropertyChanged<ButtonToolViewModel>(
                            () => tool.IsDefaultBackgroundVisible = true,
                            m => m.BackgroundTemplate);
        }

        [TestMethod]
        public void ShouldFireEventFromEventBus()
        {
            EventBus.IsAsynchronous = false;
            EventBus.ShouldFire<IToolEvent>(() => ((ButtonTool)tool).FireClick());
        }
        #endregion
    }
}
