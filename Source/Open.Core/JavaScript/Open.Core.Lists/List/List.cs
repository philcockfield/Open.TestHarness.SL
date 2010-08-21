using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>Renders a simple list.</summary>
    public class List : ViewBase
    {
        #region Methods
        private IViewFactory itemFactory;
        private IViewFactory defaultItemFactory;
        private jQueryObject listElement;

        public List(jQueryObject element)
        {
            Initialize(element);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the factory that creates each item in the list.</summary>
        public IViewFactory ItemFactory
        {
            get { return itemFactory ?? (defaultItemFactory ?? (defaultItemFactory = new ListItemFactory())); }
            set { itemFactory = value; }
        }
        #endregion

        #region Methods
        protected override void OnInitialize(jQueryObject element)
        {
            // Insert the <UL> list.
            listElement = element.Append("<ul></ul>");
        }

        /// <summary>Loads the collection of models into the list.</summary>
        /// <param name="models">A collection models.</param>
        public void Load(IEnumerable models)
        {
            // Setup initial conditions.
            listElement.Empty();
            if (models == null) return;

            // TODO - Call Dispose on existing list-items before clearing.

            // Add the models as list-items.
            IViewFactory factory = ItemFactory;
            foreach (object model in models)
            {
                jQueryObject li = jQuery.FromHtml(string.Format("<li></li>"));
                listElement.Append(li);
                IView view = factory.CreateView(li, model);
            }
        }
        #endregion
    }
}
