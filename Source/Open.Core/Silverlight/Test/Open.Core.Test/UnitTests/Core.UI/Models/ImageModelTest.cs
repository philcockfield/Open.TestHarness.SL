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
using Open.Core.Common.Testing;
using Open.Core.UI;
using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Models
{
    [Tag("current")]
    [TestClass]
    public class ImageModelTest
    {
        #region Head
        [Import]
        public ExportFactory<IImage> ImageFactory { get; set; }
        private IImage image;

        public ImageModelTest()
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
        public void ShouldCreateView()
        {
            var control = image.CreateView();
            control.ShouldBeInstanceOfType<CoreImage>();
            control.DataContext.ShouldBeInstanceOfType<CoreImageViewModel>();
        }
        #endregion
    }
}
