using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.ToolBar
{
    [TestClass]
    public class ToolDividerTest : SilverlightUnitTest
    {
        #region Head
        [Import(typeof(IToolDivider))]
        public ExportFactory<IToolDivider> ToolCreator { get; set; }

        private IToolDivider divider;


        [TestInitialize]
        public void TestSetup()
        {
            CompositionInitializer.SatisfyImports(this);
            divider = ToolCreator.CreateExport().Value;
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldCreateToolFromExportFactory()
        {
            ToolCreator.CreateExport().Value.ShouldBeInstanceOfType<ToolDivider>();
        }

        [TestMethod]
        public void ShouldCreateView()
        {
            var view = divider.CreateView();
            view.ShouldBeInstanceOfType<ToolDividerView>();
            view.DataContext.ShouldBeInstanceOfType<ToolDivider>();
        }

        [TestMethod]
        public void ShouldHaveDefaultValues()
        {
            divider.HorizontalAlignment.ShouldBe(HorizontalAlignment.Left);
            divider.VerticalAlignment.ShouldBe(VerticalAlignment.Stretch);
        }

        [TestMethod]
        public void ShouldHaveTemplate()
        {
            var defaultTemplate = Templates.Instance.Dictionary["ToolDivider.Default"];
            defaultTemplate.ShouldBeInstanceOfType<DataTemplate>();
            divider.Template.ShouldBe(defaultTemplate);
        }
        #endregion
    }
}
