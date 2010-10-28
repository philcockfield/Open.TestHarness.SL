using System.Web.Mvc;

namespace Open.Core.Web.Controllers
{
    public partial class TestHarnessController : ControllerBase
    {
        #region Head
        public const string Name = "TestHarness";
        public const string ActionPage = "Page";
        public const string ActionEmbed = "Embed";
        public const string ActionSidebar = "Sidebar";
        public const string ActionMain = "Main";
        public const string ActionHead = "Head";
        public const string ActionBody = "Body";
        public const string ActionLog = "Log";
        public const string ActionMethodList = "MethodList";
        public const string ActionGoogleAnalytics = "GoogleAnalytics";

        private const string GoogleAnalyticsKey = "UA-12876655-1";
        #endregion

        #region Methods : Actions
        /// <summary>The TestHarness page.</summary>
        public virtual ActionResult Page()
        {
            return View();
        }

        /// <summary>The root of the TestHarness with all CSS and script links (but not in a page structure).</summary>
        public virtual ActionResult Embed()
        {
            return View();
        }

        /// <summary>The HEAD content.</summary>
        public virtual ActionResult Head()
        {
            // Cause the scripts to be loaded the local folder (not the embedded versions of the scripts in [Open.Core.Web]).
            // NB:  This allows quicker compilation during development.
            //        Just compile the test JS project causing all Open.Core filed to be copied locally.  
            if (Request.IsLocal) WebConstants.Script.OpenCorePath = "/Content/Scripts/";

            return View();
        }

        /// <summary>The BODY content.</summary>
        public virtual ActionResult Body() { return View(); }
        #endregion

        #region Methods : Child Actions
        /// <summary>The Google Analytics script.</summary>
        [ChildActionOnly]
        public virtual ActionResult GoogleAnalytics()
        {
            ViewData["Key"] = GoogleAnalyticsKey;
            ViewData["IsLocal"] = Request.IsLocal;
            return View();
        }

        /// <summary>The Output Log.</summary>
        [ChildActionOnly]
        public virtual ActionResult Log()
        {
            return View();
        }

        /// <summary>The Sidebar index.</summary>
        [ChildActionOnly]
        public virtual ActionResult Sidebar() { return View(); }

        /// <summary>The panel that contains the list of tests within the Sidebar.</summary>
        [ChildActionOnly]
        public virtual ActionResult MethodList()
        {
            return View();
        }

        /// <summary>The Main content panel.</summary>
        [ChildActionOnly]
        public virtual ActionResult Main() { return View(); }
        #endregion

        #region Methods : Component Screens
        /// <summary>The 'Add Package' screen .</summary>
        public virtual ActionResult AddPackage()
        {
            return View();
        }
        #endregion
    }
}
