namespace Open.Core
{
    /// <summary>A direction on the X plane.</summary>
    public enum HorizontalDirection
    {
        Left = 0,
        Right = 1
    }

    /// <summary>A direction on the Y plane.</summary>
    public enum VerticalDirection
    {
        Up = 0,
        Down = 1,
    }

    /// <summary>Flags representing the width or height of an object.</summary>
    public enum SizeDimension
    {
        Width = 0,
        Height = 1
    }

    /// <summary>The target of an HTML link.</summary>
    public enum LinkTarget
    {
        /// <summary>Load in a new window.</summary>
        Blank = 0,

        /// <summary>Load in the same frame as it was clicked.</summary>
        Self = 1,

        /// <summary>Load in the parent frameset.</summary>
        Parent = 2,

        /// <summary>Load in the full body of the window.</summary>
        Top = 2
    }

    /// <summary>The various kinds of mouse-related states a button can be in.</summary>
    public enum ButtonMouseState
    {
        Normal = 0,
        MouseOver = 1,
        Pressed = 2,
    }
}
