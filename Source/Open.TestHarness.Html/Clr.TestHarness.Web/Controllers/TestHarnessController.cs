using System.Web.Mvc;

namespace Open.Testing.Web.Controllers
{
    public partial class TestHarnessController : ControllerBase
    {
        #region Head
        private const string KeyOutputTitle = "Output_Title";
        private const string KeyAppTitle = "App_Title";
        private const string KeyMethodListTitle = "MethodList_Title";

        private const string GoogleAnalyticsKey = "UA-12876655-1";
        #endregion

        #region Methods : Actions
        /// <summary>Page containing the bootstrapper script.</summary>
        /// <returns></returns>
        public virtual ActionResult Bootstrap() { return View(ViewModel); }

        /// <summary>The root of the TestHarness.</summary>
        public virtual ActionResult Index()
        {
            ViewModel.AppTitle = GetResource(KeyAppTitle);
            return View(ViewModel);
        }

        /// <summary>The HEAD content.</summary>
        public virtual ActionResult Head() { return View(ViewModel); }

        /// <summary>The BODY content.</summary>
        public virtual ActionResult Body() { return View(ViewModel); }
        #endregion

        #region Methods : Child Actions
        /// <summary>The Google Analytics script.</summary>
        [ChildActionOnly]
        public virtual ActionResult GoogleAnalytics()
        {
            ViewModel.GoogleAnalyticsKey = GoogleAnalyticsKey;
            ViewModel.IsLocal = Request.IsLocal;
            return View(ViewModel);
        }

        /// <summary>The Output Log.</summary>
        [ChildActionOnly]
        public virtual ActionResult Log()
        {
            ViewModel.OutputTitle = GetResource(KeyOutputTitle);
            return View(ViewModel);
        }

        /// <summary>The Sidebar index.</summary>
        [ChildActionOnly]
        public virtual ActionResult Sidebar() { return View(ViewModel); }

        /// <summary>The panel that contains the list of tests within the Sidebar.</summary>
        [ChildActionOnly]
        public virtual ActionResult MethodList()
        {
            ViewModel.MethodListTitle = GetResource(KeyMethodListTitle);
            return View(ViewModel);
        }

        /// <summary>The Main content panel.</summary>
        [ChildActionOnly]
        public virtual ActionResult Main() { return View(ViewModel); }
        #endregion
    }
}
