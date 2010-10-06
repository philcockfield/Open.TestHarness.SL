using System.Web.Mvc;
using System.Web.Mvc.Html;
using Open.Core.Web.Controllers;

namespace Open.Core.Web
{
    /// <summary>The modes for embedding the TestHarness within a page.</summary>
    public enum TestHarnessEmbed
    {
        /// <summary>The complete page.</summary>
        Page,

        /// <summary>
        ///     Only embeds the scripts, css and HTML.  Not the HTML/HEAD/BODY, 
        ///     allowing it to be included within a page that declares it's own scripts
        ///     in the HEAD.
        /// </summary>
        Partial,
    }


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
        /// <param name="mode">The embed mode to use.</param>
        public static void RenderTestHarness(this HtmlHelper helper, TestHarnessEmbed mode = TestHarnessEmbed.Page)
        {
            var url = "http://TestHarness.org";
            if (mode == TestHarnessEmbed.Partial) url += "/embed";
            helper.RenderUrl(url);
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
