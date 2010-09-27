using System;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Manages the events for a button.</summary>
    internal class ButtonEventController : ControllerBase
    {
        #region Head
        private readonly ButtonView control;
        private bool ignoreIsPressedChanged;

        /// <summary>Constructor.</summary>
        /// <param name="control">The clickable button element.</param>
        /// <param name="divMask">The mask element used to monitor mouse events with.</param>
        public ButtonEventController(ButtonView control, jQueryObject divMask)
        {
            // Setup initial conditions.
            this.control = control;

            // Wire up events.
            Model.IsPressedChanged += OnModelIsPressedChanged;

            // -- Mouse events.
            divMask.MouseOver(OnMouseOver);
            divMask.MouseOut(OnMouseOut);
            divMask.MouseDown(OnMouseDown);
            divMask.MouseUp(OnMouseUp);
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
            if (IsEnabled && IsMouseOver && wasMouseDown) InvokeClick();
            UpdateMouseState();
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
            get { return (ButtonState)Get(ButtonView.PropState, ButtonState.Normal); }
            private set { Set(ButtonView.PropState, value, ButtonState.Normal); }
        }

        public bool IsMouseOver
        {
            get { return (bool)Get(ButtonView.PropIsMouseOver, false); }
            private set { Set(ButtonView.PropIsMouseOver, value, false); }
        }

        public bool IsMouseDown
        {
            get { return (bool)Get(ButtonView.PropIsMouseDown, false); }
            private set { Set(ButtonView.PropIsMouseDown, value, false); }
        }
        #endregion

        #region Properties : Internal
        private IButton Model { get { return control.Model; } }
        private bool IsEnabled { get { return Model.IsEnabled; } }
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
                State = Model.IsPressed ? ButtonState.Pressed : ButtonState.Normal;
            }
            else if ((IsMouseOver && IsMouseDown))
            {
                State = ButtonState.MouseDown;
            }
            else if (Model.IsPressed)
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
