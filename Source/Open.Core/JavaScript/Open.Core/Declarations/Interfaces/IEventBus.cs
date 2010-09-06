using System;

namespace Open.Core
{
    public interface IEventBus
    {
        object Key { get; }

        /// <summary>Adds a handler for a specific event type.</summary>
        /// <param name="eventType">The type of the event.</param>
        /// <param name="handler">The handler to invoke when the event is fired.</param>
        void Subscribe(Type eventType, Action handler);

        /// <summary>Removes a handler.</summary>
        /// <param name="handler">The event handle to unsubscribe.</param>
        void Unsubscribe(Action handler);
    }
}