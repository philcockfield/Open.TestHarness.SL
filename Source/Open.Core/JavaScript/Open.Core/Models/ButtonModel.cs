using System;
using System.Collections;

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

        private Dictionary templateData;
        private readonly ArrayList invokeKeyCodes = new ArrayList();

        /// <summary>Constructor.</summary>
        public ButtonModel()
        {
            // Add the default key-codes that will cause the button to be invoked when pressed.
            InvokeKeyCodes.Add(Key.Enter);
            InvokeKeyCodes.Add(Key.Space);
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
            set
            {
                if(Set(PropCanToggle, value, false))
                {
                    // If the button can no longer toggle, and it's in a pressed state, release the press.
                    if (value == false && IsPressed) IsPressed = false;
                }
            }
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

        public Dictionary TemplateData
        {
            get { return templateData ?? (templateData = new Dictionary()); }
        }

        public ArrayList InvokeKeyCodes { get { return invokeKeyCodes; } }
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