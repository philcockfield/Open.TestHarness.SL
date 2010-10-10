namespace Open.Core
{
    public enum CssOverflow
    {
        Visible = 0,
        Hidden = 1,
        Scroll = 2,
        Auto = 3,
        Inherit = 4
    }

    public enum CssTextAlign
    {
        Left = 0,
        Right = 1,
        Center = 2,
        Justify = 3
    }

    public enum CssPosition
    {
        /// <summary>
        ///     Default (Static). No position, the element occurs in the normal flow 
        ///     (ignores any top, bottom, left, right, or z-index declarations).
        /// </summary>
        None = 0, 

        /// <summary>
        ///     Generates a relatively positioned element, positioned relative to its normal position, 
        ///     so "left:20" adds 20 pixels to the element's LEFT position.
        /// </summary>
        Relative = 1,

        /// <summary>
        ///     Generates an absolutely positioned element, positioned relative to the first parent element that 
        ///     has a position other than static. The element's position is specified with the "left", "top", "right", 
        ///     and "bottom" properties.
        /// </summary>
        Absolute = 2,

        /// <summary>
        ///     Generates an absolutely positioned element, positioned relative to the browser window. 
        ///     The element's position is specified with the "left", "top", "right", and "bottom" properties.
        /// </summary>
        Fixed = 3,

        /// <summary>
        ///     Specifies that the value of the position property should be inherited from the parent element.
        /// </summary>
        Inherit = 4
    }
}
