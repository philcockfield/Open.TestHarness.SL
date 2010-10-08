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

        /// <summary>Fires when the 'Enter' key is pressed</summary>
        public event EventHandler EnterPress;
        private void FireEnterPress(){if (EnterPress != null) EnterPress(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropText = "Text";
        public const string PropEventDelay = "EventDelay";
        public const string PropLeftIcon = "LeftIcon";
        public const string PropSelectOnFocus = "SelectOnFocus";

        private const string ClassFocus = "focus";
        private const string ClassTextbox = "textbox";
        private const string ClassIcon = "icon";
        private NullableBool wasFocusedOnMouseDown = NullableBool.Nothing;

        private readonly jQueryObject liningElement;
        private readonly jQueryObject input;
        private jQueryObject icon;
        private Spacing leftIconMargin;
        private readonly DelayedAction eventDelay;
        private string previousText;
        private readonly Spacing padding;

        /// <summary>Constructor.</summary>
        public Textbox() : base()
        {
            // Setup initial conditions.
            SetCss(Css.Position, Css.Relative);

            // Create lining element (to override Padding crazyness).
            liningElement = Html.CreateDiv();
            liningElement.AppendTo(Container);
            Css.AbsoluteFill(liningElement);

            // Create INPUT.
            input = Html.CreateElement("input");
            input.Attribute(Html.Type, "text");
            input.AppendTo(liningElement);
            input.AddClass(ClassTextbox);
            input.CSS(Css.Position, Css.Absolute);
            input.CSS(Css.Width, "100%");
            input.CSS(Css.Height, "100%");

            // Set initial size.
            Height = 40;

            // Assign focus control to the input element.
            ChangeFocusElement(input);
            Focus.BrowserHighlighting = false;
            Focus.CanFocus = true;

            // Setup padding.
            padding = Spacing.Create(input, SpacingMode.Padding, OnBeforePaddingSync);
            padding.Uniform(10, 0);

            // Create child objects.
            eventDelay = new DelayedAction(0.3, OnDelayElapsed);


            // Wire up events.
            input.Keyup(delegate(jQueryEvent e)
                                    {
                                        if (previousText != Text) FireTextChanged();
                                        if (Int32.Parse(e.Which) == (int)Key.Enter) FireEnterPress();
                                    });
            input.MouseDown(delegate(jQueryEvent e) { OnMouseDown(); });
            input.MouseUp(delegate(jQueryEvent e) { OnMouseUp(); });
            Focus.GotFocus += OnGotFocus;
            Focus.LostFocus += OnLostFocus;
            IsEnabledChanged += delegate { SyncEnabled(); };

            // Finish up.
            previousText = Text;
            UpdateLayout();
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
        private void OnGotFocus(object sender, EventArgs e)
        {
            input.AddClass(ClassFocus);
            if (SelectOnFocus) Select();
        }
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

        private void OnMouseDown()
        {
            if (!SelectOnFocus) return;
            wasFocusedOnMouseDown = Focus.IsFocused ? NullableBool.Yes : NullableBool.No;
        }

        private void OnMouseUp()
        {
            if (!SelectOnFocus) return;
            if (wasFocusedOnMouseDown == NullableBool.Nothing) return;
            if (wasFocusedOnMouseDown == NullableBool.No)
            {
                // NB: Delayed action avoid default textbox behavior removing the selection on mouse-up.
                DelayedAction.Invoke(0.01, Select);
            }
            wasFocusedOnMouseDown = NullableBool.Nothing;
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

        /// <summary>Gets or sets whether the text is selected when the control recieves focus.</summary>
        public bool SelectOnFocus
        {
            get { return (bool) Get(PropSelectOnFocus, true); }
            set { Set(PropSelectOnFocus, value, true); }
        }
        #endregion

        #region Methods
        /// <summary>Updates the layout of the control.</summary>
        public void UpdateLayout()
        {
            PositionIcon();
            SyncEnabled();
        }

        /// <summary>Selects the text.</summary>
        public void Select() { input.Select(); }
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
            icon.Load(delegate(jQueryEvent e)
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

        private void SyncEnabled()
        {
            Html.SetDisabled(input, IsEnabled);
        }
        #endregion
    }
}
