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
        private readonly Template html;
        private readonly string cssClasses;

        public StateContent(ButtonState state, Template html, string cssClasses)
        {
            this.state = state;
            this.html = html;
            this.cssClasses = cssClasses;
        }
        #endregion

        #region Properties
        /// <summary>Gets the state the content refers to.</summary>
        public ButtonState State { get { return state; } }

        /// <summary>Gets the template that produces the HTML for the state..</summary>
        public Template Html { get { return html; } }

        /// <summary>Gets the CSS class (or classes, sepearted by spaces) to apply to the state.</summary>
        public string CssClasses{get { return cssClasses; }}
        #endregion
    }
}
