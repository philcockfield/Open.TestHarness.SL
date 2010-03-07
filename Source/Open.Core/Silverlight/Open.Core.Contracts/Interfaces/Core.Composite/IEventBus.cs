using System;

namespace Open.Core.Composite
{
    /// <summary>Provides a unified hub for subscribing and publishing events (aka. EventAggregator).</summary>
    public interface IEventBus
    {
        /// <summary>Publishes (fires) an event to all subscribed (listening) parties.</summary>
        /// <typeparam name="TEvent">The type of the event to publish.</typeparam>
        /// <param name="message">The payload of the event.</param>
        void Publish<TEvent>(TEvent message);

        /// <summary>Unsubscribes from an event within the event-bus.</summary>
        /// <typeparam name="TEvent">The type of the event to unsubscribe from.</typeparam>
        /// <param name="action">The action that is performed when the event fires.</param>
        void Unsubscribe<TEvent>(Action<TEvent> action);

        /// <summary>Subscribes to an event within the event-bus.</summary>
        /// <typeparam name="TEvent">The type of the event to subscribe to (which is also the event payload).</typeparam>
        /// <param name="action">The action that is performed when the event fires.</param>
        void Subscribe<TEvent>(Action<TEvent> action);
    }
}