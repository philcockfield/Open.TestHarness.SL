using System.Web.Mvc;

namespace Open.TestHarness.Web.Controllers
{
    public class SamplesController : Controller
    {
        public ActionResult Templates() { return View(); }
        public ActionResult JQuery() { return View(); }
    }
}
