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

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using Open.Core.Common;
using Open.Core.UI.Common;

using T = Open.Core.UI.Controls.DialogPresenter;

namespace Open.Core.UI.Controls
{
    /// <summary>A control that presents it's contents as a modal dialog.</summary>
    /// <remarks>Requires the 'DialogPresenter' template-style.</remarks>
    public class DialogPresenter : ContentControl, IDialogPresenter
    {
        #region Head
        public const string PropIsShowing = "IsShowing";
        public const string PropAnimationDuration = "AnimationDuration";
        public const string PropMaskOpacity = "MaskOpacity";
        public const string PropMaskBrush = "MaskBrush";
        public const string PropEasing = "Easing";

        private ContentPresenter content;
        private Canvas canvas;
        private DropShadowEffect dropShadow;
        private Border mask;
        private int animationCount;
        private bool isInitialized;

        public DialogPresenter()
        {
            Templates.Instance.ApplyTemplate(this);
        }

        public override void OnApplyTemplate()
        {
            // Setup initial conditions.
            base.OnApplyTemplate();

            // Retreive elements.
            content = GetTemplateChild("PART_Content") as ContentPresenter;
            canvas = GetTemplateChild("PART_Canvas") as Canvas;
            mask = GetTemplateChild("PART_Mask") as Border;
            dropShadow = GetTemplateChild("PART_DropShadow") as DropShadowEffect;

            // Ensure all required parts are available.
            if (content == null || canvas == null || mask == null || dropShadow == null) throw new TemplateNotSetException();

            // Wire up events.
            content.Loaded += Handle_ContentLoaded;
            content.SizeChanged += delegate { UpdateDialogPosition(); };
            canvas.SizeChanged += Handle_Canvas_SizeChanged;

            // Finish up.
            isInitialized = true;
        }
        #endregion

        #region Event Handlers
        private void Handle_ContentLoaded(object sender, RoutedEventArgs e)
        {
            content.Loaded -= Handle_ContentLoaded;
            UpdateElementVisibility();
            UpdateDialogPosition();
        }

        private void Handle_Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateDialogPosition();
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets whether the dialog is currently being shown.</summary>
        public bool IsShowing
        {
            get { return (bool) (GetValue(IsShowingProperty)); }
            set { SetValue(IsShowingProperty, value); }
        }
        /// <summary>Gets or sets whether the dialog is currently being shown.</summary>
        public static readonly DependencyProperty IsShowingProperty =
            DependencyProperty.Register(
                PropIsShowing,
                typeof (bool),
                typeof (DialogPresenter),
                new PropertyMetadata(false, (sender, e) => ((DialogPresenter)sender).OnIsShowingChanged()));


        /// <summary>Gets or sets the duration (in seconds) of the animated slide when the dialog is shown or hidden.</summary>
        public double AnimationDuration
        {
            get { return (double) (GetValue(AnimationDurationProperty)); }
            set { SetValue(AnimationDurationProperty, value); }
        }
        /// <summary>Gets or sets the duration (in seconds) of the animated slide when the dialog is shown or hidden.</summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register(
                PropAnimationDuration,
                typeof (double),
                typeof (DialogPresenter),
                new PropertyMetadata(0.2d));


        /// <summary>Gets or sets the opacity of the mask which covers the UI when the dialog is showing.</summary>
        public double MaskOpacity
        {
            get { return (double)(GetValue(MaskOpacityProperty)); }
            set { SetValue(MaskOpacityProperty, value); }
        }
        /// <summary>Gets or sets the opacity of the mask which covers the UI when the dialog is showing.</summary>
        public static readonly DependencyProperty MaskOpacityProperty =
            DependencyProperty.Register(
                PropMaskOpacity,
                typeof(double),
                typeof(DialogPresenter),
                new PropertyMetadata(0.7));


        /// <summary>Gets or sets the brush used for the mask which covers the UI when the dialog is showing.</summary>
        public Brush MaskBrush
        {
            get { return (Brush)(GetValue(MaskBrushProperty)); }
            set { SetValue(MaskBrushProperty, value); }
        }
        /// <summary>Gets or sets the brush used for the mask which covers the UI when the dialog is showing.</summary>
        public static readonly DependencyProperty MaskBrushProperty =
            DependencyProperty.Register(
                PropMaskBrush,
                typeof(Brush),
                typeof(DialogPresenter),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));


        /// <summary>Gets or sets the easing function to animate the slide with (Null if not required).</summary>
        public IEasingFunction Easing
        {
            get { return (IEasingFunction) (GetValue(EasingProperty)); }
            set { SetValue(EasingProperty, value); }
        }
        /// <summary>Gets or sets the easing function to animate the slide with (Null if not required).</summary>
        public static readonly DependencyProperty EasingProperty =
            DependencyProperty.Register(
                PropEasing,
                typeof (IEasingFunction),
                typeof (DialogPresenter),
                new PropertyMetadata(new QuadraticEase{EasingMode = EasingMode.EaseIn}));
        #endregion

        #region Properties - Internal
        private bool IsAnimating { get { return animationCount > 0; } }
        #endregion

        #region Internal
        private void OnIsShowingChanged()
        {
            if (! isInitialized) return;
            if (content == null) return;
            AnimateDialog(AnimationDuration);
        }

        private void AnimateDialog(double duration)
        {
            // Setup initial conditions.
            animationCount++;

            // Start the mask-fade in animation.
            mask.Visibility = Visibility.Visible;
            var fromOpacity = IsShowing ? 0 : MaskOpacity;
            var toOpacity = IsShowing ? MaskOpacity : 0;
            AnimationUtil.Fade(mask, fromOpacity, toOpacity, duration, null, null);

            // Start the slide animation.
            Point startPosition;
            Point endPosition;
            GetSlidePositions(out startPosition, out endPosition);
            dropShadow.Opacity = 0;
            AnimationUtil.Move(content, startPosition, endPosition, duration, Easing, ()=>
                                                                                          {
                                                                                              animationCount--;
                                                                                              UpdateElementVisibility();
                                                                                              UpdateDialogPosition();
                                                                                              if (IsShowing) FocusContent();
                                                                                          });
        }

        private void UpdateElementVisibility()
        {
            mask.Visibility = IsShowing ? Visibility.Visible : Visibility.Collapsed;
            dropShadow.Opacity = IsShowing && !IsAnimating ? 0.2 : 0;
        }

        private void GetSlidePositions(out Point start, out Point end)
        {
            UpdateContentMeasurements();
            var x = GetDialogLeft();
            var offStage = new Point(x, 0 - content.DesiredSize.Height);
            var onStage = new Point(x, 0);

            start = IsShowing ? offStage : onStage;
            end = IsShowing ? onStage : offStage;
        }

        private void UpdateDialogPosition()
        {
            // Setup initial conditions.
            if (! isInitialized) return;
            UpdateContentMeasurements();

            // Center the dialog (X).
            Canvas.SetLeft(content, GetDialogLeft());

            // Set top value (Y).
            if (!IsAnimating)
            {
                var y = IsShowing ? 0 : -500000;
                Canvas.SetTop(content, y);
            }
        }

        private double GetDialogLeft()
        {
            return Math.Round((ActualWidth / 2) - (content.DesiredSize.Width / 2));
        }

        private void UpdateContentMeasurements()
        {
            content.Measure(new Size(ActualWidth, ActualHeight));
        }

        private void FocusContent()
        {
            var contentElement = Content as UIElement;
            if (contentElement == null) return;
            if (contentElement.ContainsFocus()) return;

            // Attempt to set focus to a child adorned with the 'Focus.IsDefault' attached property.
            var defaultChildFocused = contentElement.FocusDefaultChild();

            // Focus the content directly if there was no default child.
            if (!defaultChildFocused) contentElement.Focus();
        }
        #endregion
    }
}
