using System;
using jQueryApi;

namespace Open.Core.UI
{
    /// <summary>Controls the resizing of a panel on the Y plane.</summary>
    public class VerticalPanelResizer : PanelResizerBase
    {
        #region Head
        private double minHeight;
        private double maxHeightMargin;

        /// <summary>Constructor.</summary>
        /// <param name="panelId">The unique identifier of the panel being resized.</param>
        /// <param name="cookieKey">The unique key to store the panel size within (null if saving not required).</param>
        public VerticalPanelResizer(string panelId, string cookieKey) : base(panelId, cookieKey)
        {
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the minimum height the panel can be.</summary>
        public double MinHeight
        {
            get { return minHeight; }
            set
            {
                if (value == minHeight) return;
                minHeight = value;
                SetMinHeight();
            }
        }

        /// <summary>Gets or sets the margin buffer used to calculate the max-height of the panel relative to the root container.</summary>
        public double MaxHeightMargin
        {
            get { return maxHeightMargin; }
            set { maxHeightMargin = value; }
        }

        private double RootContainerHeight
        {
            get { return HasRootContainer ? GetRootContainer().GetHeight() : -1; }
        }

        private double MaxHeight
        {
            get { return HasRootContainer ? RootContainerHeight - MaxHeightMargin : -1; }
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
            jQueryObject panel = GetPanel();
            panel.CSS(Css.Width, String.Empty);
            panel.CSS(Css.Top, String.Empty);
        }
        protected override void OnWindowSizeChanged()
        {
            if (!IsInitialized) return;
            SetMinMaxHeight();

            // Shrink the panel if the window is too small.
            if (HasRootContainer)
            {
                ShrinkIfOverflowing(GetPanel(), GetCurrentSize(), MinHeight, MaxHeight, Css.Height);
            }
        }

        protected override double GetCurrentSize() { return GetPanel().GetHeight(); }
        protected override void SetCurrentSize(double size)
        {
            GetPanel().CSS(Css.Height, size + Css.Px);
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
