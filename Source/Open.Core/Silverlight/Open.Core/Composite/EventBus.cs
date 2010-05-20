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
using System.Reflection;
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
            Publish(message, IsAsynchronous);
        }

        /// <summary>Publishes (fires) an event to all subscribed (listening) parties.</summary>
        /// <typeparam name="TEvent">The type of the event to publish.</typeparam>
        /// <param name="message">The payload of the event.</param>
        /// <param name="isAsynchronous">Flag indicating if the event should be published asynchronously.</param>
        public void Publish<TEvent>(TEvent message, bool isAsynchronous)
        {
            var handlers = GetHandlerCollection<TEvent>();
            if (handlers == null) return;
            if (isAsynchronous)
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
            // Setup initial conditions.
            if (action == null) throw new ArgumentNullException("action");
            if (IsSubscribed(action)) return;

#if SILVERLIGHT
            // Ensure the action is public.
            // NB: This is because reflection in Silverlight can only work against public members.
            if (!action.Method.IsPublic) throw new ArgumentOutOfRangeException(
                            "action",
                            string.Format("Cannot subscribe to event '{0}' because the callback delegate end-point is not a public method.", 
                            typeof(TEvent).Name));
#endif

            var collection = GetOrCreateHandlerCollection<TEvent>();
            lock (collection)
            {
                collection.Add(action);
            }
        }
        #endregion

        #region Methods
        /// <summary>Retrieves the subscribed actions for the given event-type.</summary>
        /// <typeparam name="TEvent">The type of the event to retrieve the actions for.</typeparam>
        public IEnumerable<Action<TEvent>> GetActions<TEvent>()
        {
            // Retrieve the corresponding type-handler collection.
            var collection = GetHandlerCollection<TEvent>();
            if (collection == null) return new List<Action<TEvent>>(); // Empty collection.

            // Return the handlers.
            return collection.Handlers.Select(m => m.TryGetDelegate() as Action<TEvent>);
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
            return collection.Handlers.Count(m => m.IsMatch(action)) != 0;
        }

        /// <summary>Gets the number of handlers for the given event-type that have subscribed.</summary>
        /// <typeparam name="TEvent">The type of the event to look for.</typeparam>
        public int SubscribedCount<TEvent>()
        {
            var collection = GetHandlerCollection<TEvent>();
            return collection == null ? 0 : collection.Handlers.Count;
        }

        /// <summary>Gets the number of times the given event has been published.</summary>
        /// <typeparam name="TEvent">The type of the event to look for.</typeparam>
        public long PublishedCount<TEvent>()
        {
            var collection = GetHandlerCollection<TEvent>();
            return collection == null ? 0 : collection.InvokeCount;
        }
        #endregion

        #region Internal
        private EventTypeHandlers GetHandlerCollection<TEvent>()
        {
            var eventType = typeof(TEvent);
            return typeHandlers.FirstOrDefault(m => m.Type.FullName == eventType.FullName);
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
                Handlers = new List<ActionReference>();
            }

            protected override void OnDisposed()
            {
                Handlers.Clear();
            }
            #endregion

            #region Properties
            public Type Type { get; private set; }
            public List<ActionReference> Handlers { get; private set; }
            public long  InvokeCount { get; private set; }
            #endregion

            #region Methods
            public void Add<TEvent>(Action<TEvent> action)
            {
                Handlers.Add(ActionReference.Create(action));
            }

            public void Remove<TEvent>(Action<TEvent> action)
            {
                var reference = Handlers.FirstOrDefault(m => m.IsMatch(action));
                if (reference != null) Handlers.Remove(reference);
            }

            public void InvokeActions<TEvent>(TEvent message)
            {
                foreach (var reference in Handlers.ToList())
                {
                    if (reference.Invoke(message))
                    {
                        InvokeCount++;
                    }
                    else
                    {
                        Handlers.Remove(reference);
                    }
                }
            }
            #endregion
        }

        public class ActionReference : WeakDelegateReference
        {
            #region Head
            private ActionReference(object actionTarget, MethodInfo actionMethod, Type actionType) 
                : base(actionTarget, actionMethod, actionType)
            {
            }

            public static ActionReference Create<TEvent>(Action<TEvent> action)
            {
                return new ActionReference(action.Target, action.Method, action.GetType());
            }
            #endregion

            #region Methods
            public bool Invoke<TEvent>(TEvent message)
            {
                // Setup initial conditions.
                if (!TargetWeakReference.IsAlive) return false;

                // Invoke the Action.
                var action = TryGetDelegate() as Action<TEvent>;
                if (action != null) action(message);

                // Finish up.
                return true;
            }
            #endregion
        }
    }
}
