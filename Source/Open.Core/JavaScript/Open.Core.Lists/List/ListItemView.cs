using System;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>Represents a single item within a list.</summary>
    public class ListItemView : ViewBase, IListItemView
    {
        #region Head
        private readonly object model;
        private bool isSelected;

        /// <summary>Constructor.</summary>
        /// <param name="liElement">The containing <li></li> element.</param>
        /// <param name="model">The data model for the list item.</param>
        public ListItemView(jQueryObject liElement, object model)
        {
            this.model = model;
            Initialize(liElement);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the item is currently selected.</summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (value == isSelected) return;
                isSelected = value;
                UpdateVisualState();
            }
        }

        /// <summary>Gets or sets the data model.</summary>
        public object Model { get { return model; } }
        #endregion

        #region Methods
        /// <summary>Initializes the list-item.</summary>
        /// <param name="container">The containing <li></li> element.</param>
        protected override void OnInitialize(jQueryObject container)
        {
            // Apply CSS classes.
            container.AddClass(ListCss.Classes.ListItem);

            // Insert HTML.
            object text = Type.GetProperty(model, "text");
            jQueryObject html = jQuery.FromHtml(
                                            string.Format("<span>{0}</span>",
                                                                Script.IsNullOrUndefined(text)
                                                                            ? "No Text"
                                                                            : text));
            html.AppendTo(container);

            // Adorn with classes.
            html.AddClass(ListCss.Classes.ItemLabel);
            html.AddClass(Css.Classes.TitleFont);
        }
        #endregion

        #region Internal
        private void UpdateVisualState()
        {
            // Update selection CSS.
            if (IsSelected)
            {
                Container.AddClass(ListCss.Classes.SelectedListItem);
            }
            else
            {
                Container.RemoveClass(ListCss.Classes.SelectedListItem);
            }
        }
        #endregion
    }
}
