namespace Open.Core
{
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

    /// <summary>Flags representing the various types of HTML list.</summary>
    public enum HtmlListType
    {
        /// <summary>An unordered list <ul></ul>.</summary>
        Unordered = 0,

        /// <summary>An ordered list <ol></ol>.</summary>
        Ordered = 1
    }

}
