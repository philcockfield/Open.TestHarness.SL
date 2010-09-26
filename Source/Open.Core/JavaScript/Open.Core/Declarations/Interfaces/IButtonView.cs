namespace Open.Core
{
    /// <summary>The visual control for a clickable button.</summary>
    public interface IButtonView
    {
        /// <summary>Gets the logical model of the button.</summary>
        IButton Model { get; }

        /// <summary>Gets the current mouse state of the button (has bool analogs, see IsPressed etc).</summary>
        ButtonState State { get; }

        /// <summary>Gets whether the mouse is currently over the button.</summary>
        bool IsMouseOver { get; }

        /// <summary>Gets whether the mouse is currently pressing the button.</summary>
        bool IsMouseDown { get; }
    }
}
