using System;
using System.Runtime.CompilerServices;
using jQueryApi;
using Open.Core.Controls.Buttons;

namespace Open.Core.Controls
{
    /// <summary>A panel that can be pinned open or closed.</summary>
    public class PinnablePanel : CollapsePanel
    {
        #region Events
        /// <summary>Fires when the panel is pinned (the pin button is pressed).</summary>
        public event EventHandler Pinned;
        private void FirePinned(){if (Pinned != null) Pinned(this, new EventArgs());}

        /// <summary>Fires when the panel is unpinned (the pin button is released).</summary>
        public event EventHandler Unpinned;
        private void FireUnpinned(){if (Unpinned != null) Unpinned(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropIsPinned = "IsPinned";
        public const string ButtonCssClass = "pinButton";

        private readonly ImageButton pin;

        [AlternateSignature]
        extern public PinnablePanel();
        public PinnablePanel(jQueryObject container) : base(container)
        {
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

            // Finish up.
            SyncButton();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the pinned state of the panel..</summary>
        public bool IsPinned
        {
            get { return (bool) Get(PropIsPinned, false); }
            set
            {
                if (Set(PropIsPinned, value, false))
                {
                    if (value) { FirePinned(); } else { FireUnpinned(); }
                    SyncButton();
                }
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
