using System;

namespace Open.Core.Controls.Buttons
{
    internal abstract class ButtonStateContent
    {
        #region Head
        protected ButtonStateContent(ButtonState[] states, NullableBool forDisabled, NullableBool forFocused)
        {
            if (Script.IsNullOrUndefined(forDisabled)) forDisabled = NullableBool.Nothing;
            if (Script.IsNullOrUndefined(forFocused)) forFocused = NullableBool.Nothing;

            States = states;
            ForDisabled = forDisabled;
            ForFocused = forFocused;
        }
        #endregion

        #region Properties
        /// <summary>Gets the state the content refers to.</summary>
        public readonly ButtonState[] States;

        /// <summary>Gets whether the content is for a disabled state.</summary>
        public readonly NullableBool ForDisabled;

        /// <summary>Gets whether the content is for a focused state.</summary>
        public readonly NullableBool ForFocused;
        #endregion

        #region Methods
        public bool IsCurrent(ButtonView button)
        {
            // Check that the button-state matches.
            if (!States.Contains(button.State)) return false;

            // Check that the disabled and focused conditions are met.
            if (ForDisabled == NullableBool.Yes && button.IsEnabled) return false;
            if (ForFocused == NullableBool.Yes && !button.Focus.IsFocused) return false;

            // Finish up.
            return true;
        }
        #endregion
    }
}