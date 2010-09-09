namespace Open.Core
{
    /// <summary>A visual representation of a log.</summary>
    public interface ILogView
    {
        /// <summary>Appends the given message to the log.</summary>
        /// <param name="message">The message to write (HTML).</param>
        /// <param name="cssClass">The CSS class to apply to the message text.</param>
        void Insert(string message, string cssClass);

        /// <summary>Inserts a visual divider into the log.</summary>
        /// <param name="type">The type of divider to insert.</param>
        void Divider(LogDivider type);

        /// <summary>Clears the log.</summary>
        void Clear();
    }
}