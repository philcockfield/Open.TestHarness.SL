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

    /// <summary>Edges of an element.</summary>
    public enum Edge
    {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3
    }

    /// <summary>Flags representing the width or height of an object.</summary>
    public enum SizeDimension
    {
        Width = 0,
        Height = 1
    }

    /// <summary>Flags indicating the various strategies for inserting content.</summary>
    public enum InsertMode
    {
        /// <summary>The target element is replaced with the inserted content.</summary>
        Replace = 0
    }

    /// <summary>The various kinds of mouse-related states a button can be in.</summary>
    public enum ButtonState
    {
        Normal = 0,
        MouseOver = 1,
        MouseDown = 2,
        Pressed = 3,
    }

    /// <summary>A boolean value that can also be null.</summary>
    public enum NullableBool
    {
        No = 0,
        Yes = 1,
        Nothing = 2,
    }
}
