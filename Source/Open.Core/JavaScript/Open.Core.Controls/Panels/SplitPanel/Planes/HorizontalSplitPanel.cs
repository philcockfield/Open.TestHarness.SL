using System;
using jQueryApi;

namespace Open.Core.Controls
{
    /// <summary>A split-panel on the horizontal plane (left and right panes).</summary>
    public class HorizontalSplitPanel : SplitPanel
    {
        #region Head
        private readonly HorizontalEdge fillPane;
        private readonly jQueryObject divLeft;
        private readonly jQueryObject divRight;

        /// <summary>Constructor.</summary>
        /// <param name="container">The root HTML element of the control (if null a <DIV></DIV> is generated).</param>
        /// <param name="fillPane">Flag indicating which pane is the filler pane (assumes the size not taken up by the other pane).</param>
        internal HorizontalSplitPanel(jQueryObject container, HorizontalEdge fillPane) : base(container)
        {
            // Store values.
            this.fillPane = fillPane;

            // Construct panes.
            divLeft = CreateDiv();
            divRight = CreateDiv();

            // Setup common panel CSS.
            divLeft.CSS(Css.Left, "0px");
            divRight.CSS(Css.Right, "0px");

            // Insert into DOM.
            Container.Empty();
            divLeft.AppendTo(Container);
            divRight.AppendTo(Container);

            // Finish up.
            UpdateLayout();
        }
        #endregion

        #region Properties : Public
        /// <summary>Gets the flag indicating which pane is the filler pane (assumes the size not taken up by the other pane).</summary>
        public HorizontalEdge FillPane { get { return fillPane; } }

        /// <summary>Gets the DIV element used for the left-hand pane.</summary>
        public jQueryObject DivLeft { get { return divLeft; } }

        /// <summary>Gets the DIV element usd for the right-hand pane.</summary>
        public jQueryObject DivRight { get { return divRight; } }
        #endregion

        #region Properties : Internal
        protected override jQueryObject FillElement
        {
            get { return FillPane == HorizontalEdge.Left ? DivLeft : DivRight; }
        }

        protected override jQueryObject FixedElement
        {
            get { return FillPane == HorizontalEdge.Left ? DivRight : DivLeft; }
        }
        #endregion

        #region Methods
        protected override void OnUpdateLayout()
        {
            UpdateWidths();
            base.OnUpdateLayout();
        }
        #endregion

        #region Internal
        private static jQueryObject CreateDiv()
        {
            jQueryObject div = Html.CreateDiv();
            div.CSS(Css.Position, Css.Absolute);
            div.CSS(Css.Top, "0px");
            div.CSS(Css.Bottom, "0px");
            return div;
        }

        private void UpdateWidths()
        {
            FillElement.CSS(
                    FillPane == HorizontalEdge.Left ? Css.Right : Css.Left, 
                    FixedElement.GetWidth() + Css.Px);
        }
        #endregion
    }
}
