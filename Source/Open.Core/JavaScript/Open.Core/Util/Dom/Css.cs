using System;
using System.Html;
using jQueryApi;

namespace Open.Core
{
    #region Enumerations
    public enum CssOverflow
    {
        Visible = 0,
        Hidden = 1,
        Scroll = 2,
        Auto = 3,
        Inherit = 4
    }
    #endregion

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
        public const string Background = "background";
        public const string Display = "display";

        // Values.
        public const string Block = "block";
        public const string None = "none";

        // Units.
        public const string Px = "px";

        // Classes.
        public static readonly CoreCssClasses Classes = new CoreCssClasses();
        #endregion

        #region Methods
        /// <summary>Determines whether the element is visible (has any display value other than 'None').</summary>
        /// <param name="element">The element to display.</param>
        public static bool IsVisible(jQueryObject element)
        {
            return Script.IsNullOrUndefined(element) ? false : element.GetCSS(Display).ToLowerCase() != None;
        }
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
        /// <summary>Inserts a CSS link within the document head (only if the CSS is not already present).</summary>
        /// <param name="url">The URL of the CSS to load.</param>
        /// <returns>True if the link was inserted, of False if the link was already present.</returns>
        public static bool InsertLink(string url)
        {
            if (IsLinked(url)) return false;
            jQuery
                .Select(Html.Head)
                .Append(string.Format("<link rel='Stylesheet' href='{0}' type='text/css' />", url));
            return true;
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

        #region Methods : Assign Styles
        /// <summary>Sets the given element to absolute positioning and sets all edges to 0px.</summary>
        /// <param name="element">The element to update.</param>
        public static void AbsoluteFill(jQueryObject element)
        {
            element.CSS("position", "absolute");
            element.CSS(Left, "0px");
            element.CSS(Top, "0px");
            element.CSS(Right, "0px");
            element.CSS(Bottom, "0px");
        }

        /// <summary>Sets the overflow style on the given element.</summary>
        /// <param name="element">The element to update.</param>
        /// <param name="value">The overflow value.</param>
        public static void SetOverflow(jQueryObject element, CssOverflow value)
        {
            element.CSS("overflow", value.ToString());
        }

        /// <summary>Applies a drop shadow.</summary>
        /// <param name="element">The element to update.</param>
        /// <param name="opacity">Opacity of the drop-shadow (005, 010, 020, 030, 040, 050, 060).</param>
        public static void ApplyDropshadow(jQueryObject element, string opacity)
        {
            element.CSS("background-image", string.Format("url(/Open.Core/Images/DropShadow.12.{0}.png)", opacity));
            element.CSS("background-repeat", "repeat-x");
            element.CSS("height", "12px");
        }
        #endregion
    }

    public class CoreCssClasses
    {
        public readonly string TitleFont = "titleFont";
        public readonly string AbsoluteFill = "absoluteFill";
    }
}
