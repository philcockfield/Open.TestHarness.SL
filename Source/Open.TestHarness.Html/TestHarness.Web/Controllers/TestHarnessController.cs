using System.Web.Mvc;

namespace Open.TestHarness.Web.Controllers
{
    public partial class TestHarnessController : ControllerBase
    {
        #region Head
        private const string KeyOutputTitle = "Output_Title";
        private const string KeyAppTitle = "App_Title";
        #endregion

        #region Methods : Actions
        /// <summary>The root of the TestHarness.</summary>
        public virtual ActionResult Index()
        {
            ViewModel.AppTitle = GetResource(KeyAppTitle);
            return View(ViewModel);
        }

        /// <summary>The Output Log.</summary>
        public virtual ActionResult Log()
        {
            ViewModel.OutputTitle = GetResource(KeyOutputTitle);
            return View(ViewModel);
        }

        /// <summary>The Sidebar index.</summary>
        public virtual ActionResult Sidebar()
        {
            return View(ViewModel);
        }
        #endregion
    }
}
