namespace Open.TestHarness
{
    /// <summary>Constants for common CSS selectors.</summary>
    public static class CssSelectors
    {
        // Sidebar.
        public static readonly string Sidebar = "#testHarnessSidebar";
        public static readonly string SidebarList = "#testHarnessSidebar .th-sidebarList";
        public static readonly string SidebarToolbar = "#testHarnessSidebar div.th-toolbar";
        public static readonly string HomeButton = "#testHarnessSidebar img.th-home";

        // Main.
        public static readonly string Main = "#testHarness .th-main";

        // Log.
        public static readonly string LogTitlebar = "#testHarnessLog .th-log-titlebar";
        public static readonly string Log = "#testHarnessLog .coreLog";
    }
}
