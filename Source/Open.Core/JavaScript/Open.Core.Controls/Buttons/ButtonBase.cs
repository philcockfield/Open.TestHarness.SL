using System;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Base class for buttons.</summary>
    public abstract class ButtonBase : ViewBase, IButton
    {
        #region Events
        public event EventHandler Click;
        private void FireClick() { if (Click != null) Click(this, new EventArgs()); }

        public event EventHandler IsPressedChanged;
        private void FireIsPressedChanged() { if (IsPressedChanged != null) IsPressedChanged(this, new EventArgs()); }
        #endregion

        #region Head
        public const string PropCanToggle = "CanToggle";
        public const string PropState = "State";
        public const string PropIsPressed = "IsPressed";
        public const string PropIsMouseOver = "IsMouseOver";
        public const string PropIsMouseDown = "IsMouseDown";

        private readonly ButtonEventManager eventManager;

        /// <summary>Constructor.</summary>
        [AlternateSignature]
        protected extern ButtonBase();

        /// <summary>Constructor.</summary>
        /// <param name="element">The HTML of the button.</param>
        protected ButtonBase(jQueryObject element) : base(element)
        {
            // Setup initial conditions.
            eventManager = new ButtonEventManager(this, delegate { InvokeClick(true); });

            // Wire up events.
            eventManager.PropertyChanged += OnEventManagerPropertyChanged;
        }

        /// <summary>Finalize.</summary>
        protected override void OnDisposed()
        {
            eventManager.Dispose();
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnEventManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            FirePropertyChanged(e.Property.Name);
            if (e.Property.Name == PropIsPressed) FireIsPressedChanged();
        }
        #endregion

        #region Properties : IButton
        public bool CanToggle
        {
            get { return eventManager.CanToggle; }
            set { eventManager.CanToggle = value; }
        }

        public ButtonState State { get { return eventManager.State; } }
        public bool IsPressed { get { return eventManager.IsPressed; } }
        public bool IsMouseOver { get { return eventManager.IsMouseOver; } }
        public bool IsMouseDown { get { return eventManager.IsMouseDown; } }
        #endregion

        #region Methods : IButton
        public void InvokeClick(bool force)
        {
            if (!IsEnabled && !force) return;
            if (CanToggle) eventManager.IsPressed = !eventManager.IsPressed;
            FireClick();
        }
        #endregion

        #region Methods
        /// <summary>Sets the content to use for the given state.</summary>
        /// <param name="state">The button state the content applies to.</param>
        /// <param name="html">The HTML element to use.</param>
        [AlternateSignature]
        protected virtual extern void StateContent(ButtonState state, jQueryObject html);

        /// <summary>Sets the content to use for the given state.</summary>
        /// <param name="state">The button state the content applies to.</param>
        /// <param name="cssClasses">The CSS class(es) to apply (space delimited).</param>
        [AlternateSignature]
        protected virtual extern void StateContent(ButtonState state, string cssClasses);

        /// <summary>Sets the content to use for the given state.</summary>
        /// <param name="state">The button state the content applies to.</param>
        /// <param name="html">The HTML element to use.</param>
        /// <param name="cssClasses">The CSS class(es) to apply (space delimited).</param>
        protected virtual void StateContent(ButtonState state, jQueryObject html, string cssClasses)
        {
            // TODO - StateContent
        }
        #endregion
    }
}