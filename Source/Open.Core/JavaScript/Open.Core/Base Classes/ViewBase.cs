using System;
using jQueryApi;

namespace Open.Core
{
    /// <summary>Base for classes that represent, manage and construct views ("UI").</summary>
    public abstract class ViewBase : ModelBase, IView
    {
        #region Head
        private bool isInitialized;
        private jQueryObject container;
        #endregion

        #region Properties : IView
        public bool IsInitialized { get { return isInitialized; } }
        #endregion

        #region Properties
        /// <summary>Gets the element that the view is contained within.</summary>
        protected jQueryObject Container { get { return container; } }
        #endregion

        #region Methods
        public void Initialize(jQueryObject container)
        {
            // Setup initial conditions.)
            if (IsInitialized) throw new Exception("View is already initialized.");

            // Store reference to the container.
            this.container = container;

            // Finish up.
            OnInitialize(this.container);
            isInitialized = true;
        }

        /// <summary>Deriving implementation of Initialize.</summary>
        protected virtual void OnInitialize(jQueryObject container)
        {
        }
        #endregion
    }
}
