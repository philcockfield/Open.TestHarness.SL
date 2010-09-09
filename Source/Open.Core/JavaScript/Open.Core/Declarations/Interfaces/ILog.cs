namespace Open.Core
{
    /// <summary>Flags indicating the level of severity of a message being written to the log.</summary>
    public enum LogSeverity
    {
        Info = 0,
        Debug = 1,
        Warning = 2,
        Error = 3,
        Success = 4,
    }

    /// <summary>Flags representing the type of visual dividers that can be inserted into the log.</summary>
    public enum LogDivider
    {
        LineBreak = 0,
        Section = 1,
    }


    /// <summary>An output log.</summary>
    public interface ILog
    {
        /// <summary>Writes a message to the log.</summary>
        /// <param name="message">The message to write (HTML).</param>
        /// <param name="severity">The severity of the message.</param>
        void Write(string message, LogSeverity severity);

        /// <summary>Inserts a visual divider into the log.</summary>
        /// <param name="type">The type of divider to insert.</param>
        void Divider(LogDivider type);

        /// <summary>Clears the log.</summary>
        void Clear();

        /// <summary>Registers a log viewer to emit output to (multiple views can be associated with the log).</summary>
        /// <param name="view">The log view to emit to.</param>
        void RegisterView(ILogView view);
    }
}
