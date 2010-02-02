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
using System.Windows.Media.Animation;
using Open.Core.Common;
using Open.Core.UI.Common;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Controls
{
    /// <summary>A set of collapsable child data under a title.</summary>
    public partial class TitleContainer : UserControl
    {
        #region Head
        public const string PropTitle = "Title";
        public const string PropTitleTemplate = "TitleTemplate";
        public const string PropTitleOpacity = "TitleOpacity";
        public const string PropIconTemplate = "IconTemplate";
        public const string PropChild = "Child";
        public const string PropIsOpen = "IsOpen";
        public const string PropAnimationDuration = "AnimationDuration";
        public const string PropAnimateIcon = "AnimateIcon";
        public const string PropEasing = "Easing";
        public const string PropToggleIsOpenOn = "ToggleIsOpenOn";

        public TitleContainer()
        {
            // Setup initial conditions.
            InitializeComponent();

            // Set default values.
            TitleTemplate = Templates.Instance.Dictionary["DefaultTitle"] as ControlTemplate;
            IconTemplate = Templates.Instance.Dictionary["DefaultIcon"] as ControlTemplate;

            // Wire up events.
            icon.MouseLeftButtonUp += delegate { ToggleIsOpen(); };
            title.MouseLeftButtonUp += delegate { if (ToggleIsOpenOn == ClickGesture.SingleClick) ToggleIsOpen(); };
            new DoubleClickMonitor(title, () => { if (ToggleIsOpenOn == ClickGesture.DoubleClick) ToggleIsOpen(); }); 

            // Finish up.
            HandleAnimateIcon();
        }
        #endregion

        #region Event Handlers
        private static void HandleIsOpenedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((TitleContainer)o).AnimateIsOpen();
        }

        private static void HandleAnimateIcon(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((TitleContainer) o).HandleAnimateIcon();
        }

        private void HandleAnimateIcon()
        {
            if ((icon.RenderTransform as RotateTransform) == null) icon.RenderTransform = new RotateTransform { Angle = GetIconAngle() };
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the title content.</summary>
        public object Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        /// <summary>Gets or sets the title content.</summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                PropTitle,
                typeof(object),
                typeof(TitleContainer),
                new PropertyMetadata(null));

        /// <summary>Gets or sets the template of the title.</summary>
        public ControlTemplate TitleTemplate
        {
            get { return (ControlTemplate)(GetValue(TitleTemplateProperty)); }
            set { SetValue(TitleTemplateProperty, value); }
        }
        /// <summary>Gets or sets the template of the title.</summary>
        public static readonly DependencyProperty TitleTemplateProperty =
            DependencyProperty.Register(
                PropTitleTemplate,
                typeof(ControlTemplate),
                typeof (TitleContainer),
                new PropertyMetadata(null));

        /// <summary>Gets or sets the opacity of the title.</summary>
        public double TitleOpacity
        {
            get { return (double) (GetValue(TitleOpacityProperty)); }
            set { SetValue(TitleOpacityProperty, value); }
        }
        /// <summary>Gets or sets the opacity of the title.</summary>
        public static readonly DependencyProperty TitleOpacityProperty =
            DependencyProperty.Register(
                PropTitleOpacity,
                typeof (double),
                typeof (TitleContainer),
                new PropertyMetadata(1d));


        /// <summary>Gets or sets the template for the title icon.</summary>
        public ControlTemplate IconTemplate
        {
            get { return (ControlTemplate) (GetValue(IconTemplateProperty)); }
            set { SetValue(IconTemplateProperty, value); }
        }
        /// <summary>Gets or sets the template for the title icon.</summary>
        public static readonly DependencyProperty IconTemplateProperty =
            DependencyProperty.Register(
                PropIconTemplate,
                typeof (ControlTemplate),
                typeof (TitleContainer),
                new PropertyMetadata(null));
        

        /// <summary>Gets or sets the child content data.</summary>
        public UIElement Child
        {
            get { return (UIElement)GetValue(ChildProperty); }
            set { SetValue(ChildProperty, value); }
        }
        /// <summary>Gets or sets the child content data.</summary>
        public static readonly DependencyProperty ChildProperty =
            DependencyProperty.Register(
                PropChild,
                typeof(UIElement),
                typeof (TitleContainer),
                new PropertyMetadata(null));


        /// <summary>Gets or sets whether the container is open or closed.</summary>
        public bool IsOpen
        {
            get { return (bool) (GetValue(IsOpenProperty)); }
            set { SetValue(IsOpenProperty, value); }
        }
        /// <summary>Gets or sets whether the container is open or closed.</summary>
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(
                PropIsOpen,
                typeof (bool),
                typeof (TitleContainer),
                new PropertyMetadata(true, HandleIsOpenedChanged));


        /// <summary>Gets or sets the duration (in seconds) of the open/close animation.</summary>
        public double AnimationDuration
        {
            get { return (double) (GetValue(AnimationDurationProperty)); }
            set { SetValue(AnimationDurationProperty, value); }
        }
        /// <summary>Gets or sets the duration (in seconds) of the open/close animation.</summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register(
                PropAnimationDuration,
                typeof (double),
                typeof (TitleContainer),
                new PropertyMetadata(0.15));


        /// <summary>Gets or sets whether the icon is animated (rotation) when opening and closing.</summary>
        public bool AnimateIcon
        {
            get { return (bool) (GetValue(AnimateIconProperty)); }
            set { SetValue(AnimateIconProperty, value); }
        }
        /// <summary>Gets or sets whether the icon is animated (rotation) when opening and closing.</summary>
        public static readonly DependencyProperty AnimateIconProperty =
            DependencyProperty.Register(
                PropAnimateIcon,
                typeof (bool),
                typeof (TitleContainer),
                new PropertyMetadata(true, HandleAnimateIcon));


        /// <summary>Gets or sets the easing function to apply to the animation.</summary>
        public IEasingFunction Easing
        {
            get { return (IEasingFunction)(GetValue(EasingProperty)); }
            set { SetValue(EasingProperty, value); }
        }
        /// <summary>Gets or sets the easing function to apply to the animation.</summary>
        public static readonly DependencyProperty EasingProperty =
            DependencyProperty.Register(
                PropEasing,
                typeof(IEasingFunction),
                typeof(TitleContainer),
                new PropertyMetadata(new QuadraticEase{EasingMode = EasingMode.EaseInOut}));


        /// <summary>Gets or sets the type of click gesture that causes the container to open or close.</summary>
        public ClickGesture ToggleIsOpenOn
        {
            get { return (ClickGesture) (GetValue(ToggleIsOpenOnProperty)); }
            set { SetValue(ToggleIsOpenOnProperty, value); }
        }
        /// <summary>Gets or sets the type of click gesture that causes the container to open or close.</summary>
        public static readonly DependencyProperty ToggleIsOpenOnProperty =
            DependencyProperty.Register(
                PropToggleIsOpenOn,
                typeof (ClickGesture),
                typeof (TitleContainer),
                new PropertyMetadata(ClickGesture.SingleClick));
        #endregion

        #region Internal
        private void ToggleIsOpen()
        {
            IsOpen = !IsOpen;
        }

        private void AnimateIsOpen()
        {
            // Rotate twisty (NB: the 'CollapsingPanel' takes care of the animation for the child Content).
            if (AnimateIcon) AnimationUtil.Rotate(icon, GetIconAngle(), AnimationDuration, null, null);
        }

        private double GetIconAngle()
        {
            return IsOpen ? 90 : 0;
        }
        #endregion
    }
}
