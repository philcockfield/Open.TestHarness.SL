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
using System.ComponentModel;

using T = Open.Core.Common.ModelBase;

namespace Open.Core.Common
{
    /// <summary>Event args that are handed back after an asynchronous operation has completed.</summary>
    /// <remarks>See 'BeginInvoke' method.</remarks>
    public class AsyncCallbackArgs : EventArgs
    {
        /// <summary>Gets or sets an error that occured during the background operation.</summary>
        public Exception Error { get; set; }

        /// <summary>Gets whether the callback args contains an error.</summary>
        public bool HasError { get { return Error != null; } }

        /// <summary>Gets or sets whether the recieving routine has handled the error.</summary>
        /// <remarks>If left 'False' the ModelBase will throw the error.</remarks>
        public bool IsHandled { get; set; }
    }

    /// <summary>The base class for models.</summary>
    /// <remarks>Implements functionality for invoking action asynchronously.</remarks>
    public partial class ModelBase
    {
        #region Head
        /// <summary>Fires when an error occurs on a background thread (see 'BeginInvoke' method).</summary>
        /// <remarks>Set the 'IsHandled' flag to true to suppress the error if the error condition has been successfully handled by the UI .</remarks>
        public event EventHandler<AsyncCallbackArgs> AsyncError;
        private void OnAsyncError(AsyncCallbackArgs e){if (AsyncError != null) AsyncError(this, e);}

        private static bool isAsyncEnabled = true;
        #endregion

        #region Properties
        /// <summary>Gets or sets whether operations (invoked via the BeginInvoke) are executed asynchonously.</summary>
        /// <remarks>This method is True by default and should only be turned off for testing purposes.</remarks>
        public static bool IsAsyncEnabled
        {
            get { return isAsyncEnabled; }
            set { isAsyncEnabled = value; }
        }
        #endregion

        #region Methods
        /// <summary>Invokes an action asynchonously.</summary>
        /// <param name="action">The action to invoke.</param>
        /// <returns>The background worker used to execute the action asynchronously.</returns>
        protected BackgroundWorker BeginInvoke(Action action)
        {
            return BeginInvoke(action, (Action)null);
        }

        /// <summary>Invokes an action asynchonously and then calls back when complete on the thread that began the invocation.</summary>
        /// <param name="action">The action to invoke.</param>
        /// <param name="onComplete">The callback to invoke when complete.</param>
        /// <returns>The background worker used to execute the action asynchronously.</returns>
        /// <remarks>
        ///    The callback will be invoked irrespective of whether the operation was cancelled via the BackgroundWorker
        ///    object.  If supporting cancellable operations, use your reference to the 'BackgroundWorker' to handle cancelled
        ///    operations within the calling code.
        /// </remarks>
        protected BackgroundWorker BeginInvoke(Action action, Action onComplete)
        {
            return BeginInvoke(action, delegate(AsyncCallbackArgs args)
                                    {
                                        if (onComplete != null) onComplete();
                                    });
        }

        /// <summary>Invokes an action asynchonously and then calls back when complete on the thread that began the invocation.</summary>
        /// <param name="action">The action to invoke.</param>
        /// <param name="onComplete">
        ///    The callback to invoke when complete, including error state.  
        ///    Use the IsHandled property to prevent the error from being thrown.
        /// </param>
        /// <returns>The background worker used to execute the action asynchronously.</returns>
        /// <remarks>
        ///    The callback will be invoked irrespective of whether the operation was cancelled via the BackgroundWorker
        ///    object.  If supporting cancellable operations, use your reference to the 'BackgroundWorker' to handle cancelled
        ///    operations within the calling code.
        /// </remarks>
        protected BackgroundWorker BeginInvoke(Action action, Action<AsyncCallbackArgs> onComplete)
        {
            // Setup initial conditions.
            if (action == null) throw new ArgumentNullException("action");

            // Complete callback.
            Action<AsyncCallbackArgs> callback = args =>
                                  {
                                      // Invoke the callback, and throw the error (if there is one) if no listeners handled it.
                                      if (args.HasError) OnAsyncError(args);
                                      if (onComplete != null) onComplete(args);
                                      if (args.HasError && !args.IsHandled) throw args.Error;
                                  };

            // Invoke the appropriate 
            if (IsAsyncEnabled)
            {
                return InvokeAsynchronous(action, callback);
            }
            else
            {
                InvokeSynchronous(action, callback);
                return null;
            }
        }
        #endregion

        #region Internal
        private BackgroundWorker InvokeAsynchronous(Action action, Action<AsyncCallbackArgs> onComplete)
        {
            // Prepare the background thread worker.
            var worker = new BackgroundWorker();
            AsyncCallbackArgs args = null;
            worker.DoWork += delegate
                                 {
                                     args = ProcessAction(action);
                                 };
            worker.RunWorkerCompleted += delegate
                                 {
                                     RunningAsyncOperations--;
                                     if (onComplete != null) onComplete(args);
                                 };

            // Launch the action on a background thread.
            RunningAsyncOperations++;
            worker.RunWorkerAsync();

            // Finish up.
            return worker;
        }

        private void InvokeSynchronous(Action action, Action<AsyncCallbackArgs> onComplete)
        {
            // Setup initial conditions.
            RunningAsyncOperations++;

            // Execute the action.
            var args = ProcessAction(action);

            // Finish up.
            RunningAsyncOperations--;
            if (onComplete != null) onComplete(args);
        }

        private static AsyncCallbackArgs ProcessAction(Action action)
        {
            // Setup initial conditions.
            var invoker = new ActionInvoker(action);

            // First attempt to invoke the action (on the current thread).
            invoker.Invoke();
            if (!invoker.CallbackArgs.HasError) return invoker.CallbackArgs;

            // If the invoke operation failed because of a cross-thread error, attempt to re-invoke on the UI thread.
            if (invoker.CallbackArgs.Error.GetType() == typeof(UnauthorizedAccessException) && SynchronizationContext != null)
            {
                invoker = new ActionInvoker(action);
                SynchronizationContext.Send(state => invoker.Invoke(), null);
            }

            // Finish up.
            return invoker.CallbackArgs;
        }
        #endregion

        private class ActionInvoker
        {
            #region Head
            private readonly Action action;
            public ActionInvoker(Action action)
            {
                this.action = action;
                CallbackArgs = new AsyncCallbackArgs();
            }
            #endregion

            #region Properties
            /// <summary>Gets the call-back args generated by the invokation.</summary>
            public AsyncCallbackArgs CallbackArgs { get; private set; }
            #endregion

            #region Methods
            public void Invoke()
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    CallbackArgs.Error = e;
                }
            }
            #endregion
        }
    }
}
