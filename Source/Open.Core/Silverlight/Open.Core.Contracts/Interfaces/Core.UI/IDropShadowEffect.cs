using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Open.Core.UI
{
    /// <summary>Defines a drop-shadow effect.</summary>
    public interface IDropShadowEffect : INotifyPropertyChanged
    {
        /// <summary>Gets or sets how defined the edges of the shadow are (how blurry the shadow is).</summary>
        double BlurRadius { get; set; }

        /// <summary>Gets or sets the color of the shadow.</summary>
        Color Color { get; set; }

        /// <summary>Gets or sets the angle at which the shadow is cast.</summary>
        double Direction { get; set; }

        /// <summary>Gets or sets the degree of opacity of the shadow.</summary>
        double Opacity { get; set; }

        /// <summary>Gets or sets the distance between the object and the shadow that it casts.</summary>
        double ShadowDepth { get; set; }

        /// <summary>Copies the models values to the given effect element.</summary>
        void CopyTo(DropShadowEffect effect);
    }
}