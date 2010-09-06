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

        /// <summary>Retrieves the child at the specified index, otherwise Null.</summary>
        /// <param name="index">The index of the child (0-based).</param>
        /// <param name="parent">The parent to look within.</param>
        public static jQueryObject ChildAt(int index, jQueryObject parent)
        {
            jQueryObject element = parent.Children(string.Format(":nth-child({0})", index + 1));
            return element.Length == 0 ? null : element;
        }

        /// <summary>Gets the elements ID, creating a unique ID of the element doesn't already have one.</summary>
        /// <param name="element">The element to get the ID for.</param>
        public static string GetOrCreateId(jQueryObject element)
        {
            string id = element.GetAttribute(Id);
            if (string.IsNullOrEmpty(id))
            {
                id = Helper.CreateId();
                element.Attribute(Id, id);
            }
            return id;
        }

        /// <summary>Formats the an hyperlink.</summary>
        /// <param name="url">The url to link to.</param>
        /// <param name="text">The display text of the link (null to use the URL).</param>
        /// <param name="target">The target attribute.</param>
        public static string ToHyperlink(string url, string text, LinkTarget target)
        {
            if (text == null) text = url;
            return string.Format("<a href='{0}' target='_{2}'>{1}</a>", url, text, target.ToString());
        }

        /// <summary>Wraps the given text in <b></b> elements.</summary>
        /// <param name="text">The text to wrap.</param>
        public static string ToBold(string text) { return string.Format("<b>{0}</b>", text); }

        /// <summary>Creates a SPAN element with a magin-left set to the specified pixels (useful for indenting text).</summary>
        /// <param name="pixels">The number of pixels to indent.</param>
        public static string SpanIndent(int pixels) { return string.Format("<span style='margin-left:{0}px;'></span>", pixels); }

        /// <summary>Retrieves the width of the specified element.</summary>
        /// <param name="cssSelector">The CSS selector of the element to measure.</param>
        public static int Width(string cssSelector) { return jQuery.Select(cssSelector).GetWidth(); }

        /// <summary>Retrieves the height of the specified element.</summary>
        /// <param name="cssSelector">The CSS selector of the element to measure.</param>
        public static int Height(string cssSelector) { return jQuery.Select(cssSelector).GetHeight(); }

        #endregion
    }
}
