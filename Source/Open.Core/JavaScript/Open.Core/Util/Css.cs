using jQueryApi;

namespace Open.Core
{
    /// <summary>CSS constants.</summary>
    public static class Css
    {
        #region Constants
        public const string Left = "left";
        public const string Right = "right";
        public const string Top = "top";
        public const string Bottom = "bottom";
        public const string Width = "width";
        public const string Height = "height";

        public const string Px = "px";
        #endregion

        #region Methods
        /// <summary>Prepends the # to a CSS identifier (eg: id='one' would be '#one').</summary>
        /// <param name="identifier">The ID value.</param>
        public static string ToId(string identifier)
        {
            if (string.IsNullOrEmpty(identifier)) return identifier;
            return identifier.Substr(0, 1) == "#" 
                            ? identifier 
                            : "#" + identifier;
        }

        /// <summary>
        ///     Performs a jQuery CSS selection with the given ID 
        ///     (pre-processing the ID format using the ToId() method).
        /// </summary>
        /// <param name="identifier">The ID of the element.</param>
        public static jQueryObject SelectFromId(string identifier)
        {
            return jQuery.Select(ToId(identifier));
        }

        /// <summary>Inserts a CSS link witin the document head.</summary>
        /// <param name="url">The URL of the CSS to load.</param>
        public static void Insert(string url)
        {
            string link = string.Format("<link rel='Stylesheet' href='{0}' type='text/css' />", url);
            jQuery.Select("head").Append(link);
        }
        #endregion
    }
}
