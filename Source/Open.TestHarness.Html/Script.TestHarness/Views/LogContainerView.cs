using System;
using jQueryApi;
using Open.Core.Controls.Buttons;

namespace Open.Testing.Views
{
    /// <summary>The log container view.</summary>
    public class LogContainerView : TestHarnessViewBase
    {
        #region Event Handlers
        /// <summary>Fires when the 'Clear' button is clicked.</summary>
        public event EventHandler ClearClick;
        private void FireClearClick(){if (ClearClick != null) ClearClick(this, new EventArgs());}
        #endregion

        #region Head
        private const int ButtonHeight = 32;

        /// <summary>Constructor.</summary>
        public LogContainerView() : base(jQuery.Select(CssSelectors.LogContainer))
        {
            // Clear button.
            ButtonHelper.InsertButton(
                                    ImageButtons.Remove,
                                    CssSelectors.LogClearButton,
                                    ButtonHeight,
                                    delegate { FireClearClick(); });
        }
        #endregion
    }
}
