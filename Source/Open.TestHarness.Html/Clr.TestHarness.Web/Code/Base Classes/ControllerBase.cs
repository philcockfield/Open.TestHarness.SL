using System.Threading;
using System.Web.Mvc;

namespace Open.Testing.Web.Controllers
{
    /// <summary>Base class for controllers.</summary>
    public abstract class ControllerBase : Core.Web.ControllerBase
    {
        #region Head
        private const string StringResx = "TestHarnessStrings";
        #endregion

        #region Methods
        /// <summary>Retrieves a localized string with the given key.</summary>
        /// <param name="key">The key of the string to retrieve.</param>
        protected string GetResource(string key)
        {
            return base.GetResource(StringResx, key);
        }
        #endregion
    }
}