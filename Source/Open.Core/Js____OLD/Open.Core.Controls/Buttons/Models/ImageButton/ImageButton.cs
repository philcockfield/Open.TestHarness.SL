namespace Open.Core.Controls.Buttons
{
    /// <summary>A button made up of images for each state.</summary>
    /// <remarks>To derive explicit image-button inherit directly from [ImageButtonBase].</remarks>
    public sealed class ImageButton : ImageButtonBase
    {
        #region Properties
#pragma warning disable 108,114
        /// <summary>Gets or sets the base path that images are stored within.</summary>
        public string BasePath
#pragma warning restore 108,114
        {
            get { return base.BasePath; }
            set { base.BasePath = value; }
        }

#pragma warning disable 108,114
        /// <summary>Gets the set of URL's for the default state (enabled, not-focused).</summary>
        public ImageButtonUrls DefaultUrls { get { return base.DefaultUrls; } }
#pragma warning restore 108,114

#pragma warning disable 108,114
        /// <summary>Gets the set of URL's for the focused state.</summary>
        public ImageButtonUrls FocusedUrls { get { return base.FocusedUrls; } }
#pragma warning restore 108,114

#pragma warning disable 108,114
        /// <summary>Gets the set of URL's for the disabled.</summary>
        public ImageButtonUrls DisabledUrls { get { return base.DisabledUrls; } }
#pragma warning restore 108,114
        #endregion
    }
}
