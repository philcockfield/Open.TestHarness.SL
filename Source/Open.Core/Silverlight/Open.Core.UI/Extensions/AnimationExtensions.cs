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
using System.Windows.Media.Animation;
using Open.Core.UI;
using Open.Core.UI.Common;

namespace Open.Core.Common
{
    public static class AnimationExtensions
    {
        #region Methods - IOpacity (Fading)
        /// <summary>Animates the opacity property on the given element.</summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="fromOpacity">The starting opacity value (0-1).</param>
        /// <param name="toOpacity">The ending opacity value (0-1).</param>
        /// <param name="seconds">The duration in seconds.</param>
        /// <param name="easing">The animating easing to apply.</param>
        /// <param name="onComplete">Method to execute when the animation is complete.</param>
        public static void Fade(this IOpacity element, double fromOpacity, double toOpacity, double seconds, IEasingFunction easing = null, Action onComplete = null)
        {
            if (element == null) throw new ArgumentNullException("element");
            var wrapper = new OpacityWrapper(element);
            AnimationUtil.Fade(wrapper, fromOpacity, toOpacity, seconds, easing, onComplete);
        }

        /// <summary>Fades the given element in from it's current opacity to 100% opacity.</summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="seconds">The duration in seconds.</param>
        /// <param name="easing">The easing function to apply to the animation (Null if not required).</param>
        /// <param name="onComplete">Method to execute when the animation is complete.</param>
        public static void FadeIn(this IOpacity element, double seconds, IEasingFunction easing = null, Action onComplete = null)
        {
            element.Fade(element.Opacity, 1, seconds, easing, onComplete);
        }

        /// <summary>Fades the given element in from it's current opacity to 0% opacity.</summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="seconds">The duration in seconds.</param>
        /// <param name="easing">The easing function to apply to the animation (Null if not required).</param>
        /// <param name="onComplete">Method to execute when the animation is complete.</param>
        public static void FadeOut(this IOpacity element, double seconds, IEasingFunction easing = null, Action onComplete = null)
        {
            element.Fade(element.Opacity, 0, seconds, easing, onComplete);
        }
        #endregion

        private class OpacityWrapper : DependencyObject
        {
            private readonly IOpacity element;
            public OpacityWrapper(IOpacity element) { this.element = element; }
            public double Opacity
            {
                get { return (double) (GetValue(OpacityProperty)); }
                set { SetValue(OpacityProperty, value); }
            }
            public static readonly DependencyProperty OpacityProperty =
                DependencyProperty.Register(
                    LinqExtensions.GetPropertyName<OpacityWrapper>(m => m.Opacity),
                    typeof (double),
                    typeof (OpacityWrapper),
                    new PropertyMetadata(1d, (s, e) => ((OpacityWrapper) s).OnOpacityChanged()));
            private void OnOpacityChanged() { element.Opacity = Opacity; }
        }
    }
}
