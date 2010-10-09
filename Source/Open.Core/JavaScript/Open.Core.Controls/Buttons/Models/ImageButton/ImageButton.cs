using System;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A button made up of images for each state.</summary>
    public abstract class ImageButton : ButtonModel
    {
        #region Head
        public const string PropBackgroundHighlighting = "BackgroundHighlighting";

        private readonly ImageButtonUrls defaultUrls = new ImageButtonUrls();
        private readonly ImageButtonUrls focusedUrls = new ImageButtonUrls();
        private readonly ImageButtonUrls disabledUrls = new ImageButtonUrls();

        private Spacing imageOffset;
        #endregion

        #region Properties
        /// <summary>Gets the pixel offset to apply to the image (applied when centering image within the button bounds).</summary>
        public Spacing ImageOffset { get { return imageOffset ?? (imageOffset = new Spacing()); } }

        /// <summary>Gets or sets whether a background is shown for highlighting.</summary>
        public bool BackgroundHighlighting
        {
            get { return (bool) Get(PropBackgroundHighlighting, true); }
            set { Set(PropBackgroundHighlighting, value, true); }
        }
        #endregion

        #region Properties : Protected
        /// <summary>Gets or sets the base path that images are stored within.</summary>
        protected string BasePath;

        /// <summary>Gets the set of URL's for the default state (enabled, not-focused).</summary>
        protected ImageButtonUrls DefaultUrls { get { return defaultUrls; } }

        /// <summary>Gets the set of URL's for the focused state.</summary>
        protected ImageButtonUrls FocusedUrls { get { return focusedUrls; } }

        /// <summary>Gets the set of URL's for the disabled.</summary>
        protected ImageButtonUrls DisabledUrls { get { return disabledUrls; } }
        #endregion

        #region Methods
        public override IButtonView CreateView()
        {
            return new ImageButtonView(this);
        }

        /// <summary>Gets the current image.</summary>
        /// <param name="state">The state of the button.</param>
        /// <param name="isFocused">Flag indicating if the button currently has focus.</param>
        public string GetCurrentImage(ButtonState state, bool isFocused)
        {
            string path;
            if (!IsEnabled) // Disabled.
            {
                path = DisabledUrls.GetPath(state, BasePath);
                if (path != null) return path;
            }

            if (isFocused)
            {
                path = FocusedUrls.GetPath(state, BasePath);
                if (path != null) return path;
            }

            return DefaultUrls.GetPath(state, BasePath);
        }
        #endregion
    }
}
