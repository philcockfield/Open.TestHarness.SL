using System;
using System.Collections;
using jQueryApi;
using Open.Core;

namespace Open.Testing.Views
{
    /// <summary>View for defining a new test-package to add to the side bar.</summary>
    public class AddPackageView : TestHarnessViewBase
    {
        #region Head
        private const string ContentUrl = "/TestHarness/AddPackage/";
        private const double SlideDuration = 0.25;
        private jQueryObject divInnerSlide;
        private string offLeft;

        /// <summary>Constructor.</summary>
        public AddPackageView()
        {
            // Load the HTML content.
            RetrieveHtml(ContentUrl, delegate
                                         {
                                             divInnerSlide = jQuery.Select(CssSelectors.AddPackageInnerSlide);
                                             offLeft = divInnerSlide.GetCSS(Css.Left);
                                             SlideOn(null);
                                         });
        }
        #endregion

        #region Methods
        /// <summary>Inserts an instance of the view into the TestHarness' main canvas.</summary>
        public static AddPackageView AddToTestHarness()
        {
            AddPackageView view = new AddPackageView();
            TestHarness.Reset();
            TestHarness.CanScroll = false;
            TestHarness.DisplayMode = ControlDisplayMode.Fill;
            TestHarness.AddControl(view);
            return view;
        }

        /// <summary>Slides the panel on screen.</summary>
        /// <param name="onComplete">Action to invoke upon completion.</param>
        public void SlideOn(Action onComplete) { Slide("0px", onComplete); }

        /// <summary>Slides the panel off screen.</summary>
        /// <param name="onComplete">Action to invoke upon completion.</param>
        public void SlideOff(Action onComplete) { Slide(offLeft, onComplete); }
        #endregion

        #region Internal
        private void Slide(string left, Action onComplete)
        {
            // Configure the animation.
            Dictionary properties = new Dictionary();
            properties[Css.Left] = left;

            // Perform animation.
            divInnerSlide.Animate(
                properties,
                Helper.Time.ToMsecs(SlideDuration),
                EffectEasing.Swing,
                delegate
                    {
                        Helper.Invoke(onComplete);
                    });
        }
        #endregion
    }
}
