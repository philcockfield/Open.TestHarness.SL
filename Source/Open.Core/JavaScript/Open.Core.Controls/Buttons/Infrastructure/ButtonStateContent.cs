using System;
using System.Runtime.CompilerServices;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Represents display content for a single state.</summary>
    public class ButtonStateContent
    {
        #region Head
        private readonly ButtonState[] states;
        private readonly Template template;
        private readonly string cssClasses;
        private readonly NullableBool forDisabled;
        private readonly NullableBool forFocused;

        [AlternateSignature]
        public extern ButtonStateContent(ButtonState[] states, string cssClasses);

        [AlternateSignature]
        public extern ButtonStateContent(ButtonState[] states, string cssClasses, Template template);

        public ButtonStateContent(ButtonState[] states, string cssClasses, Template template, NullableBool forDisabled, NullableBool forFocused)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(forDisabled)) forDisabled = NullableBool.Neither;
            if (Script.IsNullOrUndefined(forFocused)) forFocused = NullableBool.Neither;
            if (Script.IsUndefined(template)) template = null;

            // Store values.
            this.states = states;
            this.template = template;
            this.cssClasses = cssClasses;
            this.forDisabled = forDisabled;
            this.forFocused = forFocused;
        }
        #endregion

        #region Properties
        /// <summary>Gets the state the content refers to.</summary>
        public ButtonState[] States { get { return states; } }

        /// <summary>Gets the template that produces the HTML for the state..</summary>
        public Template Template { get { return template; } }

        /// <summary>Gets the CSS class (or classes, sepearted by spaces) to apply to the state.</summary>
        public string CssClasses{get { return cssClasses; }}

        /// <summary>Gets whether the content is for a disabled state.</summary>
        public NullableBool ForDisabled { get { return forDisabled; } }

        /// <summary>Gets whether the content is for a focused state.</summary>
        public NullableBool ForFocused { get { return forFocused; } }
        #endregion
    }
}
