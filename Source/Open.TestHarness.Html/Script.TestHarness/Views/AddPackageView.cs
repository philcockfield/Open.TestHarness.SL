using System;
using jQueryApi;
using Open.Core;

namespace Open.Testing.Views
{
    /// <summary>View for defining a new test-package to add to the side bar.</summary>
    public class AddPackageView : ViewBase
    {
        #region Head
        private const string contentUrl = "/TestHarness/AddPackage/";

        /// <summary>Constructor.</summary>
        public AddPackageView()
        {
            RetrieveHtml(contentUrl, delegate
                                         {
                                             
                                         });
        }
        #endregion

        #region Methods
        /// <summary>Inserts an instance of the view into the TestHarness' main canvas.</summary>
        public static AddPackageView AddToTestHarness()
        {
            AddPackageView view = new AddPackageView();
            TestHarness.Reset();
            TestHarness.DisplayMode = ControlDisplayMode.Fill;
            TestHarness.AddControl(view);
            return view;
        }
        #endregion
    }
}
