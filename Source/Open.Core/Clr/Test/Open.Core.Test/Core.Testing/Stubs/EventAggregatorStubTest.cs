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
using System.Collections.Generic;
using Microsoft.Practices.Composite.Presentation.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Common.Testing.Stubs
{
    [TestClass]
    public class EventAggregatorStubTest
    {
        #region Head
        private EventAggregatorStub aggregator;

        [TestInitialize]
        public void TestSetup()
        {
            aggregator = new EventAggregatorStub();
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldSubscribeToEventOverload1()
        {
            aggregator.SubscribedCount<MyEvent>().ShouldBe(0);
            aggregator.Subscribe<MyEvent>(obj => { });
            aggregator.SubscribedCount<MyEvent>().ShouldBe(1);

            aggregator.Subscribe<MyEvent>(obj => { });
            aggregator.SubscribedCount<MyEvent>().ShouldBe(2);
        }

        [TestMethod]
        public void ShouldSubscribeToEventOverload2()
        {
            aggregator.Subscribe<MyEvent>(obj => { }, false);
            aggregator.SubscribedCount<MyEvent>().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldSubscribeToEventOverload3()
        {
            aggregator.Subscribe<MyEvent>(obj => { }, ThreadOption.PublisherThread);
            aggregator.SubscribedCount<MyEvent>().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldSubscribeToEventOverload4()
        {
            aggregator.Subscribe<MyEvent>(obj => { }, ThreadOption.BackgroundThread, false);
            aggregator.SubscribedCount<MyEvent>().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldSubscribeToEventOverload5()
        {
            aggregator.Subscribe<MyEvent>(obj => { }, ThreadOption.BackgroundThread, false, o => true);
            aggregator.SubscribedCount<MyEvent>().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldIncrementPublishedCountWhenEventIsFired()
        {
            aggregator.PublishedCount<MyEvent>().ShouldBe(0);

            aggregator.Publish(new MyEvent());
            aggregator.PublishedCount<MyEvent>().ShouldBe(1);

            aggregator.Publish(new MyEvent());
            aggregator.Publish(new MyEvent());
            aggregator.Publish(new MyEvent());
            aggregator.PublishedCount<MyEvent>().ShouldBe(4);
        }

        [TestMethod]
        public void ShouldPublishPayloadToSubscribedActions()
        {
            var firedPayloads = new List<MyEvent>();
            aggregator.Subscribe<MyEvent>(firedPayloads.Add);

            var args1 = new MyEvent { Text = "one" };
            var args2 = new MyEvent { Text = "two" };

            aggregator.Publish(args1);
            aggregator.Publish(args2);

            firedPayloads.ShouldContain(args1);
            firedPayloads.ShouldContain(args2);
        }

        [TestMethod]
        public void ShouldUnsubscribe()
        {
            aggregator.PublishedCount<MyEvent>().ShouldBe(0);

            var firedPayloads = new List<MyEvent>();
            Action<MyEvent> action = firedPayloads.Add;

            // ---

            aggregator.Subscribe(action);
            aggregator.SubscribedCount<MyEvent>().ShouldBe(1);

            aggregator.Unsubscribe(action);
            aggregator.SubscribedCount<MyEvent>().ShouldBe(0);

            // ---

            aggregator.Publish(new MyEvent());
            aggregator.Publish(new MyEvent());
            firedPayloads.Count.ShouldBe(0);
            aggregator.PublishedCount<MyEvent>().ShouldBe(2); // The published count is still recorded even though there were no subscriptions.
        }

        [TestMethod]
        public void ShouldDoNothingWhenUnsubscribingFromEventThatHasNotBeenSubscribedTo()
        {
            aggregator.Unsubscribe<MyEvent>(obj => {});

            // --

            Action<MyEvent> action1 = e => { };
            Action<MyEvent> action2 = e => { };

            aggregator.Subscribe(action1);
            aggregator.SubscribedCount<MyEvent>().ShouldBe(1);

            aggregator.Unsubscribe(action2);
            aggregator.SubscribedCount<MyEvent>().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldFireTestMethod_FireAtLeastOnce()
        {
            aggregator.ShouldFire<MyEvent>(() => aggregator.Publish(new MyEvent()));
            aggregator.ShouldFire<MyEvent>(() =>
                                                {
                                                    aggregator.Publish(new MyEvent());
                                                    aggregator.Publish(new MyEvent());
                                                });
            Should.Throw<AssertionException>(() => aggregator.ShouldFire<MyEvent>(() => { }));
        }

        [TestMethod]
        public void ShouldFireTestMethod_ShouldAccountForPreexistingPublishCount()
        {
            aggregator.Publish(new MyEvent());
            aggregator.Publish(new MyEvent());
            aggregator.PublishedCount<MyEvent>().ShouldBe(2);

            aggregator.ShouldFire<MyEvent>(() => aggregator.Publish(new MyEvent()));
        }

        [TestMethod]
        public void ShouldFireTestMethod_SpecificNumberOfTimes()
        {
            aggregator.ShouldFire<MyEvent>(2, () =>
                                               {
                                                   aggregator.Publish(new MyEvent());
                                                   aggregator.Publish(new MyEvent());
                                               });
            Should.Throw<AssertionException>(() => aggregator.ShouldFire<MyEvent>(2, () =>
                                                                                         {
                                                                                             aggregator.Publish(new MyEvent());
                                                                                             aggregator.Publish(new MyEvent());
                                                                                             aggregator.Publish(new MyEvent());
                                                                                         }));

            Should.Throw<AssertionException>(() => aggregator.ShouldFire<MyEvent>(2, () => { }));
        }

        [TestMethod]
        public void ShouldNotFireTestMethod()
        {
            aggregator.ShouldNotFire<MyEvent>(() => { });
            Should.Throw<AssertionException>(() => aggregator.ShouldNotFire<MyEvent>(() => aggregator.Publish(new MyEvent())));
        }
        #endregion

        #region Stubs
        public class MyEvent : CompositePresentationEvent<MyEvent>
        {
            public string Text { get; set; }
        }
        #endregion
    }
}
