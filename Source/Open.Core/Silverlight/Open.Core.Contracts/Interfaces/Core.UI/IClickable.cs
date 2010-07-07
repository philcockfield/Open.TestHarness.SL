using System;

namespace Open.Core.UI
{
    /// <summary>An element (namely a button) which is clickable.</summary>
    public interface IClickable
    {
        /// <summary>Fires when the button is clicked.</summary>
        event EventHandler Click;

        /// <summary>Programaticaly invokes a click.</summary>
        /// <param name="force">Flag indicating if the click action should be forced (even if the button is disabled).</param>
        /// <remarks>Typically used for programmatic testing.</remarks>
        void InvokeClick(bool force = false);
    }
}