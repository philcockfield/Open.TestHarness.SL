using System;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>Control that can render a view-factory.</summary>
    public interface IViewFactoryContent
    {
        /// <summary>Fires when the 'IsViewLoaded' property changes.</summary>
        event EventHandler IsViewLoadedChanged;

        /// <summary>Gets or sets the ViewFactory to bind to (typically a View-Model).</summary>
        IViewFactory ViewFactory { get; set; }

        /// <summary>Gets or sets whether the view has loaded.</summary>
        bool IsViewLoaded { get; }
    }
}
