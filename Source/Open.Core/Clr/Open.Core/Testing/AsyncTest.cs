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
using System.Threading;

namespace Open.Core.Common.Testing
{
    /// <summary>Enables the testing of asynchronous code.</summary>
    public class AsyncTest
    {
        #region Head
        private readonly Action<AsyncTest> action;
        private readonly EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

        private AsyncTest(Action<AsyncTest> action)
        {
            this.action = action;
        }
        #endregion

        #region Methods
        /// <summary>Starts an asynchronous unit-test, blocking the calling thread until the test is complete.</summary>
        /// <param name="action">The action to perform.  Call 'Complete' on the parameter to signal when the test has completed.</param>
        public static void Start(Action<AsyncTest> action)
        {
            // Setup initial conditions.
            if (action == null) throw new ArgumentNullException("action");
            var test = new AsyncTest(action);

            // Start processing the action on a new thread.
            var t = new Thread(ProcessAction);
            t.Start(test);

            // Block thread.
            WaitHandle.WaitAny(new [] { test.waitHandle });
        }

        /// <summary>Signals that the unit-test is complete.</summary>
        public void Complete()
        {
            waitHandle.Set();
        }
        #endregion

        #region Internal
        private static void ProcessAction(object param)
        {
            var test = (AsyncTest)param;
            test.action(test);
        }
        #endregion
    }
}
