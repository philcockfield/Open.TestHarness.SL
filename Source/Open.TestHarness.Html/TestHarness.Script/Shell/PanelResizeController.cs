using System;
using jQueryApi;
using Open.Core;
using Open.Core.UI;

namespace Open.TestHarness.Shell
{
    /// <summary>Handles resizing of panels within the shell.</summary>
    public class PanelResizeController
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
            sideBarResizer = new HorizontalPanelResizer(Elements.SideBar, "TH_SB");
            sideBarResizer.Resized += delegate
                                          {
                                              SyncMainPanelWidth();
                                          };
            sideBarResizer.MinWidth = SidebarMinWidth;
            sideBarResizer.MaxWidthMargin = SidebarMaxWidthMargin;
            InitializeResizer(sideBarResizer);
            SyncMainPanelWidth();

            // Setup the 'Output Log' resizer.
            outputResizer = new VerticalPanelResizer(Elements.OutputLog, "TH_OL");
            outputResizer.Resized += delegate
                                         {
                                             
                                         };
            outputResizer.MinHeight = Css.SelectFromId(Elements.OutputLogTitlebar).GetHeight();
            outputResizer.MaxHeightMargin = OutputLogMaxHeightMargin;
            InitializeResizer(outputResizer);
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
            Css.SelectFromId(Elements.Main)
                .CSS(
                    Css.Left,
                    Css.SelectFromId(Elements.SideBar).GetWidth() + 1 + Css.Px);
        }
        #endregion
    }
}
