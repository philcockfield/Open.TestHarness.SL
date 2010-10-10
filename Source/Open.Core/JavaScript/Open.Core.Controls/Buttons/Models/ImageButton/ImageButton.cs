namespace Open.Core.Controls.Buttons
{
    /// <summary>A button made up of images for each state.</summary>
    /// <remarks>To derive explicit image-button inherit directly from [ImageButtonBase].</remarks>
    public sealed class ImageButton : ImageButtonBase
    {
        #region Properties
        /// <summary>Gets or sets the base path that images are stored within.</summary>
        public string BasePath
        {
            get { return base.BasePath; }
            set { base.BasePath = value; }
        }

        /// <summary>Gets the set of URL's for the default state (enabled, not-focused).</summary>
        public  ImageButtonUrls DefaultUrls { get { return base.DefaultUrls; } }

        /// <summary>Gets the set of URL's for the focused state.</summary>
        public  ImageButtonUrls FocusedUrls { get { return base.FocusedUrls; } }

        /// <summary>Gets the set of URL's for the disabled.</summary>
        public  ImageButtonUrls DisabledUrls { get { return base.DisabledUrls; } }
        #endregion
    }
}
