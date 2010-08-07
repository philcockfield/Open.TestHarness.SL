using System.Threading;
using System.Web.Mvc;

namespace Open.TestHarness.Web.Controllers
{
    /// <summary>Base class for controllers.</summary>
    public abstract class ControllerBase : Controller
    {
        #region Head
        private const string StringResx = "TestHarnessStrings";
        #endregion

        #region Methods
        /// <summary>Retrieves a localized string with the given key.</summary>
        /// <param name="key">The key of the string to retrieve.</param>
        protected string GetResource(string key)
        {
            var httpContext = ControllerContext.HttpContext;
            var culture = Thread.CurrentThread.CurrentUICulture;
            return httpContext.GetGlobalResourceObject(StringResx, key, culture) as string;
        }
        #endregion
    }
}