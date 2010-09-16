using System.Web.Mvc;
using System.Web.Mvc.Html;
using Open.Core.Web.Controllers;

namespace Open.Core.Web
{
    /// <summary>Extension methods for the HtmlHelper.</summary>
    public static class HtmlHelperExtensions
    {
        #region Methods
        /// <summary>Retrieves HTML from a remote URL and renders it.</summary>
        /// <param name="helper">The HTML helper to extend.</param>
        /// <param name="url">The URL of the remote content.</param>
        public static void RenderUrl(this HtmlHelper helper, string url)
        {
            RenderAction(
                        helper, 
                        CoreController.Name,
                        CoreController.ActionRenderUrl, 
                        new { Area = AreaRegistration.Name, Url = url });
        }

        /// <summary>Renders the TestHarness from the public domain.</summary>
        /// <param name="helper">The HTML helper to extend.</param>
        public static void RenderTestHarness(this HtmlHelper helper)
        {
            helper.RenderUrl("http://TestHarness.org");
        }
        #endregion

        #region Internal
        internal static void RenderAction(this HtmlHelper helper, string controller, string action)
        {
            RenderAction(helper, controller, action, new { Area = AreaRegistration.Name });
        }

        private static void RenderAction(HtmlHelper helper, string controller, string action, object parameters)
        {
            helper.RenderAction(action, controller, parameters);
        }
        #endregion
    }
}
