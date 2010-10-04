using System;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Provides common styles for buttons.</summary>
    public static class ButtonStyles
    {
        /// <summary>Adds a silver background to the given button.</summary>
        /// <param name="layer">The layer to assign the content to (typically '0').</param>
        /// <param name="button">The button to effect.</param>
        /// <param name="showForNormal">Flag indicating if the background should be visible for the default (normal) state.</param>
        public static void SilverBackground(int layer, ButtonView button, bool showForNormal)
        {
            // Setup initial conditions.
            ButtonState[] upStates = showForNormal
                                         ? new ButtonState[] { ButtonState.Normal, ButtonState.MouseOver }
                                         : new ButtonState[] { ButtonState.MouseOver };
            ButtonState[] downStates = new ButtonState[] { ButtonState.MouseDown, ButtonState.Pressed };

            // Insert the DIV element that will show the CSS styles.
            button.SetTemplateForStates(layer, ButtonView.AllStates, ButtonTemplates.CommonBg, EnabledCondition.Either, FocusCondition.Either);

            // Apply CSS.
            button.SetCssForStates(layer, upStates, string.Format("{0} {1}", ButtonCss.ClassSilver, ButtonCss.ClassUp));
            button.SetCssForStates(layer, downStates, string.Format("{0} {1}", ButtonCss.ClassSilver, ButtonCss.ClassDown));
            button.SetCssForStates(layer, new ButtonState[] { ButtonState.MouseOver, ButtonState.MouseDown }, ButtonCss.ClassHighlight);
        }
    }
}
