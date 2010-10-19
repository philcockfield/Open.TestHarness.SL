using System;
using System.Collections;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core.Controls
{
    /// <summary>A panel that can collapse.</summary>
    public class CollapsePanel : ViewBase
    {
        #region Events
        /// <summary>Fires when the panels starts collapsing.</summary>
        public event EventHandler Collapsing;
        private void FireCollapsing() { if (Collapsing != null) Collapsing(this, new EventArgs()); }

        /// <summary>Fires when the panel has finished collapsing.</summary>
        public event EventHandler Collapsed;
        private void FireCollapsed()
        {
            Helper.Invoke(collapsingCallback);
            collapsingCallback = null;
            if (Collapsed != null) Collapsed(this, new EventArgs());
        }

        /// <summary>Fires when the panel starts inflating (uncollapsing).</summary>
        public event EventHandler Inflating;
        private void FireInflating() { if (Inflating != null) Inflating(this, new EventArgs()); }

        /// <summary>Fires when the panel has finished inflating (uncollapsed).</summary>
        public event EventHandler Inflated;
        private void FireInflated()
        {
            Helper.Invoke(inflatingCallback);
            inflatingCallback = null;
            inflateToTarget = NoTargetSize;
            if (Inflated != null) Inflated(this, new EventArgs());
        }
        #endregion

        #region Head
        public const string PropIsCollapsed = "IsCollapsed";
        public const string PropIsInflated = "IsInflated";
        public const string PropPlane = "Plane";
        public const string CssClass = "collapsePanel";

        private const bool DefaultIsCollapsed = false;
        private const Plane DefaultPlane = Plane.Horizontal;
        internal const int NoTargetSize = -1;

        private Action collapsingCallback;
        private Action inflatingCallback;
        private AnimationSettings slide;
        internal int inflateToTarget = NoTargetSize;
        private readonly Spacing padding = new Spacing();

        private readonly jQueryObject content;
        private int inflatedSize;


        /// <summary>Constructor.</summary>
        [AlternateSignature]
        public extern CollapsePanel();

        /// <summary>Constructor.</summary>
        /// <param name="container">The root HTML element of the control (if null a <DIV></DIV> is generated).</param>
        public CollapsePanel(jQueryObject container) : base(container)
        {
            // Setup initial conditions.
            Container.AddClass(CssClass);

            // Initialize the content panel.
            content = Html.CreateDiv();
            Padding.Sync(content);
            Container.Append(content);
        }
        #endregion

        #region Properties
        /// <summary>Gets the DIV that contains the display content of the panel.  </summary>
        public jQueryObject Content { get { return content; } }

        /// <summary>Gets the slide animation settings.</summary>
        public AnimationSettings Slide { get { return slide ?? (slide = new AnimationSettings()); } }

        /// <summary>Gets or sets the X or Y plane that the panel collapses on.</summary>
        public Plane Plane
        {
            get { return (Plane)Get(PropPlane, DefaultPlane); }
            set { Set(PropPlane, value, DefaultPlane); }
        }

        /// <summary>Gets or sets whether the panel is collapsed or not.</summary>
        public bool IsCollapsed
        {
            get { return (bool)Get(PropIsCollapsed, DefaultIsCollapsed); }
            set
            {
                if (Set(PropIsCollapsed, value, DefaultIsCollapsed))
                {
                    if (value) { CollapseInternal(null); } else { InflateInternal(null); } // Show or hide if value has changed.
                    FirePropertyChanged(PropIsInflated); // Fire change event for inverse property.
                }
            }
        }

        /// <summary>Gets or sets whether the panel is inflated.</summary>
        public bool IsInflated
        {
            get { return !IsCollapsed; }
            set { IsCollapsed = !value; }
        }

        /// <summary>Gets whether the panel is currently collapsing.</summary>
        public bool IsCollapsing { get { return collapsingCallback != null; } }

        /// <summary>Gets whether the panel is currently showing (uncollapsing).</summary>
        public bool IsInflating { get { return inflatingCallback != null; } }

        /// <summary>Gets or sets the pixel padding within the panel.</summary>
        public Spacing Padding { get { return padding; } }
        #endregion

        #region Properties : Internal
        private string CssAttribute { get { return Plane == Plane.Horizontal ? Css.Width : Css.Height; } }
        private SizeDimension Dimension { get { return Css.ToSizeDimension(Plane); } }
        #endregion

        #region Methods
        /// <summary>Inflates the panel (uncollapse).</summary>
        /// <param name="callback">Action to invoke upon completion.</param>
        [AlternateSignature]
        public extern void Inflate(Action callback);

        /// <summary>Inflates the panel (uncollapse).</summary>
        /// <param name="callback">Action to invoke upon completion.</param>
        /// <param name="targetSize">The target size to aim for.</param>
        public void Inflate(Action callback, int targetSize)
        {
            if (!IsCollapsed) return;
            inflateToTarget = Script.IsNullOrUndefined(targetSize) ? NoTargetSize : targetSize;
            IsCollapsed = false; // Internal animation is called via the IsCollapsed property logic.
        }
        private void InflateInternal(Action callback)
        {
            // Store callback.
            if (callback == null) callback = delegate { };  // Dummy callback.
            inflatingCallback = callback;

            // Alert listeners.
            FireInflating();
            InflateAnimation();
        }

        /// <summary>Collapses the panel.</summary>
        /// <param name="callback">Action to invoke upon completion.</param>
        public void Collapse(Action callback)
        {
            if (IsCollapsed) return;
            IsCollapsed = true; // Internal animation is called via the IsCollapsed property logic.
        }
        private void CollapseInternal(Action callback)
        {
            // Store callback.
            if (callback == null) callback = delegate { }; // Dummy callback.
            collapsingCallback = callback;

            // Alert listeners.
            FireCollapsing();
            CollapseAnimation();
        }
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

        #region Internal : Animation
        private void InflateAnimation()
        {
            // Setup initial conditions.
            int finalSize = inflateToTarget == CollapsePanel.NoTargetSize
                                                                                        ? inflatedSize
                                                                                        : inflateToTarget;
            SetContentSize(finalSize - Padding.GetOffset(Plane));

            // Animate.
            Dictionary properties = new Dictionary();
            properties[CssAttribute] = finalSize;
            Container.Animate(
                                properties,
                                Slide.ToMsecs(),
                                Slide.Easing,
                                delegate
                                {
                                    ReleaseContentSize();
                                    FireInflated();
                                });
        }

        private void CollapseAnimation()
        {
            // Store original size.
            inflatedSize = Plane == Plane.Horizontal ? Width : Height;
            FixContentSize();

            // Animate.
            Dictionary properties = new Dictionary();
            properties[CssAttribute] = 0;
            Container.Animate(
                                properties,
                                Slide.ToMsecs(),
                                Slide.Easing,
                                FireCollapsed);
        }
        #endregion

    }
}
