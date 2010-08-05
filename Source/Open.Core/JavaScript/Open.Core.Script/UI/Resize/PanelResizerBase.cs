using System;
using jQueryApi;

namespace Open.Core.UI
{
    /// <summary>Base class for resizing panels.</summary>
    public abstract class PanelResizerBase
    {
        #region Events
        /// <summary>Fires during the resize operation.</summary>
        public event EventHandler Resized;
        private void FireResized(){if (Resized != null) Resized(this, new EventArgs());}

        /// <summary>Fires when the resize operation starts.</summary>
        public event EventHandler ResizeStarted;
        private void FireResizeStarted(){if (ResizeStarted != null) ResizeStarted(this, new EventArgs());}

        /// <summary>Fires when the resize operation stops.</summary>
        public event EventHandler ResizeStopped;
        private void FireResizeStopped(){if (ResizeStopped != null) ResizeStopped(this, new EventArgs());}
        #endregion

        #region Head
        private const string EventStart = "start";
        private const string EventStop = "eventStop";
        private const string EventResize = "eventResize";
        private string rootContainerId;
        protected readonly string PanelId;
        protected bool IsInitialized;

        /// <summary>Constructor.</summary>
        /// <param name="panelId">The unique identifier of the panel being resized.</param>
        protected PanelResizerBase(string panelId)
        {
            PanelId = panelId;
            jQuery.Window.Bind(Events.Resize, delegate(jQueryEvent e) { OnWindowSizeChanged(); });
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the unique identifier of the root container of the panel(s).</summary>
        /// <remarks>Used for calculating max-size of the resizing panel.</remarks>
        public string RootContainerId
        {
            get { return rootContainerId; }
            set { rootContainerId = value; }
        }

        protected bool HasRootContainer { get { return !string.IsNullOrEmpty(RootContainerId); } }
        #endregion

        #region Methods
        /// <summary>Sets the panel up to be resizable.</summary>
        public void Initialize()
        {
            // Setup event callbacks.
            EventCallback eventCallback = delegate(string eventName) { HandleEvent(eventName); };

            // Prepare the script.
            string script = string.Format(
                ResizeScript,
                            PanelId,
                            GetHandles(),
                            DelegateUtil.ToEventCallbackString(eventCallback, EventStart),
                            DelegateUtil.ToEventCallbackString(eventCallback, EventStop),
                            DelegateUtil.ToEventCallbackString(eventCallback, EventResize));
            Script.Eval(script);

            // Finish up.
            OnInitialize();
            IsInitialized = true;
        }
        #endregion

        #region Methods : Protected
        protected abstract string GetHandles();
        protected virtual void OnInitialize() { }
        protected virtual void OnStarted() { }
        protected virtual void OnResize() { }
        protected virtual void OnStopped() { }
        protected virtual void OnWindowSizeChanged() { } 


        protected jQueryObject GetPanel() { return jQuery.Select(PanelId); }
        protected jQueryObject GetRootContainer() { return HasRootContainer ? jQuery.Select(RootContainerId) : null; }

        protected void SetResizeOption(string option, object value)
        {
            string script = string.Format("$('{0}').resizable('option', '{1}', {2});", PanelId, option, value);
            Script.Eval(script);
        }
        #endregion

        #region Internal
        private void HandleEvent(string eventName)
        {
            if (eventName == EventStart)
            {
                OnStarted();
                FireResizeStarted();
            }
            else if (eventName == EventResize)
            {
                OnResize();
                FireResized();
            }
            else if (eventName == EventStop)
            {
                OnStopped();
                FireResizeStopped();
            }
        }
        #endregion

        private const string ResizeScript = @"
$('{0}').resizable({
    handles: '{1}',
    start: {2},
    stop: {3},
    resize: {4}
    });
";
    }

}
