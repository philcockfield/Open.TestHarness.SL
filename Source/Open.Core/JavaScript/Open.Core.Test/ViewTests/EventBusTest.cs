// EventBusTest.cs
//

using System;
using System.Collections;

namespace Open.Core.Test.ViewTests
{
    public class EventBusTest
    {
        #region Head
        private IEventBus iEventBus;
        private EventBus eventBus;
        private int invokeCount1;
        private int invokeCount2;

        public void ClassInitialize(){}

        public void TestInitialize()
        {
            iEventBus = EventBus.GetSingleton("TestKey");
            eventBus = iEventBus as EventBus;
        }

        public void TestCleanup()
        {
            eventBus.Dispose();
            invokeCount1 = 0;
        }

        internal void MyHandler1() { invokeCount1++; }
        internal void MyHandler2() { invokeCount2++; }
        #endregion

        #region Methods
        public void TEMP()
        {
            
        }

        public void ShouldCreateEventBus()
        {
            Should.NotBeNull(EventBus.GetSingleton("Key"));
        }

        public void ShouldGetSingleton()
        {
            object key = "MyKey";
            Should.Equal(EventBus.GetSingleton(key), EventBus.GetSingleton(key));
        }

        public void ShouldIncreaseCountWhenHandlerAdded()
        {
            Should.Equal(eventBus.Count, 0);
            eventBus.Subscribe(typeof(MyEvent), MyHandler1);
            Should.Equal(eventBus.Count, 1);
        }

        public void ShouldDecreaseCountWhenHandlerRemoved()
        {
            iEventBus.Subscribe(typeof(MyEvent), MyHandler1);
            iEventBus.Subscribe(typeof(MyEvent), MyHandler2);
            Should.Equal(eventBus.Count, 2);
            
            iEventBus.Unsubscribe(MyHandler1);
            Should.Equal(eventBus.Count, 1);
        }

        public void ShouldNotAddSameHandlerTwice()
        {
            iEventBus.Subscribe(typeof(MyEvent), MyHandler1);
            iEventBus.Subscribe(typeof(MyEvent), MyHandler1);
            Should.Equal(eventBus.Count, 1);
        }

        public void ShouldClearCountWhenCleared()
        {
            iEventBus.Subscribe(typeof(MyEvent), MyHandler1);
            iEventBus.Subscribe(typeof(MyEvent), MyHandler2);
            Should.Equal(eventBus.Count, 2);
            eventBus.Clear();
            Should.Equal(eventBus.Count, 0);
        }

        public void ShouldClearCountWhenDisposed()
        {
            iEventBus.Subscribe(typeof(MyEvent), MyHandler1);
            iEventBus.Subscribe(typeof(MyEvent), MyHandler2);
            Should.Equal(eventBus.Count, 2);
            eventBus.Dispose();
            Should.Equal(eventBus.Count, 0);
        }
        #endregion
    }

    public class MyEvent { }
}
