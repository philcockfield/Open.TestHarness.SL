namespace Open.Core.Controls.Buttons
{
    /// <summary>Represents CSS class(es) for a single state.</summary>
    internal class ButtonStateCss : ButtonStateContent
    {
        #region Head
        private readonly string cssClasses;

        public ButtonStateCss(ButtonState[] states, string cssClasses, EnabledCondition enabledCondition, FocusCondition focusCondition)
            : base(states, enabledCondition, focusCondition)
        {
            this.cssClasses = cssClasses;
        }
        #endregion

        #region Properties
        /// <summary>Gets the CSS class (or classes, sepearted by spaces) to apply to the state.</summary>
        public string CssClasses{get { return cssClasses; }}
        #endregion
    }
}
