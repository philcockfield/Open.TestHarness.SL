using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Open.Core.Web.Controllers
{
    public partial class FooController : ControllerBase
    {
        public virtual ActionResult Index()
        {
            return View();
        }

    }
}