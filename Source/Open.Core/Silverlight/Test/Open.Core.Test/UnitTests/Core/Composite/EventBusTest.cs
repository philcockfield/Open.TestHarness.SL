using System;
using System.ComponentModel.Composition;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.Composite;

namespace Open.Core.Test.UnitTests.Core.Composite
{
    [Tag("current")]
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

        public void OnFire(MyEvent e)
        {
            fireCount++;
            if (e.Callback != null) e.Callback();
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

            Action callback = () =>
                                  {
                                      fireCount.ShouldBe(1);
                                      EnqueueTestComplete();
                                  };

            var publishArgs = new MyEvent
                      {
                          Message = "My Message",
                          Callback = callback,
                      };

            eventBus.Subscribe<MyEvent>(OnFire);
            eventBus.Publish(publishArgs);
            fireCount.ShouldBe(0);
        }


        [TestMethod]
        public void ShouldFireOnHandler()
        {
            eventBus.IsAsynchronous = false;
            eventBus.Subscribe<MyEvent>(OnFire);
            eventBus.Publish(new MyEvent());
            fireCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldFireAssertion()
        {
            eventBus.IsAsynchronous = false;
            eventBus.ShouldFire<MyEvent>(() => eventBus.Publish(new MyEvent()));
        }

        [TestMethod]
        public void ShouldThrowIfCallbackDelegateIsNotPublic()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => eventBus.Subscribe<MyEvent>(@event => { }));  
        }
        #endregion

        #region Stubs
        public class MyEvent
        {
            public string Message { get; set; }
            public Action Callback { get; set; }
        }
        #endregion
    }
}
