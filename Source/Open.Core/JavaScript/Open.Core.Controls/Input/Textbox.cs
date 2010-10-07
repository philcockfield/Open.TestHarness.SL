using System;
using jQueryApi;

namespace Open.Core.Controls
{
    /// <summary>Represents a textbox.</summary>
    public class Textbox : ViewBase
    {
        #region Events
        /// <summary>Fires when the Text value changes.</summary>
        public event EventHandler TextChanged;
        private void FireTextChanged()
        {
            if (TextChanged != null) TextChanged(this, new EventArgs());
            FirePropertyChanged(PropText);
            eventDelay.Start();
            previousText = Text;
        }

        /// <summary>Fires when when the Text value changes (after a delay).</summary>
        /// <remarks>Use this event to capture a set of keystroke events.</remarks>
        public event EventHandler TextChangedDelay;
        private void FireTextChangedDelay(){if (TextChangedDelay != null) TextChangedDelay(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropText = "Text";
        public const string PropEventDelay = "EventDelay";
        public const string PropLeftIcon = "LeftIcon";

        private const string ClassFocus = "focus";
        private const string ClassTextbox = "textbox";
        private const string ClassIcon = "icon";

        private readonly jQueryObject input;
        private jQueryObject icon;
        private Spacing leftIconMargin;
        private readonly DelayedAction eventDelay;
        private string previousText;
        private readonly Spacing padding;

        /// <summary>Constructor.</summary>
        public Textbox() : base(Html.CreateSpan())
        {
            // Setup initial conditions.
            SetCss(Css.Position, Css.Relative);

            // Create INPUT.
            input = Html.CreateElement("input");
            input.Attribute(Html.Type, "text");
            input.AppendTo(Container);
            input.AddClass(ClassTextbox);
            Css.AbsoluteFill(input);

            // Set initial size.
            Height = 40;

            // Assign focus control to the input element.
            ChangeFocusElement(input);
            Focus.BrowserHighlighting = false;
            Focus.CanFocus = true;

            // Wire up events.
            input.Keyup(delegate(jQueryEvent e)
                                    {
                                        if (previousText != Text) FireTextChanged();
                                    });
            Focus.GotFocus += OnGotFocus;
            Focus.LostFocus += OnLostFocus;

            // Setup padding.
            padding = Spacing.Create(input, SpacingMode.Padding, OnBeforePaddingSync);
            padding.Uniform(10, 5);

            // Create child objects.
            eventDelay = new DelayedAction(0.3, OnDelayElapsed);

            // Finish up.
            previousText = Text;
            FireSizeChanged();
        }

        /// <summary>Destructor.</summary>
        protected override void OnDisposed()
        {
            eventDelay.Dispose();
            Container.Remove();
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnDelayElapsed() { FireTextChangedDelay(); }
        private void OnGotFocus(object sender, EventArgs e) { input.AddClass(ClassFocus); }
        private void OnLostFocus(object sender, EventArgs e) { input.RemoveClass(ClassFocus); }
        private int OnBeforePaddingSync(Edge edge, int value)
        {
            // Adjust left padding if an icon is visible.
            if (!HasLeftIcon) return value;
            if (leftIconMargin == null) return value;
            if (edge != Edge.Left) return value;

            // Add the icon offset.
            return value + (LeftIconMargin.HorizontalOffset + icon.GetWidth());
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the text value in the textbox.</summary>
        public string Text
        {
            get { return input.GetAttribute(Html.Value); }
            set
            {
                if (value == Text) return;
                input.Attribute(Html.Value, value);
                FireTextChanged();
            }
        }

        /// <summary>Gets or sets the delay (in seconds) after the Text changes and the TextChangedDelay event is fired.</summary>
        public double EventDelay
        {
            get { return eventDelay.Delay; }
            set
            {
                if (value == EventDelay) return;
                eventDelay.Delay = value;
                FirePropertyChanged(PropEventDelay);
            }
        }

        /// <summary>Gets the pixel padding within the Textbox.</summary>
        public Spacing Padding { get { return padding; } }

        /// <summary>Gets whether there is currently a left icon set.</summary>
        public bool HasLeftIcon { get { return LeftIcon != null; } }

        /// <summary>Gets or sets the source of the left-hand icon.</summary>
        public string LeftIcon
        {
            get { return (string) Get(PropLeftIcon, null); }
            set
            {
                if (string.IsNullOrEmpty(value)) value = null;
                if (Set(PropLeftIcon, value, null))
                {
                    InsertIcon();
                    icon.Attribute(Html.Src, value);
                    Css.SetVisibility(icon, !string.IsNullOrEmpty(value));
                    PositionIcon();
                }
            }
        }

        /// <summary>Gets the margin for the left-hand icon.</summary>
        public Spacing LeftIconMargin { get { return leftIconMargin ?? (leftIconMargin = new Spacing(8, 0, 0, 0)); } }
        #endregion

        #region Internal
        private void InsertIcon()
        {
            // Setup initial conditions.
            if (icon != null) return; // Already inserted.

            // Create the icon.
            icon = Html.CreateElement(Html.Img);
            icon.AddClass(ClassTextbox);
            icon.AddClass(ClassIcon);
            Css.SetVisibility(icon, false);
            icon.AppendTo(Container);

            // Wire up events.
            icon.Load(delegate(jQueryEvent @event)
                          {
                              PositionIcon();
                              Padding.SyncElement();
                          });
        }

        private void PositionIcon()
        {
            // Setup initial conditions.
            if (icon == null || string.IsNullOrEmpty(LeftIcon)) return;

            // Vertical and Left.
            Css.CenterVertically(icon, Container, LeftIconMargin);
            icon.CSS(Css.Left, LeftIconMargin.Left + Css.Px);
        }
        #endregion
    }
}
