using System;
using Open.Core.Helpers;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A set of Url's for an ImageButton.</summary>
    public class ImageButtonUrls
    {
        #region Properties
        /// <summary>Gets or sets the base path that images are stored within.</summary>
        public string BasePath;

        /// <summary>Gets or sets the SRC value for the 'Normal' state.</summary>
        public string Normal;

        /// <summary>Gets or sets the SRC value for the 'Over' state.</summary>
        public string Over;

        /// <summary>Gets or sets the SRC value for the 'Down' state.</summary>
        public string Down;

        /// <summary>Gets or sets the SRC value for the 'Pressed' state.</summary>
        public string Pressed;
        #endregion

        #region Methods
        /// <summary>Removes all URL values.</summary>
        public void Reset()
        {
            BasePath = null;
            Normal = null;
            Over = null;
            Down = null;
            Pressed = null;
        }

        internal string GetPath(ButtonState state, string basePath)
        {
            // See if there's a value specified for the state.
            string path = ForStateWithFallback(state);
            if (string.IsNullOrEmpty(path)) return null;

            // Prepend the base path.
            if (!string.IsNullOrEmpty(BasePath)) basePath = BasePath;
            return basePath + path;
        }

        internal void Preload(string basePath)
        {
            ImagePreloader.Preload(GetPath(ButtonState.Normal, basePath));
            ImagePreloader.Preload(GetPath(ButtonState.MouseOver, basePath));
            ImagePreloader.Preload(GetPath(ButtonState.MouseDown, basePath));
            ImagePreloader.Preload(GetPath(ButtonState.Pressed, basePath));
        }
        #endregion

        #region Internal
        private string ForStateWithFallback(ButtonState state)
        {
            string path = ForState(state);
            switch (state)
            {
                case ButtonState.Normal: break; // No fallback.

                case ButtonState.MouseOver: 
                    path = Over ?? ForState(ButtonState.Normal);
                    break;

                case ButtonState.MouseDown: 
                    path = Down ?? ForState(ButtonState.Normal);
                    break;

                case ButtonState.Pressed: 
                    path = Pressed ?? ForStateWithFallback(ButtonState.MouseDown);
                    break;

                default: throw new Exception("Not Supported: " + state.ToString());
            }
            return path;
        }

        private string ForState(ButtonState state)
        {
            string path;
            switch (state)
            {
                case ButtonState.Normal: path = Normal; break;
                case ButtonState.MouseOver: path = Over; break;
                case ButtonState.MouseDown: path = Down; break;
                case ButtonState.Pressed: path = Pressed; break;
                default: throw new Exception("Not Supported: " + state.ToString());
            }
            if (string.IsNullOrEmpty(path)) path = null;
            return path;
        }
        #endregion
    }
}
