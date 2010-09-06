using jQueryApi;
using Open.Core;

namespace Open.Testing.Views
{
    /// <summary>Represents the container for a test-control.</summary>
    public class ControlWrapperView : TestHarnessViewBase
    {
        #region Head
        private readonly jQueryObject divRoot;
        private readonly jQueryObject controlContainer;


        /// <summary>Constructor.</summary>
        /// <param name="divControlHost">The control host DIV.</param>
        /// <param name="controlContainer">The control content (supplied by the test class. This is the control that is under test).</param>
        public ControlWrapperView(jQueryObject divControlHost, jQueryObject controlContainer)
        {
            this.controlContainer = controlContainer;
            // Setup initial conditions.
            Initialize(divControlHost);

            // Insert the root.
            divRoot = Html.CreateDiv();
            divRoot.AppendTo(divControlHost);



            //TEMP 
            divRoot.Append("Yo!");
            divRoot.CSS("background", "orange");
            divRoot.CSS("border", "solid 1px black");
        }

        protected override void OnDisposed()
        {
            divRoot.Remove();
            base.OnDisposed();
        }
        #endregion
    }
}
