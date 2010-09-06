using System;
using jQueryApi;

namespace Open.Core
{
    /// <summary>Handles and fires global events.</summary>
    public static class GlobalEvents
    {
        #region Events
        /// <summary>Fires when the browser Window is resizing.</summary>
        public static event EventHandler WindowResize;
        private static void FireWindowResize() { if (WindowResize != null) WindowResize(typeof(GlobalEvents), new EventArgs()); }

        /// <summary>Fires when the browser Window is completed resizing (uses an event-delay).</summary>
        public static event EventHandler WindowResizeComplete;
        private static void FireWindowResizeComplete() { if (WindowResizeComplete != null) WindowResizeComplete(typeof(GlobalEvents), new EventArgs()); }

        /// <summary>Fires when any PanelResizer is resizing.</summary>
        public static event EventHandler PanelResized;
        internal static void FirePanelResized(object sender) { if (PanelResized != null) PanelResized(sender, new EventArgs()); }

        /// <summary>Fires when the PanelResizer has compled resizing (uses an event-delay).</summary>
        public static event EventHandler PanelResizeComplete;
        private static void FirePanelResizeComplete() { if (PanelResizeComplete != null) PanelResizeComplete(typeof(GlobalEvents), new EventArgs()); }

        /// <summary>Fires when any HorizontalPanelResizer is resizing.</summary>
        public static event EventHandler HorizontalPanelResized;
        internal static void FireHorizontalPanelResized(object sender) { if (HorizontalPanelResized != null) HorizontalPanelResized(sender, new EventArgs()); }

        /// <summary>Fires when any VerticalPanelResizer is resizing.</summary>
        public static event EventHandler VerticalPanelResized;
        internal static void FireVerticalPanelResized(object sender) { if (VerticalPanelResized != null) VerticalPanelResized(sender, new EventArgs()); }
        #endregion

        #region Head
        private const double ResizeDelay = 0.1;
        private static readonly DelayedAction windowResizeDelay;
        private static readonly DelayedAction panelResizeDelay;

        /// <summary>Constructor.</summary>
        static GlobalEvents()
        {
            // Setup initial conditions.
            windowResizeDelay = new DelayedAction(ResizeDelay, FireWindowResizeComplete);
            panelResizeDelay = new DelayedAction(ResizeDelay, FirePanelResizeComplete);

            // Bind to window resize.
            jQuery.OnDocumentReady(delegate
                                       {
                                           jQuery.Window.Bind(DomEvents.Resize, delegate(jQueryEvent e) { FireWindowResize(); });
                                       });

            // Wire up events.
            WindowResize += delegate { windowResizeDelay.Start(); };
            PanelResized += delegate { panelResizeDelay.Start(); };
        }
        #endregion
    }
}
