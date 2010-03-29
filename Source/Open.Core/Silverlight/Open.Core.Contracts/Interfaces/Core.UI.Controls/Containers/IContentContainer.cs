using System;
using System.ComponentModel;
using System.Windows;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>A flexible content container that can safely work with bound UIElements or DataTemplates.</summary>
    public interface IContentContainer : IViewFactory, INotifyPropertyChanged, INotifyDisposed
    {
        /// <summary>Fires when the 'Content' value changes.</summary>
        event EventHandler ContentChanged;

        /// <summary>Fires when the 'ContentTemplate' value changes.</summary>
        event EventHandler ContentTemplateChanged;

        /// <summary>Fires when the 'Model' value changes.</summary>
        event EventHandler ModelChanged;

        /// <summary>Gets or sets the data-template used to render the content (this is overridden by 'Content' if set).</summary>
        DataTemplate ContentTemplate { get; set; }

        /// <summary>Gets or sets the model to bind the ContentTemplate to.</summary>
        object Model { get; set; }

        /// <summary>Gets or sets a specific UI element to render (overrides 'ContentTemplate').</summary>
        object Content { get; set; }
    }
}