using System;
using System.Runtime.CompilerServices;
using jQueryApi;
using Open.Core.Controls.Buttons;

namespace Open.Core.Controls
{
    /// <summary>A panel that can be pinned open or closed.</summary>
    public class PinPanel : CollapsePanel
    {
        #region Events
        /// <summary>Fires when the panel is pinned (the pin button is pressed).</summary>
        public event EventHandler Pinned;
        private void FirePinned(){if (Pinned != null) Pinned(this, new EventArgs());}

        /// <summary>Fires when the panel is unpinned (the pin button is released).</summary>
        public event EventHandler Unpinned;
        private void FireUnpinned(){if (Unpinned != null) Unpinned(this, new EventArgs());}

        /// <summary>Fires when the IsPinned property changes.</summary>
        public event EventHandler IsPinnedChanged;
        private void FireIsPinnedChanged(){if (IsPinnedChanged != null) IsPinnedChanged(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropHideDelay = "HideDelay";
        public const string PropIsPinned = "IsPinned";
        public const string ButtonCssClass = "pinButton";
        public const double DefaultHideDelay = 0.3;
        public const bool DefaultIsPinned = true;

        private readonly ImageButton pin;
        private readonly DelayedAction hideDelay;

        [AlternateSignature]
        extern public PinPanel();
        public PinPanel(jQueryObject container) : base(container)
        {
            // Setup initial conditions.
            hideDelay = new DelayedAction(DefaultHideDelay, OnHideDelayElapsed);

            // Insert the pin button.
            pin = ImageButtonFactory.Create(ImageButtons.PushPin);
            IButtonView view = pin.CreateView();
            view.Container.AddClass(ButtonCssClass);
            Container.Append(view.Container);

            // Wire up events.
            pin.IsPressedChanged += delegate
                                        {
                                            IsPinned = pin.IsPressed;
                                        };
            Container.MouseEnter(delegate { hideDelay.Stop(); });
            Container.MouseLeave(delegate { hideDelay.Start(); });

            // Finish up.
            SyncButton();
        }

        protected override void OnDisposed()
        {
            hideDelay.Dispose();
            base.OnDisposed();
        }

        #endregion

        #region Event Handlers
        private void OnHideDelayElapsed()
        {
            if (!IsPinned) Collapse(null);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the pinned state of the panel.</summary>
        public bool IsPinned
        {
            get { return (bool)Get(PropIsPinned, DefaultIsPinned); }
            set
            {
                if (Set(PropIsPinned, value, DefaultIsPinned))
                {
                    if (value) { FirePinned(); } else { FireUnpinned(); }
                    SyncButton();
                    if (value) hideDelay.Stop();
                    FireIsPinnedChanged(); 
                }
            }
        }

        /// <summary>
        ///     Gets or sets the delay to wait for (in seconds) before 
        ///     auto-collapsing the panel when unpinned and the mouse 
        ///     leaves the panel.
        /// </summary>
        public double HideDelay
        {
            get { return hideDelay.Delay; }
            set { 
                hideDelay.Delay = value;
                FirePropertyChanged(PropHideDelay);
            }
        }
        #endregion

        #region Internal
        private void SyncButton()
        {
            pin.IsPressed = IsPinned;
        }
        #endregion
    }
}
