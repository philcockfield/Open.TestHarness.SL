using System;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using Open.Core.Common;

using TClass = Open.Core.Ria.DataService<System.ServiceModel.DomainServices.Client.DomainContext>;

namespace Open.Core.Ria
{
    /// <summary>Base class for a RIA Services 'Domain Context' wrapper.</summary>
    /// <remarks>
    ///     Use a data-service to wrap the domain-context, and pass all interactions with the server through this.
    ///     This is sometimes known as the repository pattern, and aids in maintainable code and makes unit-testing
    ///     the RIA Service possible.
    /// </remarks>
    public abstract class DataService<TContext> : ModelBase, IDataService where TContext : DomainContext
    {
        #region Events
        /// <summary>Fires when the client-side changes have been reverted.</summary>
        public event EventHandler Reverted;
        private void OnReverted() { if (Reverted != null) Reverted(this, new EventArgs()); }
        #endregion

        #region Head
        private readonly PropertyObserver<TContext> contextObserver;
        private bool previousIsLoading;
        private bool previousIsSaving;

        /// <summary>Constructor.</summary>
        /// <param name="context">The domain context being wrapped.</param>
        protected DataService(TContext context)
        {
            // Setup initial conditions.
            if (context == null) throw new ArgumentNullException("context");
            Context = context;

            // Wire up events.
            contextObserver = new PropertyObserver<TContext>(context)
                .RegisterHandler(m => m.IsLoading, m => FireIsLoadingChanged())
                .RegisterHandler(m => m.IsSubmitting, m => FireIsSavingChanged())
                .RegisterHandler(m => m.HasChanges, m => OnPropertyChanged<TClass>(o => o.HasChanges));
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            contextObserver.Dispose();
        }
        #endregion

        #region Properties
        /// <summary>Gets the domain context.</summary>
        protected TContext Context { get; private set; }

        /// <summary>Gets or sets whether the service is currently loading.</summary>
        public bool IsLoading { get { return Context.IsLoading; } }

        /// <summary>Gets or sets whether the service is currently saving.</summary>
        public bool IsSaving { get { return Context.IsSubmitting; } }

        /// <summary>Gets whether the service is currently in the process of loading or saving.</summary>
        public bool IsBusy { get { return IsLoading || IsSaving; } }

        /// <summary>Gets whether the service is currently idle (not busy).</summary>
        public bool IsIdle { get { return !IsBusy; } }

        /// <summary>Gets whether there are any pending changes.</summary>
        public bool HasChanges { get { return Context.HasChanges; } }
        #endregion

        #region Methods
        /// <summary>Initiates a load operation for the specified query (uses the 'MergeIntoCurrent' load behavior).</summary>
        /// <typeparam name="T">The entity Type being loaded.</typeparam>
        /// <param name="query">The query to invoke.</param>
        /// <param name="callback">Optional callback to be called when the load operation completes.</param>
        public void Load<T>(EntityQuery<T> query, Action<LoadOperation<T>> callback = null) where T : Entity
        {
            Load(query, LoadBehavior.MergeIntoCurrent, callback);
        }

        /// <summary>Initiates a load operation for the specified query.</summary>
        /// <typeparam name="T">The entity Type being loaded.</typeparam>
        /// <param name="query">The query to invoke.</param>
        /// <param name="refresh">Flag indicating the refresh strategy to use (True:RefreshCurrent, False:MergeIntoCurrent).</param>
        /// <param name="callback">Optional callback to be called when the load operation completes.</param>
        public void Load<T>(EntityQuery<T> query, bool refresh, Action<LoadOperation<T>> callback) where T : Entity
        {
            var loadBehavior = refresh ? LoadBehavior.RefreshCurrent : LoadBehavior.MergeIntoCurrent;
            Load(query, loadBehavior, callback);
        }

        /// <summary>Initiates a load operation for the specified query.</summary>
        /// <typeparam name="T">The entity Type being loaded.</typeparam>
        /// <param name="query">The query to invoke.</param>
        /// <param name="loadBehavior">Flag indicating what strategy to use when loading.</param>
        /// <param name="callback">Optional callback to be called when the load operation completes.</param>
        public void Load<T>(EntityQuery<T> query, LoadBehavior loadBehavior, Action<LoadOperation<T>> callback = null) where T : Entity
        {
            Context.Load(query, loadBehavior, callback, null);
        }

        /// <summary>Initiates submitting changes to the server.</summary>
        /// <param name="callback">Optional callback to be called when the save operation completes.</param>
        public void Save(Action<SubmitOperation> callback = null)
        {
            Context.SubmitChanges(callback, null);
        }

        /// <summary>Revert all pending changes.</summary>
        public void RevertChanges()
        {
            if (!HasChanges) return;
            Context.RejectChanges();
            OnReverted();
        }
        #endregion

        #region Methods - Protected
        /// <summary>Provides a one-line way of invoking the callback with all appropriate return args setup.</summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="callback">The callback to invoke (may be null).</param>
        /// <param name="collection">The returned collecion</param>
        protected static void InvokeCallback<T>(CallbackAction<IEnumerable<T>> callback, IEnumerable<T> collection)
        {
            if (callback == null) return;
            callback(GetCollectionArgs(collection));
        }

        /// <summary>Provides a one-line way of invoking the callback with all appropriate return args setup.</summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="callback">The callback to invoke (may be null).</param>
        /// <param name="operation">The returned load operation.</param>
        protected static void InvokeCallback<T>(CallbackAction<IEnumerable<T>> callback, LoadOperation<T> operation) where T : Entity
        {
            // Setup initial conditions.
            if (callback == null) return;

            // Prepare the callback.
            var args = GetCollectionArgs(operation.Entities);
            args.Cancelled = operation.IsCanceled;
            args.Error = operation.Error;

            // Invoke.
            callback(args);
        }

        /// <summary>Provides a one-line way of invoking the callback with all appropriate return args setup (returns the FirstOrDefault item from the entity set).</summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="callback">The callback to invoke (may be null).</param>
        /// <param name="operation">The returned load operation.</param>
        protected static void InvokeCallbackFirstOrDefault<T>(CallbackAction<T> callback, LoadOperation<T> operation) where T : Entity
        {
            // Setup initial conditions.
            if (callback == null) return;

            // Prepare the callback.
            var args = new Callback<T> { Result = operation.Entities.FirstOrDefault() };
            args.Cancelled = operation.IsCanceled;
            args.Error = operation.Error;

            // Invoke.
            callback(args);
        }
        #endregion

        #region Internal
        private static Callback<IEnumerable<T>> GetCollectionArgs<T>(IEnumerable<T> collection)
        {
            return new Callback<IEnumerable<T>> { Result = collection };
        }

        private void FireIsLoadingChanged()
        {
            if (previousIsLoading == IsLoading) return;
            OnPropertyChanged<TClass>(o => o.IsLoading);
            FireBusyIdle();
            previousIsLoading = IsLoading;
        }

        private void FireIsSavingChanged()
        {
            if (previousIsSaving == IsSaving) return;
            OnPropertyChanged<TClass>(o => o.IsSaving);
            FireBusyIdle();
            previousIsSaving = IsSaving;
        }

        private void FireBusyIdle()
        {
            OnPropertyChanged<TClass>(o => o.IsBusy, o => o.IsIdle);
        }
        #endregion
    }
}
