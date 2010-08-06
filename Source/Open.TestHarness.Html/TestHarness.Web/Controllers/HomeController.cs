using System.Web.Mvc;

namespace Open.TestHarness.Web.Controllers
{
    /// <summary>Default controller.</summary>
    public class HomeController : ControllerBase
    {
        #region Head
        private const string KeyOutputTitle = "Output_Title";
        #endregion

        #region Methods
        // GET: /Index/
        public ActionResult Index()
        {
            ViewModel.OutputTitle = GetResource(KeyOutputTitle);
            return View(ViewModel);
        }
        #endregion
    }
}
