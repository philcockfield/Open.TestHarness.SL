using System;
using System.Collections;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A logical model for a button.</summary>
    public class ButtonModel : ModelBase, IButton, ISize
    {
        #region Events
        public event EventHandler Click;
        private void FireClick() { if (Click != null) Click(this, new EventArgs()); }

        public event EventHandler IsPressedChanged;
        private void FireIsPressedChanged() { if (IsPressedChanged != null) IsPressedChanged(this, new EventArgs()); }

        public event EventHandler LayoutInvalidated;
        protected void FireLayoutInvalidated(){if (LayoutInvalidated != null) LayoutInvalidated(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropIsEnabled = "IsEnabled";
        public const string PropCanToggle = "CanToggle";
        public const string PropIsPressed = "IsPressed";
        public const string PropWidth = "Width";
        public const string PropHeight = "Height";
        public const string PropCanFocus = "CanFocus";

        public const int NoSize = -1;

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

        public bool CanFocus
        {
            get { return (bool) Get(PropCanFocus, true); }
            set { Set(PropCanFocus, value, true); }
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

        #region Properties : ISize
        public int Width
        {
            get { return (int)Get(PropWidth, NoSize); }
            set { Set(PropWidth, value, NoSize); }
        }

        public int Height
        {
            get { return (int) Get(PropHeight, NoSize); }
            set { Set(PropHeight, value, NoSize); }
        }
        #endregion

        #region Methods : IButton
        public void InvokeClick(bool force)
        {
            if (!IsEnabled && !force) return;
            if (CanToggle) IsPressed = !IsPressed;
            FireClick();
        }

        public virtual IButtonView CreateView()
        {
            return null; // Optionally implemented in deriving classes.
        }
        #endregion

        #region Methods : ISize
        public void SetSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
        #endregion
    }
}