using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Open.Core.Controls.Buttons;

namespace Open.Core.Web.Controllers
{
    /// <summary>Provides HTML templates for client-side scripted buttons.</summary>
    public class ButtonsController : ControllerBase
    {
        #region Head
        public const string Name = "Buttons";
        public const string ActionTemplate = "Template";
        #endregion

        #region Methods
        public virtual ActionResult Template(ButtonTemplate? type)
        {
            return View();
        }
        #endregion
    }
}
