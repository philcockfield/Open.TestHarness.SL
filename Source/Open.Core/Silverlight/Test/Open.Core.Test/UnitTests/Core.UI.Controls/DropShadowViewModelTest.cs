using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.UI.Controls;
using Open.Core.Common.Testing;

namespace Open.Core.Test.UnitTests.Core.UI.Controls
{
    [TestClass]
    public class DropShadowViewModelTest
    {
        #region Head
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        public IDropShadow DropShadow { get; set; }
        public DropShadowViewModel ViewModel { get; set; }

        [TestInitialize]
        public void TestSetup()
        {
            ViewModel = new DropShadowViewModel();
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldImport()
        {
            CompositionInitializer.SatisfyImports(this);
            DropShadow.ShouldBeInstanceOfType<DropShadowViewModel>();
        }

        [TestMethod]
        public void ShouldKeepOpacityWithinBounds()
        {
            ViewModel.Opacity = -1;
            ViewModel.Opacity.ShouldBe(0d);

            ViewModel.Opacity = 2;
            ViewModel.Opacity.ShouldBe(1d);
        }

        [TestMethod]
        public void ShouldKeepSizeWithinBounds()
        {
            ViewModel.Size = -1;
            ViewModel.Size.ShouldBe(double.NaN);

            ViewModel.Size = 0;
            ViewModel.Size.ShouldBe(double.NaN);
        }

        [TestMethod]
        public void ShouldHaveDefaultValues()
        {
            ViewModel.Size.ShouldBe(15d);
            ViewModel.Opacity.ShouldBe(0.15);
            ViewModel.Color.ShouldBe(Colors.Black);
            ViewModel.Direction.ShouldBe(Direction.Down);
        }

        [TestMethod]
        public void ShouldDetermineVisibility()
        {
            ViewModel.Visibility.ShouldBe(Visibility.Visible);

            ViewModel.Opacity = 0;
            ViewModel.Visibility.ShouldBe(Visibility.Collapsed);

            ViewModel.Opacity = 1;
            ViewModel.Size = 0;
            ViewModel.Visibility.ShouldBe(Visibility.Collapsed);

            ViewModel.Size = 1;
            ViewModel.Visibility.ShouldBe(Visibility.Visible);
        }

        [TestMethod]
        public void ControlShouldAutoCreateModel()
        {
            var control = new DropShadow();
            control.ViewModel.ShouldBeInstanceOfType<DropShadowViewModel>();
            control.DataContext.ShouldBe(control.ViewModel);
        }

        [TestMethod]
        public void ShouldTranslateSizeIntoWidthOrHeight()
        {
            ViewModel.Size = 30;

            ViewModel.Direction = Direction.Up;
            ViewModel.Width.ShouldBe(double.NaN);
            ViewModel.Height.ShouldBe(30d);

            ViewModel.Direction = Direction.Down;
            ViewModel.Width.ShouldBe(double.NaN);
            ViewModel.Height.ShouldBe(30d);

            // ---

            ViewModel.Direction = Direction.Left;
            ViewModel.Width.ShouldBe(30d);
            ViewModel.Height.ShouldBe(double.NaN);

            ViewModel.Direction = Direction.Right;
            ViewModel.Width.ShouldBe(30d);
            ViewModel.Height.ShouldBe(double.NaN);
        }
        #endregion
    }
}
