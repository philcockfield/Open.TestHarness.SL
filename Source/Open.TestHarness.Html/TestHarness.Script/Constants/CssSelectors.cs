namespace Open.TestHarness
{
    /// <summary>Constants for common CSS selectors.</summary>
    public static class CssSelectors
    {
        public const string ClassLogListItem = "th-log-listItem";

        public static readonly string Main = "#testHarness .th-main";
        public static readonly string Sidebar = "#testHarness .th-sidebar";

        public static readonly string LogTitlebar = "#testHarnessLog .th-log-titlebar";
        public static readonly string LogList = "#testHarnessLog div.th-log-list";
        public static readonly string LogListItem = "#testHarnessLog div." + ClassLogListItem;
    }
}
