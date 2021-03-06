using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>The visual control for a clickable button.</summary>
    public interface IButtonView
    {
        /// <summary>Gets the element that the control is contained within.</summary>
        jQueryObject Container { get; }

        /// <summary>Gets the logical model of the button.</summary>
        IButton Model { get; }

        /// <summary>Gets the current mouse state of the button (has bool analogs, see IsPressed etc).</summary>
        ButtonState State { get; }

        /// <summary>Gets whether the mouse is currently over the button.</summary>
        bool IsMouseOver { get; }

        /// <summary>Gets whether the mouse is currently pressing the button.</summary>
        bool IsMouseDown { get; }

        /// <summary>Finds the element at the given CSS selector and replaces it with this button.</summary>
        /// <param name="cssSeletor">The CSS selector of the element to replace.</param>
        /// <param name="mode">The strategy used for the insertion.</param>
        void Insert(string cssSeletor, InsertMode mode);
    }
}
