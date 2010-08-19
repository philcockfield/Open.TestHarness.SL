namespace Open.Core
{
    /// <summary>
    ///     Used to callback into a single delegate form different events 
    ///     (with args distinguishing which event caused the delegate to be invoked).
    /// </summary>
    /// <param name="eventIdentifier">The identifier of the event.</param>
    public delegate void EventCallback(string eventIdentifier);
}
