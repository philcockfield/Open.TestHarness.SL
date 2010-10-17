namespace Open.Core
{
    /// <summary>CSS constants for the log.</summary>
    public static class LogCss
    {
        // Stylesheets.
        public const string Url = "/Open.Core/Css/Core.Controls.css";

        // Classes.
        private const string RootClass = "c_log";
        public const string ListItemClass = "c_log_listItem";
        public const string SectionBreak = "c_log_sectionBreak";

        public const string LineBreakClass = "c_log_lineBreak";
        public const string CounterClass = "c_log_counter";
        public const string MessageClass = "c_log_message";

        // Selectors.
        public static readonly string List = "div." + RootClass + "_list";
        public static readonly string ListItem = "." + ListItemClass;

        /// <summary>Retrieves a CSS class for the given severity level.</summary>
        /// <param name="severity">The severity of the log message.</param>
        public static string SeverityClass(LogSeverity severity)
        {
            return string.Format("{0}_{1}", RootClass, severity.ToString());
        }
    }
}
