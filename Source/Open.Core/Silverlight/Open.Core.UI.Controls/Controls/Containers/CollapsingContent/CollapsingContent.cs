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
using System.Windows.Media.Animation;
using Open.Core.Common;
using Open.Core.UI.Common;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Controls
{
    /// <summary>A container of content which is collapsible.</summary> 
    public class CollapsingContent : ContentControl
    {
        #region Head
        public const string PropIsOpen = "IsOpen";
        public const string PropChild = "Child";
        public const string PropAnimationDuration = "AnimationDuration";
        public const string PropAnimateOnLoad = "AnimateOnLoad";
        public const string PropEasing = "Easing";

        private int animationCount;
        private Border container;
        private ContentPresenter child;
        private bool isChildLoaded;

        public CollapsingContent()
        {
            Templates.Instance.ApplyTemplate(this);
        }

        public override void OnApplyTemplate()
        {
            // Setup initial conditions.
            base.OnApplyTemplate();

            // Retrieve elements.
            container = GetTemplateChild("PART_Container") as Border;
            child = GetTemplateChild("PART_Child") as ContentPresenter;
            if (container == null || child == null) throw new TemplateNotSetException();

            // Setup state.
            UpdateContainerHeight();
            UpdateVisibility();

            // Wire up events.
            if (child != null)
            {
                child.Loaded += Handle_Child_Loaded;
                child.SizeChanged += Handle_Child_SizeChanged;
            }
        }
        #endregion

        #region Event Handlers
        void Handle_Child_Loaded(object sender, RoutedEventArgs e)
        {
            child.Loaded -= Handle_Child_Loaded;
            isChildLoaded = true;
        }

        void Handle_Child_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!IsOpen || IsAnimating) return;
            if (isChildLoaded || AnimateOnLoad)
            {
                AnimateToChildHeight(container.ActualHeight);
            }
            else
            {
                UpdateContainerHeight();
            }
        }
        #endregion

        #region Dependecy Properties
        /// <summary>Gets or sets whether the container is open or closed.</summary>
        public bool IsOpen
        {
            get { return (bool)(GetValue(IsOpenProperty)); }
            set { SetValue(IsOpenProperty, value); }
        }
        /// <summary>Gets or sets whether the container is open or closed.</summary>
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(
                PropIsOpen,
                typeof(bool),
                typeof(CollapsingContent),
                new PropertyMetadata(true, (s, e) => ((CollapsingContent)s).AnimateIsOpenChanged()));


        /// <summary>Gets or sets the duration (in seconds) of the open/close animation.</summary>
        public double AnimationDuration
        {
            get { return (double)(GetValue(AnimationDurationProperty)); }
            set { SetValue(AnimationDurationProperty, value); }
        }
        /// <summary>Gets or sets the duration (in seconds) of the open/close animation.</summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register(
                PropAnimationDuration,
                typeof(double),
                typeof(CollapsingContent),
                new PropertyMetadata(0.15));


        /// <summary>Gets or sets whether the container animates it's size upon loading a new child.</summary>
        public bool AnimateOnLoad
        {
            get { return (bool)(GetValue(AnimateOnLoadProperty)); }
            set { SetValue(AnimateOnLoadProperty, value); }
        }
        /// <summary>Gets or sets whether the container animates it's size upon loading a new child.</summary>
        public static readonly DependencyProperty AnimateOnLoadProperty =
            DependencyProperty.Register(
                PropAnimateOnLoad,
                typeof(bool),
                typeof(CollapsingContent),
                new PropertyMetadata(false));


        /// <summary>Gets or sets the easing function to apply to the animation.</summary>
        public IEasingFunction Easing
        {
            get { return (IEasingFunction) (GetValue(EasingProperty)); }
            set { SetValue(EasingProperty, value); }
        }
        /// <summary>Gets or sets the easing function to apply to the animation.</summary>
        public static readonly DependencyProperty EasingProperty =
            DependencyProperty.Register(
                PropEasing,
                typeof (IEasingFunction),
                typeof (CollapsingContent),
                new PropertyMetadata(new QuadraticEase{EasingMode = EasingMode.EaseOut}));
        #endregion

        #region Properties - Private
        private bool IsAnimating { get { return animationCount > 0; } }
        #endregion

        #region Internal
        private void AnimateIsOpenChanged()
        {
            if (container == null) return;
            var fromHeight = IsOpen ? 0 : container.ActualHeight;
            AnimateToChildHeight(fromHeight);
        }

        private void AnimateToChildHeight(double fromHeight)
        {
            // Setup initial conditions.
            animationCount++;
            var toHeight = GetToHeight();

            // Handle animation complete.
            Action callback = delegate
                            {
                                // Finalize animation.
                                UpdateVisibility();
                                animationCount--;

                                // If the child has changed size during the animation, resize again.
                                var finalHeight = GetToHeight();
                                if (finalHeight != toHeight) AnimateToChildHeight(toHeight);
                            };

            // Perform the animation.
            container.Visibility = Visibility.Visible;
            AnimationUtil.DoubleAnimate(container, fromHeight, toHeight, AnimationDuration, "Height", Easing, callback);
        }

        private double GetToHeight()
        {
            if (IsOpen) RefreshChildDesiredSize();
            return IsOpen ? child.DesiredSize.Height : 0;
        }

        private void RefreshChildDesiredSize()
        {
            // Force an accurate reading of the desired size of the child.
            try
            {
                child.Measure(new Size(container.ActualWidth, 1000000));
            }
            catch{}
        }

        private void UpdateContainerHeight()
        {
            RefreshChildDesiredSize();
            container.Height = child.DesiredSize.Height;
        }

        private void UpdateVisibility()
        {
            container.Visibility = IsOpen ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}
