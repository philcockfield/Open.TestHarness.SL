using System;
using System.Collections;
using System.Runtime.CompilerServices;
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
    
    /// <summary>Factory method for creating list item view.</summary>
    /// <param name="model">The model for the list item.</param>
    /// <returns>The list-item visual control.</returns>
    public delegate IView CreateListItem(object model);
    

    /// <summary>Renders a simple list.</summary>
    public class ListView : ViewBase
    {
        #region Head
        private ListSelectionMode selectionMode = ListSelectionMode.Single;
        private readonly ArrayList itemViews = new ArrayList();
        private CreateListItem itemFactory;

        /// <summary>Constructor.</summary>
        [AlternateSignature]
        public extern ListView();

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing element.</param>
        public ListView(jQueryObject container) : base(container)
        {
            ListCss.InsertCss();
            Container.AddClass(ListCss.Classes.Root);
        }
        #endregion

        #region Event Handlers
        private void OnItemClick(jQueryEvent e, IListItemView item)
        {
            Helper.Event.FireClick(item.Model);
            if (SelectionMode != ListSelectionMode.None) item.IsSelected = true;
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
        public CreateListItem ItemFactory
        {
            get { return itemFactory; }
            set { itemFactory = value; }
        }

        /// <summary>Gets or sets whether items within the list are selecable.</summary>
        public ListSelectionMode SelectionMode
        {
            get { return selectionMode; }
            set { selectionMode = value; }
        }

        /// <summary>Gets the number of items currently in the list.</summary>
        public int Count { get { return itemViews.Count; } }

        /// <summary>Gets the current scroll height of the list (the height of the list within it's scrolling pane).</summary>
        public int ScrollHeight { get { return Int32.Parse(Container.GetAttribute(Html.ScrollHeight)); } }

        /// <summary>Gets the offset height of the items within the list.</summary>
        public int ContentHeight
        {
            get
            {
                int height = 0;
                foreach (ListItemView view in itemViews)
                {
                    height += view.Container.GetHeight();
                }
                return height;
            }
        }
        #endregion

        #region Methods : Load | Insert
        /// <summary>Loads the collection of models into the list.</summary>
        /// <param name="items">A collection models.</param>
        public void Load(IEnumerable items)
        {
            Clear();
            InsertRange(0, items);
        }

        /// <summary>Loads the collection of models into the list.</summary>
        /// <param name="startingAt">Index to start inserting at (0-based).</param>
        /// <param name="models">A collection models.</param>
        public void InsertRange(int startingAt, IEnumerable models)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(models)) return;
            if (startingAt < 0) startingAt = 0;

            // Insert each item.
            foreach (object model in models)
            {
                Insert(startingAt, model);
                startingAt++;
            }
        }

        /// <summary>Inserts a list-item for the given model at the specified index.</summary>
        /// <param name="index">The index to insert at (0-based).</param>
        /// <param name="model">The data-model for the item.</param>
        public void Insert(int index, object model)
        {
            // Setup initial conditions.
            jQueryObject insertBefore = InsertBefore(index);

            // Construct the list-item control.
            IView view = CreateItem(model);
            IListItemView listItemView = view as IListItemView;

            // Insert the containing DIV.
            jQueryObject div = view.Container;
            if (insertBefore == null)
            {
                div.AppendTo(Container);
            }
            else
            {
                div.InsertBefore(insertBefore);
            }

            // Store values.
            itemViews.Add(view);

            // Wire up events.
            if (listItemView != null) div.Click(delegate(jQueryEvent e) { OnItemClick(e, listItemView); });

            INotifyPropertyChanged observableView = view as INotifyPropertyChanged;
            if (observableView != null) observableView.PropertyChanged += OnViewPropertyChanged;
        }
        #endregion

        #region Methods : Remove | Clear
        /// <summary>Removes the list item with the specified model.</summary>
        /// <param name="model">The model of the item to remove.</param>
        public void Remove(object model)
        {
            // Setup initial conditions.
            if (model == null) return;
            IListItemView view = GetView(model);
            RemoveView(view as IView);
        }

        private void RemoveView(IView view)
        {
            // Setup initial conditions.
            if (view == null) return;

            // Unwire events.
            INotifyPropertyChanged observableView = view as INotifyPropertyChanged;
            if (observableView != null) observableView.PropertyChanged -= OnViewPropertyChanged;

            // Destroy.
            view.Container.Remove();
            view.Dispose();

            // Finish up.
            itemViews.Remove(view);
        }

        /// <summary>Clears the list (disposing of all children).</summary>
        public void Clear()
        {
            foreach (IView view in itemViews.Clone())
            {
                RemoveView(view);
            }
        }
        #endregion

        #region Internal
        private IView CreateItem(object model)
        {
            // Defer to the externally set ItemFactory if there is one.
            if (ItemFactory != null) return ItemFactory(model);

            // No item factory, check if the model can create views itself.
            IViewFactory viewFactory = model as IViewFactory;
            if (viewFactory != null) return viewFactory.CreateView();

            // Create the default list-item control.
            return new ListItemView(model);
        }

        private jQueryObject InsertBefore(int insertAt)
        {
            // Prepare the index.
            if (insertAt < 0 || Count == 0) return null;
            int lastItem = Count - 1;
            if (insertAt > lastItem) return null;

            // Query for the child.
            return Html.ChildAt(insertAt, Container);
        }

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
            foreach (IListItemView view in GetListItemViews())
            {
                if (!Script.IsNullOrUndefined(view) && view != exclude) view.IsSelected = false;
            }
        }

        private IEnumerable GetListItemViews()
        {
            return Helper.Collection.Filter(itemViews, delegate(object o)
                                                           {
                                                               return (o as IListItemView) != null;
                                                           });
        }

        private IListItemView GetView(object model)
        {
            return Helper.Collection.First(GetListItemViews(), delegate(object o)
                                                                   {
                                                                       return ((IListItemView) o).Model == model;
                                                                   }) as IListItemView;
        }
        #endregion
    }
}
