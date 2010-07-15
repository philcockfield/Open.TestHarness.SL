using System;

namespace Open.Core.UI
{
    /// <summary>Event arguments that accompany the clicking of a prompt button.</summary>
    public class PromptButtonClickEventArgs : EventArgs
    {
        /// <summary>Constructor.</summary>
        public PromptButtonClickEventArgs(PromptResult buttonType)
        {
            ButtonType = buttonType;
        }

        /// <summary>Gets the type of button that was clicked.</summary>
        public PromptResult ButtonType { get; set; }
    }
}