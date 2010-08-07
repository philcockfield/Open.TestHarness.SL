using System.Web.Mvc;

namespace Open.TestHarness.Web.Controllers
{
    public partial class TestHarnessController : ControllerBase
    {
        #region Head
        private const string KeyOutputTitle = "Output_Title";
        #endregion

        #region Methods : Actions
        /// <summary>The root of the TestHarness.</summary>
        public virtual ActionResult Index()
        {
            return View();
        }

        /// <summary>The Output Log.</summary>
        public virtual ActionResult Log()
        {
            ViewModel.OutputTitle = GetResource(KeyOutputTitle);
            return View(ViewModel);
        }
        #endregion
    }
}
