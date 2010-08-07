using System.Web.Mvc;

namespace Open.TestHarness.Web.Controllers
{
    /// <summary>Default Home controller.</summary>
    public partial class HomeController : ControllerBase
    {
        public virtual ActionResult Index()
        {
            return RedirectToAction(MVC.TestHarness.Index());
        }
    }
}
