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
        public static readonly ListItemClasses ItemClasses = new ListItemClasses();
        public static readonly ListClasses Classes = new ListClasses();
        #endregion

        #region Methods
        /// <summary>Inserts the CSS for the 'Open.Core.Lists' library.</summary>
        public static void InsertCss()
        {
            if (isCssInserted) return;
            Css.InsertLink(Url);
            isCssInserted = true;
        }
        #endregion
    }

    public class ListClasses
    {
        public readonly string Root = "c-list";
    }

    public class ListItemClasses
    {
        public readonly string Root = "c-listItem";
        public readonly string DefaultRoot = "c-listItem-default";
        public readonly string Selected = "c-listItem-selected";
        public readonly string Label = "c-listItem-label";
        public readonly string IconRight = "c-listItem-iconRight";
    }
}
