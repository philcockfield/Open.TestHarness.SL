using System;
using System.ComponentModel.Composition;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.Composite;

namespace Open.Core.Test.UnitTests.Core.Composite
{
    [TestClass]
    public class EventBusTest : SilverlightUnitTest
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

        private void OnFire(MyEvent e)
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
            CompositionInitializer.SatisfyImports(this);
            EventBus.ShouldNotBe(null);
        }

        [TestMethod]
        [Asynchronous]
        public void ShouldPublishAsynchronously()
        {
            eventBus.IsAsynchronous.ShouldBe(true);

            var publishArgs = new MyEvent { Message = "My Message" };
            var fired = false;
            Action<MyEvent> handler = e =>
                                        {
                                            fired = true;
                                            e.ShouldBe(publishArgs);
                                            EnqueueTestComplete();
                                        };

                eventBus.Subscribe(handler);
                eventBus.Publish(publishArgs);
                fired.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldFireOnPrivateHandler()
        {
            eventBus.IsAsynchronous = false;
            eventBus.Subscribe<MyEvent>(OnFire);
            eventBus.Publish(new MyEvent());
            fireCount.ShouldBe(1);
        }
        #endregion

        #region Stubs
        public class MyEvent
        {
            public string Message { get; set; }
        }
        #endregion
    }
}
