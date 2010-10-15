using System;
using System.Runtime.CompilerServices;

namespace Open.Core.Controls
{
    /// <summary>A panel that can collapse.</summary>
    public class CollapsePanel : ModelBase, IViewFactory
    {
        #region Events
        /// <summary>Fires when the panels starts collapsing.</summary>
        public event EventHandler Collapsing;
        private void FireCollapsing() { if (Collapsing != null) Collapsing(this, new EventArgs()); }

        /// <summary>Fires when the panel has finished collapsing.</summary>
        public event EventHandler Collapsed;
        internal void FireCollapsed()
        {
            Helper.Invoke(collapsingCallback);
            collapsingCallback = null;
            if (Collapsed != null) Collapsed(this, new EventArgs());
        }

        /// <summary>Fires when the panel starts inflating (uncollapsing).</summary>
        public event EventHandler Inflating;
        private void FireInflating(){if (Inflating != null) Inflating(this, new EventArgs());}

        /// <summary>Fires when the panel has finished inflating (uncollapsed).</summary>
        public event EventHandler Inflated;
        internal void FireInflated()
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
        
        private const bool DefaultIsCollapsed = false;
        internal const int NoTargetSize = -1;

        private Action collapsingCallback;
        private Action inflatingCallback;
        private AnimationSettings slide;
        internal int inflateToTarget;
        #endregion

        #region Properties
        /// <summary>Gets the slide animation settings.</summary>
        public AnimationSettings Slide { get { return slide ?? (slide = new AnimationSettings()); } }

        /// <summary>Gets or sets whether the panel is collapsed or not.</summary>
        public bool IsCollapsed
        {
            get { return (bool)Get(PropIsCollapsed, DefaultIsCollapsed); }
            set
            {
                if (Set(PropIsCollapsed, value, DefaultIsCollapsed))
                {
                    if (value) { HideInternal(null); } else { InflateInternal(null); } // Show or hide if value has changed.
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
        }

        /// <summary>Collapses the panel.</summary>
        /// <param name="callback">Action to invoke upon completion.</param>
        public void Collapse(Action callback)
        {
            if (IsCollapsed) return;
            IsCollapsed = true; // Internal animation is called via the IsCollapsed property logic.
        }
        private void HideInternal(Action callback)
        {
            // Store callback.
            if (callback == null) callback = delegate { }; // Dummy callback.
            collapsingCallback = callback;

            // Alert listeners.
            FireCollapsing();
        }

        public IView CreateView() { return new CollapsePanelView(this); }
        #endregion
    }
}
