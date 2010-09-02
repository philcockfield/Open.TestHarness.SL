namespace Open.Core.Lists
{
    /// <summary>An item that resides within a List.</summary>
    /// <remarks>
    ///     Objects added to the ListTree do not need to implement this interface.
    ///     The control pulls reads these property values in a late-bound manner.
    ///     This interface provides guidance for what the control is looking for.
    /// </remarks>
    public interface IListItem
    {
        /// <summary>Gets or sets the display text of the node.</summary>
        string Text { get; set; }

        /// <summary>Gets or sets whether the list item is selectable.</summary>
        bool CanSelect { get; set; }

        /// <summary>Gets or sets the SRC for the right-hand icon (if null default icon is used).</summary>
        string RightIconSrc { get; set; }
    }
}