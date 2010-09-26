using System;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Manages the events for a button.</summary>
    internal class ButtonEventManager : ModelBase
    {
        #region Head
        public const string PropState = "State";
        public const string PropIsMouseOver = "IsMouseOver";
        public const string PropIsMouseDown = "IsMouseDown";

        private readonly ButtonView control;
        private bool ignoreIsPressedChanged;

        /// <summary>Constructor.</summary>
        /// <param name="control">The clickable button element.</param>
        public ButtonEventManager(ButtonView control)
        {
            // Setup initial conditions.
            this.control = control;

            // Wire up events.
            Model.IsPressedChanged += OnModelIsPressedChanged;

            // -- Mouse events.
            jQueryObject element = control.Container;
            element.MouseOver(OnMouseOver);
            element.MouseOut(OnMouseOut);
            element.MouseDown(OnMouseDown);
            element.MouseUp(OnMouseUp);
        }

        protected override void OnDisposed()
        {
            Model.IsPressedChanged -= OnModelIsPressedChanged;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnMouseOver(jQueryEvent e)
        {
            IsMouseOver = true;
            UpdateMouseState();
        }

        private void OnMouseOut(jQueryEvent e)
        {
            IsMouseOver = false;
            UpdateMouseState();
        }

        private void OnMouseDown(jQueryEvent e)
        {
            IsMouseDown = true;
            UpdateMouseState();
        }

        private void OnMouseUp(jQueryEvent e)
        {
            bool wasMouseDown = IsMouseDown;
            IsMouseDown = false;
            UpdateMouseState();
            if (IsEnabled && IsMouseOver && wasMouseDown) InvokeClick();
        }

        private void OnModelIsPressedChanged(object sender, EventArgs e)
        {
            if (ignoreIsPressedChanged) return;
            UpdateMouseState();
        }
        #endregion

        #region Properties : IButtonView
        public ButtonState State
        {
            get { return (ButtonState)Get(PropState, ButtonState.Normal); }
            private set { Set(PropState, value, ButtonState.Normal); }
        }

        public bool IsMouseOver
        {
            get { return (bool)Get(PropIsMouseOver, false); }
            private set { Set(PropIsMouseOver, value, false); }
        }

        public bool IsMouseDown
        {
            get { return (bool)Get(PropIsMouseDown, false); }
            private set { Set(PropIsMouseDown, value, false); }
        }
        #endregion

        #region Properties : Internal
        private IButton Model { get { return control.Model; } }
        private bool IsEnabled { get { return control.IsEnabled && Model.IsEnabled; } }
        #endregion

        #region Internal
        private void InvokeClick()
        {
            ignoreIsPressedChanged = true;
            Model.InvokeClick(true);
            ignoreIsPressedChanged = false;
        }

        private void UpdateMouseState()
        {
            if (!IsEnabled)
            {
                State = ButtonState.Normal;
            }
            else if ((IsMouseOver && IsMouseDown) || Model.IsPressed)
            {
                State = ButtonState.Pressed;
            }
            else if (IsMouseOver)
            {
                State = ButtonState.MouseOver;
            }
            else
            {
                State = ButtonState.Normal;
            }
        }
        #endregion
    }
}
