using System;

namespace Open.Core
{
    /// <summary>Focus options for a visual control.</summary>
    public interface IFocus
    {
        /// <summary>Fires when the control recieves keyboard focus.</summary>
        event EventHandler GotFocus;

        /// <summary>Fires when the control loses keyboard focus (blur).</summary>
        event EventHandler LostFocus;

        /// <summary>Gets or sets whether the control is currently focused.</summary>
        bool IsFocused { get; }

        /// <summary>Gets or sets whether the control can recieve keyboard focus.</summary>
        /// <remarks>This causes the 'TabIndex' property to change.</remarks>
        bool CanFocus { get; set; }

        /// <summary>Gets or sets whether the automatic browser highlighting is to be used.</summary>
        /// <remarks>CSS: output</remarks>
        bool BrowserHighlighting { get; set; }

        /// <summary>Gets or sets the index of the control within the keyboard tab order.</summary>
        /// <remarks>-1:Not in tab order. 0:In tab order (source order). >0: Explicit ordering (overrides source order).</remarks>
        int TabIndex { get; set; }

        /// <summary>Gives keyboard focus to the control (see also: CanFocus, IsFocused, TabIndex properties).</summary>
        /// <returns>True if the control can recieve focus (and therefore focus was applied), otherwise False.</returns>
        bool Apply();

        /// <summary>Removes keyboard focus to the control (see also: CanFocus, IsFocused, TabIndex properties).</summary>
        /// <returns>True if the control can recieve focus (and therefore focus was removed), otherwise False.</returns>
        bool Blur();
    }
}
