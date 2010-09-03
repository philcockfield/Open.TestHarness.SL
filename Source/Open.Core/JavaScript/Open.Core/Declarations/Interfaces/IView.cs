using jQueryApi;

namespace Open.Core
{
    /// <summary>The logical controller for a view (visual UI) contained with an HTML element.</summary>
    public interface IView
    {
        /// <summary>Gets whether the view has been disposed of.</summary>
        bool IsDisposed { get; }

        /// <summary>Gets or sets whether the view has been initialized.</summary>
        bool IsInitialized { get; }

        /// <summary>Initializes the view.</summary>
        /// <param name="container">The containing element of the view.</param>
        void Initialize(jQueryObject container);

        /// <summary>Gets the element that the view is contained within.</summary>
        jQueryObject Container { get; }

        /// <summary>Destroys the view and clears resources.</summary>
        void Dispose();
    }
}
