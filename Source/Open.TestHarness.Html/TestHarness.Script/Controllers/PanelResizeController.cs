using System;
using jQueryApi;
using Open.Core;
using Open.Core.UI;

namespace Open.Testing.Controllers
{
    /// <summary>Handles resizing of panels within the shell.</summary>
    public class PanelResizeController : ControllerBase
    {
        #region Head
        private const int SidebarMinWidth = 200;
        private const int SidebarMaxWidthMargin = 80;
        private const int OutputLogMaxHeightMargin = 80;

        private readonly HorizontalPanelResizer sideBarResizer;
        private readonly VerticalPanelResizer outputResizer;

        public PanelResizeController()
        {
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
            outputResizer = new VerticalPanelResizer(Css.ToId(Elements.OutputLog), "TH_OL");
            outputResizer.Resized += delegate
                                         {
                                             SyncControlHostHeight();
                                         };
            outputResizer.MinHeight = Html.Height(CssSelectors.LogTitlebar);
            outputResizer.MaxHeightMargin = OutputLogMaxHeightMargin;
            InitializeResizer(outputResizer);

            // Wire up events.
            GlobalEvents.WindowResize += delegate { SyncControlHostHeight(); };

            // Finish up.
            UpdateLayout();
        }
        #endregion

        #region Methods
        /// <summary>Updates the layout of the panels.</summary>
        public void UpdateLayout()
        {
            SyncMainPanelWidth();
            SyncControlHostHeight();
        }
        #endregion

        #region Internal
        private static void InitializeResizer(PanelResizerBase resizer)
        {
            resizer.RootContainerId = Elements.Root;
            resizer.Initialize();
        }

        private static void SyncMainPanelWidth()
        {
            jQuery.Select(CssSelectors.Main)
                .CSS(
                    Css.Left,
                    (Html.Width(CssSelectors.Sidebar) + 1) + Css.Px);
        }

        private static void SyncControlHostHeight()
        {
            int height = Html.Height(CssSelectors.MainContent) - Html.Height(CssSelectors.LogContainer);
            jQuery.Select(CssSelectors.ControlHost)
                .CSS(
                    Css.Height,
                    (height - 1) + Css.Px); 
        }
        #endregion
    }
}