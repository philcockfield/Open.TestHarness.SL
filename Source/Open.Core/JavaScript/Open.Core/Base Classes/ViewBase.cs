using System;
using jQueryApi;

namespace Open.Core
{
    public abstract class ViewBase : IView
    {
        #region Head
        private bool isDisposed;
        private bool isInitialized;
        private object model;
        private jQueryObject element;

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
        protected jQueryObject Element { get { return element; } }
        #endregion

        #region Methods
        public void Initialize(jQueryObject element)
        {
            if (IsInitialized) throw new Exception("View is already initialized.");
            this.element = element;
            OnInitialize(element);
            isInitialized = true;
        }

        /// <summary>Deriving implementation of Initialize.</summary>
        protected abstract void OnInitialize(jQueryObject element);
        #endregion
    }
}
