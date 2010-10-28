using System;
using jQueryApi;

namespace Open.Testing.Views
{
    /// <summary>The view of the control-host, the canvas which hosts test controls.</summary>
    public class ControlHostView : TestHarnessViewBase
    {
        #region Head
        private readonly jQueryObject divMain;

        /// <summary>Constructor.</summary>
        public ControlHostView() : base(jQuery.Select(CssSelectors.ControlHost))
        {
            divMain = jQuery.Select(CssSelectors.Main);
        }
        #endregion

        #region Properties
        /// <summary>Gets the main container DIV.</summary>
        public jQueryObject DivMain { get { return divMain; } }
        #endregion
    }
}
