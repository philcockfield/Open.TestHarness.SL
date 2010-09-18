using System;
using System.Collections;
using jQueryApi;
using Open.Core;
using Open.Core.Controls;

namespace Open.Testing.Controllers
{
    /// <summary>Constroller for the Log panel.</summary>
    /// <remarks>The log-list control is a part of Open.Core.  See 'Open.Core.Controls'</remarks>
    public class LogController : TestHarnessControllerBase
    {
        #region Head
        private readonly TestHarnessEvents events;
        private readonly jQueryObject divLogContainer;
        private const double SlideDuration = 0.3;
        private readonly IPanelResizeController panelResizer;

        /// <summary>Constructor.</summary>
        public LogController()
        {
            // Setup initial conditions.
            events = Common.Events;
            divLogContainer = jQuery.Select(CssSelectors.LogContainer);
            panelResizer = Common.Container.GetSingleton(typeof(IPanelResizeController)) as IPanelResizeController;

            // Setup the output log.
            Log.View = new LogView(jQuery.Select(CssSelectors.LogControl).First());

            // Wire up events.
            events.ChangeLogHeight += OnChangeLogHeight;
        }

        /// <summary>Destroy.</summary>
        protected override void OnDisposed()
        {
            events.ChangeLogHeight -= OnChangeLogHeight;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnChangeLogHeight(object sender, ChangeHeightEventArgs e)
        {
            AnimateHeight(e.Height, delegate
                                        {
                                            events.FireUpdateLayout();
                                            panelResizer.UpdateLayout();
                                            panelResizer.Save();
                                        });
        }
        #endregion

        #region Internal
        private void AnimateHeight(int height, Action onComplete)
        {
            // Configure the animation.
            Dictionary properties = new Dictionary();
            properties[Css.Height] = HeightWithinRange(height) + Css.Px;

            // Perform animation.
            divLogContainer.Animate(
                properties,
                Helper.Time.ToMsecs(SlideDuration),
                EffectEasing.Swing,
                delegate
                            {
                                Helper.InvokeOrDefault(onComplete);
                            });
        }

        private int HeightWithinRange(int height)
        {
            if (height < PanelResizeController.LogMinHeight) height = PanelResizeController.LogMinHeight;
            if (height > panelResizer.LogResizer.MaxHeight) height = panelResizer.LogResizer.MaxHeight;
            return height;
        }
        #endregion
    }
}
