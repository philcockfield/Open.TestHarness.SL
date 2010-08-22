using System;
using System.Collections;

namespace Open.Core.Lists
{
    /// <summary>CSS declarations for lists.</summary>
    public static class ListCss
    {
        #region Head
        public const string Url = "/Open.Core/Css/Core.Lists.css";
        private static bool isCssInserted;
        #endregion

        #region Properties
        public static readonly ListCssClasses Classes = new ListCssClasses();
        #endregion

        #region Methods
        /// <summary>Inserts the CSS for the 'Open.Core.Lists' library.</summary>
        public static void InsertCss()
        {
            if (isCssInserted) return;
            if (!Css.IsLinked(Url)) Css.InsertLink(Url);
            isCssInserted = true;
        }
        #endregion
    }

    public class ListCssClasses
    {
        public readonly string ListItem = "coreListItem";
        public readonly string SelectedListItem = "selectedListItem";
        public readonly string ItemLabel = "itemLabel";
    }
}
