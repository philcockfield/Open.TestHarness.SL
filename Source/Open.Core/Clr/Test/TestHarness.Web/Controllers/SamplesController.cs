﻿using System.Web.Mvc;

namespace Open.TestHarness.Web.Controllers
{
    public partial class SamplesController : Controller
    {
        public virtual ActionResult Temp() { return View(); }

        public virtual ActionResult Templates() { return View(); }
        public virtual ActionResult ButtonTemplate() { return View(); }

        public virtual ActionResult JQuery() { return View(); }
        public virtual ActionResult Embed() { return View(); }

        public virtual ActionResult SamplePart() { return View(); }

        public virtual ActionResult AccordianTest() { return View(); }

    }
}
