using System;
using jQueryApi;
using Open.Core;
using Open.Core.UI;

namespace Open.Testing.Controllers
{
    /// <summary>Handles resizing of panels within the shell.</summary>
    public class PanelResizeController : TestHarnessControllerBase, IPanelResizeController
    {
        #region Head
        private const int SidebarMinWidth = 200;
        private const int SidebarMaxWidthMargin = 80;
        public const int LogMinHeight = 32;
        public const int LogMaxHeightMargin = 80;

        private readonly HorizontalPanelResizer sideBarResizer;
        private readonly VerticalPanelResizer logResizer;
        private readonly TestHarnessEvents events;

        public PanelResizeController()
        {
            // Setup initial conditions.
            events = Common.Events;

            // Setup the 'SidePanel' resizer.
            sideBarResizer = new HorizontalPanelResizer(CssSelectors.Sidebar, "TH_SB");
            sideBarResizer.Resized += delegate
                                          {
                                              SyncMainPanelWidth();
                                          };
            sideBarResizer.MinWidth = SidebarMinWidth;
            sideBarResizer.MaxWidthMargin = SidebarMaxWidthMargin;
            InitializeResizer(sideBarResizer);

            // Setup the 'Output Log' resizer.
            logResizer = new VerticalPanelResizer(Css.ToId(Elements.OutputLog), "TH_OL");
            logResizer.Resized += delegate
                                         {
                                             SyncControlHostHeight();
                                         };
            logResizer.MinHeight = Html.Height(CssSelectors.LogTitlebar);
            logResizer.MaxHeightMargin = LogMaxHeightMargin;
            InitializeResizer(logResizer);

            // Wire up events.
            GlobalEvents.WindowResize += delegate { SyncControlHostHeight(); };

            // Finish up.
            UpdateLayout();
        }
        #endregion

        #region Properties
        /// <summary>Gets the Log resizer.</summary>
        public VerticalPanelResizer LogResizer { get { return logResizer; } }

        /// <summary>Gets the SideBar resizer.</summary>
        public HorizontalPanelResizer SideBarResizer{get { return sideBarResizer; }}
        #endregion

        #region Methods
        /// <summary>Updates the layout of the panels.</summary>
        public void UpdateLayout()
        {
            SyncMainPanelWidth();
            SyncControlHostHeight();
        }

        /// <summary>Saves panel sizes to storage.</summary>
        /// <remarks>Only required if some programmatic change has been made to the panels.</remarks>
        public void Save()
        {
            sideBarResizer.Save();
            logResizer.Save();
        }
        #endregion

        #region Internal
        private static void InitializeResizer(PanelResizerBase resizer)
        {
            resizer.RootContainerId = Elements.Root;
            resizer.Initialize();
        }

        private void SyncMainPanelWidth()
        {
            jQuery.Select(CssSelectors.Main)
                .CSS(
                    Css.Left,
                    (Html.Width(CssSelectors.Sidebar) + 1) + Css.Px);
            events.FireControlHostSizeChanged();
        }

        private void SyncControlHostHeight()
        {
            int height = Html.Height(CssSelectors.MainContent) - Html.Height(CssSelectors.LogContainer);
            jQuery.Select(CssSelectors.ControlHost)
                .CSS(
                    Css.Height,
                    (height - 1) + Css.Px);
            events.FireControlHostSizeChanged();
        }
        #endregion
    }
}