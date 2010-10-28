using System.Web.Mvc;
using Open.Core.Web;

namespace Open.TestHarness.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectPermanent("/Open.Core/"); // The URL defaults to the TestHarness controller.
        }
    }
}
