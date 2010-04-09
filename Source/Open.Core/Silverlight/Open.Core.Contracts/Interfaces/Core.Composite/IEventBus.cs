using System;

namespace Open.Core.Composite
{
    /// <summary>Provides a unified hub for subscribing and publishing events (aka. EventAggregator).</summary>
    public interface IEventBus
    {
        /// <summary>Gets or sets whether events are fired asynchronously (used for testing).</summary>
        bool IsAsynchronous { get; set; }

        /// <summary>Publishes (fires) an event to all subscribed (listening) parties.</summary>
        /// <typeparam name="TEventArgs">The type of the event to publish.</typeparam>
        /// <param name="message">The payload of the event.</param>
        void Publish<TEventArgs>(TEventArgs message);

        /// <summary>Unsubscribes from an event within the event-bus.</summary>
        /// <typeparam name="TEventArgs">The type of the event to unsubscribe from.</typeparam>
        /// <param name="action">The action that is performed when the event fires.</param>
        void Unsubscribe<TEventArgs>(Action<TEventArgs> action);

        /// <summary>Subscribes to an event within the event-bus.</summary>
        /// <typeparam name="TEventArgs">The type of the event to subscribe to (which is also the event payload).</typeparam>
        /// <param name="action">The action that is performed when the event fires.</param>
        void Subscribe<TEventArgs>(Action<TEventArgs> action);
    }
}