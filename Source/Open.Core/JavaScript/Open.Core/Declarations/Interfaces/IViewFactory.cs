using jQueryApi;

namespace Open.Core
{
    /// <summary>Defined a model that is capable of creating the view for itself.</summary>
    public interface IViewFactory
    {
        /// <summary>Creates a view within the given element.</summary>
        /// <param name="container">The element to construct the view within.</param>
        IView CreateView(jQueryObject container);
    }
}
