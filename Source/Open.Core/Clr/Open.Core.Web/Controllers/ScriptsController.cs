using System.Web.Mvc;

namespace Open.Core.Web.Controllers
{
    /// <summary>Controller for working with JavaScript files.</summary>
    public class ScriptsController : ControllerBase
    {
        #region Head
        public const string Name = "Scripts";
        public const string ActionCoreScripts = "CoreScripts";
        #endregion

        #region Methods
        public virtual ActionResult CoreScripts()
        {
            return View();
        }
        #endregion
    }
}
