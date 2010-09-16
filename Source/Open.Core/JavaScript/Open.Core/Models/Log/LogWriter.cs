using System;
using System.Collections;

namespace Open.Core
{
    /// <summary>An output log.</summary>
    public class LogWriter : ModelBase, ILog
    {
        #region Head
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
        public void Title(string message) { Write(Html.ToBold(message), LogSeverity.Info); }

        /// <summary>Writes a informational message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public void Info(string message) { Write(message, LogSeverity.Info); }

        /// <summary>Writes a debug message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public void Debug(string message) { Write(message, LogSeverity.Debug); }

        /// <summary>Writes a warning to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public void Warning(string message) { Write(message, LogSeverity.Warning); }

        /// <summary>Writes an error message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public void Error(string message) { Write(message, LogSeverity.Error); }

        /// <summary>Writes a success message to the log.</summary>
        /// <param name="message">The messge to write (HTML).</param>
        public void Success(string message) { Write(message, LogSeverity.Success); }
        #endregion

        #region Methods : ILog
        public void Write(string message, LogSeverity severity)
        {
            if (!CanWrite) return;
            string css = LogCss.SeverityClass(severity);

            View.Insert(message, css);
            canInsertSection = true;
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
    }
}
