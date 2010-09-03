using System;

namespace Open.Core
{
    /// <summary>Handles and fires global events.</summary>
    public static class GlobalEvents
    {
        #region Events
        /// <summary>Fires when any PanelResizer is resizing.</summary>
        public static event EventHandler PanelResized;
        internal static void FirePanelResized(object sender) { if (PanelResized != null) PanelResized(sender, new EventArgs()); }

        /// <summary>Fires when any HorizontalPanelResizer is resizing.</summary>
        public static event EventHandler HorizontalPanelResized;
        internal static void FireHorizontalPanelResized(object sender) { if (HorizontalPanelResized != null) HorizontalPanelResized(sender, new EventArgs()); }

        /// <summary>Fires when any VerticalPanelResizer is resizing.</summary>
        public static event EventHandler VerticalPanelResized;
        internal static void FireVerticalPanelResized(object sender) { if (VerticalPanelResized != null) VerticalPanelResized(sender, new EventArgs()); }
        #endregion
    }
}
