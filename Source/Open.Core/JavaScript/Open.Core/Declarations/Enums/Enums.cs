namespace Open.Core
{
    /// <summary>Edges of an element.</summary>
    public enum Edge
    {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3
    }

    /// <summary>Edges on the horizontal plane.</summary>
    public enum HorizontalEdge
    {
        Left = 0,
        Right = 1
    }

    /// <summary>Edges on the vertical plane.</summary>
    public enum VerticalEdge
    {
        Top = 0,
        Bottom = 1,
    }

    /// <summary>Flags representing the vertical or horizontal planes.</summary>
    public enum Plane
    {
        Horizontal = 0,
        Vertical = 1,
    }

    /// <summary>Flags representing the width or height of an object.</summary>
    public enum SizeDimension
    {
        Width = 0,
        Height = 1
    }

    /// <summary>Alignment on the X plane.</summary>
    public enum HorizontalAlign
    {
        Left = 0,
        Center = 1,
        Right = 2,
    }

    /// <summary>Alignment on the Y plane.</summary>
    public enum VerticalAlign
    {
        Top = 0,
        Center = 1,
        Bottom = 2,
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
