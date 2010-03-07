using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Composite;
using Open.Core.Composition;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Composite
{
    [TestClass]
    public class EventBusTest
    {
        #region Head
        private EventBus eventBus;
        private int fireCount;

        [TestInitialize]
        public void TestSetup()
        {
            fireCount = 0;
            eventBus = new EventBus();
        }

        private void OnFire(Event1 e)
        {
            fireCount++;
        }
        #endregion
        
        #region Tests
        [Import(typeof(IEventBus))]
        public IEventBus EventBus { get; set; }

        [TestMethod]
        public void ShouldImport()
        {
            DirectoryCompositionInitializer.SatisfyImports(this);
            EventBus.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldBeAsynchronousByDefault()
        {
            eventBus.IsAsynchronous.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldThrowIfNullPassed()
        {
            Should.Throw<ArgumentNullException>(() => eventBus.Subscribe<object>(null));
        }

        [TestMethod]
        public void ShouldContainNoHandlers()
        {
            eventBus.SubscribedCount<Event1>().ShouldBe(0);
            eventBus.GetActions<Event1>().Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldSubscribe()
        {
            Action<Event1> handler = e => {};
            eventBus.Subscribe(handler);
            eventBus.GetActions<Event1>().ShouldContain(handler);
        }

        [TestMethod]
        public void ShouldSubscribeMultipleTypes()
        {
            Action<Event1> handler1 = e => { };
            Action<Event2> handler2 = e => { };

            eventBus.Subscribe(handler1);
            eventBus.Subscribe(handler2);

            eventBus.GetActions<Event1>().Count().ShouldBe(1);
            eventBus.GetActions<Event2>().Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldSubscribeMultipleHandlers()
        {
            Action<Event1> handler1 = e => { };
            Action<Event1> handler2 = e => { };

            eventBus.Subscribe(handler1);
            eventBus.Subscribe(handler2);

            eventBus.SubscribedCount<Event1>().ShouldBe(2);
            eventBus.GetActions<Event1>().ShouldContain(handler1);
            eventBus.GetActions<Event1>().ShouldContain(handler2);
        }

        [TestMethod]
        public void ShouldAllowUnsubscribeWithNullParam()
        {
            eventBus.Unsubscribe<Event1>(null);
        }


        [TestMethod]
        public void ShouldUnsubscribe()
        {
            Action<Event1> handler = e => { };
            eventBus.Subscribe(handler);
            eventBus.IsSubscribed(handler).ShouldBe(true);

            eventBus.Unsubscribe(handler);
            eventBus.IsSubscribed(handler).ShouldBe(false);
        }

        [TestMethod]
        public void ShouldNotUnsubscribeNonSubscribedHandler()
        {
            Action<Event1> handler1 = e => { };
            Action<Event1> handler2 = e => { };
            eventBus.Subscribe(handler1);

            eventBus.IsSubscribed(handler1).ShouldBe(true);
            eventBus.IsSubscribed(handler2).ShouldBe(false);

            eventBus.Unsubscribe(handler2);
            eventBus.IsSubscribed(handler1).ShouldBe(true);
            eventBus.IsSubscribed(handler2).ShouldBe(false);
        }

        [TestMethod]
        public void ShouldNotSubscribeSameHandlerTwice()
        {
            Action<Event1> handler = e => { };
            eventBus.Subscribe(handler);
            eventBus.Subscribe(handler);
            eventBus.Subscribe(handler);

            eventBus.GetActions<Event1>().Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldDetermineIfIsSubscribed()
        {
            eventBus.IsSubscribed<Event1>(null).ShouldBe(false);

            Action<Event1> handler = e => { };
            eventBus.IsSubscribed(handler).ShouldBe(false);

            eventBus.Subscribe(handler);
            eventBus.IsSubscribed(handler).ShouldBe(true);
        }


        [TestMethod]
        public void ShouldClearReferencesOnDisposed()
        {
            Action<Event1> handler1 = e => { };
            Action<Event1> handler2 = e => { };
            eventBus.Subscribe(handler1);
            eventBus.Subscribe(handler2);

            eventBus.GetActions<Event1>().Count().ShouldBe(2);

            eventBus.Dispose();
            eventBus.GetActions<Event1>().Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldHaveNothingPublished()
        {
            eventBus.PublishedCount<Event1>().ShouldBe(0L);
        }

        [TestMethod]
        public void ShouldPublishSynchronously()
        {
            eventBus.IsAsynchronous = false;
            Event1 firedArgs = null;

            Action<Event1> handler = e => { firedArgs = e; };
            eventBus.Subscribe(handler);

            // ---

            var publishArgs = new Event1 {Message = "My Message"};
            eventBus.Publish(publishArgs);

            firedArgs.ShouldBe(publishArgs);
            eventBus.PublishedCount<Event1>().ShouldBe(1L);
            eventBus.PublishedCount<Event2>().ShouldBe(0L);
        }

        [TestMethod]
        public void ShouldPublishEventAsynchronously()
        {
            eventBus.IsAsynchronous.ShouldBe(true);
            AsyncTest.Start(test =>
                                {
                                    var publishArgs = new Event1 { Message = "My Message" };
                                    var fired = false;
                                    Action<Event1> handler = e => 
                                                        {
                                                            fired = true;
                                                            e.ShouldBe(publishArgs);
                                                            test.Complete();
                                                        };

                                    eventBus.Subscribe(handler);
                                    eventBus.Publish(publishArgs);
                                    fired.ShouldBe(false);
                                });
        }

        [TestMethod]
        public void ShouldUnsubscribeFromStaleEvents()
        {
            eventBus.IsAsynchronous = false;

            var mock = new HandlerContainer();
            eventBus.Subscribe<Event1>(mock.OnFire);

            // ---

            mock.Dispose();
            GC.Collect();

            eventBus.Publish(new Event1());
            mock.FireCount.ShouldBe(0);
            eventBus.IsSubscribed<Event1>(mock.OnFire).ShouldBe(false);
            eventBus.GetActions<Event1>().Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldFireOnPrivateHandler()
        {
            eventBus.IsAsynchronous = false;
            eventBus.Subscribe<Event1>(OnFire);
            eventBus.Publish(new Event1());
            fireCount.ShouldBe(1);
        }
        #endregion

        #region Stubs
        public class Event1
        {
            public string Message { get; set; }
        }

        public class Event2
        {
            public int Number { get; set; }
        }

        public class HandlerContainer : DisposableBase
        {
            public int FireCount;
            public void OnFire(Event1 e)
            {
                FireCount++;
            }
        }


        #endregion
    }
}
