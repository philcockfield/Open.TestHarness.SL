using System;
using System.Collections;
using System.Html;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>Flags indicating how items within a list are selected.</summary>
    public enum ListSelectionMode
    {
        /// <summary>Items are not selectable.</summary>
        None = 0,

        /// <summary>Only one item at a time can be selected.</summary>
        Single = 1,
    }


    /// <summary>Renders a simple list.</summary>
    public class ListView : ViewBase
    {
        #region Head
        private ListItemFactory itemFactory;
        private ListSelectionMode selectionMode = ListSelectionMode.Single;
        private readonly ArrayList views = new ArrayList();

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing element.</param>
        public ListView(jQueryObject container)
        {
            Initialize(container);
            ListCss.InsertCss();
        }
        #endregion

        #region Event Handlers
        private void OnItemClick(jQueryEvent e, IListItemView view)
        {
            if (SelectionMode == ListSelectionMode.None) return;
            view.IsSelected = true;
        }

        private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.Property.Name == TreeNode.PropIsSelected)
            {
                IListItemView view = sender as IListItemView;
                if (view != null && view.IsSelected) SelectItem(view);
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the factory that creates each item in the list.</summary>
        private ListItemFactory ItemFactory
        {
            get { return itemFactory ?? (itemFactory = new ListItemFactory()); }
        }

        /// <summary>Gets or sets whether items within the list are selecable.</summary>
        public ListSelectionMode SelectionMode
        {
            get { return selectionMode; }
            set { selectionMode = value; }
        }

        /// <summary>Gets the number of items currently in the list.</summary>
        public int Count { get { return views.Count; } }
        #endregion

        #region Methods
        /// <summary>Loads the collection of models into the list.</summary>
        /// <param name="items">A collection models.</param>
        public void Load(IEnumerable items)
        {
            // Setup initial conditions.
            Clear();
            if (Script.IsNullOrUndefined(items)) return;
            ArrayList models = Helper.Collection.ToArrayList(items);

            // Insert a DIV for each model.
            for (int i = 0; i < models.Count; i++)
            {
                jQueryObject div = Html.AppendDiv(Container);
                div.AppendTo(Container);
            }

            // Create the views for each model.
            Container.Children(Html.Div).Each(delegate(int index, Element element)
                                    {
                                        // Prepare data.
                                        jQueryObject div = jQuery.FromElement(element);
                                        object model = models[index];

                                        // Construct the view.
                                        IView view = ItemFactory.CreateView(div, model);
                                        IListItemView listItemView = view as IListItemView;

                                        // Store values.
                                        views.Add(view);

                                        // Wire up events.
                                        if (listItemView != null) div.Click(delegate(jQueryEvent e) { OnItemClick(e, listItemView); });

                                        INotifyPropertyChanged observableView = view as INotifyPropertyChanged;
                                        if (observableView != null) observableView.PropertyChanged += OnViewPropertyChanged;
                                    });
        }

        /// <summary>Clears the list (disposing of all children).</summary>
        public void Clear()
        {
            // Dispose of all views.
            foreach (IView view in views)
            {
                // Unwire events.
                INotifyPropertyChanged observableView = view as INotifyPropertyChanged;
                if (observableView != null) observableView.PropertyChanged -= OnViewPropertyChanged;

                // Destroy.
                view.Dispose();
            }

            // Clear DOM elements.
            Container.Empty();
            views.Clear();
        }
        #endregion

        #region Internal
        private void SelectItem(IListItemView item)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(item)) return;

            // Update the selection.
            ClearSelection(item);
            item.IsSelected = true; // NB: The model is updated as well because the View uses a PropertRef to the model's property behind the scenes.
        }

        private void ClearSelection(IListItemView exclude)
        {
            foreach (IListItemView item in GetItems())
            {
                if (!Script.IsNullOrUndefined(item) && item != exclude) item.IsSelected = false;
            }
        }

        private IEnumerable GetItems()
        {
            ArrayList list = new ArrayList(views.Count);
            foreach (IView view in views)
            {
                IListItemView item = view as IListItemView;
                if (!Script.IsNullOrUndefined(item)) list.Add(item);
            }
            return list;
        }
        #endregion
    }
}
