using System;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Base class for buttons.</summary>
    public abstract class ButtonBase : ViewBase, IButton
    {
        #region Events
        public event EventHandler Click;
        private void FireClick(){if (Click != null) Click(this, new EventArgs());}

        public event EventHandler IsPressedChanged;
        private void FireIsPressedChanged(){if (IsPressedChanged != null) IsPressedChanged(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropCanToggle = "CanToggle";
        public const string PropMouseState = "MouseState";
        public const string PropIsPressed = "IsPressed";
        public const string PropIsMouseOver = "IsMouseOver";
        public const string PropIsMouseDown = "IsMouseDown";

        /// <summary>Constructor.</summary>
        /// <param name="html">The HTML of the button.</param>
        protected ButtonBase(jQueryObject html) : base(html)
        {
            // Wire up events.
            html.MouseOver(OnMouseOver);
            html.MouseOut(OnMouseOut);
            html.MouseDown(OnMouseDown);
            html.MouseUp(OnMouseUp);
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
            if (IsEnabled && IsMouseOver && wasMouseDown)
            {
                InvokeClick(true);
            }
        }
        #endregion

        #region Properties : IButton
        public bool CanToggle
        {
            get { return (bool) Get(PropCanToggle, false); }
            set { Set(PropCanToggle, value, false); }
        }

        public ButtonMouseState MouseState
        {
            get { return (ButtonMouseState)Get(PropMouseState, ButtonMouseState.Normal); }
            private set { Set(PropMouseState, value, ButtonMouseState.Normal); }
        }

        public bool IsPressed
        {
            get { return (bool) Get(PropIsPressed, false); }
            private set { if (Set(PropIsPressed, value, false)) FireIsPressedChanged(); }
        }

        public bool IsMouseOver
        {
            get { return (bool) Get(PropIsMouseOver, false); }
            private set { Set(PropIsMouseOver, value, false); }
        }

        public bool IsMouseDown
        {
            get { return (bool) Get(PropIsMouseDown, false); }
            private set { Set(PropIsMouseDown, value, false); }
        }
        #endregion

        #region Methods
        public void InvokeClick(bool force)
        {
            if (!IsEnabled && !force) return;
            if (CanToggle) IsPressed = !IsPressed;
            FireClick();
        }
        #endregion

        #region Internal
        private void UpdateMouseState()
        {
            if (!IsEnabled)
            {
                MouseState = ButtonMouseState.Normal;
            }
            else if (IsMouseOver && IsMouseDown)
            {
                MouseState = ButtonMouseState.Pressed;
            }
            else if (IsMouseOver)
            {
                MouseState = ButtonMouseState.MouseOver;
            }
            else
            {
                MouseState = ButtonMouseState.Normal;
            }
        }
        #endregion
    }
}