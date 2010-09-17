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
        public const string Background = "background";
        public const string Color = "color";
        public const string Display = "display";
        public const string Position = "position";
        public const string Padding = "padding";
        public const string Margin = "margin";
        public const string Overflow = "overflow";
        public const string Opacity = "opacity";
        public const string FontSize = "font-size";
        public const string TextAlign = "text-align";

        // Values.
        public const string Block = "block";
        public const string None = "none";
        public const string Relative = "relative";
        public const string Absolute = "absolute";

        // Units.
        public const string Px = "px";

        // Classes.
        public static readonly CoreCssClasses Classes = new CoreCssClasses();

        // Urls.
        public static readonly CoreCssUrls Urls = new CoreCssUrls();
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

        /// <summary>Adds or removes a class from the given element.</summary>
        /// <param name="element">The element to add or remove from.</param>
        /// <param name="cssClass">The CSS class name.</param>
        /// <param name="add">Flag indicating whether the class should be added (true) or removed (false).</param>
        public static void AddOrRemoveClass(jQueryObject element, string cssClass, bool add)
        {
            if (add)
            {
                element.AddClass(cssClass);
            }
            else
            {
                element.RemoveClass(cssClass);
            }
        }

        /// <summary>Copies the CSS classes from one element to another.</summary>
        /// <param name="source">The source element to copy from.</param>
        /// <param name="target">The target element to copy to.</param>
        public static void CopyClasses(jQueryObject source, jQueryObject target)
        {
            string classes = source.GetAttribute(Html.ClassAttr);
            if (string.IsNullOrEmpty(classes)) return;
            AddClasses(target, classes);
        }

        /// <summary>Adds a single class, or multiple classs from a space seperated list.</summary>
        /// <param name="target">The target element to add to.</param>
        /// <param name="classValue">The class attribute value to apply.</param>
        public static void AddClasses(jQueryObject target, string classValue)
        {
            if (string.IsNullOrEmpty(classValue)) return;
            foreach (string className in classValue.Split(" "))
            {
                target.AddClass(className);
            }
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
            element.CSS(Position, Absolute);
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
            element.CSS(Overflow, value.ToString());
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

        /// <summary>Shows or hides the given element.</summary>
        /// <param name="element">The element to effect.</param>
        /// <param name="isVisible">The desired visibility state.</param>
        public static void SetVisible(jQueryObject element, bool isVisible)
        {
            element.CSS(Display, isVisible ? Block : None);
        }

        /// <summary>Sets the size of the element.</summary>
        /// <param name="element">The element to effect.</param>
        /// <param name="width">The pixel width of the element.</param>
        /// <param name="height">The pixel height of the element.</param>
        public static void SetSize(jQueryObject element, int width, int height)
        {
            element.CSS(Width, width + Px);
            element.CSS(Height, height + Px);
        }

        /// <summary>Applies opacity.</summary>
        /// <param name="element">The element to effect.</param>
        /// <param name="percent">The opacity percentage (0..1).</param>
        public static void SetOpacity(jQueryObject element, double percent)
        {
            percent = Helper.NumberDouble.WithinBounds(percent, 0, 1);
            element.CSS(Opacity, percent.ToString());
        }
        #endregion

        #region Methods : Centering
        /// <summary>Sets the left and top position of an element so it is centered within another element.</summary>
        /// <param name="element">The element to center.</param>
        /// <param name="within">The element to center within.</param>
        public static void Center(jQueryObject element, jQueryObject within)
        {
            CenterHorizontally(element, within);
            CenterVertically(element, within);
        }

        /// <summary>Sets the left position of an element so it is horizontally centered within another element.</summary>
        /// <param name="element">The element to horizontally center.</param>
        /// <param name="within">The element to center within.</param>
        public static void CenterHorizontally(jQueryObject element, jQueryObject within)
        {
            int left = (within.GetWidth() / 2) - (element.GetWidth() / 2);
            element.CSS(Css.Left, left + "px");
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

    public class CoreCssClasses
    {
        public readonly string TitleFont = "titleFont";
        public readonly string AbsoluteFill = "absoluteFill";
    }

    public class CoreCssUrls
    {
        public readonly string Core = "/Open.Core/Css/Core.css";
        public readonly string CoreLists = "/Open.Core/Css/Core.Lists.css";
        public readonly string CoreControls = "/Open.Core/Css/Core.Controls.css";
        public readonly string JitHyperTree = "/Open.Core/Css/Jit.Hypertree.css";
        public readonly string JQueryUi = "http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css";
    }
}
