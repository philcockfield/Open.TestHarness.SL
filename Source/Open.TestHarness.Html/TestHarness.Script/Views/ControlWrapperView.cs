using System;
using System.Collections;
using System.Html;
using jQueryApi;
using Open.Core;

namespace Open.Testing.Views
{
    /// <summary>Represents the container for a test-control.</summary>
    public class ControlWrapperView : TestHarnessViewBase
    {
        #region Head
        private const int fillMargin = 30;

        private readonly jQueryObject divRoot;
        private readonly jQueryObject htmlElement;
        private IView control;
        private SizeMode sizeMode;
        private readonly IEnumerable allViews;
        private readonly int index;
        private readonly TestHarnessEvents events;
        private readonly DelayedAction sizeDelay;

        /// <summary>Constructor.</summary>
        /// <param name="divHost">The control host DIV.</param>
        /// <param name="control">The logical IView control (null if not available).</param>
        /// <param name="htmlElement">The control content (supplied by the test class. This is the control that is under test).</param>
        /// <param name="sizeMode">The sizing strategy to use for the control.</param>
        /// <param name="allViews">The Collection of all controls.</param>
        public ControlWrapperView(jQueryObject divHost, IView control, jQueryObject htmlElement, SizeMode sizeMode, IEnumerable allViews)
        {
            // Setup initial conditions.
            Initialize(divHost);
            this.control = control;
            this.htmlElement = htmlElement;
            this.sizeMode = sizeMode;
            this.allViews = allViews;
            index = divHost.Children().Length; // Store the order position of the control in the host.
            events = Common.Events;
            sizeDelay = new DelayedAction(0.2, UpdateLayout);

            // Create the wrapper DIV.
            divRoot = Html.CreateDiv();
            divRoot.CSS(Css.Position, Css.Absolute);
            divRoot.AppendTo(divHost);

            // Insert the content.
            htmlElement.CSS(Css.Position, Css.Absolute);
            htmlElement.AppendTo(divRoot);

            // Wire up events.
            events.ControlHostSizeChanged += OnHostResized;

            // Finish up.
            UpdateLayout();
        }


        /// <summary>Destructor.</summary>
        protected override void OnDisposed()
        {
            // Unwire events.
            events.ControlHostSizeChanged -= OnHostResized;

            // Remove from the DOM.
            divRoot.Remove();

            // Finish up.
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnHostResized(object sender, EventArgs e)
        {
            Css.SetOverflow(Container, CssOverflow.Hidden); // Hide scroll behavior during resize operation.
            sizeDelay.Start();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the items size mode.</summary>
        public SizeMode SizeMode
        {
            get { return sizeMode; }
            set { sizeMode = value; }
        }

        /// <summary>Gets the HTML content.</summary>
        public jQueryObject HtmlElement { get { return htmlElement; } }

        /// <summary>Gets the logical control if available (otherwise null).</summary>
        public IView Control { get { return control; } }
        #endregion

        #region Methods
        /// <summary>Updates the layout of the control.</summary>
        public void UpdateLayout()
        {
            UpdateSize();
            UpdatePosition();
        }
        #endregion

        #region Internal
        private void UpdateSize()
        {
            switch (sizeMode)
            {
                case SizeMode.ControlsSize:
                    // Ignore - size of the control is assumed.
                    break;

                case SizeMode.Fill:
                    SetSizeWithPadding(0, 0);
                    break;

                case SizeMode.FillWithMargin:
                    SetSizeWithPadding(fillMargin, fillMargin);
                    break;

                default: throw new Exception(sizeMode.ToString());
            }

            // Finish up.
            Css.SetOverflow(Container, CssOverflow.Auto); // Reset the scroll behavior.
        }

        private void SetSizeWithPadding(int xPadding, int yPadding)
        {
            int width = (Container.GetWidth() - (xPadding * 2));
            int height = (Container.GetHeight() - (yPadding * 2));
            Css.SetSize(htmlElement, width, height);
        }

        private void UpdatePosition()
        {
            // Left.
            divRoot.CSS(Css.Left, GetLeft() + Css.Px);

            // Top.
            int top = Container.Children().Length == 1 ? GetTop() : GetStackedTop();
            divRoot.CSS(Css.Top, top + Css.Px);
        }

        private int GetLeft()
        {
            switch (sizeMode)
            {
                case SizeMode.ControlsSize: return (Container.GetWidth()/2) - (htmlElement.GetWidth()/2);
                case SizeMode.Fill: return 0;
                case SizeMode.FillWithMargin: return fillMargin;
                default: throw new Exception(sizeMode.ToString());
            }
        }

        private int GetTop()
        {
            switch (sizeMode)
            {
                case SizeMode.ControlsSize: return (Container.GetHeight()/2) - (htmlElement.GetHeight()/2);
                case SizeMode.Fill: return 0;
                case SizeMode.FillWithMargin: return fillMargin;
                default: throw new Exception(sizeMode.ToString());
            }
        }

        private int GetStackedTop()
        {
            return GetOffsetHeight() + ((index + 1) * fillMargin);
        }

        private int GetOffsetHeight()
        {
            int height = 0;
            foreach (ControlWrapperView wrapper in allViews)
            {
                if (wrapper == this) break;
                height += wrapper.HtmlElement.GetHeight();
            }
            return height;
        }
        #endregion
    }
}
