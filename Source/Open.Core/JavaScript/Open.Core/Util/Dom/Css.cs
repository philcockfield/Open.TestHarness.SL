using System;
using System.Html;
using jQueryApi;

namespace Open.Core
{
    /// <summary>CSS utility.</summary>
    public static class Css
    {
        #region Constants
        // Styles.
        public const string Left = "left";
        public const string Right = "right";
        public const string Top = "top";
        public const string Bottom = "bottom";
        public const string Width = "width";
        public const string Height = "height";

        // Units.
        public const string Px = "px";

        // Classes.
        public static readonly CoreCssClasses Classes = new CoreCssClasses();
        #endregion

        #region Methods : Id | Class
        /// <summary>
        ///     Performs a jQuery CSS selection with the given ID 
        ///     (pre-processing the ID format using the ToId() method).
        /// </summary>
        /// <param name="identifier">The ID of the element.</param>
        public static jQueryObject SelectFromId(string identifier)
        {
            return jQuery.Select(ToId(identifier));
        }

        /// <summary>Prepends the # to a CSS identifier if it's not present (eg: id='one' would be '#one').</summary>
        /// <param name="identifier">The ID value.</param>
        public static string ToId(string identifier) { return PrependSelectorPrefix(identifier, "#"); }

        /// <summary>Prepends the period (.) to a CSS class name if it's not present (eg: id='one' would be '.one').</summary>
        /// <param name="class">The class name.</param>
        public static string ToClass(string @class) { return PrependSelectorPrefix(@class, "."); }

        private static string PrependSelectorPrefix(string value, string prefix)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Substr(0, 1) == prefix
                            ? value
                            : prefix + value;
        }
        #endregion

        #region Methods : <link type='text/css' />
        /// <summary>Inserts a CSS link witin the document head.</summary>
        /// <param name="url">The URL of the CSS to load.</param>
        public static void InsertLink(string url)
        {
            jQuery
                .Select(Html.Head)
                .Append(string.Format("<link rel='Stylesheet' href='{0}' type='text/css' />", url));
        }

        /// <summary>Determines whether the specified URL has a link within the page.</summary>
        /// <param name="url">The path to the CSS document to match (not case-sensitive).</param>
        public static bool IsLinked(string url) { return GetLink(url) != null; }

        /// <summary>Retrieves the CSS <link type='text/css' /> with the given source (src) URL.</summary>
        /// <param name="url">The path to the CSS document to match (not case-sensitive).</param>
        /// <returns>The specified CSS link, or null if the link does not exist within the page.</returns>
        public static Element GetLink(string url)
        {
            url = url.ToLowerCase();
            foreach (Element link in jQuery.Select("link[type='text/css']").GetElements())
            {
                ElementAttribute href = link.GetAttributeNode(Html.Href);
                if (Script.IsNullOrUndefined(href)) continue;
                if (url == href.Value.ToLowerCase()) return link;
            }
            return null;
        }
        #endregion
    }

    public class CoreCssClasses
    {
        public readonly string TitleFont = "titleFont";
    }
}
