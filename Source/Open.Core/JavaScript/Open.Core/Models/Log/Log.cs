using System;
using System.Runtime.CompilerServices;
using Open.Core.Controls.HtmlPrimitive;

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
        /// <summary>Writes a message to the log.</summary>
        /// <param name="message">The message to write.</param>
        [AlternateSignature]
        public static extern void Write(object message);

        /// <summary>Writes a message to the log.</summary>
        /// <param name="message">The message to write.</param>
        /// <param name="backgroundColor">The background color to apply to the log entry.</param>
        public static void Write(object message, string backgroundColor)
        {
            Writer.Write(message, backgroundColor);
        }

        /// <summary>Writes a message to the log (prepended with an icon).</summary>
        /// <param name="message">The message to write.</param>
        /// <param name="icon">An icon .</param>
        [AlternateSignature]
        public extern static void WriteIcon(object message, Icons icon);

        /// <summary>Writes a message to the log (prepended with an icon).</summary>
        /// <param name="message">The message to write.</param>
        /// <param name="icon">An icon .</param>
        /// <param name="backgroundColor">The background color to apply to the log entry.</param>
        public static void WriteIcon(object message, Icons icon, string backgroundColor)
        {
            Writer.WriteIcon(message, icon, backgroundColor);
        }
        #endregion

        #region Methods : Write Custom
        /// <summary>Writes a informational message to the log (as a bold title).</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Title(string message) { Writer.Title(message); }

        /// <summary>Writes an event message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public static void Event(object message) { Writer.Event(message); }
        #endregion

        #region Methods : Write (Severity)
        /// <summary>Writes a message to the log.</summary>
        /// <param name="message">The message to write (HTML).</param>
        /// <param name="severity">The severity of the message.</param>
        public static void WriteSeverity(object message, LogSeverity severity) { Writer.WriteSeverity(message, severity); }

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
        #endregion

        #region Methods : List
        /// <summary>Writes an <UL></UL> to the log.</summary>
        /// <returns>The UL list object to use to populate with items.</returns>
        [AlternateSignature]
        public static extern IHtmlList WriteList();

        /// <summary>Writes an <UL></UL> to the log.</summary>
        /// <param name="title">The title of the list.</param>
        /// <returns>The UL list object to use to populate with items.</returns>
        [AlternateSignature]
        public static extern IHtmlList WriteList(string title);

        /// <summary>Writes an <UL></UL> to the log.</summary>
        /// <param name="title">The title of the list.</param>
        /// <param name="backgroundColor">The background color to apply to the log entry.</param>
        /// <returns>The UL list object to use to populate with items.</returns>
        public static IHtmlList WriteList(string title, string backgroundColor)
        {
            return Writer.WriteList(title, backgroundColor);
        }

        /// <summary>Writes an <UL></UL> to the log.</summary>
        /// <param name="title">The title of the list.</param>
        /// <param name="severity">The severity of the message.</param>
        /// <returns>The UL list object to use to populate with items.</returns>
        public static IHtmlList WriteListSeverity(string title, LogSeverity severity)
        {
            return Writer.WriteListSeverity(title, severity);
        }
        #endregion

        #region Methods : Properties
        /// <summary>Writes out the property values for the given object.</summary>
        /// <param name="instance">The object to write.</param>
        [AlternateSignature]
        public static extern void WriteProperties(object instance);

        /// <summary>Writes out the property values for the given object.</summary>
        /// <param name="instance">The object to write.</param>
        /// <param name="title">The title to put above the object.</param>
        public static void WriteProperties(object instance, string title)
        {
            Writer.WriteProperties(instance, title);
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
