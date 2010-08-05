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
        public VerticalPanelResizer(string panelId) : base(panelId)
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
        protected override void OnWindowSizeChanged() { if (IsInitialized) SetMinMaxHeight(); }
        #endregion

        #region Internal
        private void SetMinMaxHeight()
        {
            SetMinHeight();
            SetMaxHeight();
        }

        private void SetMinHeight()
        {
            SetResizeOption("minHeight", MinHeight);
        }

        private void SetMaxHeight()
        {
            string width = HasRootContainer
                                    ? (GetRootContainer().GetHeight() - MaxHeightMargin).ToString()
                                    : String.Empty;
            SetResizeOption("maxHeight", width);
        }
        #endregion
    }
}
