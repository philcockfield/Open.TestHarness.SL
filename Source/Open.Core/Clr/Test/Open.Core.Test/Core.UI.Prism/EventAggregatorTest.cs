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
using CAL = Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Open.Core.Common.Testing;
using System.Threading;


namespace Open.Core.Common.Test.Core.UI.Prism
{
    [TestClass]
    public class EventAggregatorTest
    {
        #region Head

        private const int AsyncDelay = 10;
        private IUnityContainer container;
        private EventAggregator aggregator;

        [TestInitialize]
        public void TestSetup()
        {
            container = new UnityContainer();
            aggregator = new EventAggregator(container);
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldExposeDiContainerPassedToConstructor()
        {
            aggregator.Container.ShouldBe(container);
        }

        [TestMethod]
        public void ShouldThrowWhenContainerNotPassed()
        {
            Should.Throw<ArgumentNullException>(() => new EventAggregator(null));
        }

        [TestMethod]
        public void ShouldGetBackingAggregatorFromDiContainer()
        {
            var ea = new CAL.EventAggregator();
            container.RegisterInstance(typeof(CAL.IEventAggregator),
                                       ea,
                                       new ContainerControlledLifetimeManager());
            ea.ShouldBeInstanceOfType<CAL.EventAggregator>();

            // Check that the same singleton is being used.
            new EventAggregator(container).BaseAggregator.ShouldBe(ea);
            new EventAggregator(container).BaseAggregator.ShouldBe(ea);
            new EventAggregator(container).BaseAggregator.ShouldBe(ea);
        }

        [TestMethod]
        public void ShouldCreateSingleBackingAggregatorIfContainerDoesNotHaveOneRegistered()
        {
            container = new UnityContainer();
            container.TryResolve<CAL.IEventAggregator>().ShouldBe(null);

            aggregator = new EventAggregator(container);
            aggregator.BaseAggregator.ShouldBe(container.Resolve<CAL.IEventAggregator>());
        }

        [TestMethod]
        public void ShouldUnsubscribeFromEvent()
        {
            var fireCount = 0;
            Action<MyEvent> action = obj => fireCount++;

            // Subscribe, then immediately unsubscribe from the event.
            aggregator.Subscribe(action);
            aggregator.Unsubscribe(action);

            AsyncTest.Start(test =>
                    {
                        // Fire the event.
                        aggregator.Publish(new MyEvent());

                        // Assert that the event was fired.
                        Thread.Sleep(AsyncDelay);
                        fireCount.ShouldBe(0); // Should not have fired because it was unsubscribed.
                        test.Complete();
                    });
        }
        #endregion

        #region Tests - Subscribe and Publish
        [TestMethod]
        public void ShouldPublishAndSubscribeOverload1()
        {
            var fireCount = 0;
            aggregator.Subscribe<MyEvent>(obj => fireCount++);
            AsyncPublishAssert(() => fireCount);
        }

        [TestMethod]
        public void ShouldPublishAndSubscribeOverload2()
        {
            var fireCount = 0;
            aggregator.Subscribe<MyEvent>(obj => fireCount++, false);
            AsyncPublishAssert(() => fireCount);
        }

        [TestMethod]
        public void ShouldPublishAndSubscribeOverload3()
        {
            var fireCount = 0;
            aggregator.Subscribe<MyEvent>(obj => fireCount++, ThreadOption.PublisherThread);
            AsyncPublishAssert(() => fireCount);
        }

        [TestMethod]
        public void ShouldPublishAndSubscribeOverload4()
        {
            var fireCount = 0;
            aggregator.Subscribe<MyEvent>(obj => fireCount++, ThreadOption.BackgroundThread, true);
            AsyncPublishAssert(() => fireCount);
        }

        [TestMethod]
        public void ShouldPublishAndSubscribeOverload5()
        {
            var fireCount = 0;
            aggregator.Subscribe<MyEvent>(obj => fireCount++, ThreadOption.BackgroundThread, true, o => true);
            AsyncPublishAssert(() => fireCount);
        }

        private void AsyncPublishAssert(Func<int> fireCount)
        {
            AsyncTest.Start(test =>
                            {
                                // Fire the event.
                                aggregator.Publish(new MyEvent());

                                // Assert that the event was fired.
                                Thread.Sleep(AsyncDelay);
                                fireCount().ShouldBe(1);
                                test.Complete();
                            });
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
