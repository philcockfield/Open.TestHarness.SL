using System;
using System.Collections;

namespace Open.Core
{
    /// <summary>An output log.</summary>
    public class LogWriter : ModelBase, ILog
    {
        #region Head
        private readonly ArrayList views = new ArrayList();

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

        #region Methods : ILog
        public void Write(string message, LogSeverity severity)
        {
            string css = LogCss.SeverityClass(severity);
            foreach (ILogView view in views)
            {
                view.Insert(message, css);
            }
        }

        public void LineBreak()
        {
            foreach (ILogView view in views) { view.LineBreak(); }
        }

        public void Clear()
        {
            foreach (ILogView view in views) { view.Clear(); }
        }

        public void RegisterView(ILogView view)
        {
            if (view != null) views.Add(view);
        }
        #endregion
    }
}
