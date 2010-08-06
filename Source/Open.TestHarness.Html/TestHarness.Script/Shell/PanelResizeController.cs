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
        private const int OutputLogMaxWidthMargin = 80;

        private readonly HorizontalPanelResizer sideBarResizer;
        private readonly VerticalPanelResizer outputResizer;

        public PanelResizeController()
        {
            // Setup the 'SidePanel' resizer.
            sideBarResizer = new HorizontalPanelResizer(Elements.SideBar, "Panel_Sidebar");
            sideBarResizer.Resized += delegate
                                          {
                                              SyncMainPanelWidth();
                                          };
            sideBarResizer.MinWidth = SidebarMinWidth;
            sideBarResizer.MaxWidthMargin = SidebarMaxWidthMargin;
            InitializeResizer(sideBarResizer);
            SyncMainPanelWidth();

            // Setup the 'Output Log' resizer.
            outputResizer = new VerticalPanelResizer(Elements.OutputLog, "Panel_Output");
            outputResizer.Resized += delegate
                                         {
                                             
                                         };
            outputResizer.MinHeight = jQuery.Select(Elements.OutputLogToolbar).GetHeight();
            outputResizer.MaxHeightMargin = OutputLogMaxWidthMargin;
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
            jQuery.Select(Elements.Main)
                .CSS(
                    Css.Left, 
                    jQuery.Select(Elements.SideBar).GetWidth() + 1 + Css.Px);
        }
        #endregion
    }
}
