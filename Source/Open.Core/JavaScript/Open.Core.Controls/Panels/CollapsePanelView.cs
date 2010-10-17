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
        private readonly jQueryObject content;
        private int inflatedSize;

        /// <summary>Constructor.</summary>
        /// <param name="model">The model for the control.</param>
        /// <param name="content">The content element.</param>
        public CollapsePanelView(CollapsePanel model, jQueryObject content)
        {
            // Setup initial conditions.
            this.model = model;
            this.content = content;
            Container.AddClass(CollapsePanel.CssClass);

            // Setup the child content.
            model.Padding.Sync(content);
            Container.Append(content);

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
            // Setup initial conditions.
            int finalSize = model.inflateToTarget == CollapsePanel.NoTargetSize
                                                                                                    ? inflatedSize
                                                                                                    : model.inflateToTarget;
            SetContentSize(finalSize - model.Padding.GetOffset(model.Plane));

            // Animate.
            Dictionary properties = new Dictionary();
            properties[CssAttribute] = finalSize;
            Container.Animate(
                                properties,
                                model.Slide.ToMsecs(),
                                model.Slide.Easing,
                                delegate
                                {
                                    ReleaseContentSize();
                                    model.FireInflated();
                                });
        }

        private void OnCollapsing(object sender, EventArgs e)
        {
            // Store original size.
            inflatedSize = model.Plane == Plane.Horizontal ? Width : Height;
            FixContentSize();

            // Animate.
            Dictionary properties = new Dictionary();
            properties[CssAttribute] = 0;
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

        #region Properties
        private string CssAttribute { get { return model.Plane == Plane.Horizontal ? Css.Width : Css.Height; } }
        private SizeDimension Dimension { get { return Css.ToSizeDimension(model.Plane); } }
        #endregion

        #region Internal
        private void FixContentSize()
        {
            // NB: Fixing the size prevents the content from wrapping during a a collapse 
            //       operation.  Wrapping is restored after inflaction via 'ReleaseContentSize'.
            SetContentSize(Css.GetDimension(content, Dimension));
        }

        private void ReleaseContentSize()
        {
            content.CSS(CssAttribute, string.Empty);
        }

        private void SetContentSize(int value)
        {
            content.CSS(CssAttribute, value + Css.Px);
        }
        #endregion
    }
}
