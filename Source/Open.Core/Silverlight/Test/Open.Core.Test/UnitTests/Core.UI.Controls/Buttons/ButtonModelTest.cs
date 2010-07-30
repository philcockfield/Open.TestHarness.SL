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
        }

        [TestMethod]
        public void ShouldBeEnabledByDefault()
        {
            button.IsEnabled.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldBeVisibleByDefault()
        {
            button.IsVisible.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldUpdateCommandEnabledState()
        {
            var model = (ButtonModel)button;

            model.IsEnabled.ShouldBe(true);
            model.Command.CanExecute(null).ShouldBe(true);

            model.IsEnabled = false;
            model.Command.CanExecute(null).ShouldBe(false);
        }

        [TestMethod]
        public void ShouldFireCommandExecuteChanged()
        {
            var model = (ButtonModel)button;

            var count = 0;
            model.Command.CanExecuteChanged += delegate { count++; };

            button.IsEnabled = false;
            count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldFireClickEventViaCommand()
        {
            var model = (ButtonModel)button;

            var count = 0;
            button.Click += delegate { count++; };

            model.Command.Execute(null);
            count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldFireClickEventViaInvokeMethod()
        {
            var count = 0;
            button.Click += delegate { count++; };

            button.InvokeClick();
            count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotFireClickEventViaInvokeMethodWhenDisabled()
        {
            var count = 0;
            button.Click += delegate { count++; };

            button.IsEnabled = false;
            button.InvokeClick(force:false);
            count.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldFireClickEventViaInvokeMethodWhenDisabledButForced()
        {
            var count = 0;
            button.Click += delegate { count++; };

            button.IsEnabled = false;
            button.InvokeClick(force: true);
            count.ShouldBe(1);
        }


        [TestMethod]
        public void ShouldNotFireClickEvent()
        {
            var count = 0;
            button.Click += delegate { count++; };

            button.IsEnabled = false;
            button.InvokeClick();

            count.ShouldBe(0);
        }
        #endregion
    }
}
