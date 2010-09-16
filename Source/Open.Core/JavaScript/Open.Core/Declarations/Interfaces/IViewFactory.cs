using jQueryApi;

namespace Open.Core
{
    /// <summary>An object that is can create the view for itself.</summary>
    public interface IViewFactory
    {
        /// <summary>Creates a view within the given element.</summary>
        IView CreateView();
    }
}
