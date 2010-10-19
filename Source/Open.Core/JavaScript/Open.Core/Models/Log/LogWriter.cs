using Open.Core.Controls.HtmlPrimitive;
using Open.Core.Helpers;

namespace Open.Core
{
    public class LogCssClasses
    {
        public const string PropertyList = "c_log_propList";
        public const string PropertyName = "c_log_propName";
        public const string PropertyValue = "c_log_propValue";
        public const string PropertyError = "c_log_propError";
        public const string PropertyEvent = "c_log_event";
        public const string Icon = "c_log_icon";
        public const string ListTitle = "c_log_listTitle";
        public const string TotalChars = "c_log_totalChars";
    }

    /// <summary>An output log.</summary>
    public class LogWriter : ModelBase, ILog
    {
        #region Head
        public static readonly string ClassIcon = ImagePaths.ApiIconPath + "Class.png";
        public static readonly string EventIcon = ImagePaths.ApiIconPath + "Event.png";

        private bool isActive = true;
        private bool canInsertSection = true;
        private ILogView view;

        /// <summary>Constructor.</summary>
        public LogWriter()
        {
            // Ensure CSS is available.
            Css.InsertLink(LogCss.Url);
        }

        /// <summary>Destructor.</summary>
        protected override void OnDisposed()
        {
            Helper.Dispose(View);
            base.OnDisposed();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the view-control to write to.</summary>
        public ILogView View
        {
            get { return view; }
            set { view = value; }
        }

        /// <summary>Gets or sets whether the log is active.  When False nothing is written to the log.</summary>
        /// <remarks>Use this for pausing output to the log.</remarks>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        private bool CanWrite { get { return IsActive && View != null; } }
        #endregion

        #region Methods : ILog (Write Severity Message)
        /// <summary>Writes a informational message to the log (as a bold title).</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public void Title(string message) { WriteSeverity(Html.ToBold(message), LogSeverity.Info); }

        /// <summary>Writes a informational message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public void Info(object message) { WriteSeverity(message, LogSeverity.Info); }

        /// <summary>Writes a debug message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public void Debug(object message) { WriteSeverity(message, LogSeverity.Debug); }

        /// <summary>Writes a warning to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public void Warning(object message) { WriteSeverity(message, LogSeverity.Warning); }

        /// <summary>Writes an error message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public void Error(object message) { WriteSeverity(message, LogSeverity.Error); }

        /// <summary>Writes a success message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public void Success(object message) { WriteSeverity(message, LogSeverity.Success); }
        #endregion

        #region Methods : ILog
        public void Write(object message, string backgroundColor)
        {
            WriteInternal(message, null, backgroundColor, null);
        }

        public void WriteSeverity(object message, LogSeverity severity)
        {
            WriteInternal(message, LogCss.SeverityClass(severity), null, ToIconPath(severity));
            BrowserConsole.WriteSeverity(message, severity);
        }

        public IHtmlList WriteList(string title, string backgroundColor)
        {
            return View.InsertList(title, null, backgroundColor, null);
        }

        public IHtmlList WriteListSeverity(string title, LogSeverity severity)
        {
            return View.InsertList(title, LogCss.SeverityClass(severity), null, ToIconPath(severity));
        }

        public void WriteIcon(object message, Icons icon, string backgroundColor)
        {
            View.InsertMessage(message, null, backgroundColor, Helper.Icon.Path(icon));
        }

        public void Event(object message)
        {
            View.InsertMessage(message, LogCssClasses.PropertyEvent, null, EventIcon);
        }

        public void WriteProperties(object instance, string title)
        {
            new PropertyWriter(this).Write(instance, title);
        }

        public void Clear()
        {
            if (!CanWrite) return;
            View.Clear();
        }
        #endregion

        #region Methods : Dividers
        /// <summary>Inserts a line break to the log.</summary>
        public void LineBreak()
        {
            if (!CanWrite) return;
            View.Divider(LogDivider.LineBreak);        }

        /// <summary>Inserts a new section divider.</summary>
        public void NewSection()
        {
            if (!CanWrite) return;
            if (!canInsertSection) return;

            View.Divider(LogDivider.Section);
            canInsertSection = false;
        }
        #endregion

        #region Internal
        private void WriteInternal(object message, string cssClass, string backgroundColor, string iconPath)
        {
            if (!CanWrite) return;
            View.InsertMessage(message, cssClass, backgroundColor, iconPath);
            canInsertSection = true;
        }

        private static string ToIconPath(LogSeverity severity)
        {
            IconHelper icon = Helper.Icon;
            switch (severity)
            {
                case LogSeverity.Success: return icon.Path(Icons.SilkAccept);
                case LogSeverity.Warning: return icon.Path(Icons.SilkError);
                case LogSeverity.Error: return icon.Path(Icons.SilkExclamation);
            }
            return null;
        }
        #endregion
    }
}
