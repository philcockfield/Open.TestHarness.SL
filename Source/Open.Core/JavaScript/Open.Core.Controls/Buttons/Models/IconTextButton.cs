using System;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A simple button which displays either an icon, or text, or both.</summary>
    public class IconTextButton : ButtonBase
    {
        #region Head
        public const string PropText = "Text";
        #endregion

        #region Properties
        /// <summary>Gets or sets the text label of the button.</summary>
        public string Text
        {
            get
            {
                string value = TemplateData[PropText] as string;
                return string.IsNullOrEmpty(value) ? null : value;
            }
            set
            {
                if (value == Text) return;
                TemplateData[PropText] = value;
                FirePropertyChanged(PropText);
            }
        }
        #endregion

        #region Methods
        public override IButtonView CreateView()
        {
            return new IconTextButtonView(this);
        }
        #endregion
    }
}
