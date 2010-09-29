using System;
using System.Runtime.CompilerServices;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Represents display content for a single state.</summary>
    internal class ButtonStateTemplate : ButtonStateContent
    {
        #region Head
        private readonly Template template;

        public ButtonStateTemplate(ButtonState[] states, Template template, NullableBool forDisabled, NullableBool forFocused) : base(states, forDisabled, forFocused)
        {
            // Setup initial conditions.
            if (Script.IsUndefined(template)) template = null;

            // Store values.
            this.template = template;
        }
        #endregion

        #region Properties
        /// <summary>Gets the template that produces the HTML for the state..</summary>
        public Template Template { get { return template; } }
        #endregion
    }
}
