using System;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>Represents a single item within a list.</summary>
    public class ListItemView : ViewBase, IListItemView
    {
        #region Head
        public const string PropText = "Text";
        public const string PropIsSelected = "IsSelected";

        private readonly object model;
        private jQueryObject spanLabel;
        private string text;
        private readonly PropertyRef isSelectedPropertyRef;


        /// <summary>Constructor.</summary>
        /// <param name="liElement">The containing <li></li> element.</param>
        /// <param name="model">The data model for the list item.</param>
        public ListItemView(jQueryObject liElement, object model)
        {
            // Setup initial conditions.
            this.model = model;
            Initialize(liElement);

            // Retreive property.
            isSelectedPropertyRef = PropertyRef.GetFromModel(model, PropIsSelected);

            // Wire up events.
            if (isSelectedPropertyRef != null) isSelectedPropertyRef.Changed += OnIsSelectedChanged;

            // Finish up.
            UpdateVisualState();
        }

        protected override void OnDisposed()
        {
            if (isSelectedPropertyRef != null) isSelectedPropertyRef.Changed -= OnIsSelectedChanged;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnIsSelectedChanged(object sender, EventArgs e)
        {
            UpdateVisualState();
            FirePropertyChanged(PropIsSelected);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the item is currently selected.</summary>
        public bool IsSelected
        {
            get { return isSelectedPropertyRef == null ? false : (bool)isSelectedPropertyRef.Value; }
            set
            {
                if (value == IsSelected) return;
                if (isSelectedPropertyRef != null) isSelectedPropertyRef.Value = value;
            }
        }

        /// <summary>Gets or sets the data model.</summary>
        public object Model { get { return model; } }
        #endregion

        #region Properties : Display
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                if (spanLabel != null) spanLabel.Html(text);
            }
        }
        #endregion

        #region Methods
        /// <summary>Initializes the list-item.</summary>
        /// <param name="container">The containing <li></li> element.</param>
        protected override void OnInitialize(jQueryObject container)
        {
            // Insert HTML.
            spanLabel = Html.CreateElement(Html.Span);
            spanLabel.AppendTo(container);

            // Apply CSS classes.
            container.AddClass(ListCss.Classes.ListItem);
            spanLabel.AddClass(ListCss.Classes.ItemLabel);
            spanLabel.AddClass(Css.Classes.TitleFont);

            // Setup databinding.
            SetupBindings(Model as IModel);
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

        private void SetupBindings(IModel bindable)
        {
            if (bindable == null) return;
            SetBinding(bindable, PropText);
        }

        private void SetBinding(IModel bindable, string propertyName)
        {
            PropertyRef sourceProperty = bindable.GetPropertyRef(propertyName);
            if (sourceProperty != null) GetPropertyRef(propertyName).BindTo = sourceProperty;
        }
        #endregion
    }
}
