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
        /// <summary>Gets or sets the view-control to write to.</summary>
        ILogView View { get; set; }

        /// <summary>Writes a informational message to the log (as a bold title).</summary>
        /// <param name="message">The messge to write (HTML).</param>
        void Title(string message);

        /// <summary>Writes a informational message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        void Info(string message);

        /// <summary>Writes a debug message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        void Debug(string message);

        /// <summary>Writes a warning to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        void Warning(string message);

        /// <summary>Writes an error message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        void Error(string message);

        /// <summary>Writes a success message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        void Success(string message);

        /// <summary>Writes a message to the log.</summary>
        /// <param name="message">The message to write (HTML).</param>
        /// <param name="severity">The severity of the message.</param>
        void Write(string message, LogSeverity severity);

        /// <summary>Inserts a line break to the log.</summary>
        void LineBreak();

        /// <summary>Inserts a new section divider.</summary>
        void NewSection();

        /// <summary>Clears the log.</summary>
        void Clear();
    }
}
