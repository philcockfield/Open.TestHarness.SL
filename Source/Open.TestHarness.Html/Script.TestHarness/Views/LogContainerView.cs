using System;
using jQueryApi;

namespace Open.Testing.Views
{
    /// <summary>The log container view.</summary>
    public class LogContainerView : TestHarnessViewBase
    {
        #region Head
        /// <summary>Constructor.</summary>
        public LogContainerView() : base(jQuery.Select(CssSelectors.LogContainer))
        {
        }
        #endregion
    }
}
