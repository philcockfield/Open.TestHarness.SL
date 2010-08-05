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
        /// <param name="panelId">The unique identifier of the panel being resized.</param>
        public HorizontalPanelResizer(string panelId) : base(panelId)
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
            jQueryObject panel = GetPanel();
            panel.CSS(Css.Height, String.Empty);
        }
        protected override void OnWindowSizeChanged() { if (IsInitialized) SetMinMaxWidth(); }
        #endregion

        #region Internal
        private void SetMinMaxWidth()
        {
            SetMinWidth();
            SetMaxWidth();
        }

        private void SetMinWidth()
        {
            SetResizeOption("minWidth", MinWidth); 
        }

        private void SetMaxWidth()
        {
            string width = HasRootContainer
                                    ? (GetRootContainer().GetWidth() - MaxWidthMargin).ToString()
                                    : String.Empty;
            SetResizeOption("maxWidth", width);
        }
        #endregion
    }
}
