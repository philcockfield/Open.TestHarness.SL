using jQueryApi;

namespace Open.Core.Lists
{
    /// <summary>The default view-factory for a list.</summary>
    internal class ListItemFactory : IViewFactory
    {
        public IView CreateView(jQueryObject liElement, object model)
        {
            return new ListItemView(liElement, model);
        }
    }
}