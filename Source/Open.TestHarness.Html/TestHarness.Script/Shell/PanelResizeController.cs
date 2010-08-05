using jQueryApi;
using Open.Core.UI;

namespace Open.TestHarness.Shell
{
    /// <summary>Handles resizing of panels within the shell.</summary>
    public class PanelResizeController
    {
        #region Head
        private const int SidebarMinWidth = 200;
        private const int SidebarMaxWidthMargin = 80;
        private const int OutputLogMinHeight = 30;
        private const int OutputLogMaxWidthMargin = 80;

        private readonly HorizontalPanelResizer sideBarResizer;
        private readonly VerticalPanelResizer outputResizer;

        public PanelResizeController()
        {
            // Setup the 'SidePanel' resizer.
            sideBarResizer = new HorizontalPanelResizer(Elements.SideBar);
            sideBarResizer.Resized += delegate
                                          {
                                              SyncMainPanelWidth();
                                          };
            sideBarResizer.MinWidth = SidebarMinWidth;
            sideBarResizer.MaxWidthMargin = SidebarMaxWidthMargin;
            InitializeResizer(sideBarResizer);

            // Setup the 'Output Log' resizer.
            outputResizer = new VerticalPanelResizer(Elements.OutputLog);
            outputResizer.Resized += delegate
                                         {
                                             
                                         };
            outputResizer.MinHeight = OutputLogMinHeight;
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
            jQuery.Select(Elements.Main).CSS("left", jQuery.Select(Elements.SideBar).GetWidth() + 1 + "px");
        }
        #endregion

        //TEMP 
        private const string SidebarResizeScript1111 =
@"
$('{0}').resizable({
    handles: 'e',
    minWidth: {1},
    maxWidth: $('{2}').width() - {3},
    resize: function (event, ui) {
        $('{4}').css('left', $('{0}').width() + 1);
    }
});
";

    }
}
