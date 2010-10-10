using System;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Factory for creating image buttons.</summary>
    public static class ImageButtonFactory
    {
        #region Head
        private const string PathFlatDark = "/Open.Assets/Icons/Flat/Dark/";
        #endregion

        #region Methods
        /// <summary>Factory method for creating an pre-canned image button.</summary>
        /// <param name="type">Flag indicating the type of button to create.</param>
        public static ImageButton Create(ImageButtons type)
        {
            // Setup initial conditions.
            ImageButton button = new ImageButton();
            button.CanFocus = false;

            // Configure image URL's.
            switch (type)
            {
                case ImageButtons.PlusDark:
                    button.BasePath = PathFlatDark;
                    button.DefaultUrls.Normal = "Plus.15x15.png";
                    button.DefaultUrls.Over = "Plus.Over.15x15.png";
                    button.DefaultUrls.Down = "Plus.Down.15x15.png";
                    button.SetSize(15, 15);
                    break;
                
                default: throw new Exception("ImageButton not supported: " + type.ToString());
            }

            // Finish up.
            return button;
        }
        #endregion
    }
}
