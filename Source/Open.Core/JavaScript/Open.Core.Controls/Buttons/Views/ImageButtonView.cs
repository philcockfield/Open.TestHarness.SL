using System;

namespace Open.Core.Controls.Buttons
{
    /// <summary>The view for the ImageButton.</summary>
    internal class ImageButtonView : ButtonView
    {
        #region Head
        public ImageButtonView(IButton model) : base(model)
        {

            SetSize(20,20);
            Background = Color.HotPink;

        }
        #endregion
    }
}
