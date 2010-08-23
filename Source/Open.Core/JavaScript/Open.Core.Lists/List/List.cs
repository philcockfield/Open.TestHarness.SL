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
    public class List : ViewBase
    {
        #region Head
        /// <summary>Fires when the item selection changes.</summary>
        public event EventHandler SelectionChanged;
        private void FireSelectionChanged() { if (SelectionChanged != null) SelectionChanged(this, new EventArgs()); }

        private IViewFactory itemFactory;
        private IViewFactory defaultItemFactory;
        private ListSelectionMode selectionMode = ListSelectionMode.Single;
        private readonly ArrayList views = new ArrayList();

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing element.</param>
        public List(jQueryObject container)
        {
            Initialize(container);
            ListCss.InsertCss();
        }
        #endregion

        #region Event Handlers
        private void OnItemClick(jQueryEvent e, IView view)
        {
            if (SelectionMode == ListSelectionMode.None) return;
            SelectItem(view as IListItemView);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the factory that creates each item in the list.</summary>
        public IViewFactory ItemFactory
        {
            get { return itemFactory ?? (defaultItemFactory ?? (defaultItemFactory = new ListItemFactory())); }
            set { itemFactory = value; }
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
        protected override void OnInitialize(jQueryObject container)
        {
        }

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
            IViewFactory factory = ItemFactory;
            Container.Children(Html.Div).Each(delegate(int index, Element element)
                                    {
                                        // Prepare data.
                                        jQueryObject div = jQuery.FromElement(element);
                                        object model = models[index];

                                        // Construct the view.
                                        IView view = factory.CreateView(div, model);

                                        // Store values.
                                        this.views.Add(view);

                                        // Wire up events.
                                        div.Click(delegate(jQueryEvent e) { OnItemClick(e, view); });
                                    });
        }

        /// <summary>Clears the list (disposing of all children).</summary>
        public void Clear()
        {
            // Dispose of all views.
            foreach (IView view in views)
            {
                view.Dispose();
            }

            // Clear DOM elements.
            Container.Empty();
            views.Clear();
        }
        #endregion

        #region Methods : Select
        /// <summary>Selects the item corresponding to the given model.</summary>
        /// <param name="model">The item's model.</param>
        public void Select(object model)
        {
            if (Script.IsNullOrUndefined(model)) return;
            SelectItem(GetListItem(model));
            FireSelectionChanged();
        }

        /// <summary>Selects the first item in the list.</summary>
        public void SelectFirst() { SelectIndex(0); }

        /// <summary>Selects the first item in the list.</summary>
        public void SelectLast() { if (views.Count > 0) SelectIndex(views.Count - 1); }

        /// <summary>Selects the list item at the specified index.</summary>
        /// <param name="index">The index of the item to select (0-based).</param>
        /// <remarks>If the given index is out of range no action is taken (and no error is thrown).</remarks>
        public void SelectIndex(int index)
        {
            if (views.Count == 0 || index > views.Count - 1 || index < 0) return;
            SelectItem(views[index] as IListItemView);
        }
        #endregion

        #region Internal
        private void SelectItem(IListItemView item)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(item)) return;

            // Update the selection.
            ClearSelection();
            item.IsSelected = true;
        }

        private void ClearSelection()
        {
            foreach (IListItemView item in GetItems())
            {
                if (!Script.IsNullOrUndefined(item)) item.IsSelected = false;
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

        private IListItemView GetListItem(object model)
        {
            foreach (IListItemView item in GetItems())
            {
                if (item.Model == model) return item;
            }
            return null;
        }
        #endregion
    }
}
