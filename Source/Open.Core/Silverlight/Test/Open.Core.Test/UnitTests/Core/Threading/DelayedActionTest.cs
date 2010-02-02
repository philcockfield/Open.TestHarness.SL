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
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Common.Threading
{
    [TestClass]
    public class DelayedActionTest : SilverlightUnitTest
    {
        #region Head
        [TestInitialize]
        public void TestSetup()
        {
            DelayedAction.IsAsyncronous = true;
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldStoreSeconds()
        {
            var action = new DelayedAction(5, null);
            action.Delay.ShouldBe(5.0);
        }

        [TestMethod][Asynchronous]
        public void ShouldInvokeAfterTimeout()
        {
            Action callback = EnqueueTestComplete;
            var action = new DelayedAction(0.2, callback);
            action.Start();
        }

        [TestMethod][Asynchronous]
        public void ShouldNotCallActionWhenStopped()
        {
            DelayedAction action2 = null;

            var wasCalled = false;
            Action callback2 = delegate
                                  {
                                      action2.IsRunning.ShouldBe(false);
                                      wasCalled.ShouldBe(false);
                                      EnqueueTestComplete();
                                  };

            var action1 = new DelayedAction(0.2, ()=> wasCalled = true);
            action2 = new DelayedAction(1, callback2);

            action1.Start();
            action2.Start();

            action1.Stop();
        }

        [TestMethod][Asynchronous]
        public void ShouldUpdateIsRunningProperty()
        {
            DelayedAction action = null;
            Action callback = delegate
                                            {
                                                action.IsRunning.ShouldBe(false);
                                                EnqueueTestComplete();
                                            };
            action = new DelayedAction(0.1, callback);
            action.Start();
            action.IsRunning.ShouldBe(true);
        }

        [TestMethod][Asynchronous]
        public void ShouldInvokeFromStaticMethod()
        {
            DelayedAction.Invoke(0.1, EnqueueTestComplete);
        }

        [TestMethod]
        public void ShouldRunSynchronously()
        {
            var value = 0;
            DelayedAction.IsAsyncronous = false;
            var action = new DelayedAction(0.1, () => value ++);

            action.Start();
            value.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldRunStaticMethodSynchronously()
        {
            var value = 0;

            DelayedAction.IsAsyncronous = true;
            DelayedAction.Invoke(0.1, () => value = 1);
            value.ShouldBe(0);

            DelayedAction.IsAsyncronous = false;
            DelayedAction.Invoke(0.1, () => value = 2);
            value.ShouldBe(2);
        }

        [TestMethod]
        public void ShouldStopOnDispose()
        {
            var action = new DelayedAction(1, () => { });

            action.Start();
            action.IsRunning.ShouldBe(true);

            action.Dispose();
            action.IsRunning.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldFireStartedEvent()
        {
            var delayedAction = new DelayedAction(0.1, () => { });

            var fireCount = 0;
            delayedAction.Started += delegate { fireCount++; };
            
            delayedAction.Start();
            fireCount.ShouldBe(1);

            delayedAction.Start();
            fireCount.ShouldBe(2);
        }

        [TestMethod]
        public void ShouldFireStoppedEvent()
        {
            var delayedAction = new DelayedAction(0.1, () => { });

            var fireCount = 0;
            delayedAction.Stopped += delegate { fireCount++; };

            delayedAction.Stop();
            fireCount.ShouldBe(0);

            delayedAction.Start();
            delayedAction.Stop();
            fireCount.ShouldBe(1);

            delayedAction.Stop();
            fireCount.ShouldBe(1);
        }

        [TestMethod]
        [Asynchronous]
        public void ShouldFireInvokedEvent()
        {
            var fireCount = 0;
            var fireCountDuringAction = 0;

            var delayedAction = new DelayedAction(0.01, () =>
                                                            {
                                                                fireCountDuringAction = fireCount;
                                                            });

            delayedAction.ActionInvoked += delegate { fireCount++; };
            delayedAction.Start();

            DelayedAction.Invoke(0.2, () =>
                                          {
                                              fireCountDuringAction.ShouldBe(0);
                                              fireCount.ShouldBe(1);
                                              EnqueueTestComplete();
                                          });
        }
        #endregion
    }
}
