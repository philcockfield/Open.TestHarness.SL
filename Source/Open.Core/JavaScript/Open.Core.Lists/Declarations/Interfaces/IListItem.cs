using System;

namespace Open.Core.Lists
{
    /// <summary>Represents a single item within a list.</summary>
    public interface IListItem
    {
        /// <summary>Fires when the item is selected of deselected.</summary>
        event EventHandler SelectionChanged;

        /// <summary>Gets or sets whether the item is currently selected.</summary>
        bool IsSelected { get; set; }

        /// <summary>Gets or sets the data model.</summary>
        object Model { get; }
    }
}
