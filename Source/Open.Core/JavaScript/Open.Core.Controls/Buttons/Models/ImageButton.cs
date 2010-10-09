using System;
using System.Collections;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A button made up of images for each state.</summary>
    public abstract class ImageButton : ButtonModel
    {
        #region Head
        public const string PropCurrentImage = "CurrentImage";

        #endregion

        #region Properties

        /// <summary>Gets the current image.</summary>
        internal string CurrentImage { get { return null; } }

        #endregion

        #region Methods
        public override IButtonView CreateView()
        {
            return new ImageButtonView(this);
        }
        #endregion
    }
}
