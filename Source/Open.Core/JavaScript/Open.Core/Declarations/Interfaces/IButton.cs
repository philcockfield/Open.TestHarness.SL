using System;
using System.Collections;

namespace Open.Core
{
    /// <summary>A clickable button.</summary>
    public interface IButton
    {
        #region Events
        /// <summary>Fires when the button is clicked.</summary>
        event EventHandler Click;

        /// <summary>Fires when the IsPressed state changes.</summary>
        event EventHandler IsPressedChanged;
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the control is enabled.</summary>
        bool IsEnabled { get; set; }

        /// <summary>Gets or sets whether the button retains it's state on each click.</summary>
        bool CanToggle { get; set; }

        /// <summary>
        ///     Gets or sets whether the button is currently pressed 
        ///     (see also IsMouseDown.  Relevant when IsToggleButton is True).
        /// </summary>
        bool IsPressed { get; set; }

        /// <summary>Gets the data used to render the template.</summary>
        Dictionary TemplateData { get; }
        #endregion

        #region Methods
        /// <summary>Programaticaly invokes a click.</summary>
        /// <param name="force">Flag indicating if the click action should be forced (even if the button is disabled).</param>
        /// <remarks>Typically used for programmatic testing.</remarks>
        void InvokeClick(bool force);
        #endregion
    }
}
