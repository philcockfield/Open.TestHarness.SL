using System.Web.Mvc;

namespace Open.Core.Web.Controllers
{
    /// <summary>Controller for working with JavaScript files.</summary>
    public partial class ScriptsController : ControllerBase
    {
        #region Head
        public const string Name = "Scripts";
        public const string ActionCoreScripts = "CoreScripts";
        public const string ActionCoreJQuery = "CoreJQuery";
        #endregion

        #region Methods
        public virtual ActionResult CoreScripts() { return View(); }
        public virtual ActionResult CoreJQuery() { return View(); }
        #endregion
    }
}
