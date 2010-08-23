using System;

namespace Open.Core
{
    /// <summary>Notifies clients that a property value has changed.</summary>
    public interface INotifyPropertyChanged
    {
        /// <summary>Fires when a property values changes.</summary>
        event PropertyChangedHandler PropertyChanged;
    }
}
