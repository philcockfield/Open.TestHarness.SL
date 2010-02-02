//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Open.Core.Test")]

namespace Open.Core.Common.Network
{
    /// <summary>Flags representing the current state of the loader.</summary>
    public enum LoaderState
    {
        Unloaded, // State prior to the 'Load' method being called.
        Loading, // Load operation currently in progress.
        Loaded, // Load operation completed at least once successfully.
        LoadError // Last load operation failed.
    }

    /// <summary>Base class for objects that implement server loading functions.</summary>
    public abstract class LoaderBase : NotifyPropertyChangedBase
    {
        #region Head
        public const string PropLoaderState = "State";
        private LoaderState state = LoaderState.Unloaded;
        private Action loadCallback;
        private DateTime loadStartedAt;

        /// <summary>Constructor.</summary>
        protected LoaderBase() : this(new TestableWebClient()){}

        /// <summary>Constructor.</summary>
        /// <param name="webClient">The web client to use.</param>
        /// <remarks>Inject a different client to teh constructor for testing purposes.</remarks>
        protected LoaderBase(TestableWebClient webClient)
        {
            WebClient = webClient;
            WebClient.OpenReadCompleted += Handle_OpenReadCompleted;
        }
        #endregion

        #region Events
        /// <summary>Fires when the application has been downloaded and initialized.</summary>
        /// <remarks>See the 'Load' method.</remarks>
        public event EventHandler Loaded;
        protected void OnLoaded() { if (Loaded != null) Loaded(this, new EventArgs()); }
        
        /// <summary>Fires when an error occurs during the load operation.</summary>
        public event EventHandler LoadFailed;
        protected void OnLoadFailed(){if (LoadFailed != null) LoadFailed(this, new EventArgs());}
        #endregion

        #region Event Handlers
        void Handle_OpenReadCompleted(object sender, TestableOpenReadCompletedEventArgs e)
        {
            // Check for errors.
            Error = CheckForError(e);
            if (Error != null)
            {
                State = LoaderState.LoadError;
                OnLoadFailed();
            }

            // Pass execution to the callback, and alert listeners.
            if (State != LoaderState.LoadError) state = LoaderState.Loaded; // Update the load state prior to executing the callback...
            OnLoadCallback(e);
            if (State != LoaderState.LoadError) OnPropertyChanged(PropLoaderState); // ...but wait for the callback to be executed before firing the property changed event.

            // Calculate the load time.
            LoadTime = DateTime.UtcNow.Subtract(loadStartedAt);

            // Finish up.
            OnLoaded();
            if (loadCallback != null)
            {
                loadCallback.Invoke();
                loadCallback = null;
            }
        }

        private static Exception CheckForError(TestableOpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return e.Error;
            try
            {
                var resultStream = e.Result; // Attempt to retreive the content, will force error if there was a problem.
            }
            catch (TargetInvocationException error) { return error; }
            return null;
        }
        #endregion

        #region Properties
        /// <summary>Flag indicating the current state of the loader.</summary>
        public LoaderState State
        {
            get { return state; }
            private set
            {
                if (value == State) return;
                state = value;
                OnPropertyChanged(PropLoaderState);
            }
        }

        /// <summary>Gets the exception that occured during load, if there was one.</summary>
        public Exception Error { get; private set; }

        /// <summary>Gets the time taken to perform the load operation.</summary>
        public TimeSpan LoadTime{ get; private set; }

        /// <summary>The web client used to interact with the server.</summary>
        /// <remarks>Created automatically at construction.  Inject a different client to teh constructor for testing purposes.</remarks>
        internal TestableWebClient WebClient { get; private set; }
        #endregion

        #region Methods
        /// <summary>Commences the load operation.</summary>
        /// <remarks>
        ///    Implementers see the 'OnPreload' and 'OnLoadCallback' method.
        ///  </remarks>
        public void Load()
        {
            // Only initiate the server call if the pre-load operation allows it.
            if (!OnPreload()) return;
            State = LoaderState.Loading;

            // Invoke the call to the server.
            loadStartedAt = DateTime.UtcNow;
            WebClient.OpenReadAsync(GetUri());
        }

        /// <summary>Commences the load operation, and invokes a callback on complete.</summary>
        /// <param name="callback">The callback to invoke.</param>
        public void Load(Action callback)
        {
            loadCallback = callback;
            Load();
        }

        /// <summary>Fired before the server call is initiated.</summary>
        /// <returns>Implementers return False to cancel server call, otherwise return True.</returns>
        protected virtual bool OnPreload()
        {
            return true; // Default value causes Load operation to proceed.
        }

        /// <summary>Called after the load operation has completed.</summary>
        /// <param name="e">The event arguments containing information about the server call.</param>
        /// <remarks>Check the 'LoadError' property to see whether the operation was successful.</remarks>
        protected abstract void OnLoadCallback(TestableOpenReadCompletedEventArgs e);

        /// <summary>Retrieves the URI of the server location to access.</summary>
        /// <returns>A URI.</returns>
        protected abstract Uri GetUri();
        #endregion
    }
}
