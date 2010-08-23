using System;
using jQueryApi;

namespace Open.Core
{
    /// <summary>Base for classes that represent, manage and construct views ("UI").</summary>
    public abstract class ViewBase : ModelBase, IView
    {
        #region Head
        private bool isDisposed;
        private bool isInitialized;
        private object model;
        private jQueryObject container;

        /// <summary>Destroys the view and cleans up resources.</summary>
        public void Dispose()
        {
            if (isDisposed) return;
            OnDispose();
            isDisposed = true;
        }

        /// <summary>Deriving implementation of Dispose.</summary>
        protected virtual void OnDispose() { }
        #endregion

        #region Properties : IView
        public bool IsDisposed { get { return isDisposed; } }
        public bool IsInitialized { get { return isInitialized; } }
        #endregion

        #region Properties
        /// <summary>Gets the element that the view is contained within.</summary>
        protected jQueryObject Container { get { return container; } }
        #endregion

        #region Methods
        public void Initialize(jQueryObject container)
        {
            if (IsInitialized) throw new Exception("View is already initialized.");
            this.container = container;
            OnInitialize(container);
            isInitialized = true;
        }

        /// <summary>Deriving implementation of Initialize.</summary>
        protected abstract void OnInitialize(jQueryObject container);
        #endregion
    }
}
