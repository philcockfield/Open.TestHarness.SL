using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.UI;
using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Models
{
    [TestClass]
    public class CoreImageViewModelTest
    {
        #region Head
        [Import]
        public ExportFactory<IImage> ImageFactory { get; set; }
        private IImage image;

        public CoreImageViewModelTest()
        {
            CompositionInitializer.SatisfyImports(this);
        }

        [TestInitialize]
        public void TestSetup()
        {
            image = ImageFactory.CreateExport().Value;
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldHaveZeroOpacityShadowByDefault()
        {
            image.DropShadow.Opacity.ShouldBe(0d);
        }

        [TestMethod]
        public void ShouldNotStretchByDefault()
        {
            image.Stretch.ShouldBe(Stretch.None);
        }

        [TestMethod]
        public void ShouldHaveNoOffsetByDefault()
        {
            image.Margin.ShouldBe(new Thickness(0));
        }

        [TestMethod]
        public void ShouldCreateView()
        {
            var control = image.CreateView();
            control.ShouldBeInstanceOfType<CoreImage>();
            control.DataContext.ShouldBeInstanceOfType<CoreImageViewModel>();
        }

        [TestMethod]
        public void ShouldBeVisibleByDefault()
        {
            image.IsVisible.ShouldBe(true);
        }
        #endregion
    }
}
