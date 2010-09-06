using System;
using System.Collections;

namespace Open.Core
{
    /// <summary>An event aggregator.</summary>
    public class EventBus : IEventBus, IDisposable
    {
        #region Head
        private readonly object key;
        private static readonly ArrayList singletons = new ArrayList();
        private readonly ArrayList handlers = new ArrayList();

        /// <summary>Constructor.</summary>
        /// <param name="key">The unique identifier of the event-bus.</param>
        private EventBus(object key)
        {
            this.key = key;
        }

        /// <summary>Destructor.</summary>
        public void Dispose()
        {
            Clear();
        }
        #endregion

        #region Properties : IEventBus
        public object Key { get { return key; } }
        public int Count { get { return handlers.Count; } }
        #endregion

        #region Methods
        /// <summary>Adds a handler for a specific event type.</summary>
        /// <param name="eventType">The type of the event.</param>
        /// <param name="handler">The handler to invoke when the event is fired.</param>
        public void Subscribe(Type eventType, Action handler)
        {
            // Create the storage wrapper.
            EventBusHandler item = new EventBusHandler(eventType, handler);

            // Store the wrapper.
            handlers.Add(item);
        }

        /// <summary>Removes a handler.</summary>
        /// <param name="handler">The event handle to unsubscribe.</param>
        public void Unsubscribe(Action handler)
        {
            EventBusHandler item = GetHandler(handler);
            if (item == null) return;
            item.Dispose();
            handlers.Remove(item);
        }

        /// <summary>Clears all event handlers.</summary>
        public void Clear()
        {
            Helper.Collection.DisposeAndClear(handlers);
        }
        #endregion

        #region Methods : Static
        /// <summary>Retrieves a singleton instance of an event-bus with the given key.</summary>
        /// <param name="key">The unique identifier of the event-bus.</param>
        public static IEventBus GetSingleton(object key)
        {
            // Look for existing event-bus.
            IEventBus bus = Helper.Collection.First(singletons, delegate(object o)
                                                                   {
                                                                       return ((IEventBus)o).Key == key;
                                                                   }) as IEventBus;
            if (bus != null) return bus;

            // Create and store a new instance of the event-bus.
            bus = new EventBus(key);
            singletons.Add(bus);

            // Finish up.
            return bus;
        }
        #endregion

        #region Internal
        private EventBusHandler GetHandler(Action handler)
        {
            if (handler == null) return null;
            return Helper.Collection.First(handlers, delegate(object o)
                                                         {
                                                             return true; //TEMP 
//                                                             return ((EventBusHandler)o).Handler == handler;
                                                         }) as EventBusHandler;
        }
        #endregion
    }

    internal class EventBusHandler : IDisposable
    {
        #region Head
        public Type type;
        public Action handler;

        public EventBusHandler(Type type, Action handler)
        {
            this.type = type;
            this.handler = handler;
        }

        public void Dispose()
        {
            type = null;
            handler = null;
        }
        #endregion

        #region Properties
        public Type Type { get { return type; } }
        public Action Handler { get { return handler; } }
        #endregion
    }
}
