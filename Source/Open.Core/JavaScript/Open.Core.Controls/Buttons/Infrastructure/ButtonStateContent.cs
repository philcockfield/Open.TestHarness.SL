using System;

namespace Open.Core.Controls.Buttons
{
    internal abstract class ButtonStateContent
    {
        #region Head
        protected ButtonStateContent(ButtonState[] states, NullableBool forDisabled, NullableBool forFocused)
        {
            if (Script.IsNullOrUndefined(forDisabled)) forDisabled = NullableBool.Neither;
            if (Script.IsNullOrUndefined(forFocused)) forFocused = NullableBool.Neither;

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
    }
}