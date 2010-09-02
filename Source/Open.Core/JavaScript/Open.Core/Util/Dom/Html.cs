using jQueryApi;

namespace Open.Core
{
    /// <summary>HTML constants and DOM manipulation.</summary>
    public static class Html
    {
        #region Constants
        // Elements.
        public const string Head = "head";
        public const string Div = "div";
        public const string Span = "span";
        public const string Img = "img";

        // Attributes.
        public const string Id = "id";
        public const string Href = "href";
        public const string Src = "src";

        // Properties.
        public const string ScrollTop = "scrollTop";
        public const string ScrollHeight = "scrollHeight";

        // Events.
        public const string Click = "click";
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

        /// <summary>Creates a DIV element.</summary>
        public static jQueryObject CreateDiv() { return CreateElement(Div); }

        /// <summary>Creates a SPAN element.</summary>
        public static jQueryObject CreateSpan() { return CreateElement(Span); }

        /// <summary>Creates an IMG element.</summary>
        /// <param name="src">The URL to the image.</param>
        /// <param name="alt">The alternative text for the image.</param>
        public static jQueryObject CreateImage(string src, string  alt)
        {
            return jQuery.FromHtml(string.Format("<img src='{0}' alt='{1}' />", src, alt));
        }

        /// <summary>Creates a new element with the given tag.</summary>
        /// <param name="tag">The HTML tag.</param>
        public static jQueryObject CreateElement(string tag)
        {
            return jQuery.FromHtml(string.Format("<{0}></{0}>", tag));
        }

        /// <summary>Sets the top position of an element so it is vertically centered within another element.</summary>
        /// <param name="element">The element to vertically center.</param>
        /// <param name="within">The element to center within.</param>
        public static void CenterVertically(jQueryObject element, jQueryObject within)
        {
            int top = (within.GetHeight() / 2) - (element.GetHeight() / 2);
            element.CSS(Css.Top, top + "px");
        }
        #endregion
    }
}
