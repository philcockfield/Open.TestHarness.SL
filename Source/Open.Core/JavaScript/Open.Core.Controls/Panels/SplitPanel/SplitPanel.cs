using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Controls
{
    /// <summary>The base class for a panel that can be split in two (with one side fixed size, and the other fill-size).</summary>
    public abstract class SplitPanel : ViewBase
    {
        #region Head
        /// <summary>Constructor.</summary>
        /// <param name="container">The root HTML element of the control (if null a <DIV></DIV> is generated).</param>
        protected SplitPanel(jQueryObject container) : base(container)
        {
        }
        #endregion

        #region Properties
        /// <summary>Gets the DIV element that fills the available space.</summary>
        protected abstract jQueryObject FillElement { get; }

        /// <summary>Gets the DIV element that is of a fixed size.</summary>
        protected abstract jQueryObject FixedElement { get; }
        #endregion

        #region Methods : Static
        public static HorizontalSplitPanel CreateHorizontal(jQueryObject container, HorizontalEdge fillPane)
        {
            return new HorizontalSplitPanel(container, fillPane);
        }
        #endregion
    }
}
