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

        public ViewFocus(ViewBase view)
        {
            // Setup initial conditions.
            this.view = view;

            // Wire up events.
            view.Container.Bind(Html.Focus, delegate(jQueryEvent e) { HandleFocusChanged(true); });
            view.Container.Bind(Html.Blur, delegate(jQueryEvent e) { HandleFocusChanged(false); });
            view.IsEnabledChanged += HandleIsEnabledChanged;
        }

        protected override void OnDisposed()
        {
            view.Container.Unbind(Html.Focus);
            view.Container.Unbind(Html.Blur);
            view.IsEnabledChanged -= HandleIsEnabledChanged;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void HandleFocusChanged(bool gotFocus)
        {
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
                    view.Container.CSS(Css.Outline, value ? string.Empty : "0");
                }
            }
        }
        #endregion

        #region Methods : IFocus
        public bool Apply()
        {
            if (!CanFocus) return false;
            view.Container.Focus();
            return true;
        }

        public bool Blur()
        {
            if (!CanFocus) return false;
            view.Container.Blur();
            return true;
        }
        #endregion

        #region Internal
        [AlternateSignature]
        private extern void SyncTabIndexOnHtml();
        private void SyncTabIndexOnHtml(int value)
        {
            if (Script.IsNullOrUndefined(value)) value = TabIndex;
            view.Container.Attribute(Html.TabIndex, value.ToString());
        }
        #endregion
    }
}
