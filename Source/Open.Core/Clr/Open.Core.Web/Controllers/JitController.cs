using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Open.Core.Web.Controllers
{
    /// <summary>Controls the insertion of JIT visualization controls.</summary>
    public class JitController : ControllerBase
    {
        #region Head
        public const string Name = "Jit";
        public const string ActionHyperTree = "HyperTree";
        
        #endregion

        #region Methods
        public virtual ActionResult HyperTree()
        {
            return View();
        }
        #endregion
    }
}
