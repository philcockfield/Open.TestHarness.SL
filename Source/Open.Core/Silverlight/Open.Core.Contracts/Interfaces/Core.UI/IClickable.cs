using System;

namespace Open.Core.UI
{
    /// <summary>An element (namely a button) which is clickable.</summary>
    public interface IClickable
    {
        /// <summary>Fires when the button is clicked.</summary>
        event EventHandler Click;

        /// <summary>Programaticaly invokes a click.</summary>
        /// <remarks>Typically used for programmatic testing.</remarks>
        void InvokeClick();
    }
}