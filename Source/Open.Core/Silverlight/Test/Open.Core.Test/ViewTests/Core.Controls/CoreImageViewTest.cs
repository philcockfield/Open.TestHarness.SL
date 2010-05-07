using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.UI;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.Controls
{
    [ViewTestClass]
    public class CoreImageViewTest
    {
        #region Head
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        public IImage Image { get; set; }


        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ViewFactoryContent control)
        {
            CompositionInitializer.SatisfyImports(this);
            control.ViewFactory = Image;

            Set__Source(control);
            Toggle__DropShadow(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Set__Source(ViewFactoryContent control)
        {
            Image.Source = IconImage.SilkAccept.ToImageSource();
        }

        [ViewTest]
        public void Set_Next__Stretch(ViewFactoryContent control)
        {
            Image.Stretch = Image.Stretch.NextValue<Stretch>();
        }

        [ViewTest]
        public void Toggle__DropShadow(ViewFactoryContent control)
        {
            Image.DropShadow.Opacity = Image.DropShadow.Opacity == 0 ? 0.3 : 0;
            Output.Write("DropShadow: " + Image.DropShadow);
        }

        [ViewTest]
        public void Toggle__Margin(ViewFactoryContent control)
        {
            Image.Margin = Image.Margin.Left == 0 ? new Thickness(10, 10, 0, 0) : new Thickness(0);
            Output.Write("Margin: " + Image.Margin);
        }

        [ViewTest]
        public void Toggle__IsVisible(ViewFactoryContent control)
        {
            Image.IsVisible = !Image.IsVisible;
        }
        #endregion
    }
}
