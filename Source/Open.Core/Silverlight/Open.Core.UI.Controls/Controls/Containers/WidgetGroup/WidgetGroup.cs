//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>A visual container of a set of logically related controls.</summary>
    public class WidgetGroup : ContentControl
    {
        #region Head
        public const string PropCornerRadius = "CornerRadius";
        public const string PropDropShadowOpacity = "DropShadowOpacity";
        public const string PropDropShadowHeight = "DropShadowHeight";

        public const string PropTitle = "Title";
        public const string PropIsTitleVisible = "IsTitleVisible";
        public const string PropTitleHeight = "TitleHeight";
        public const string PropTitleOpacity = "TitleOpacity";
        public const string PropTitleBackground = "TitleBackground";
        public const string PropTitleBorderBrush = "TitleBorderBrush";
        public const string PropTitleTemplate = "TitleTemplate";

        private Grid titleGrid;
        private ContentPresenter content;

        public WidgetGroup()
        {
            // Setup initial conditions.
            Templates.Instance.ApplyTemplate(this);

            // Set default values.
            Background = StyleResources.Colors["Brush.Black.002"] as Brush;
            BorderBrush = StyleResources.Colors["Brush.Black.020"] as Brush;
        }

        public override void OnApplyTemplate()
        {
            // Setup initial conditions.
            base.OnApplyTemplate();

            // Retrieve elements.
            var root = GetTemplateChild("root") as Grid;
            content = GetTemplateChild("content") as ContentPresenter;
            titleGrid = GetTemplateChild("titleGrid") as Grid;
            if (root == null || content == null || titleGrid == null) throw new TemplateNotSetException();
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the radius of the rounded corners.</summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius) (GetValue(CornerRadiusProperty)); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        /// <summary>Gets or sets the radius of the rounded corners.</summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(
                PropCornerRadius,
                typeof (CornerRadius),
                typeof (WidgetGroup),
                new PropertyMetadata(new CornerRadius(4)));
        #endregion

        #region Dependency Properties - Drop Shadow
        /// <summary>Gets or sets the opacity of the title drop-shadow.</summary>
        public double DropShadowOpacity
        {
            get { return (double) (GetValue(DropShadowOpacityProperty)); }
            set { SetValue(DropShadowOpacityProperty, value); }
        }
        /// <summary>Gets or sets the opacity of the title drop-shadow.</summary>
        public static readonly DependencyProperty DropShadowOpacityProperty =
            DependencyProperty.Register(
                PropDropShadowOpacity,
                typeof (double),
                typeof (WidgetGroup),
                new PropertyMetadata(0.15d));
        

        /// <summary>Gets or sets the height of the title drop-shadow.</summary>
        public double DropShadowHeight
        {
            get { return (double) (GetValue(DropShadowHeightProperty)); }
            set { SetValue(DropShadowHeightProperty, value); }
        }
        /// <summary>Gets or sets the height of the title drop-shadow.</summary>
        public static readonly DependencyProperty DropShadowHeightProperty =
            DependencyProperty.Register(
                PropDropShadowHeight,
                typeof (double),
                typeof (WidgetGroup),
                new PropertyMetadata(7d));
        #endregion

        #region Dependency Properties - Title
        /// <summary>Gets or sets the title content (typically a View-Model).</summary>
        /// <remarks>Using the default 'TitleTemplate' simple text can be passed to this property.</remarks>
        public object Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        /// <summary>Gets or sets the title content (typically a View-Model).</summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                PropTitle,
                typeof (object),
                typeof (WidgetGroup),
                new PropertyMetadata(null));
        
        /// <summary>Gets or sets the template to use to render the title content.</summary>
        public ControlTemplate TitleTemplate
        {
            get { return (ControlTemplate) (GetValue(TitleTemplateProperty)); }
            set { SetValue(TitleTemplateProperty, value); }
        }
        /// <summary>Gets or sets the template to use to render the title content.</summary>
        public static readonly DependencyProperty TitleTemplateProperty =
            DependencyProperty.Register(
                PropTitleTemplate,
                typeof (ControlTemplate),
                typeof (WidgetGroup),
                new PropertyMetadata(Templates.Instance.Dictionary["DefaultWidgetGroupTitle"] as ControlTemplate));


        /// <summary>Gets or sets whether the title is visible.</summary>
        public bool IsTitleVisible
        {
            get { return (bool) (GetValue(IsTitleVisibleProperty)); }
            set { SetValue(IsTitleVisibleProperty, value); }
        }
        /// <summary>Gets or sets whether the title is visible.</summary>
        public static readonly DependencyProperty IsTitleVisibleProperty =
            DependencyProperty.Register(
                PropIsTitleVisible,
                typeof (bool),
                typeof (WidgetGroup),
                new PropertyMetadata(true, (s, e) => ((WidgetGroup)s).UpdateState()));


        /// <summary>Gets or sets the height of the title.</summary>
        public double TitleHeight
        {
            get { return (double) (GetValue(TitleHeightProperty)); }
            set { SetValue(TitleHeightProperty, value); }
        }
        /// <summary>Gets or sets the height of the title.</summary>
        public static readonly DependencyProperty TitleHeightProperty =
            DependencyProperty.Register(
                PropTitleHeight,
                typeof (double),
                typeof (WidgetGroup),
                new PropertyMetadata(26d));


        /// <summary>Gets or sets the opacity of the title content.</summary>
        public double TitleOpacity
        {
            get { return (double) (GetValue(TitleOpacityProperty)); }
            set { SetValue(TitleOpacityProperty, value); }
        }
        /// <summary>Gets or sets the opacity of the title content.</summary>
        public static readonly DependencyProperty TitleOpacityProperty =
            DependencyProperty.Register(
                PropTitleOpacity,
                typeof (double),
                typeof (WidgetGroup),
                new PropertyMetadata(1d));

        
        /// <summary>Gets or sets the background color of the title section.</summary>
        public Brush TitleBackground
        {
            get { return (Brush) (GetValue(TitleBackgroundProperty)); }
            set { SetValue(TitleBackgroundProperty, value); }
        }
        /// <summary>Gets or sets the background color of the title section.</summary>
        public static readonly DependencyProperty TitleBackgroundProperty =
            DependencyProperty.Register(
                PropTitleBackground,
                typeof (Brush),
                typeof (WidgetGroup),
                new PropertyMetadata(StyleResources.Colors["Brush.Black.003"] as Brush));

        
        /// <summary>Gets or sets the color of the underline border of the title section.</summary>
        public Brush TitleBorderBrush
        {
            get { return (Brush) (GetValue(TitleBorderBrushProperty)); }
            set { SetValue(TitleBorderBrushProperty, value); }
        }
        /// <summary>Gets or sets the color of the underline border of the title section.</summary>
        public static readonly DependencyProperty TitleBorderBrushProperty =
            DependencyProperty.Register(
                PropTitleBorderBrush,
                typeof (Brush),
                typeof (WidgetGroup),
                new PropertyMetadata(StyleResources.Colors["Brush.Black.010"] as Brush));
        #endregion

        #region Internal
        private void UpdateState()
        {
            if (content == null) return;
            if (content.DataContext != DataContext) content.DataContext = DataContext;
            titleGrid.Visibility = IsTitleVisible ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}
