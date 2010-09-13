namespace Open.Core
{
    /// <summary>Static log writer.</summary>
    public static class Log
    {
        #region Head
        private static LogWriter writer;
        private static bool isActive = true;
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the log is active.  When False nothing is written to the log.</summary>
        /// <remarks>Use this for pausing output to the log.</remarks>
        public static bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>Gets the specific log-writer instance that the static methods write to.</summary>
        public static LogWriter Writer { get { return writer ?? (writer = new LogWriter()); } }
        #endregion

        #region Methods : Write
        /// <summary>Writes a informational message to the log (as a bold title).</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Title(string message) { Write(Html.ToBold(message), LogSeverity.Info); }

        /// <summary>Writes a informational message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Info(string message) { Write(message, LogSeverity.Info); }

        /// <summary>Writes a debug message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Debug(string message) { Write(message, LogSeverity.Debug); }

        /// <summary>Writes a warning to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Warning(string message) { Write(message, LogSeverity.Warning); }

        /// <summary>Writes an error message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Error(string message) { Write(message, LogSeverity.Error); }

        /// <summary>Writes a success message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Success(string message) { Write(message, LogSeverity.Success); }

        /// <summary>Writes a message to the log.</summary>
        /// <param name="message">The message to write (HTML).</param>
        /// <param name="severity">The severity of the message.</param>
        public static void Write(string message, LogSeverity severity)
        {
            if (IsActive) Writer.Write(message, severity);
        }
        #endregion

        #region Methods : Dividers
        /// <summary>Inserts a line break to the log.</summary>
        public static void LineBreak() { if (IsActive) Writer.Divider(LogDivider.LineBreak); }

        /// <summary>Inserts a new section divider.</summary>
        public static void NewSection() { if (IsActive) Writer.Divider(LogDivider.Section); }
        #endregion

        #region Methods
        /// <summary>Clears the log.</summary>
        public static void Clear() { if (IsActive) Writer.Clear(); }

        /// <summary>Registers a log viewer to emit output to (multiple views can be associated with the log).</summary>
        /// <param name="view">The log view to emit to.</param>
        public static void RegisterView(ILogView view) { Writer.RegisterView(view); }
        #endregion
    }
}
