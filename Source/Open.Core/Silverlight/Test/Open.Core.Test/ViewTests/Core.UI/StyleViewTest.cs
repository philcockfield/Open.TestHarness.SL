using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Open.Core.Common;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.UI
{
    [ViewTestClass]
    public class StyleViewTest
    {
        #region Head
        public enum GradientStyle
        {
            VerticalBlackToTransparent,
            VerticalTransparentToBlack,
            VerticalWhiteToTransparent,
            VerticalTransparentToWhite,

            HorizontalBlackToTransparent,
            HorizontalTransparentToBlack,
            HorizontalWhiteToTransparent,
            HorizontalTransparentToWhite,
        }

        private GradientStyle currentGradientStyle;
        private Border border;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(Grid control)
        {
            control.Width = 300;
            control.Height = 300;
            
            border = new Border();
            control.Children.Add(new Placeholder());
            control.Children.Add(border);

            Set__Gradient_Style(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Set__Gradient_Style(Grid control, GradientStyle style = GradientStyle.HorizontalBlackToTransparent)
        {
            var brush = StyleResources.Colors[ToKey(style)] as Brush;
            border.Background = brush;
            currentGradientStyle = style;
        }

        [ViewTest]
        public void Set__Next_Gradient_Style(Grid control)
        {
            Set__Gradient_Style(control, currentGradientStyle.NextValue<GradientStyle>());
        }
        #endregion

        #region Internal
        private static string ToKey(GradientStyle style)
        {
            switch (style)
            {
                case GradientStyle.VerticalBlackToTransparent: return "Gradient.Vertical.Black-Transparent";
                case GradientStyle.VerticalTransparentToBlack: return "Gradient.Vertical.Transparent-Black";
                case GradientStyle.VerticalWhiteToTransparent: return "Gradient.Vertical.White-Transparent";
                case GradientStyle.VerticalTransparentToWhite: return "Gradient.Vertical.Transparent-White";
                case GradientStyle.HorizontalBlackToTransparent: return "Gradient.Horizontal.Black-Transparent";
                case GradientStyle.HorizontalTransparentToBlack: return "Gradient.Horizontal.Transparent-Black";
                case GradientStyle.HorizontalWhiteToTransparent: return "Gradient.Horizontal.White-Transparent";
                case GradientStyle.HorizontalTransparentToWhite: return "Gradient.Horizontal.Transparent-White";
            }
            return null;
        }
        #endregion
    }
}

