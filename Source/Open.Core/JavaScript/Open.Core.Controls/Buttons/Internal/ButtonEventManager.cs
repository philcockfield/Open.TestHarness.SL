using System;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Manages the events for a button.</summary>
    internal class ButtonEventManager : ModelBase
    {
        #region Head
        private readonly IView control;
        private readonly Action invokeClick;

        /// <summary>Constructor.</summary>
        /// <param name="control">The clickable button element.</param>
        /// <param name="invokeClick">Action that invokes the click.</param>
        public ButtonEventManager(IView control, Action invokeClick)
        {
            // Setup initial conditions.
            this.control = control;
            this.invokeClick = invokeClick;

            // Wire up events.
            jQueryObject element = control.Container;
            element.MouseOver(OnMouseOver);
            element.MouseOut(OnMouseOut);
            element.MouseDown(OnMouseDown);
            element.MouseUp(OnMouseUp);
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
            if (control.IsEnabled && IsMouseOver && wasMouseDown)
            {
                Helper.InvokeOrDefault(invokeClick);
            }
        }
        #endregion

        #region Properties : IButton
        public bool CanToggle
        {
            get { return (bool)Get(ButtonBase.PropCanToggle, false); }
            set { Set(ButtonBase.PropCanToggle, value, false); }
        }

        public ButtonState State
        {
            get { return (ButtonState)Get(ButtonBase.PropState, ButtonState.Normal); }
            private set { Set(ButtonBase.PropState, value, ButtonState.Normal); }
        }

        public bool IsPressed
        {
            get { return (bool)Get(ButtonBase.PropIsPressed, false); }
            internal set { Set(ButtonBase.PropIsPressed, value, false); }
        }

        public bool IsMouseOver
        {
            get { return (bool)Get(ButtonBase.PropIsMouseOver, false); }
            private set { Set(ButtonBase.PropIsMouseOver, value, false); }
        }

        public bool IsMouseDown
        {
            get { return (bool)Get(ButtonBase.PropIsMouseDown, false); }
            private set { Set(ButtonBase.PropIsMouseDown, value, false); }
        }
        #endregion

        #region Internal
        private void UpdateMouseState()
        {
            if (!control.IsEnabled)
            {
                State = ButtonState.Normal;
            }
            else if (IsMouseOver && IsMouseDown)
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
