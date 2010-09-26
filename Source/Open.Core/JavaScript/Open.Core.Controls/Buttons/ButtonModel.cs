using System;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A logical model for a button.</summary>
    public class ButtonModel : ModelBase, IButton
    {
        #region Events
        public event EventHandler Click;
        private void FireClick() { if (Click != null) Click(this, new EventArgs()); }

        public event EventHandler IsPressedChanged;
        private void FireIsPressedChanged() { if (IsPressedChanged != null) IsPressedChanged(this, new EventArgs()); }
        #endregion

        #region Head
        public const string PropIsEnabled = "IsEnabled";
        public const string PropCanToggle = "CanToggle";
        public const string PropIsPressed = "IsPressed";

        /// <summary>Constructor.</summary>
        public ButtonModel()
        {
        }
        #endregion

        #region Properties : IButton
        public bool IsEnabled
        {
            get { return (bool) Get(PropIsEnabled, true); }
            set { Set(PropIsEnabled, value, true); }
        }

        public bool CanToggle
        {
            get { return (bool) Get(PropCanToggle, false); }
            set { Set(PropCanToggle, value, false); }
        }

        public bool IsPressed
        {
            get { return (bool)Get(PropIsPressed, false); }
            set
            {
                if(Set(PropIsPressed, value, false))
                {
                    FireIsPressedChanged();
                }
            }
        }
        #endregion

        #region Methods : IButton
        public void InvokeClick(bool force)
        {
            if (!IsEnabled && !force) return;
            if (CanToggle) IsPressed = !IsPressed;
            FireClick();
        }
        #endregion
    }
}