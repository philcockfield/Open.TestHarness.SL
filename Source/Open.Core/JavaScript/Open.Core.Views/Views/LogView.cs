using System;
using jQueryApi;

namespace Open.Core.Controls
{
    public class LogView : ViewBase, ILogView
    {
        #region Head
        private jQueryObject divList;
        private jQueryObject divRow;
        private int counter = 0;
        private double scrollDuration = 0;
        private readonly DelayedAction scrollDelay;

        /// <summary>Constructor.</summary>
        /// <param name="container">The container of the log</param>
        public LogView(jQueryObject container)
        {
            // Setup initial conditions.
            Initialize(container);
            scrollDelay = new DelayedAction(0.05, OnScrollDelayElapsed);

            // Wire up events.
            jQuery.Window.Bind(DomEvents.Resize, delegate(jQueryEvent e) { UpdateVisualState(); });
            GlobalEvents.HorizontalPanelResized += delegate { UpdateVisualState(); };
            GlobalEvents.VerticalPanelResized += delegate { scrollDelay.Start(); };

            // Finish up.
            UpdateVisualState();
        }

        protected override void OnDisposed()
        {
            Clear();
            scrollDelay.Dispose();
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnScrollDelayElapsed()
        {
            if (divRow == null) return;
            Helper.Scroll.ToBottom(divList, ScrollDuration, EffectEasing.Swing, null);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the scroll duration (in seconds) used when scrolling to the bottom.</summary>
        public double ScrollDuration
        {
            get { return scrollDuration; }
            set { scrollDuration = value; }
        }
        #endregion

        #region Methods : ILogView
        protected override void OnInitialize(jQueryObject container)
        {
            divList = container.Children(LogCss.List).First();
            base.OnInitialize(container);
        }

        public void Insert(string message, string cssClass)
        {
            // Setup initial conditions.
            counter++;
            if (message == null) message = "<null>".HtmlEncode();
            if (message == string.Empty) message = "<Empty String>".HtmlEncode();

            // Create the row.
            divRow = Html.CreateDiv();
            divRow.AddClass(cssClass);
            divRow.AddClass(LogCss.ListItemClass);
            divRow.Attribute(Html.Id, Helper.CreateId());

            // Prepare the 'counter'.
            jQueryObject spanCounter = Html.CreateSpan();
            spanCounter.AddClass(LogCss.CounterClass);
            spanCounter.Append(counter.ToString());

            // Prepare the 'message'.
            jQueryObject divMessage = Html.CreateDiv();
            divMessage.AddClass(LogCss.MessageClass);
            divMessage.Append(message);

            // Perform inserts.
            spanCounter.AppendTo(divRow);
            divMessage.AppendTo(divRow);
            divRow.AppendTo(divList);

            // Finish up.
            scrollDelay.Start();
        }

        public void LineBreak()
        {
            if (divRow == null) return;
            divRow.AddClass(LogCss.LineBreakClass);
        }

        public void Clear()
        {
            scrollDelay.Stop();
            counter = 0;
            divRow = null;
            divList.Html(string.Empty);
        }
        #endregion

        #region Internal
        private void UpdateVisualState()
        {
            // Setup initial conditions.
            if (!IsInitialized) return;

            // Sync list width (prevents horizontal scroll bar appearing).
            divList.Width(Container.GetWidth());
        }
        #endregion
    }
}