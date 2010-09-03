namespace Open.Core
{
    /// <summary>
    ///     Used to callback into a single delegate form different events 
    ///     (with args distinguishing which event caused the delegate to be invoked).
    /// </summary>
    /// <param name="eventIdentifier">The identifier of the event.</param>
    public delegate void EventCallback(string eventIdentifier);

    /// <summary>Filter delegate used to determine whether an object is a match.</summary>
    /// <param name="obj">The object to examine.</param>
    /// <returns>True if the object is a match, otherwise False.</returns>
    public delegate bool IsMatch(object obj);
}
