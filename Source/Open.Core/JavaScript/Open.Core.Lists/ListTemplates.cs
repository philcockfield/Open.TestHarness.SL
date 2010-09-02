using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>HTML templates for lists and list-items.</summary>
    public static class ListTemplates
    {
        /// <summary>Constructs the default HTML for an item in a list.</summary>
        public static jQueryObject DefaultListItem(object model)
        {
            // Setup initial conditions.
            IListItem listItem = model as IListItem;

            // Create the root container.
            jQueryObject divRoot = Html.CreateDiv();

            // Create the label.
            jQueryObject spanLabel = Html.CreateSpan();
            spanLabel.AppendTo(divRoot);

            // Insert right-hand icon.
            string src = listItem == null
                                         ? ListHtml.ChildPointerIcon
                                         : listItem.RightIconSrc ?? ListHtml.ChildPointerIcon;
            jQueryObject img = Html.CreateImage(src, null);
            img.AddClass(ListCss.ItemClasses.IconRight);
            img.AppendTo(divRoot);

            // Apply CSS.
            divRoot.AddClass(ListCss.ItemClasses.DefaultRoot);
            spanLabel.AddClass(ListCss.ItemClasses.Label);
            spanLabel.AddClass(Css.Classes.TitleFont);

            // Finish up.
            return divRoot;
        }
    }
}
