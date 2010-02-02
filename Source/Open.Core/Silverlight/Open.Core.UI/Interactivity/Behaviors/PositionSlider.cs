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
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Open.Core.UI.Common;
using System.ComponentModel;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Allows an element to be positioned with a sliding motion.</summary>
    public class PositionSlider : Behavior<FrameworkElement>, INotifyPropertyChanged
    {
        #region Events
        /// <summary>Fires when the slide animation starts.</summary>
        public event EventHandler SlideStarted;
        protected void OnSlideStarted() { if (SlideStarted != null) SlideStarted(this, new EventArgs()); }

        /// <summary>Fires when the slide animation completes</summary>
        public event EventHandler SlideComplete;
        protected void OnSlideComplete() { if (SlideComplete != null) SlideComplete(this, new EventArgs()); }

        /// <summary>Fires when summary</summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void FirePropertyChangedEvent(PropertyChangedEventArgs e) { if (PropertyChanged != null) PropertyChanged(this, e); }
        #endregion

        #region Head
        public const string PropPosition = "Position";
        public const string PropX = "X";
        public const string PropY = "Y";
        public const string PropIsSlideEnabled = "IsSlideEnabled";
        public const string PropDuration = "Duration";
        public const string PropEasing = "Easing";

        private NotifyPropertyChangedInvoker invoker;
        private readonly SynchronizationContext syncContext;

        private Canvas canvas;
        private Point position;
        private bool isSlideEnabled = true;
        private double duration = 0.3;
        private IEasingFunction easing;
        private int animationCount;

        public PositionSlider()
        {
            syncContext = SynchronizationContext.Current;
        }
        #endregion

        #region Event Handlers
        void HandleLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= HandleLoaded;
            UpdatePositionValue();
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the X:Y position of the element within it's parent canvas.</summary>
        public Point Position
        {
            get { return position; }
            set
            {
                // Setup initial conditions.
                if (Equals(value, Position)) return;

                // Determine which axis changed.
                var xChanged = value.X != ActualX;
                var yChanged = value.Y != ActualY;

                // Store value.
                position = value;

                // Slide (if required).
                if (xChanged || yChanged)
                {
                    if (IsWithinCanvas && AssociatedObject != null)
                    {
                        if (IsSlideEnabled) SlideAnimate(position);
                        else Canvas.SetPosition(AssociatedObject, position);
                    }
                }

                // Finish up.
                if (xChanged) Invoker.OnPropertyChanged(PropX);
                if (yChanged) Invoker.OnPropertyChanged(PropY);
                Invoker.OnPropertyChanged(PropPosition);
            }
        }

        /// <summary>Gets or sets the X pixel position of the element within it's parent canvas.</summary>
        public double X
        {
            get { return Position.X; }
            set
            {
                if (Equals(value, X)) return;
                Position = new Point(value, Position.Y);
            }
        }

        /// <summary>Gets or sets the Y pixel position of the element within it's parent canvas.</summary>
        public double Y
        {
            get { return Position.Y; }
            set
            {
                if (Equals(value, Y)) return;
                Position = new Point(Position.X, value);
            }
        }

        /// <summary>Gets or sets whether changes to the X:Y position values invoke slide animates to move the element to it's new location.</summary>
        public bool IsSlideEnabled
        {
            get { return isSlideEnabled; }
            set
            {
                if (value == isSlideEnabled) return;
                isSlideEnabled = value;
                Invoker.OnPropertyChanged(PropIsSlideEnabled);
            }
        }

        /// <summary>Gets the canvas that the element resides within.</summary>
        public Canvas Canvas
        {
            get
            {
                if (AssociatedObject == null) return null;
                if (canvas == null) canvas = VisualTreeHelper.GetParent(AssociatedObject) as Canvas;
                return canvas;
            }
        }

        /// <summary>Gets whether the object resides within a Canvas.</summary>
        public bool IsWithinCanvas { get { return Canvas != null; } }

        /// <summary>Gets whether the element is currently sliding.</summary>
        public bool IsAnimating { get { return animationCount > 0; } }

        /// <summary>Gets or sets the duration (in seconds) of the slide animation.</summary>
        public double Duration
        {
            get { return duration; }
            set
            {
                if (value == Duration) return;
                duration = value;
                Invoker.OnPropertyChanged(PropDuration);
            }
        }

        /// <summary>Gets or sets the easing function to apply to the slide animation.</summary>
        public IEasingFunction Easing
        {
            get { return easing; }
            set
            {
                if (value == Easing) return;
                easing = value;
                Invoker.OnPropertyChanged(PropEasing);
            }
        }
        #endregion

        #region Properties - Private
        private double ActualX { get { return Canvas.GetLeft(AssociatedObject); } }
        private double ActualY { get { return Canvas.GetTop(AssociatedObject); } }
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += HandleLoaded;
        }

        /// <summary>Updates the 'Position' property value with the elements current position within the containing Canvas.</summary>
        public void UpdatePositionValue()
        {
            if (IsWithinCanvas) position = Canvas.GetChildPosition(AssociatedObject);
        }

        private NotifyPropertyChangedInvoker Invoker
        {
            get
            {
                if (invoker == null) invoker = new NotifyPropertyChangedInvoker(syncContext, FirePropertyChangedEvent);
                return invoker;
            }
        }
        #endregion

        #region Internal
        private void SlideAnimate(Point to)
        {
            if (!IsAnimating) OnSlideStarted();
            Action callback = delegate
                                  {
                                      animationCount--;
                                      if (! IsAnimating) OnSlideComplete();
                                  };
            AnimationUtil.Move(AssociatedObject, Canvas.GetChildPosition(AssociatedObject), to, Duration, Easing, callback);
            animationCount++;
        }
        #endregion
    }
}
