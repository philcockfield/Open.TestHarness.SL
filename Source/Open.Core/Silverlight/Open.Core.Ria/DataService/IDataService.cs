using System;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;

namespace Open.Core.Ria
{
    public interface IDataService : INotifyPropertyChanged
    {
        /// <summary>Fires when the client-side changes have been reverted.</summary>
        event EventHandler Reverted;

        /// <summary>Fires when either loading starts or stops.  See related 'IsLoading', 'IsBusy', 'IsIdle' properties.</summary>
        event EventHandler LoadStateChanged;

        /// <summary>Gets or sets whether the service is currently loading.</summary>
        bool IsLoading { get; }

        /// <summary>Gets or sets whether the service is currently saving.</summary>
        bool IsSaving { get; }

        /// <summary>Gets whether the service is currently in the process of loading or saving.</summary>
        bool IsBusy { get; }

        /// <summary>Gets whether there are any pending changes.</summary>
        bool HasChanges { get; }

        /// <summary>Initiates a load operation for the specified query (uses the 'MergeIntoCurrent' load behavior).</summary>
        /// <typeparam name="T">The entity Type being loaded.</typeparam>
        /// <param name="query">The query to invoke.</param>
        /// <param name="callback">Optional callback to be called when the load operation completes.</param>
        void Load<T>(EntityQuery<T> query, Action<LoadOperation<T>> callback = null) where T : Entity;

        /// <summary>Initiates a load operation for the specified query.</summary>
        /// <typeparam name="T">The entity Type being loaded.</typeparam>
        /// <param name="query">The query to invoke.</param>
        /// <param name="refresh">Flag indicating the refresh strategy to use (True:RefreshCurrent, False:MergeIntoCurrent).</param>
        /// <param name="callback">Optional callback to be called when the load operation completes.</param>
        void Load<T>(EntityQuery<T> query, bool refresh, Action<LoadOperation<T>> callback = null) where T : Entity;

        /// <summary>Initiates a load operation for the specified query.</summary>
        /// <typeparam name="T">The entity Type being loaded.</typeparam>
        /// <param name="query">The query to invoke.</param>
        /// <param name="loadBehavior">Flag indicating what strategy to use when loading.</param>
        /// <param name="callback">Optional callback to be called when the load operation completes.</param>
        void Load<T>(EntityQuery<T> query, LoadBehavior loadBehavior, Action<LoadOperation<T>> callback = null) where T : Entity;

        /// <summary>Initiates submitting changes to the server.</summary>
        /// <param name="callback">Optional callback to be called when the save operation completes.</param>
        void Save(Action<SubmitOperation> callback = null);

        /// <summary>Revert all pending changes.</summary>
        void RevertChanges();
    }
}