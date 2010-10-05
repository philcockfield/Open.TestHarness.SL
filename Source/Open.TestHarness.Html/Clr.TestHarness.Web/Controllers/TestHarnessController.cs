using System.Web.Mvc;
using Open.Core.Web;

namespace Open.Testing.Web.Controllers
{
    public partial class TestHarnessController : ControllerBase
    {
        #region Head
        private const string KeyOutputTitle = "Output_Title";
        private const string KeyAppTitle = "App_Title";
        private const string KeyMethodListTitle = "MethodList_Title";
        private const string KeyAddPackageTitle = "Add_Package_Title";
        private const string KeyAddPackageLabelScriptUrl = "Add_Package_Label_ScriptUrl";
        private const string KeyAddPackageLabelInitMethod = "Add_Package_Label_InitMethod";

        private const string GoogleAnalyticsKey = "UA-12876655-1";
        #endregion

        #region Methods : Actions
        /// <summary>The TestHarness page.</summary>
        public virtual ActionResult Index()
        {
            ViewModel.AppTitle = GetResource(KeyAppTitle);
            return View(MVC.TestHarness.Views.Page, ViewModel);
        }

        /// <summary>The root of the TestHarness with all CSS and script links (but not in a page structure).</summary>
        public virtual ActionResult Embed()
        {
            return View(MVC.TestHarness.Views.Embed, ViewModel);
        }

        /// <summary>The HEAD content.</summary>
        public virtual ActionResult Head()
        {
            WebConstants.Script.OpenCorePath = "/Content/Scripts/";
            return View(ViewModel);
        }

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

        #region Methods : Component Screens
        /// <summary>The 'Add Package' screen .</summary>
        public virtual ActionResult AddPackage()
        {
            ViewModel.Title = GetResource(KeyAddPackageTitle);
            ViewModel.ScriptUrlLabel = GetResource(KeyAddPackageLabelScriptUrl);
            ViewModel.InitMethodLabel = GetResource(KeyAddPackageLabelInitMethod);

            return View(ViewModel);
        }
        #endregion
    }
}
