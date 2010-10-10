using Open.Core;
using Open.Core.Lists;
using Open.Testing.Views;

namespace Open.Testing.Models
{
    /// <summary>Flag representing the custom view to produce.</summary>
    public enum CustomListItemType
    {
        /// <summary>Produces the view that gives the option to add a new test-package to the sidebar.</summary>
        AddPackage = 0,
    }

    /// <summary>A list-item node for custom UI.</summary>
    public class CustomListItem : ListItem, IViewFactory
    {
        #region Head
        private readonly CustomListItemType viewType;

        /// <summary>Constructor.</summary>
        public CustomListItem(CustomListItemType viewType)
        {
            this.viewType = viewType;
        }
        #endregion

        #region Properties
        /// <summary>Gets the type of view this list item produces.</summary>
        public CustomListItemType ViewType { get { return viewType; } }
        #endregion

        #region Methods
        public IView CreateView()
        {
            switch (ViewType)
            {
                case CustomListItemType.AddPackage: return new AddPackageListItemView();
                default: throw Helper.Exception.NotSupported(ViewType.ToString());
            }
        }
        #endregion
    }
}