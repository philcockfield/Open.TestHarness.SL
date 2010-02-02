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
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Collections.Generic;

namespace Open.Core.UI.Common
{
    #region Return Types
    /// <summary>Contains the X,Y animations for a move operation.</summary>
    public class MoveAnimation
    {
        public MoveAnimation(DoubleAnimation x, DoubleAnimation y)
        {
            X = x;
            Y = y;
        }

        /// <summary>Gets the horizontal animation.</summary>
        public DoubleAnimation X { get; private set; }

        /// <summary>Gets the vertical animation.</summary>
        public DoubleAnimation Y { get; private set; }
    }
    #endregion

    /// <summary>Provides utility functionality for working with animations.</summary>
    /// <remarks>
    ///    Easing functions:
    ///    - CircleEase, SineEase, BackEase
    ///    - ExpoentialEase, PowerEase
    ///    - QuadradicEase, CubicEase
    ///    - QuarticEase, QuinticEase
    ///    - ElasticEase, BounceEase
    /// </remarks>
    public static partial class AnimationUtil
    {
        #region Head
        private static readonly List<Storyboard> storyboardsList = new List<Storyboard>();
        private const string PropOpacity = "Opacity";
        #endregion

        #region Methods
        /// <summary>Fades the given element in from it's current opacity to 0% opacity.</summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="seconds">The duration in seconds.</param>
        /// <param name="easing">The easing function to apply to the animation (Null if not required).</param>
        /// <param name="callback">Method to execute when the animation is complete.</param>
        public static void FadeOut(FrameworkElement element, double seconds, IEasingFunction easing, Action callback)
        {
            Fade(element, element.Opacity, 0, seconds, easing, callback);
        }

        /// <summary>Fades the given element in from it's current opacity to 100% opacity.</summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="seconds">The duration in seconds.</param>
        /// <param name="easing">The easing function to apply to the animation (Null if not required).</param>
        /// <param name="callback">Method to execute when the animation is complete.</param>
        public static void FadeIn(FrameworkElement element, double seconds, IEasingFunction easing, Action callback)
        {
            Fade(element, element.Opacity, 1, seconds, easing, callback);
        }

        /// <summary>Animates the opacity property on the given element.</summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="fromOpacity">The starting opacity value (0-1).</param>
        /// <param name="toOpacity">The ending opacity value (0-1).</param>
        /// <param name="seconds">The duration in seconds.</param>
        /// <param name="callback">Method to execute when the animation is complete.</param>
        public static void Fade(FrameworkElement element, double fromOpacity, double toOpacity, double seconds, IEasingFunction easing, Action callback)
        {
            DoubleAnimate(element, fromOpacity, toOpacity, seconds, PropOpacity, easing, callback);
        }

        /// <summary>Animates the opacity property on the given element.</summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="fadeInOut">Flag indicating if the element should be faded in (true) or out (false).</param>
        /// <param name="seconds">The duration in seconds.</param>
        /// <param name="easing">The easing function to apply to the animation (Null if not required).</param>
        /// <param name="callback">Method to execute when the animation is complete.</param>
        public static void Fade(FrameworkElement element, bool fadeInOut, double seconds, IEasingFunction easing, Action callback)
        {
            if (fadeInOut)
            {
                FadeIn(element, seconds, easing, callback);
            }
            else
            {
                FadeOut(element, seconds, easing, callback);
            }
        }

        /// <summary>Cross fades from one element to another.</summary>
        /// <param name="from">The element to fade from.</param>
        /// <param name="to">The element to fade to.</param>
        /// <param name="seconds">The duration in seconds.</param>
        /// <param name="easing">The easing function to apply to the animation (Null if not required).</param>
        /// <param name="callback">Method to execute when the animation is complete.</param>
        /// <remarks>Make sure the element you're fading to is at zero opacity before calling this method.</remarks>
        public static void CrossFade(FrameworkElement from, FrameworkElement to, double seconds, IEasingFunction easing, Action callback)
        {
            // Construct 2 animations for fading in and out.
            var animationFadeOut = CreateDoubleAnimation(from.Opacity, 0, seconds, easing);
            var animationFadeIn = CreateDoubleAnimation(0, 1, seconds, easing);

            // Setup a story with both animations.
            var storyboard = CreateStoryboard(from, animationFadeOut, PropOpacity);
            InsertAnimation(storyboard, to, animationFadeIn, PropOpacity);

            // Run the animation.
            Animator.Animate(storyboard, callback, from, to);
        }

        /// <summary>Moves an element from one point to another.</summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="start">The X,Y coordinates for the start of the animation.</param>
        /// <param name="end">The X,Y coordinates for the end of the animation.</param>
        /// <param name="seconds">The duration in seconds.</param>
        /// <param name="easing">The easing function to apply to the animation (Null if not required).</param>
        /// <param name="callback">Method to execute when the animation is complete.</param>
        public static MoveAnimation Move(FrameworkElement element, Point start, Point end, double seconds, IEasingFunction easing, Action callback)
        {
            var x = DoubleAnimate(element, start.X, end.X, seconds, "(Canvas.Left)", easing, callback);
            var y = DoubleAnimate(element, start.Y, end.Y, seconds, "(Canvas.Top)", easing, null);
            return new MoveAnimation(x, y);
        }

        /// <summary>Rotates an element.</summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="toAngle">The angle to rotate to.</param>
        /// <param name="seconds">The duration in seconds.</param>
        /// <param name="easing">The easing function to apply to the animation (Null if not required).</param>
        /// <param name="callback">Method to execute when the animation is complete.</param>
        public static void Rotate(FrameworkElement element, double toAngle, double seconds, IEasingFunction easing, Action callback)
        {
            // Add a transform if one doesn't already exist.
            if ((element.RenderTransform as RotateTransform) == null) element.RenderTransform = new RotateTransform();
            var transform = (RotateTransform)element.RenderTransform;

            // Execute the rotation.
            var propertyPath = string.Format("({0}.RenderTransform).(RotateTransform.Angle)", element.GetType().Name);
            DoubleAnimate(element, transform.Angle, toAngle, seconds, propertyPath, easing, callback);
        }

        /// <summary>Animates the given element.</summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="fromValue">The starting property value.</param>
        /// <param name="toValue">Te ending property value.</param>
        /// <param name="seconds">The duration in seconds.</param>
        /// <param name="propertyPath">The path to the property to animate (eg. "Opacity" or "(Canvas.Left)").</param>
        /// <param name="easing">The easing function to apply to the animation (Null if not required).</param>
        /// <param name="callback">Method to execute when the animation is complete.</param>
        public static DoubleAnimation DoubleAnimate(FrameworkElement element, double fromValue, double toValue, double seconds, string propertyPath, IEasingFunction easing, Action callback)
        {
            // Construct the animation.
            var animation = CreateDoubleAnimation(fromValue, toValue, seconds, easing);
            var storyboard = CreateStoryboard(element, animation, propertyPath);

            // Run the animation.
            Animator.Animate(storyboard, callback, element);

            // Finish up.
            return animation;
        }
        #endregion

        #region Internal
        private static Storyboard CreateStoryboard(DependencyObject element, Timeline animation, string propertyPath)
        {
            return CreateStoryboard(element, new[] { animation }, new [] { propertyPath });
        }

        private static Storyboard CreateStoryboard(DependencyObject element, Timeline[] animations, string[] propertyPaths)
        {
            // Create the Storyboard.
            var storyboard = new Storyboard();
            for (var i = 0; i < animations.Length; i++)
            {
                InsertAnimation(storyboard, element, animations[i], propertyPaths[i]);
            }

            // Finish up.
            RegisterStoryboard(storyboard);
            return storyboard;
        }

        private static void InsertAnimation(Storyboard storyboard, DependencyObject element, Timeline animation, string propertyPath)
        {
            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, element);
            Storyboard.SetTargetProperty(animation, new PropertyPath(propertyPath));
        }

        private static void RegisterStoryboard(Storyboard storyboard)
        {
            // Store the story board for the duration of the animation.
            // NB: If this does not happen the animation will stop the moment the method exits.
            storyboardsList.Add(storyboard);
            storyboard.Completed += delegate
                                        {
                                            storyboardsList.Remove(storyboard);
                                        };
        }
        #endregion

        private class Animator : IDisposable
        {
            #region Head
            private Storyboard Storyboard { get; set; }
            private FrameworkElement[] Elements { get; set; }
            private Action Callback { get; set; }
            private static readonly List<Animator> instanceList = new List<Animator>();

            private Animator(Storyboard storyboard, Action callback, params FrameworkElement[] elements)
            {
                // Store values.
                Storyboard = storyboard;
                Callback = callback;
                Elements = elements;

                // Wire up events.
                Storyboard.Completed += Handle_Storyboard_Completed;
            }

            public void Dispose()
            {
                Storyboard.Completed -= Handle_Storyboard_Completed;
                instanceList.Remove(this);
            }
            #endregion

            #region Event Handlers
            private void Handle_Storyboard_Completed(object sender, EventArgs e)
            {
                if (Callback != null) Callback();
                Dispose();
            }
            #endregion

            #region Methods
            public static void Animate(Storyboard storyboard, Action callback, params FrameworkElement[] elements)
            {
                var animator = new Animator(storyboard, callback, elements);
                instanceList.Add(animator);
                storyboard.Begin();
            }
            #endregion
        }
    }
}
