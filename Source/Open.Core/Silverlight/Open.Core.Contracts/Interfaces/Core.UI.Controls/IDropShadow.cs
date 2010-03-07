using System.Windows.Media;

namespace Open.Core.UI.Controls
{
    /// <summary>Defines a simple horizontal drop-shadow.</summary>
    public interface IDropShadow
    {
        /// <summary>Gets or sets the opacity (0-1).</summary>
        double Opacity { get; set; }

        /// <summary>Gets or sets the color of the drop-shadow (Black by default).</summary>
        Color Color { get; set; }

        /// <summary>Gets or sets the direction of the shadow.</summary>
        Direction Direction { get; set; }

        /// <summary>Gets or sets the pixel width or height of the shadow (depending on the 'Direction').</summary>
        double Size { get; set; }
    }
}
