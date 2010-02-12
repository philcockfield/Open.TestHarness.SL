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

using T = Open.Core.UI.Controls.TextButton;

namespace Open.Core.UI.Controls
{
    /// <summary>Represents a simple button, rendered as text.</summary>
    public class TextButton : Button
    {
        #region Head
        private static readonly Brush enabledColor = new SolidColorBrush(Color.FromArgb(255, 11, 102, 165));
        private static readonly Brush disabledColor = Colors.Black.ToBrush(1);

        private Grid root;
        private TextBlock textBlock;
        private Border focusBorder;
        private Image leftIcon;
        private Image rightIcon;
        private bool isInitialized;

        /// <summary>Constructor.</summary>
        public TextButton()
        {
            // Setup initial conditions.
            ButtonTemplates.Instance.ApplyTemplate(this);

            // Set default values.
            Foreground = enabledColor;

            // Wire up events.
            IsEnabledChanged += delegate { UpdateColor(); };
        }

        public override void OnApplyTemplate()
        {
            // Setup initial conditions.
            base.OnApplyTemplate();

            // Retrieve elements.
            root = GetTemplateChild("root") as Grid;
            textBlock = GetTemplateChild("textBlock") as TextBlock;
            focusBorder = GetTemplateChild("focusBorder") as Border;
            leftIcon = GetTemplateChild("leftIcon") as Image;
            rightIcon = GetTemplateChild("rightIcon") as Image;
            if (root == null || textBlock == null || focusBorder == null || leftIcon == null || rightIcon == null) throw new TemplateNotSetException();

            // Finish up.
            isInitialized = true;
            UpdateVisualState();
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the display Text of the button (this is passed to the Content property).</summary>
        public string Text
        {
            get { return (string) (GetValue(TextProperty)); }
            set { SetValue(TextProperty, value); }
        }
        /// <summary>Gets or sets the display Text of the button (this is passed to the Content property).</summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.Text),
                typeof (string),
                typeof (TextButton),
                new PropertyMetadata(null));


        /// <summary>Gets or sets the color of the text when the button is disabled.</summary>
        public Brush DisabledColor
        {
            get { return (Brush) (GetValue(DisabledColorProperty)); }
            set { SetValue(DisabledColorProperty, value); }
        }
        /// <summary>Gets or sets the color of the text when the button is disabled.</summary>
        public static readonly DependencyProperty DisabledColorProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.DisabledColor),
                typeof (Brush),
                typeof (T),
                new PropertyMetadata(disabledColor));


        /// <summary>Gets or sets the opacity of the button when disabled.</summary>
        public double DisabledOpacity
        {
            get { return (double)(GetValue(DisabledOpacityProperty)); }
            set { SetValue(DisabledOpacityProperty, value); }
        }
        /// <summary>Gets or sets the opacity of the button when disabled.</summary>
        public static readonly DependencyProperty DisabledOpacityProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.DisabledOpacity),
                typeof(double),
                typeof(T),
                new PropertyMetadata(0.3, (s, e) => ((T)s).UpdateColor()));


        /// <summary>Gets or sets the left hand icon.</summary>
        public ImageSource LeftIcon
        {
            get { return (ImageSource) (GetValue(LeftIconProperty)); }
            set { SetValue(LeftIconProperty, value); }
        }
        /// <summary>Gets or sets the left hand icon.</summary>
        public static readonly DependencyProperty LeftIconProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.LeftIcon),
                typeof (ImageSource),
                typeof (T),
                new PropertyMetadata(null, (s, e) => ((T)s).UpdateIconVisibility()));
        

        /// <summary>Gets or sets the right hand icon.</summary>
        public ImageSource RightIcon
        {
            get { return (ImageSource) (GetValue(RightIconProperty)); }
            set { SetValue(RightIconProperty, value); }
        }
        /// <summary>Gets or sets the right hand icon.</summary>
        public static readonly DependencyProperty RightIconProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.RightIcon),
                typeof (ImageSource),
                typeof (T),
                new PropertyMetadata(null, (s, e) => ((T)s).UpdateIconVisibility()));


        /// <summary>Gets or sets the margin around the left hand icon.</summary>
        public Thickness LeftIconMargin
        {
            get { return (Thickness) (GetValue(LeftIconMarginProperty)); }
            set { SetValue(LeftIconMarginProperty, value); }
        }
        /// <summary>Gets or sets the margin around the left hand icon.</summary>
        public static readonly DependencyProperty LeftIconMarginProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.LeftIconMargin),
                typeof (Thickness),
                typeof (T),
                new PropertyMetadata(new Thickness(0, 0, 3, 0), (s, e) => ((T)s).UpdateIconVisibility()));
        

        /// <summary>Gets or sets the margin around the right hand icon.</summary>
        public Thickness RightIconMargin
        {
            get { return (Thickness) (GetValue(RightIconMarginProperty)); }
            set { SetValue(RightIconMarginProperty, value); }
        }
        /// <summary>Gets or sets the margin around the right hand icon.</summary>
        public static readonly DependencyProperty RightIconMarginProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.RightIconMargin),
                typeof (Thickness),
                typeof (T),
                new PropertyMetadata(new Thickness(3,0,0,0), (s, e) => ((T) s).UpdateIconVisibility()));


        /// <summary>Gets or sets the trimming rule for the text.</summary>
        public TextTrimming TextTrimming
        {
            get { return (TextTrimming) (GetValue(TextTrimmingProperty)); }
            set { SetValue(TextTrimmingProperty, value); }
        }
        /// <summary>Gets or sets the trimming rule for the text.</summary>
        public static readonly DependencyProperty TextTrimmingProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.TextTrimming),
                typeof (TextTrimming),
                typeof (T),
                new PropertyMetadata(TextTrimming.WordEllipsis));


        /// <summary>Gets or sets the opacity of the underline that indicates when the TextButton has focus.</summary>
        public double FocusOpacity
        {
            get { return (double) (GetValue(FocusOpacityProperty)); }
            set { SetValue(FocusOpacityProperty, value); }
        }
        /// <summary>Gets or sets the opacity of the underline that indicates when the TextButton has focus.</summary>
        public static readonly DependencyProperty FocusOpacityProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.FocusOpacity),
                typeof (double),
                typeof (T),
                new PropertyMetadata(0.35d));
        
        #endregion

        #region Internal
        private void UpdateVisualState()
        {
            if (!isInitialized) return;
            UpdateColor();
            UpdateIconVisibility();
        }

        private void UpdateColor()
        {
            if (!isInitialized) return;
            var brush = IsEnabled ? Foreground : DisabledColor;
            textBlock.Foreground = brush;
            focusBorder.BorderBrush = brush;
            root.Opacity = IsEnabled ? 1 : DisabledOpacity.WithinBounds(0, 1);
        }

        private void UpdateIconVisibility()
        {
            if (!isInitialized) return;
            leftIcon.Visibility = LeftIcon == null ? Visibility.Collapsed : Visibility.Visible;
            rightIcon.Visibility = RightIcon == null ? Visibility.Collapsed : Visibility.Visible;
        }
        #endregion
    }
}
