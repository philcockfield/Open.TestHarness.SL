namespace Open.TestHarness
{
    /// <summary>Constants for common CSS selectors.</summary>
    public static class CssSelectors
    {
        // Root.
        public static readonly string Root = "#testHarness";

        // Sidebar.
        public static readonly string Sidebar = "#testHarnessSidebar";
        public static readonly string SidebarRootList = "#testHarnessSidebar .th-sidebarRootList";
        public static readonly string SidebarToolbar = "#testHarnessSidebar div.th-toolbar";
        public static readonly string BackMask = "#testHarnessSidebar img.th-backMask";

        // Test List.
        public static readonly string TestList = "#testHarnessSidebar .th-testList";
        public static readonly string TestListContent = "#testHarnessSidebar .th-testList-content";
        

        // Main.
        public static readonly string Main = "#testHarness .th-main";

        // Log.
        public static readonly string LogTitlebar = "#testHarnessLog .th-log-tb";
        public static readonly string Log = "#testHarnessLog .c-log";
    }
}
