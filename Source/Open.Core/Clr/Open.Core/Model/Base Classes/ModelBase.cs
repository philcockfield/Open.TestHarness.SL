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
    /// <summary>Invokes an action asynchonously and then calls back when complete on the thread that began the invocation.</summary>
    /// <param name="action">The action to invoke.</param>
    /// <param name="onComplete">The callback to invoke when complete.</param>
    /// <returns>The background worker used to execute the action asynchronously.</returns>
    /// <remarks>
    ///    The callback will be invoked irrespective of whether the operation was cancelled via the BackgroundWorker
    ///    object.  If supporting cancellable operations, use your reference to the 'BackgroundWorker' to handle cancelled
    ///    operations within the calling code.
    /// </remarks>
    public delegate BackgroundWorker BeginInvoke(Action action, Action onComplete);

    /// <summary>The base class for models.</summary>
    public abstract partial class ModelBase : NotifyPropertyChangedBase
    {
        #region Head
        // NB: Constructor is public so the object can be de-serialized.
        public ModelBase() : base()
        {
        }
        #endregion

        #region Properties - Protected
        /// <summary>Gets the number of async operations currently in progress (see BeginInvoke).</summary>
        protected int RunningAsyncOperations
        {
            get { return GetPropertyValue<T, int>(m => m.RunningAsyncOperations); }
            set { SetPropertyValue<T, int>(m => m.RunningAsyncOperations, value); }
        }
        #endregion

        #region Methods
        /// <summary>Invokes the given action on the UI thread synchronously.</summary>
        /// <param name="action">The action to invoke.</param>
        protected void InvokeOnUiThread(Action action)
        {
            // Setup initial conditions.
            if (action == null) return;
            if (SynchronizationContext == null) throw new NullReferenceException("Cannot invoke action on UI thread because the SynchronizationContext has not been cached.");

            // Invoke the action.
            SynchronizationContext.Send(state => action(), null);
        }
        #endregion
    }
}
