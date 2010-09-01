namespace Open.Core
{
    /// <summary>A visual representation of a log.</summary>
    public interface ILogView
    {
        /// <summary>Appends the given message to the log.</summary>
        /// <param name="message">The message to write (HTML).</param>
        /// <param name="cssClass">The CSS class to apply to the message text.</param>
        void Insert(string message, string cssClass);

        /// <summary>Inserts a visual break into the log.</summary>
        void LineBreak();

        /// <summary>Clears the log.</summary>
        void Clear();
    }
}