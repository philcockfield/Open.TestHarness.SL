namespace Open.Core
{
    /// <summary>Flags indicating the level of severity of a message being written to the log.</summary>
    public enum LogSeverity
    {
        Info = 0,
        Debug = 1,
        Warning = 2,
        Error = 3,
    }


    /// <summary>An output log.</summary>
    public interface ILog
    {
        /// <summary>Writes a message to the log.</summary>
        /// <param name="message">The message to write (HTML).</param>
        /// <param name="severity">The severity of the message.</param>
        void Write(string message, LogSeverity severity);

        /// <summary>Inserts a visual break into the log.</summary>
        void LineBreak();

        /// <summary>Clears the log.</summary>
        void Clear();

        /// <summary>Registers a log viewer to emit output to (multiple views can be associated with the log).</summary>
        /// <param name="view">The log view to emit to.</param>
        void RegisterView(ILogView view);
    }
}
