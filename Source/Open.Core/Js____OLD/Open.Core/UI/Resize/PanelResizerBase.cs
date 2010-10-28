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
        protected virtual void FireResized()
        {
            if (Resized != null) Resized(this, new EventArgs());
            GlobalEvents.FirePanelResized(this);
        }

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
        private readonly jQueryObject panel;
        private readonly string cookieKey;
        protected bool IsInitialized;
        private static Cookie cookie;
        private readonly string cssSelector;

        /// <summary>Constructor.</summary>
        /// <param name="cssSelector">The CSS selector used to retrieve the panel being resized.</param>
        /// <param name="cookieKey">The unique key to store the panel size within (null if saving not required).</param>
        protected PanelResizerBase(string cssSelector, string cookieKey)
        {
            // Setup initial conditions.
            this.cssSelector = cssSelector;
            panel = jQuery.Select(cssSelector);
            this.cookieKey = cookieKey;
            if (cookie == null)
            {
                cookie = new Cookie("PanelResizeStore");
                cookie.Expires = 5000;
            }

            // Wire up events.
            GlobalEvents.WindowResize += delegate { OnWindowResize(); };

            // Finish up.
            LoadSize();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the unique identifier of the root container of the panel(s).</summary>
        /// <remarks>Used for calculating max-size of the resizing panel.</remarks>
        public string RootContainerId
        {
            get { return rootContainerId; }
            set { rootContainerId = Css.ToId(value); }
        }

        protected bool HasRootContainer { get { return !string.IsNullOrEmpty(RootContainerId); } }
        protected bool IsSaving { get { return !String.IsNullOrEmpty(cookieKey); } }

        protected jQueryObject Panel{get{return panel;}}
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
                            cssSelector,
                            GetHandles(),
                            Helper.Delegate.ToEventCallbackString(eventCallback, EventStart),
                            Helper.Delegate.ToEventCallbackString(eventCallback, EventStop),
                            Helper.Delegate.ToEventCallbackString(eventCallback, EventResize));
            Script.Eval(script);

            // Finish up.
            OnInitialize();
            IsInitialized = true;
        }

        /// <summary>Persists the current size to storage.</summary>
        public void Save()
        {
            if (!IsSaving) return;
            cookie.Set(cookieKey, GetCurrentSize());
            cookie.Save();
        }
        #endregion

        #region Methods : Protected
        protected abstract string GetHandles();
        protected virtual void OnInitialize() { }
        protected virtual void OnStarted() { }
        protected virtual void OnResize() { }
        protected virtual void OnStopped() { }
        protected virtual void OnWindowResize() { } 

        protected jQueryObject GetRootContainer() { return HasRootContainer ? jQuery.Select(RootContainerId) : null; }

        protected void SetResizeOption(string option, string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            Script.Literal("$('{0}').resizable('option', '{1}', {2})", cssSelector, option, value);
        }

        protected void ShrinkIfOverflowing(int currentValue, int minValue, int maxValue, string cssAttribute)
        {
            // Determine if the panel is overflowing it's available space.
            if (currentValue <= maxValue) return;
            if (maxValue < minValue) return; // Don't shrink smaller than the min.

            // Shrink the panel.
            Panel.CSS(cssAttribute, maxValue + Css.Px);
            FireResized();
        }

        /// <summary>Gets the current size (on the appropriate X:Y plane given the deriving class).</summary>
        protected abstract int GetCurrentSize();

        /// <summary>Sets the current size (on the appropriate X:Y plane given the deriving class).</summary>
        /// <param name="size">The size value to set.</param>
        protected abstract void SetCurrentSize(int size);
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
                Save();
                FireResizeStopped();
            }
        }

        private void LoadSize()
        {
            if (!IsSaving) return;
            object size = cookie.Get(cookieKey);
            if (Script.IsNullOrUndefined(size)) return;
            SetCurrentSize((int)size);
            FireResized();
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
