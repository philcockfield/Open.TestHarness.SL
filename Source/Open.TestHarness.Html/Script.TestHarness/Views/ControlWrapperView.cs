using System;
using System.Collections;
using jQueryApi;
using Open.Core;

namespace Open.Testing.Views
{
    /// <summary>Represents the container for a test-control.</summary>
    public class ControlWrapperView : TestHarnessViewBase
    {
        #region Head
        private const int FillMargin = 30;

        private readonly jQueryObject divRoot;
        private readonly jQueryObject htmlElement;
        private readonly IView control;
        private ControlDisplayMode displayMode;
        private readonly IEnumerable allViews;
        private readonly int index;
        private readonly TestHarnessEvents events;
        private readonly DelayedAction sizeDelay;

        /// <summary>Constructor.</summary>
        /// <param name="divHost">The control host DIV.</param>
        /// <param name="control">The logical IView control (null if not available).</param>
        /// <param name="htmlElement">The control content (supplied by the test class. This is the control that is under test).</param>
        /// <param name="displayMode">The sizing strategy to use for the control.</param>
        /// <param name="allViews">The Collection of all controls.</param>
        public ControlWrapperView(
            jQueryObject divHost, 
            IView control, 
            jQueryObject htmlElement, 
            ControlDisplayMode displayMode, 
            IEnumerable allViews) : base(divHost)
        {
            // Setup initial conditions.
            this.control = control;
            this.htmlElement = htmlElement;
            this.displayMode = displayMode;
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
        public ControlDisplayMode DisplayMode
        {
            get { return displayMode; }
            set { displayMode = value; }
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
            switch (displayMode)
            {
                case ControlDisplayMode.None:
                case ControlDisplayMode.Center:
                    // Ignore - size of the control is assumed.
                    break;

                case ControlDisplayMode.Fill:
                    SetSizeWithPadding(0, 0);
                    break;

                case ControlDisplayMode.FillWithMargin:
                    SetSizeWithPadding(FillMargin, FillMargin);
                    break;

                default: throw new Exception(displayMode.ToString());
            }

            // Finish up.
            Css.SetOverflow(Container, TestHarness.CanScroll ? CssOverflow.Auto : CssOverflow.Hidden); // Reset the scroll behavior.
        }

        private void SetSizeWithPadding(int xPadding, int yPadding)
        {
            int width = (Container.GetWidth() - (xPadding * 2));
            int height = (Container.GetHeight() - (yPadding * 2));
            Css.SetSize(htmlElement, width, height);
        }

        private void UpdatePosition()
        {
            // Setup initial conditions.
            if (DisplayMode == ControlDisplayMode.None) return;

            // Left.
            divRoot.CSS(Css.Left, GetLeft() + Css.Px);

            // Top.
            int top = Container.Children().Length == 1 ? GetTop() : GetStackedTop();
            if (displayMode != ControlDisplayMode.Fill && top < FillMargin) top = FillMargin;
            divRoot.CSS(Css.Top, top + Css.Px);
        }

        private int GetLeft()
        {
            switch (displayMode)
            {
                case ControlDisplayMode.None: return -1; // ignore.
                case ControlDisplayMode.Center: return (Container.GetWidth()/2) - (htmlElement.GetWidth()/2);
                case ControlDisplayMode.Fill: return 0;
                case ControlDisplayMode.FillWithMargin: return FillMargin;
                default: throw new Exception(displayMode.ToString());
            }
        }

        private int GetTop()
        {
            switch (displayMode)
            {
                case ControlDisplayMode.None: return -1; // ignore.
                case ControlDisplayMode.Center: return (Container.GetHeight() / 2) - (htmlElement.GetHeight() / 2);
                case ControlDisplayMode.Fill: return 0;
                case ControlDisplayMode.FillWithMargin: return FillMargin;
                default: throw new Exception(displayMode.ToString());
            }
        }

        private int GetStackedTop()
        {
            return GetOffsetHeight() + ((index + 1) * FillMargin);
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
