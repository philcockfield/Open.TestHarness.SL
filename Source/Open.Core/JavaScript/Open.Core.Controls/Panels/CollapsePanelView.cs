using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Controls
{
    /// <summary>A panel that can collapse.</summary>
    internal class CollapsePanelView : ViewBase
    {
        #region Head
        private readonly CollapsePanel model;
        private int inflatedWidth;

        /// <summary>Constructor.</summary>
        /// <param name="model">The model for the control.</param>
        public CollapsePanelView(CollapsePanel model)
        {
            // Setup initial conditions.
            this.model = model;

            // Wire up events.
            model.Collapsing += OnCollapsing;
            model.Inflating += OnInflating;
        }

        protected override void OnDisposed()
        {
            model.Collapsing -= OnCollapsing;
            model.Inflating -= OnInflating;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnInflating(object sender, EventArgs e)
        {
            // Animate.
            Dictionary properties = new Dictionary();
            properties[Css.Width] = model.inflateToTarget == CollapsePanel.NoTargetSize ? inflatedWidth : model.inflateToTarget;
            Container.Animate(
                                properties,
                                model.Slide.ToMsecs(),
                                model.Slide.Easing,
                                delegate
                                {
                                    model.FireInflated();
                                });
        }

        private void OnCollapsing(object sender, EventArgs e)
        {
            // Store original width.
            inflatedWidth = Width;

            // Animate.
            Dictionary properties = new Dictionary();
            properties[Css.Width] = 0;
            Container.Animate(
                                properties,
                                model.Slide.ToMsecs(),
                                model.Slide.Easing,
                                delegate
                                        {
                                            model.FireCollapsed();
                                        });
        }
        #endregion
    }
}
