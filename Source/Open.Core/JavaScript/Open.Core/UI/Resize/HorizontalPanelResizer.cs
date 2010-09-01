using System;
using jQueryApi;

namespace Open.Core.UI
{
    /// <summary>Controls the resizing of a panel on the X plane.</summary>
    public class HorizontalPanelResizer : PanelResizerBase
    {
        #region Head
        private double minWidth;
        private double maxWidthMargin;

        /// <summary>Constructor.</summary>
        /// <param name="cssSelector">The CSS selector used to retrieve the panel being resized.</param>
        /// <param name="cookieKey">The unique key to store the panel size within (null if saving not required).</param>
        public HorizontalPanelResizer(string cssSelector, string cookieKey) : base(cssSelector, cookieKey)
        {
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the minimum width the panel can be.</summary>
        public double MinWidth
        {
            get { return minWidth; }
            set
            {
                if (value == minWidth) return;
                minWidth = value;
                SetMinWidth();
            }
        }

        /// <summary>Gets or sets the margin buffer used to calculate the max-width of the panel relative to the root container.</summary>
        public double MaxWidthMargin
        {
            get { return maxWidthMargin; }
            set { maxWidthMargin = value; }
        }

        private double RootContainerWidth
        {
            get { return HasRootContainer ? GetRootContainer().GetWidth() : -1; }
        }

        private double MaxWidth
        {
            get { return HasRootContainer ? RootContainerWidth - MaxWidthMargin : -1; }
        }
        #endregion

        #region Methods
        protected override string GetHandles()
        {
            return "e"; // East.
        }

        protected override void OnInitialize() { SetMinMaxWidth(); }
        protected override void OnStopped()
        {
            // Clear the height value (which assigned during the resize).
            Panel.CSS(Css.Height, String.Empty);
        }
        protected override void OnWindowSizeChanged()
        {
            if (!IsInitialized) return;
            SetMinMaxWidth();

            // Shrink the panel if the window is too small.
            if (HasRootContainer)
            {
                ShrinkIfOverflowing(GetCurrentSize(), MinWidth, MaxWidth, Css.Width);
            }
        }

        protected override double GetCurrentSize() { return Panel.GetWidth(); }
        protected override void SetCurrentSize(double size)
        {
            Panel.CSS(Css.Width, size + Css.Px);
        }

        protected override void FireResized()
        {
            base.FireResized();
            GlobalEvents.FireHorizontalPanelResized(this);
        }
        #endregion

        #region Internal
        private void SetMinMaxWidth()
        {
            SetMinWidth();
            SetMaxWidth();
        }

        private void SetMinWidth()
        {
            SetResizeOption("minWidth", MinWidth.ToString()); 
        }

        private void SetMaxWidth()
        {
            string width = HasRootContainer
                                    ? MaxWidth.ToString()
                                    : String.Empty;
            SetResizeOption("maxWidth", width);
        }
        #endregion
    }
}
