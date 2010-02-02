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
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;
using System.Threading;

namespace Open.Core.UI.Test
{
    [TestClass]
    public class ModelBaseBeginInvokeTest : SilverlightTest
    {
        public const string PropRunningAsyncOperations = "RunningAsyncOperations";

        [TestMethod]
        [Asynchronous]
        public void ShouldInvokeAsyncronously()
        {
            var startContext = SynchronizationContext.Current;
            var model = new ModelStub();

            PropertyChangedEventArgs args = null;
            model.PropertyChanged += (sender, e) => args = e;

            Action onComplete = () =>
                                    {
                                        model.ActionInvoked.ShouldBe(true);
                                        model.AsyncOperationCount.ShouldBe(0);
                                        model.AsyncCountDuringAction.ShouldBe(1);
                                        SynchronizationContext.Current.ShouldBe(startContext);
                                        args.PropertyName.ShouldBe(PropRunningAsyncOperations);

                                        EnqueueTestComplete();
                                    };

            model.DoAsync(startContext, 50, onComplete);
        }


        [TestMethod]
        public void ShouldRequireAction()
        {
            var model = new ModelStub();
            Should.Throw<ArgumentNullException>(() => model.DoAsyncNull());
        }


        [TestMethod]
        public void ShouldRunSynchronously()
        {
            var model = new ModelStub();
            ModelBase.IsAsyncEnabled.ShouldBe(true);
            ModelBase.IsAsyncEnabled = false;

            PropertyChangedEventArgs args = null;
            model.PropertyChanged += (sender, e) => args = e;

            var onCompleteCalled = false;
            Action onComplete = () =>
                                    {
                                        onCompleteCalled = true;
                                    };

            var result =model.DoAsync(5, onComplete);
            result.ShouldBe(null);

            onCompleteCalled.ShouldBe(true);
            model.ActionInvoked.ShouldBe(true);
            model.AsyncOperationCount.ShouldBe(0);
            model.AsyncCountDuringAction.ShouldBe(1);
            args.PropertyName.ShouldBe(PropRunningAsyncOperations);
        }

        private class ModelStub : ModelBase
        {
            public bool ActionInvoked { get; set; }
            public int AsyncCountDuringAction { get; set; }

            public int AsyncOperationCount { get { return RunningAsyncOperations; } }

            public BackgroundWorker DoAsync(int msecs, Action callback)
            {
                return DoAsync(SynchronizationContext.Current, msecs, callback);
            }

            public BackgroundWorker DoAsync(SynchronizationContext startContext, int msecs, Action callback)
            {
                Action action = () =>
                                        {
                                            AsyncCountDuringAction = AsyncOperationCount;
                                            Thread.Sleep(msecs);
                                            ActionInvoked = true;
                                            if (IsAsyncEnabled) SynchronizationContext.Current.ShouldNotBe(startContext);
                                        };
                return BeginInvoke(action, callback);
            }

            public BackgroundWorker DoAsyncNull()
            {
                return BeginInvoke(null);
            }

        }

    }
}


