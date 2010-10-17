using System;
using jQueryApi;
using Open.Core.Controls.HtmlPrimitive;
using Open.Core.Helpers;

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

        /// <summary>Constructor.</summary>
        /// <param name="container">The container of the log</param>
        public LogView(jQueryObject container) : base(container)
        {
            // Setup initial conditions.
            scrollDelay = new DelayedAction(0.05, OnScrollDelayElapsed);
            divList = container.Children(LogCss.List).First();
            ImagePreloader.Preload(ControlsImages.LogSectionDivider);

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
        public void InsertMessage(object message, string cssClass, string backgroundColor, string iconPath)
        {
            InsertElement(CreatePara(message, iconPath), cssClass, backgroundColor);
        }

        public void InsertElement(jQueryObject element, string cssClass, string backgroundColor)
        {
            // Setup initial conditions.
            counter++;

            // Create the row.
            divRow = CreateRowDiv(cssClass);
            if (!string.IsNullOrEmpty(backgroundColor)) divRow.CSS(Css.Background, backgroundColor);

            // Prepare the 'counter'.
            jQueryObject spanCounter = Html.CreateSpan();
            spanCounter.AddClass(LogCss.CounterClass);
            spanCounter.Append(counter.ToString());

            // Prepare the 'message'.
            jQueryObject divMessage = Html.CreateDiv();
            divMessage.AddClass(LogCss.MessageClass);
            divMessage.Append(element);

            // Perform inserts.
            spanCounter.AppendTo(divRow);
            divMessage.AppendTo(divRow);
            InsertRow(divRow);
        }

        public IHtmlList InsertList(string title, string cssClass, string backgroundColor, string iconPath)
        {
            // Setup initial conditions.
            HtmlList htmlList = new HtmlList();
            jQueryObject div = Html.CreateDiv();

            // Create the title.
            jQueryObject pTitle = null;
            if (!Script.IsUndefined(title) && !string.IsNullOrEmpty(title))
            {
                pTitle = CreatePara(title, iconPath);
                pTitle.AddClass(LogCssClasses.ListTitle);
            }

            // Insert the list (and optionally the title).
            if (pTitle != null) div.Append(pTitle);
            div.Append(htmlList.Container);

            // Finish up.
            InsertElement(div, cssClass, backgroundColor);
            return htmlList;
        }

        public void Clear()
        {
            scrollDelay.Stop();
            counter = 0;
            divRow = null;
            divList.Html(string.Empty);
        }

        protected override void OnUpdateLayout()
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
            InsertRow(CreateRowDiv(LogCss.SectionBreak));
        }
        #endregion

        #region Internal
        // NB: Don't make static.  Causes error on logging from event-callbacks.
        private static jQueryObject CreateRowDiv(string cssClass)
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

        private static jQueryObject CreatePara(object message, string iconPath)
        {
            // Create the P element.
            jQueryObject p = Html.CreateElement("p");
            p.Append(Helper.String.FormatToString(message));

            // Prepend icon (if specified).
            if (iconPath != null)
            {
                p.AddClass(LogCssClasses.Icon);
                p.CSS(Css.BackgroundImage, string.Format("url({0})", iconPath));
            }

            // Finish up.
            return p;
        }
        #endregion
    }
}