using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for scrolling.</summary>
    public class ScrollHelper
    {
        /// <summary>Scrolls to the specified element.</summary>
        /// <param name="container">The element to scroll to.</param>
        /// <param name="duration">The duration of the scroll animation (in seconds).</param>
        /// <param name="easing">The easing effect to apply.</param>
        /// <param name="onComplete">Action to invoke on complete.</param>
        public void ToBottom(jQueryObject container, double duration, EffectEasing easing, Action onComplete)
        {
            // Prepare the animation properties.
            Dictionary props = new Dictionary();
            props[Html.ScrollTop] = container.GetAttribute(Html.ScrollHeight);

            // Animate.
            container.Animate(
                                props, 
                                Helper.Time.ToMsecs(duration), 
                                easing, 
                                delegate
                                    {
                                        Helper.Invoke(onComplete);
                                    });
        }
    }
}
