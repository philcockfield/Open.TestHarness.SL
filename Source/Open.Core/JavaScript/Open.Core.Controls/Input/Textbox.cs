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
        public const string PropCornerRadius = "CornerRadius";

        private const string ClassFocus = "focus";
        private const string ClassTextbox = "textbox";
        private const string ClassIcon = "icon";
        private NullableBool wasFocusedOnMouseDown = NullableBool.Nothing;

        private readonly jQueryObject input;
        private jQueryObject icon;
        private Spacing leftIconMargin;
        private readonly DelayedAction eventDelay;
        private string previousText;
        private readonly Spacing padding;

        /// <summary>Constructor.</summary>
        public Textbox() 
        {
            // Setup initial conditions.
            Container.AddClass(ClassTextbox);

            // Create INPUT.
            input = Html.CreateElement("input");
            input.Attribute(Html.Type, "text");
            input.AppendTo(Container);
            input.AddClass(ClassTextbox);

            // Set initial size.
            Height = 40;

            // Assign focus control to the input element.
            ChangeFocusElement(input);
            Focus.BrowserHighlighting = false;
            Focus.CanFocus = true;

            // Setup padding.
            padding = new Spacing().Sync(input, OnBeforePaddingSync);
            padding.Change(10, 5);

            // Create child objects.
            eventDelay = new DelayedAction(0.3, OnDelayElapsed);

            // Wire up events.
            Container.MouseDown(delegate { FocusOnClick(); });
            input.Keyup(delegate(jQueryEvent e)
                                    {
                                        if (previousText != Text) FireTextChanged();
                                        if (Int32.Parse(e.Which) == (int)Key.Enter) FireEnterPress();
                                    });
            input.MouseDown(delegate { OnInputMouseDown(); });
            input.MouseUp(delegate { OnInputMouseUp(); });
            Focus.GotFocus += OnGotFocus;
            Focus.LostFocus += OnLostFocus;
            IsEnabledChanged += delegate { SyncEnabled(); };
            SizeChanged += delegate { UpdateLayout(); };

            // Sync size and shape.
            SyncCornerRadius();

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
        private void OnGotFocus(object sender, EventArgs e)
        {
            Container.AddClass(ClassFocus);
            if (SelectOnFocus) Select();
        }
        private void OnLostFocus(object sender, EventArgs e) { Container.RemoveClass(ClassFocus); }

        private int OnBeforePaddingSync(Edge edge, int value)
        {
            switch (edge)
            {
                case Edge.Left:
                    // Adjust left padding if an icon is visible.
                    if (!HasLeftIcon) return value;
                    if (leftIconMargin == null) return value;
                    if (edge != Edge.Left) return value;

                    // Add the icon offset.
                    return value + (LeftIconMargin.HorizontalOffset + icon.GetWidth());

                case Edge.Top:
                    // Center the textbox vertically (required because FF doesn't strectch the height).
                    return GetInputTop();
                
                default: return value;
            }
        }

        private void OnInputMouseDown()
        {
            if (!SelectOnFocus) return;
            wasFocusedOnMouseDown = Focus.IsFocused ? NullableBool.Yes : NullableBool.No;
        }

        private void OnInputMouseUp()
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
                UpdateInputTop();
                FireTextChanged();
            }
        }

        /// <summary>Gets whether the textbox is empty (True if null, empty or only whitespace).</summary>
        public bool HasText
        {
            get { return Helper.String.HasValue(Text); }
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
                    Css.SetDisplay(icon, !string.IsNullOrEmpty(value));
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

        /// <summary>Gets or sets the corner radius of the textbox border.</summary>
        public int CornerRadius
        {
            get { return (int) Get(PropCornerRadius, 5); }
            set
            {
                if (value < 0) value = 0;
                if (Set(PropCornerRadius, value, 5)) SyncCornerRadius();
            }
        }
        #endregion

        #region Methods
        protected override void OnUpdateLayout()
        {
            PositionIcon();
            SyncEnabled();
            Padding.UpdateLayout();
        }

        /// <summary>Selects the text.</summary>
        public void Select() { input.Select(); }
        #endregion

        #region Methods : Override
        protected override void OnBeforeInsert(jQueryObject targetElement, InsertMode mode)
        {
            // Setup initial conditions.
            if (mode != InsertMode.Replace) return;

            // Retrieve the 'value' if there is one.
            string value = targetElement.GetAttribute(Html.Value);
            if (!Script.IsUndefined(value))
            {
                Text = value;
            }

            // Finish up.
            base.OnBeforeInsert(targetElement, mode);
        }
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
            Css.SetDisplay(icon, false);
            icon.AppendTo(Container);

            // Wire up events.
            icon.Load(delegate(jQueryEvent e)
                          {
                              PositionIcon();
                              Padding.UpdateLayout();
                          });
            icon.MouseDown(delegate(jQueryEvent e) { FocusOnClick(); });
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

        private void SyncCornerRadius()
        {
            Css.RoundedCorners(Container, CornerRadius);
        }

        private void FocusOnClick()
        {
            if (!IsEnabled) return;
            DelayedAction.Invoke(0.01, delegate { Focus.Apply(); });
        }

        private void UpdateInputHeight()
        {
            if (Padding != null) input.Height(Container.GetHeight() - Padding.VerticalOffset);
        }

        private int GetInputTop()
        {
            UpdateInputHeight();
            return (Height / 2) - (input.GetHeight() / 2);
        }

        private void UpdateInputTop()
        {
            input.CSS(Css.Top, GetInputTop() + Css.Px);
        }
        #endregion
    }
}
