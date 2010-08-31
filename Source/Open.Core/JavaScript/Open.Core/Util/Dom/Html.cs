using jQueryApi;

namespace Open.Core
{
    /// <summary>HTML utility.</summary>
    public static class Html
    {
        #region Constants
        // Elements.
        public const string Head = "head";
        public const string Div = "div";
        public const string Span = "span";

        // Attributes.
        public const string Id = "id";
        public const string Href = "href";
        #endregion

        #region Methods
        /// <summary>Creates and appends a DIV element within the given parent.</summary>
        /// <param name="parent">The parent element to insert into</param>
        /// <returns>The inserted Div element.</returns>
        public static jQueryObject AppendDiv(jQueryObject parent) { return Append(parent, Div); }

        /// <summary>Creates and appends a DIV element within the given parent.</summary>
        /// <param name="parent">The parent element to insert into</param>
        /// <param name="tag">The tag name (NOT including angle brackets).</param>
        /// <returns>The inserted element.</returns>
        public static jQueryObject Append(jQueryObject parent, string tag)
        {
            CreateElement(tag).AppendTo(parent);
            return parent.Last().Contents();
        }

        /// <summary>Creates a new element with the given tag.</summary>
        /// <param name="tag">The HTML tag.</param>
        public static jQueryObject CreateElement(string tag)
        {
            return jQuery.FromHtml(string.Format("<{0}></{0}>", tag));
        }
        #endregion
    }
}
