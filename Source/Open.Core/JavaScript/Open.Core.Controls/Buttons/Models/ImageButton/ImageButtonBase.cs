using System;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A base class for a button made up of images for each state.</summary>
    /// <remarks>
    ///     Used the derived [ImageButton] where in the consuming you code you are specifying
    ///     the set of images to use.  [ImageButtonBase] class is provided to that specific image-buttons
    ///     can be cleanly provided without exposing the API for assigning image URL's.
    /// </remarks>
    public abstract class ImageButtonBase : ButtonBase
    {
        #region Head
        public const string PropBackgroundHighlighting = "BackgroundHighlighting";

        private readonly ImageButtonUrls defaultUrls = new ImageButtonUrls();
        private readonly ImageButtonUrls focusedUrls = new ImageButtonUrls();
        private readonly ImageButtonUrls disabledUrls = new ImageButtonUrls();
        #endregion

        #region Properties
        /// <summary>Gets or sets whether a background is shown for highlighting.</summary>
        public bool BackgroundHighlighting
        {
            get { return (bool) Get(PropBackgroundHighlighting, false); }
            set { Set(PropBackgroundHighlighting, value, false); }
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

        /// <summary>Preloads the images for the button.</summary>
        public void Preload()
        {
            DefaultUrls.Preload(BasePath);
            FocusedUrls.Preload(BasePath);
            DisabledUrls.Preload(BasePath);
        }
        #endregion
    }
}
