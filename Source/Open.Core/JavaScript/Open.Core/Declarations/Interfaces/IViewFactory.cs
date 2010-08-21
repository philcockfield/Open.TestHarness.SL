using jQueryApi;

namespace Open.Core
{
    /// <summary>Factory for constructing views.</summary>
    public interface IViewFactory
    {
        /// <summary>Creates a view with the given element.</summary>
        /// <param name="element">The element to construct the view within.</param>
        /// <param name="model">The logical model for the list-item.</param>
        IView CreateView(jQueryObject element, object model);
    }
}
