using System;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core
{
    internal class ViewFocus : ModelBase, IFocus
    {
        #region Events
        public event EventHandler GotFocus;
        private void FireGotFocus()
        {
            if (GotFocus != null) GotFocus(this, new EventArgs());
            view.FireGotFocus();
        }

        public event EventHandler LostFocus;
        private void FireLostFocus()
        {
            if (LostFocus != null) LostFocus(this, new EventArgs());
            view.FireLostFocus();
        }
        #endregion

        #region Head
        public const string PropIsEnabled = "IsEnabled";
        public const string PropIsFocused = "IsFocused";
        public const string PropCanFocus = "CanFocus";
        public const string PropTabIndex = "TabIndex";
        public const string PropBrowserHighlighting = "BrowserHighlighting";

        private const int noTabIndex = -1;
        private int tabIndex = noTabIndex;
        private bool tabIndexChanging;
        private readonly ViewBase view;
        private readonly jQueryObject element;

        public ViewFocus(ViewBase view, jQueryObject element)
        {
            // Setup initial conditions.
            this.view = view;
            this.element = element;

            // Wire up events.
            element.Bind(Html.Focus, delegate(jQueryEvent e) { HandleFocusChanged(true); });
            element.Bind(Html.Blur, delegate(jQueryEvent e) { HandleFocusChanged(false); });
            view.IsEnabledChanged += HandleIsEnabledChanged;
        }

        protected override void OnDisposed()
        {
            element.Unbind(Html.Focus);
            element.Unbind(Html.Blur);
            view.IsEnabledChanged -= HandleIsEnabledChanged;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void HandleFocusChanged(bool gotFocus)
        {
            if (IsFocused == gotFocus) return;
            IsFocused = gotFocus;
            if (gotFocus) { FireGotFocus(); } else { FireLostFocus(); }
        }

        private void HandleIsEnabledChanged(object sender, EventArgs e)
        {
            if (IsFocused) Blur();
            SyncTabIndexOnHtml();
        }
        #endregion

        #region Properties : IFocus
        public bool IsFocused
        {
            get { return (bool)Get(PropIsFocused, false); }
            private set { Set(PropIsFocused, value, false); }
        }

        public bool CanFocus
        {
            get { return tabIndex >= 0; }
            set
            {
                // Setup initial conditions.
                if (value == CanFocus) return;

                // Update the value (stored in TabIndex).
                if (value && tabIndex < 0) TabIndex = 0;
                if (!value && tabIndex >= 0) TabIndex = -1;

                // Finish up.
                FirePropertyChanged(PropCanFocus);
                IsFocused = false;
            }
        }

        public int TabIndex
        {
            get
            {
                // Control can only recieve focus if it is enabled.
                return view.IsEnabled ? tabIndex : noTabIndex;
            }
            set
            {
                // Setup initial conditions.
                if (tabIndexChanging) return;
                if (value == tabIndex) return;
                tabIndex = value;

                // Sync the 'CanFocus' property.
                if (value < 0 && CanFocus)
                {
                    tabIndexChanging = true;
                    CanFocus = false;
                    tabIndexChanging = false;
                }

                // Keep the HTML element in sync (change the 'tabIndex' attribute).
                SyncTabIndexOnHtml(value);

                // Finish up.
                FirePropertyChanged(PropTabIndex);
            }
        }

        public bool BrowserHighlighting
        {
            get { return (bool)Get(PropBrowserHighlighting, true); }
            set
            {
                if (Set(PropBrowserHighlighting, value, true))
                {
                    element.CSS(Css.Outline, value ? string.Empty : "0");
                }
            }
        }
        #endregion

        #region Methods : IFocus
        public bool Apply()
        {
            if (!CanFocus) return false;
            element.Focus();
            return true;
        }

        public bool Blur()
        {
            if (!CanFocus) return false;
            element.Blur();
            return true;
        }
        #endregion

        #region Internal
        [AlternateSignature]
        private extern void SyncTabIndexOnHtml();
        private void SyncTabIndexOnHtml(int value)
        {
            if (Script.IsNullOrUndefined(value)) value = TabIndex;
            element.Attribute(Html.TabIndex, value.ToString());
        }
        #endregion
    }
}
