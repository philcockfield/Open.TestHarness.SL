using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.UI;
using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.Buttons
{
    [Tag("button")]
    [TestClass]
    public class ButtonModelTest : SilverlightUnitTest
    {
        #region Head
        [Import]
        public ExportFactory<IButton> Factory;
        private IButton button;

        public ButtonModelTest()
        {
            CompositionInitializer.SatisfyImports(this);
        }

        [TestInitialize]
        public void TestSetup()
        {
            button = Factory.CreateExport().Value;
            button.ShouldNotBe(null);
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldCreateView()
        {
            var view = button.CreateView();
            view.ShouldBeInstanceOfType<ContentControl>();
            view.DataContext.ShouldBe(button);

            var control = view as ContentControl;
            control.ContentTemplate.ShouldBe(button.Template);
        }

        [TestMethod]
        public void ShouldBeEnabledByDefault()
        {
            button.IsEnabled.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldUpdateCommandEnabledState()
        {
            button.IsEnabled.ShouldBe(true);
            button.Command.CanExecute(null).ShouldBe(true);

            button.IsEnabled = false;
            button.Command.CanExecute(null).ShouldBe(false);
        }

        [TestMethod]
        public void ShouldFireCommandExecuteChanged()
        {
            var count = 0;
            button.Command.CanExecuteChanged += delegate { count++; };

            button.IsEnabled = false;
            count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldHaveDefaultTemplateWhenNull()
        {
            button.Template = null;
            button.Template.ShouldBe(ButtonTemplates.ButtonModelDefault);
        }

        [TestMethod]
        public void ShouldFireClickEvent()
        {
            var count = 0;
            button.Click += delegate { count++; };
            
            button.Command.Execute(null);
            count.ShouldBe(1);
        }
        #endregion
    }
}
