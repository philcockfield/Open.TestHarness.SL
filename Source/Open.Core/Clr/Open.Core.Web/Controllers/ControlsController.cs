using System.Web.Mvc;

namespace Open.Core.Web.Controllers
{
    /// <summary>Provides HTML for the 'Open.Core.Controls' script library.</summary>
    public partial class ControlsController : ControllerBase
    {
        #region Head
        public const string Name = "Controls";
        public const string ActionLog = "Log";
        #endregion

        #region Methods
        public virtual ActionResult Log() { return View(); }
        #endregion
    }
}
