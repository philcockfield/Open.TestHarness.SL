namespace Open.Core
{
    /// <summary>CSS constants for the log.</summary>
    public static class LogCss
    {
        // Stylesheets.
        public const string Url = "/Open.Core/Css/Core.Controls.css";

        // Classes.
        private const string RootClass = "c-log";
        public const string ListItemClass = "c-log-listItem";
        public const string LineBreakClass = "c-log-lineBreak";
        public const string CounterClass = "c-log-counter";
        public const string MessageClass = "c-log-message";

        // Selectors.
        public static readonly string List = "div." + RootClass + "-list";
        public static readonly string ListItem = "." + ListItemClass;

        /// <summary>Retrieves a CSS class for the given severity level.</summary>
        /// <param name="severity">The severity of the log message.</param>
        public static string SeverityClass(LogSeverity severity)
        {
            return string.Format("{0}-{1}", RootClass, severity.ToString());
        }
    }
}
