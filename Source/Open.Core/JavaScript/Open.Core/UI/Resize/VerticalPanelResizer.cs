using System;

namespace Open.Core.UI
{
    /// <summary>Controls the resizing of a panel on the Y plane.</summary>
    public class VerticalPanelResizer : PanelResizerBase
    {
        #region Head
        private int minHeight;
        private int maxHeightMargin;

        /// <summary>Constructor.</summary>
        /// <param name="cssSelector">The CSS selector used to retrieve the panel being resized.</param>
        /// <param name="cookieKey">The unique key to store the panel size within (null if saving not required).</param>
        public VerticalPanelResizer(string cssSelector, string cookieKey) : base(cssSelector, cookieKey)
        {
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the minimum height the panel can be.</summary>
        public int MinHeight
        {
            get { return minHeight; }
            set
            {
                if (value == minHeight) return;
                minHeight = value;
                SetMinHeight();
            }
        }

        /// <summary>Gets the maximum height the panel can be.</summary>
        public int MaxHeight
        {
            get { return HasRootContainer ? RootContainerHeight - MaxHeightMargin : -1; }
        }

        /// <summary>Gets or sets the margin buffer used to calculate the max-height of the panel relative to the root container.</summary>
        public int MaxHeightMargin
        {
            get { return maxHeightMargin; }
            set { maxHeightMargin = value; }
        }

        private int RootContainerHeight
        {
            get { return HasRootContainer ? GetRootContainer().GetHeight() : -1; }
        }
        #endregion

        #region Methods
        protected override string GetHandles()
        {
            return "n"; // North.
        }

        protected override void OnInitialize() { SetMinMaxHeight(); }
        protected override void OnStopped()
        {
            // Clear the width and top values (which assigned during the resize).
            Panel.CSS(Css.Width, String.Empty);
            Panel.CSS(Css.Top, String.Empty);
        }
        protected override void OnWindowResize()
        {
            if (!IsInitialized) return;
            SetMinMaxHeight();

            // Shrink the panel if the window is too small.
            if (HasRootContainer)
            {
                ShrinkIfOverflowing(GetCurrentSize(), MinHeight, MaxHeight, Css.Height);
            }
        }

        protected override int GetCurrentSize() { return Panel.GetHeight(); }
        protected override void SetCurrentSize(int size)
        {
            Panel.CSS(Css.Height, size + Css.Px);
        }

        protected override void FireResized()
        {
            base.FireResized();
            GlobalEvents.FireVerticalPanelResized(this);
        }
        #endregion

        #region Internal
        private void SetMinMaxHeight()
        {
            SetMinHeight();
            SetMaxHeight();
        }

        private void SetMinHeight()
        {
            SetResizeOption("minHeight", MinHeight.ToString());
        }

        private void SetMaxHeight()
        {
            string height = HasRootContainer
                                    ? MaxHeight.ToString()
                                    : String.Empty;
            SetResizeOption("maxHeight", height);
        }
        #endregion
    }
}
