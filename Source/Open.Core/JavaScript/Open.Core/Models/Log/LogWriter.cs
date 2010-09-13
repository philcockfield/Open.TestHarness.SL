using System;
using System.Collections;

namespace Open.Core
{
    /// <summary>An output log.</summary>
    public class LogWriter : ModelBase, ILog
    {
        #region Head
        private readonly ArrayList views = new ArrayList();
        private bool isActive = true;
        private bool canInsertSection = true;

        /// <summary>Constructor.</summary>
        public LogWriter()
        {
            // Ensure CSS is available.
            Css.InsertLink(LogCss.Url);
        }

        /// <summary>Destructor.</summary>
        protected override void OnDisposed()
        {
            foreach (ILogView view in views)
            {
                IDisposable disposable = view as IDisposable;
                if (disposable != null) disposable.Dispose();
            }
            base.OnDisposed();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the log is active.  When False nothing is written to the log.</summary>
        /// <remarks>Use this for pausing output to the log.</remarks>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
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
            if (!IsActive) return;
            string css = LogCss.SeverityClass(severity);
            foreach (ILogView view in views)
            {
                view.Insert(message, css);
            }
            canInsertSection = true;
        }

        public void Clear()
        {
            if (!IsActive) return;
            foreach (ILogView view in views) { view.Clear(); }
        }

        public void RegisterView(ILogView view)
        {
            if (view != null) views.Add(view);
        }
        #endregion

        #region Methods : Dividers
        /// <summary>Inserts a line break to the log.</summary>
        public void LineBreak()
        {
            if (!IsActive) return;
            foreach (ILogView view in views) { view.Divider(LogDivider.LineBreak); }
        }

        /// <summary>Inserts a new section divider.</summary>
        public void NewSection()
        {
            if (!IsActive) return;
            if (!canInsertSection) return;

            foreach (ILogView view in views) { view.Divider(LogDivider.Section); }
            canInsertSection = false;
        }
        #endregion
    }
}
