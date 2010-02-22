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
using System.ComponentModel.Composition;
using System.Linq;
using Open.Core.Common;

namespace Open.Core.Composite
{
    /// <summary>Provides a unified hub for subscribing and publishing events (aka. EventAggregator).</summary>
    [Export(typeof(IEventBus))]
    public partial class EventBus : DisposableBase, IEventBus
    {
        #region Head
        private readonly List<EventTypeHandlers> typeHandlers = new List<EventTypeHandlers>();

        /// <summary>Constructor.</summary>
        public EventBus()
        {
            IsAsynchronous = true;
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            foreach (var item in typeHandlers)
            {
                item.Dispose();
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets whether events are fired asynchronously (used for testing).</summary>
        public bool IsAsynchronous { get; set; }
        #endregion

        #region Methods - Interface
        /// <summary>Publishes (fires) an event to all subscribed (listening) parties.</summary>
        /// <typeparam name="TEvent">The type of the event to publish.</typeparam>
        /// <param name="message">The payload of the event.</param>
        public void Publish<TEvent>(TEvent message)
        {
            var handlers = GetHandlerCollection<TEvent>();
            if (handlers == null) return;
            if (IsAsynchronous)
            {
                PublishAsynchronously(() => handlers.InvokeActions(message));
            }
            else
            {
                handlers.InvokeActions(message);
            }
        }

        /// <summary>Unsubscribes from an event within the event-bus.</summary>
        /// <typeparam name="TEvent">The type of the event to unsubscribe from.</typeparam>
        /// <param name="action">The action that is performed when the event fires.</param>
        public void Unsubscribe<TEvent>(Action<TEvent> action)
        {
            // Setup initial conditions.
            if (Equals(action, default(TEvent))) return;
            var collection = GetHandlerCollection<TEvent>();
            if (collection == null) return;

            // Remove the action.
            lock (collection)
            {
                collection.Remove(action);
            }
        }

        /// <summary>Subscribes to an event within the event-bus.</summary>
        /// <typeparam name="TEvent">The type of the event to subscribe to (which is also the event payload).</typeparam>
        /// <param name="action">The action that is performed when the event fires (event handler).</param>
        public void Subscribe<TEvent>(Action<TEvent> action)
        {
            if (Equals(action, default(TEvent))) throw new ArgumentNullException("action");
            if (IsSubscribed(action)) return;
            var collection = GetOrCreateHandlerCollection<TEvent>();
            lock (collection)
            {
                collection.Add(action);
            }
        }
        #endregion

        #region Methods
        /// <summary>Retrieves the subscribed actions for the given event-type.</summary>
        /// <typeparam name="TEvent">The type of the event to retrieve the actions for..</typeparam>
        public IEnumerable<Action<TEvent>> GetActions<TEvent>()
        {
            // Retrieve the corresponding type-handler collection.
            var collection = GetHandlerCollection<TEvent>();
            if (collection == null) return new List<Action<TEvent>>(); // Empty collection.

            // Return the handlers.
            return collection.Handlers.Select(m => m.Target).Cast<Action<TEvent>>();
        }

        /// <summary>
        ///     Gets whether the specified event-handler has been registered with the 
        ///     event-bus via the 'Subscribe' method.
        /// </summary>
        public bool IsSubscribed<TEvent>(Action<TEvent> action)
        {
            // Setup initial conditions.
            if (Equals(action, default(TEvent))) return false;

            // Retrieve the collection of handlers.
            var collection = GetHandlerCollection<TEvent>();
            if (collection == null) return false;

            // Determine if the action exists.
            return collection.Handlers.Count(m => (Action<TEvent>) m.Target == action) != 0;
        }
        #endregion

        #region Internal
        private EventTypeHandlers GetHandlerCollection<TEvent>()
        {
            var eventType = typeof(TEvent);
            return typeHandlers.FirstOrDefault(m => m.Type == eventType);
        }

        private EventTypeHandlers GetOrCreateHandlerCollection<TEvent>()
        {
            // Check if the collection has already been created.
            var collection = GetHandlerCollection<TEvent>();
            if (collection != null) return collection;

            // Create and store.
            collection = new EventTypeHandlers(typeof(TEvent));
            typeHandlers.Add(collection);

            // Finish up.
            return collection;
        }
        #endregion

        private class EventTypeHandlers : DisposableBase
        {
            #region Head
            public EventTypeHandlers(Type type)
            {
                Type = type;
                Handlers = new List<WeakReference>();
            }

            protected override void OnDisposed()
            {
                Handlers.Clear();
            }
            #endregion

            #region Properties
            public Type Type { get; private set; }
            public List<WeakReference> Handlers { get; private set; }
            #endregion

            #region Methods
            public void Add<TEvent>(Action<TEvent> action)
            {
                Handlers.Add(new WeakReference(action, false));
            }

            public void Remove<TEvent>(Action<TEvent> action)
            {
                var reference = Handlers.FirstOrDefault(m => (Action<TEvent>) m.Target == action);
                if (reference != null) Handlers.Remove(reference);
            }

            public void InvokeActions<TEvent>(TEvent message)
            {
                foreach (var reference in Handlers.ToList())
                {
                    var action = reference.Target as Action<TEvent>;
                    if (action != null)
                    {
                        action(message);
                    }
                    else
                    {
                        Handlers.Remove(reference);
                    }
                }
            }
            #endregion
        }
    }
}
