using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Represents display content for a single state.</summary>
    internal class StateContent
    {
        #region Head
        private readonly ButtonState state;
        private readonly jQueryObject html;
        private readonly string cssClasses;

        public StateContent(ButtonState state, jQueryObject html, string cssClasses)
        {
            this.state = state;
            this.html = html;
            this.cssClasses = cssClasses;
        }
        #endregion

        #region Properties
        public ButtonState State { get { return state; } }
        public jQueryObject Html{get { return html; }}
        public string CssClasses{get { return cssClasses; }}
        #endregion
    }
}
