using System;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>Represents a single item within a list.</summary>
    public class ListItemView : ViewBase, IListItemView
    {
        #region Head
        private readonly object model;
        private jQueryObject htmLabel;
        private jQueryObject imgRightIcon;
        private string text;
        private readonly PropertyRef isSelectedRef;

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing element.</param>
        /// <param name="model">The data model for the list item.</param>
        public ListItemView(jQueryObject container, object model)
        {
            // Setup initial conditions.
            this.model = model;
            Initialize(container);

            // Retreive property refs.
            isSelectedRef = PropertyRef.GetFromModel(model, TreeNode.PropIsSelected);

            // Wire up events.
            if (isSelectedRef != null) isSelectedRef.Changed += OnIsSelectedChanged;
            if (ModelAsTreeNode != null) ModelAsTreeNode.ChildrenChanged += OnTreeNodeChildrenChanged;

            // Finish up.
            UpdateVisualState();
        }

        protected override void OnDisposed()
        {
            // Unwire events.
            if (isSelectedRef != null) isSelectedRef.Changed -= OnIsSelectedChanged;
            if (ModelAsTreeNode != null) ModelAsTreeNode.ChildrenChanged -= OnTreeNodeChildrenChanged;

            // Remove from the DOM.
            Container.Remove();

            // Finish up.
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnIsSelectedChanged(object sender, EventArgs e)
        {
            UpdateVisualState();
            FirePropertyChanged(TreeNode.PropIsSelected);
        }

        private void OnTreeNodeChildrenChanged(object sender, EventArgs e)
        {
            UpdateRightIcon();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the data model.</summary>
        public object Model { get { return model; } }
       #endregion

        #region Properties : Data-bound
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                if (htmLabel != null) htmLabel.Html(text);
            }
        }

        public bool IsSelected
        {
            get { return isSelectedRef == null ? false : (bool)isSelectedRef.Value; }
            set
            {
                if (!CanSelect) value = false;
                if (value == IsSelected) return;
                if (isSelectedRef != null) isSelectedRef.Value = value;
            }
        }

        public string RightIconSrc
        {
            get { return imgRightIcon == null ? null : imgRightIcon.GetAttribute(Html.Src); }
            set
            {
                value = value ?? ListHtml.ChildPointerIcon;
                if (value == RightIconSrc) return;
                if (imgRightIcon != null) imgRightIcon.Attribute(Html.Src, value);
                UpdateRightIcon();
            }
        }
        #endregion

        #region Properties : Private
        private IModel ModelAsBindable { get { return Model as IModel; } }
        private IListItem ModelAsListItem { get { return Model as IListItem; } }
        private ITreeNode ModelAsTreeNode { get { return Model as ITreeNode; } }

        private bool CanSelect
        {
            get
            {
                IListItem item = ModelAsListItem;
                return item == null ? true : item.CanSelect;
            }
        }
        #endregion

        #region Methods
        /// <summary>Initializes the list-item.</summary>
        /// <param name="container">The containing <li></li> element.</param>
        protected override void OnInitialize(jQueryObject container)
        {
            // Setup initial conditions.
            container.AddClass(ListCss.ItemClasses.Root);

            // Construct HTML content and insert into DOM.
            string customHtml = GetFactoryHtml();
            jQueryObject content = (customHtml == null)
                                                    ? ListTemplates.DefaultListItem(Model) // No custom HTML supplied, use default.
                                                    : jQuery.FromHtml(customHtml);
            content.AppendTo(container);

            // Retrieve child elements.
            htmLabel = GetChild(content, ListCss.ItemClasses.Label);
            imgRightIcon = GetChild(content, ListCss.ItemClasses.IconRight);

            // Wire up events.
            imgRightIcon.Load(delegate(jQueryEvent @event)
                                  {
                                      UpdateRightIcon();
                                  });

            // Setup databinding.
            SetupBindings();
            UpdateVisualState();
        }
        #endregion

        #region Methods : UpdateVisualState
        /// <summary>Refrehses the visual state of the item.</summary>
        public void UpdateVisualState()
        {
            Css.AddOrRemoveClass(Container, ListCss.ItemClasses.Selected, IsSelected);
            UpdateRightIcon();
        }

        private void UpdateRightIcon()
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(imgRightIcon)) return;

            // Update visibility state.
            bool isVisible = false;
            ITreeNode treeNode = ModelAsTreeNode;
            if (CanSelect && treeNode != null && treeNode.TotalChildren > 0) isVisible = true;
            Css.SetVisible(imgRightIcon, isVisible);

            if (!isVisible) return;
            if (Container.GetHeight() == 0) return;

            // Vertically align the right icon.
            Html.CenterVertically(imgRightIcon, Container);
        }
        #endregion

        #region Internal
        private static jQueryObject GetChild(jQueryObject parent, string cssClass)
        {
            return parent.Children(Css.ToClass(cssClass)).First();  
        }

        private string GetFactoryHtml()
        {
            IHtmlFactory factory = Model as IHtmlFactory;
            return factory == null ? null : factory.CreateHtml();
        }

        private void SetupBindings()
        {
            IModel bindable = ModelAsBindable;
            if (bindable == null) return;

            SetBinding(bindable, ListItem.PropText);
            SetBinding(bindable, ListItem.PropRightIconSrc);
        }

        private void SetBinding(IModel bindable, string propertyName)
        {
            PropertyRef sourceProperty = bindable.GetPropertyRef(propertyName);
            if (sourceProperty != null) GetPropertyRef(propertyName).BindTo = sourceProperty;
        }
        #endregion
    }
}
