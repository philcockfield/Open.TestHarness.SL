using Open.Core.UI;

namespace Open.Testing.Controllers
{
    /// <summary>Handles resizing of panels within the shell.</summary>
    public interface IPanelResizeController
    {
        /// <summary>Gets the Log resizer.</summary>
        VerticalPanelResizer LogResizer { get; }

        /// <summary>Gets the SideBar resizer.</summary>
        HorizontalPanelResizer SideBarResizer { get; }

        /// <summary>Updates the layout of the panels.</summary>
        void UpdateLayout();

        /// <summary>Saves panel sizes to storage.</summary>
        /// <remarks>Only required if some programmatic change has been made to the panels.</remarks>
        void Save();
    }
}