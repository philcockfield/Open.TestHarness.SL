namespace Open.Core
{
    /// <summary>Static log writer.</summary>
    public static class Log
    {
        #region Head
        private static LogWriter writer;
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the log is active.  When False nothing is written to the log.</summary>
        /// <remarks>Use this for pausing output to the log.</remarks>
        public static bool IsActive
        {
            get { return writer.IsActive; }
            set { writer.IsActive = value; }
        }

        /// <summary>Gets the specific log-writer instance that the static methods write to.</summary>
        public static LogWriter Writer { get { return writer ?? (writer = new LogWriter()); } }

        /// <summary>Gets or sets the view-control to write to.</summary>
        public static ILogView View
        {
            get { return Writer.View; }
            set { Writer.View = value; }
        }
        #endregion

        #region Methods : Write
        /// <summary>Writes a informational message to the log (as a bold title).</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Title(string message) { Writer.Title(message); }

        /// <summary>Writes a informational message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Info(object message) { Writer.Info(message); }

        /// <summary>Writes a debug message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Debug(object message) { Writer.Debug(message); }

        /// <summary>Writes a warning to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Warning(object message) { Writer.Warning(message); }

        /// <summary>Writes an error message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Error(object message) { Writer.Error(message); }

        /// <summary>Writes a success message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Success(object message) { Writer.Success(message); }

        /// <summary>Writes a message to the log.</summary>
        /// <param name="message">The message to write (HTML).</param>
        /// <param name="severity">The severity of the message.</param>
        public static void Write(object message, LogSeverity severity)
        {
            Writer.Write(message, severity);
        }
        #endregion

        #region Methods : Dividers
        /// <summary>Inserts a line break to the log.</summary>
        public static void LineBreak() { Writer.LineBreak(); }

        /// <summary>Inserts a new section divider.</summary>
        public static void NewSection() { Writer.NewSection(); }
        #endregion

        #region Methods
        /// <summary>Clears the log.</summary>
        public static void Clear() { Writer.Clear(); }
        #endregion
    }
}
