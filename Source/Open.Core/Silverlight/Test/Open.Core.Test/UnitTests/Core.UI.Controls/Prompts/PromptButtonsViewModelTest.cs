using System.ComponentModel.Composition;
using System.Reflection;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.Core.Common.Testing;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.Prompts
{
    [Tag("current")]
    [TestClass]
    public class PromptButtonsViewModelTest
    {
        #region Head
        [Import]
        public ExportFactory<IPromptButtons> Factory;
        private IPromptButtons viewModel;

        public PromptButtonsViewModelTest()
        {
            CompositionInitializer.SatisfyImports(this);
        }

        [TestInitialize]
        public void TestSetup()
        {
            viewModel = Factory.CreateExport().Value;
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldHaveButtons()
        {
            viewModel.ShouldHaveValuesForAllProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        [TestMethod]
        public void ShouldGetSpecificButton()
        {
            viewModel.GetButton(PromptResult.Accept).ShouldBe(viewModel.AcceptButton);
            viewModel.GetButton(PromptResult.Cancel).ShouldBe(viewModel.CancelButton);
            viewModel.GetButton(PromptResult.Decline).ShouldBe(viewModel.DeclineButton);
        }

        [TestMethod]
        public void ShouldGetButtonFromAllEnumValues()
        {
            foreach (PromptResult buttonFlag in typeof(PromptResult).GetEnumValues())
            {
                viewModel.GetButton(buttonFlag).ShouldNotBe(null);
            }
        }

        [TestMethod]
        public void ShouldCreateView()
        {
            var view = viewModel.CreateView();
            view.ShouldBeInstanceOfType<PromptButtons>();
            view.DataContext.ShouldBe(viewModel);
        }
        #endregion
    }
}
