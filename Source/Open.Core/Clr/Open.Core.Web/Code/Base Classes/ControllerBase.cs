using System.Threading;
using System.Web.Mvc;

namespace Open.Core.Web
{
    /// <summary>Base class for controllers.</summary>
    public abstract class ControllerBase : Controller
    {
        #region Methods
        /// <summary>Retrieves a localized string with the given key.</summary>
        /// <param name="classKey">The key of the RESX file (eg. it's primary name without extensions).</param>
        /// <param name="resourceKey">The key of the string to retrieve.</param>
        protected string GetResource(string classKey, string resourceKey)
        {
            var httpContext = ControllerContext.HttpContext;
            var culture = Thread.CurrentThread.CurrentUICulture;
            return httpContext.GetGlobalResourceObject(classKey, resourceKey, culture) as string;
        }
        #endregion
    }
}
