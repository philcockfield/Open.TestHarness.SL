using System;
using jQueryApi;

namespace Open.Core.Controls
{
    public class LogView : ViewBase, ILogView
    {
        #region Head
        private readonly jQueryObject divList;
        private jQueryObject divRow;
        private int counter = 0;
        private double scrollDuration = 0;
        private readonly DelayedAction scrollDelay;
        private bool canInsertSection = true;

        /// <summary>Constructor.</summary>
        /// <param name="container">The container of the log</param>
        public LogView(jQueryObject container) : base(container)
        {
            // Setup initial conditions.
            scrollDelay = new DelayedAction(0.05, OnScrollDelayElapsed);
            divList = container.Children(LogCss.List).First();

            // Wire up events.
            GlobalEvents.WindowResize += delegate { UpdateLayout(); };
            GlobalEvents.HorizontalPanelResized += delegate { UpdateLayout(); };
            GlobalEvents.VerticalPanelResized += delegate { scrollDelay.Start(); };

            // Finish up.
            UpdateLayout();
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
            UpdateLayout();
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
        public void Insert(string message, string cssClass)
        {
            // Setup initial conditions.
            counter++;
            if (message == null) message = "<null>".HtmlEncode();
            if (message == string.Empty) message = "<empty-string>".HtmlEncode();
            if (string.IsNullOrEmpty(message.Trim())) message = "<whitespace>".HtmlEncode();

            // Create the row.
            divRow = CreateRowDiv(cssClass);

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
            InsertRow(divRow);

            // Finish up.
            canInsertSection = true;
        }

        public void Clear()
        {
            scrollDelay.Stop();
            counter = 0;
            divRow = null;
            divList.Html(string.Empty);
        }

        /// <summary>Updates the visual layout.</summary>
        public void UpdateLayout()
        {
            // Sync list width (prevents horizontal scroll bar appearing).
            divList.Width(Container.GetWidth());
        }
        #endregion

        #region Methods : Dividers
        /// <summary>Inserts a visual divider into the log.</summary>
        /// <param name="type">The type of divider to insert.</param>
        public void Divider(LogDivider type)
        {
            switch (type)
            {
                case LogDivider.LineBreak:
                    LineBreak();
                    break;

                case LogDivider.Section:
                    InsertSectionDivider();
                    break;

                default: throw new Exception("Not supporred: " + type.ToString());
            }
        }

        private void LineBreak()
        {
            if (divRow == null) return;
            divRow.AddClass(LogCss.LineBreakClass);
        }

        private void InsertSectionDivider()
        {
            // Setup initial conditions.
            if (!canInsertSection) return;

            // Insert the section break.
            InsertRow(CreateRowDiv(LogCss.SectionBreak));

            // Finish up.
            canInsertSection = false;
        }
        #endregion

        #region Internal
        // NB: Don't make static.  Causes error on logging from event-callbacks.
        private jQueryObject CreateRowDiv(string cssClass)
        {
            jQueryObject  div = Html.CreateDiv();
            div.AddClass(cssClass);
            div.AddClass(LogCss.ListItemClass);
            return div;
        }

        private void InsertRow(jQueryObject div)
        {
            div.AppendTo(divList);
            scrollDelay.Start();
        }
        #endregion
    }
}