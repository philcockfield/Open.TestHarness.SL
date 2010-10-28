using System;

namespace Open.Core.Controls.Buttons
{
    internal abstract class ButtonStateContent
    {
        #region Head
        protected ButtonStateContent(ButtonState[] states, EnabledCondition enabledCondition, FocusCondition focusCondition)
        {
            if (Script.IsNullOrUndefined(enabledCondition)) enabledCondition =  EnabledCondition.Either;
            if (Script.IsNullOrUndefined(focusCondition)) focusCondition =  FocusCondition.Either;

            States = states;
            EnabledCondition = enabledCondition;
            FocusCondition = focusCondition;
        }
        #endregion

        #region Properties
        /// <summary>Gets the state the content refers to.</summary>
        public readonly ButtonState[] States;

        /// <summary>Gets the enabled-related conditions for which button content applies.</summary>
        public readonly EnabledCondition EnabledCondition;

        /// <summary>Gets the focus-related conditions for which button content applies.</summary>
        public readonly FocusCondition FocusCondition;
        #endregion

        #region Methods
        public bool IsCurrent(ButtonView button)
        {
            // Check that the button-state matches.
            if (!States.Contains(button.State)) return false;

            // Check that the disabled and focused conditions are met.
            if (!FocusConditionMet(button)) return false;
            if (!EnabledConditionMet(button)) return false;

            // Finish up.
            return true;
        }
        #endregion

        #region Internal
        private bool FocusConditionMet(ButtonView button)
        {
            if (FocusCondition == FocusCondition.Either) return true;
            if (FocusCondition == FocusCondition.FocusedOnly && !button.Focus.IsFocused) return false;
            if (FocusCondition == FocusCondition.UnfocusedOnly && button.Focus.IsFocused) return false;
            return true;
        }

        private bool EnabledConditionMet(ButtonView button)
        {
            if (EnabledCondition == EnabledCondition.Either) return true;
            if (EnabledCondition == EnabledCondition.EnabledOnly && !button.IsEnabled) return false;
            if (EnabledCondition == EnabledCondition.DisabledOnly && button.IsEnabled) return false;
            return true;
        }
        #endregion
    }
}