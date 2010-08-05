using System;
using System.Collections;
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
        private readonly string panelId;

        protected PanelResizerBase(string panelId)
        {
            this.panelId = panelId;


            //panel.Bind("resize", delegate(jQueryEvent e)
            //                         {
            //                             FireResized();
            //                         });
        }

        #endregion

        #region Methods

        private int temp;

        private OnResizerEvent onResizerEvent;

        public void Initialize()
        {
            // Setup event callbacks.
            onResizerEvent = delegate(string eventName)
                                                {
                                                    temp++;

                                                    jQuery.Select("#main").Html("Event: " + eventName + " | " + temp);

//                                                    if (eventName == "stop") Initialize();

                                                    
                                                };


            // Prepare the script.
            string script = string.Format(
                ResizeScript,
                        panelId,
                        GetCallbackFunction(onResizerEvent, "start"),
                        GetCallbackFunction(onResizerEvent, "stop"),
                        GetCallbackFunction(onResizerEvent, "resize"));
            Script.Eval(script);
        }

        private static string GetCallbackFunction(Delegate callback, string eventName)
        {
            string func = String.Format("{0}('{1}');", 
                                        DelegateUtil.ToCallbackString(callback), 
                                        eventName);
            return "function(e,ui){ " + func + " }";
        }

        protected virtual void OnStarted() { }
        protected virtual void OnStopped() { }
        #endregion



        #region Internal
        private void SetMinWidth()
        {
            SetResizeOption("minWidth", 120); //TEMP 
        }

        private void SetResizeOption(string option, object value)
        {
            string script = string.Format("$('{0}').resizable('option', '{1}', {2});", panelId, option, value);
            Script.Eval(script);
        }
        #endregion

        private const string ResizeScript =
@"
$('{0}').resizable({
    handles: 'e',
    start: {1},
    stop: {2},
    resize: {3}
    });
";
    }

    internal delegate void OnResizerEvent(string eventName);
}
