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
using System.Linq;
using System.Collections.Generic;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;

namespace Open.Core.Common.Testing
{
    /// <summary>A test dummy of the 'EventAggregator'.</summary>
    public class EventAggregatorStub : IEventAggregator
    {
        #region Head
        private readonly Dictionary<Type, MockItem> mocks = new Dictionary<Type, MockItem>();
        #endregion

        #region Methods
        /// <summary>Gets the number of subscribers to the specified event.</summary>
        /// <typeparam name="T">The event type.</typeparam>
        public int SubscribedCount<T>() where T : CompositePresentationEvent<T>
        {
            return mocks.Values.Sum(m => m.SubscribedActions.Count);
        }
        
        /// <summary>Gets the number times the specified event has been published (fired).</summary>
        /// <typeparam name="T">The event type.</typeparam>
        public int PublishedCount<T>() where T : CompositePresentationEvent<T>
        {
            var item = GetMockItem<T>();
            return item == null ? 0 : item.PublishedCount;
        }
        #endregion

        #region Methods - ShouldFire | ShouldNotFire
        /// <summary>Asserts that when the specified action was invoked the event was fired at least once.</summary>
        /// <typeparam name="T">The event type.</typeparam>
        /// <param name="action"></param>
        public void ShouldFire<T>(Action action) where T : CompositePresentationEvent<T>
        {
            if (GetFireCount<T>(action) == 0) throw new AssertionException(string.Format("Expected the event '{0}' to be fired at least once.", typeof(T).Name));
        }

        /// <summary>Asserts that when the specified action is invoked the event was fired a specific number of times.</summary>
        /// <typeparam name="T">The event type.</typeparam>
        /// <param name="total">The total number of times the event should have fired.</param>
        /// <param name="action"></param>
        public void ShouldFire<T>(int total, Action action) where T : CompositePresentationEvent<T>
        {
            var fireCount = GetFireCount<T>(action);
            if (fireCount != total) throw new AssertionException(string.Format("Expected the event '{0}' to be fired {1} times. Instead it fired {2} times.", typeof(T).Name, total, fireCount));
        }

        /// <summary>Asserts that when the specified action was invoked the event was not fired.</summary>
        /// <typeparam name="T">The event type.</typeparam>
        /// <param name="action"></param>
        public void ShouldNotFire<T>(Action action) where T : CompositePresentationEvent<T>
        {
            if (GetFireCount<T>(action) != 0) throw new AssertionException(string.Format("Expected the event '{0}' to not fire.", typeof(T).Name));
        }

        private int GetFireCount<T>(Action action) where T : CompositePresentationEvent<T>
        {
            // Setup initial conditions.
            if (action == null) throw new ArgumentNullException("action", "A test action was not passed to the method");
            var preCount = PublishedCount<T>();

            // Invoke the action.
            action();

            // Return the difference in the published (fired) count.
            return PublishedCount<T>() - preCount;
        }
        #endregion

        #region Methods - Interface
        public void Publish<T>(T message) where T : CompositePresentationEvent<T>
        {
            var item = GetMockItem<T>() ?? CreateMock<T>();
            item.PublishedCount ++;
            item.InvokeSubscribedActions(message);
        }

        public void Unsubscribe<T>(Action<T> action) where T : CompositePresentationEvent<T>
        {
            var item = GetMockItem<T>();
            if (item != null) item.SubscribedActions.Remove(action);
        }

        public void Subscribe<T>(Action<T> action) where T : CompositePresentationEvent<T>
        {
            SubscribeToMock(action);
        }

        public void Subscribe<T>(Action<T> action, bool keepSubscriberReferenceAlive) where T : CompositePresentationEvent<T>
        {
            SubscribeToMock(action);
        }

        public void Subscribe<T>(Action<T> action, ThreadOption threadOption) where T : CompositePresentationEvent<T>
        {
            SubscribeToMock(action);
        }

        public void Subscribe<T>(Action<T> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive) where T : CompositePresentationEvent<T>
        {
            SubscribeToMock(action);
        }

        public void Subscribe<T>(Action<T> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<T> filter) where T : CompositePresentationEvent<T>
        {
            SubscribeToMock(action);
        }        
        #endregion

        #region Internal
        private MockItem GetMockItem<T>() where T : EventBase
        {
            var type = typeof(T);
            return mocks.ContainsKey(type) ? mocks[type] : null;
        }

        private void SubscribeToMock<T>(Action<T> action) where T : CompositePresentationEvent<T>
        {
            var item = GetMockItem<T>() ?? CreateMock<T>();
            item.SubscribedActions.Add(action);
        }

        private MockItem CreateMock<T>() where T : CompositePresentationEvent<T>
        {
            var type = typeof (T);
            var mock = new MockItem();
            mocks.Add(type, mock);
            return mock;
        }
        #endregion

        private class MockItem
        {
            #region Head
            private List<object> subscribedActions;
            #endregion

            #region Properties
            public int PublishedCount { get; set; }
            public List<object> SubscribedActions
            {
                get
                {
                    if (subscribedActions == null) subscribedActions = new List<object>();
                    return subscribedActions;
                }
            }
            #endregion

            #region Methods
            public void InvokeSubscribedActions<T>(T payload) where T : EventBase
            {
                if (subscribedActions == null) return;
                foreach (var item in SubscribedActions.ToList())
                {
                    var action = item as Action<T>;
                    if (action != null) action(payload);
                }
            }
            #endregion
        }
    }
}
