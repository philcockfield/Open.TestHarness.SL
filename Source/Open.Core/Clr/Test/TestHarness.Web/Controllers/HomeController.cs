using System.Web.Mvc;
using Open.Core.Web;

namespace Open.TestHarness.Web.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            return RedirectPermanent("/Open.Core/"); // The URL defaults to the TestHarness controller.
        }
    }
}
