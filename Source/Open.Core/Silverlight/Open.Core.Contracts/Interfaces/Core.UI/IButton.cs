using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Open.Core.Common;

namespace Open.Core.UI
{
    /// <summary>Abstractly defines a simple button.</summary>
    public interface IButton : IViewFactory, INotifyPropertyChanged, IClickable, IVisiblity
    {
        /// <summary>Gets the command the button is bound to.</summary>
        ICommand Command { get; }

        /// <summary>Gets or sets the enabled state of the button.</summary>
        bool IsEnabled { get; set; }

        /// <summary>Gets or sets the text label of the button.</summary>
        string Label { get; set; }

        /// <summary>Gets or sets the margin to put around the button.</summary>
        Thickness Margin { get; set; }

        /// <summary>Gets or sets the XAML template used to render the button.</summary>
        DataTemplate Template { get; set; }
    }
}
