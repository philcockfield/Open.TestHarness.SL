using System.ComponentModel;

namespace Open.Core.UI
{
    /// <summary>An element which has an opacity value.</summary>
    public interface IOpacity : INotifyPropertyChanged
    {
        /// <summary>Gets or sets the opacity of the element (0..1).</summary>
        double Opacity { get; set; }
    }
}