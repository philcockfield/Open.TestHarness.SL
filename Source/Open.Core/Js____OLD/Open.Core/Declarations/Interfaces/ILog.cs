using System.Runtime.CompilerServices;
using Open.Core.Controls.HtmlPrimitive;

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
        void Info(object message);

        /// <summary>Writes a debug message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        void Debug(object message);

        /// <summary>Writes a warning to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        void Warning(object message);

        /// <summary>Writes an error message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        void Error(object message);

        /// <summary>Writes a success message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        void Success(object message);

        /// <summary>Writes an event message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        void Event(object message);

        /// <summary>Writes a message to the log.</summary>
        /// <param name="message">The message to write (HTML).</param>
        /// <param name="backgroundColor">The background color to apply to the item</param>
        void Write(object message, string backgroundColor);

        /// <summary>Writes a message to the log at the given severity level.</summary>
        /// <param name="message">The message to write (HTML).</param>
        /// <param name="severity">The severity of the message.</param>
        void WriteSeverity(object message, LogSeverity severity);

        /// <summary>Writes a message to the log (prepended with an icon).</summary>
        /// <param name="message">The message to write.</param>
        /// <param name="icon">An icon .</param>
        /// <param name="backgroundColor">The background color to apply to the log entry.</param>
        void WriteIcon(object message, Icons icon, string backgroundColor);

        /// <summary>Writes an <UL></UL> to the log.</summary>
        /// <param name="title">The title of the list.</param>
        /// <param name="backgroundColor">The background color to apply to the item</param>
        /// <returns>The UL list object to use to populate with items.</returns>
        IHtmlList WriteList(string title, string backgroundColor);

        /// <summary>Writes an <UL></UL> to the log.</summary>
        /// <param name="title">The title of the list.</param>
        /// <param name="severity">The severity of the message.</param>
        /// <returns>The UL list object to use to populate with items.</returns>
        IHtmlList WriteListSeverity(string title, LogSeverity severity);

        /// <summary>Writes out the property values for the given object.</summary>
        /// <param name="instance">The object to write.</param>
        /// <param name="title">The title to put above the object.</param>
        void WriteProperties(object instance, string title);

        /// <summary>Inserts a line break to the log.</summary>
        void LineBreak();

        /// <summary>Inserts a new section divider.</summary>
        void NewSection();

        /// <summary>Clears the log.</summary>
        void Clear();
    }
}
