using System.Web.Mvc;
using System.Web.Mvc.Html;
using Open.Core.Web.Controllers;

namespace Open.Core.Web
{
    /// <summary>Extension methods for accessing the area.</summary>
    public static class AreaExtensions
    {
        #region Methods : HtmlHelper
        public static void InsertCoreScripts(this HtmlHelper helper)
        {
            RenderAction(helper, ScriptsController.Name, ScriptsController.ActionCoreScripts);
        }

        public static void InsertCoreJQuery(this HtmlHelper helper)
        {
            RenderAction(helper, ScriptsController.Name, ScriptsController.ActionCoreJQuery);
        }

        public static void InsertHyperTree(this HtmlHelper helper)
        {
            RenderAction(helper, JitController.Name, JitController.ActionHyperTree);
        }
        #endregion

        #region Methods : AreaRegistrationContext
        public static void MapEmbeddedResource(
                                            this AreaRegistrationContext context,
                                            string areaName,
                                            string routeName,
                                            string url,
                                            string action = "Index",
                                            string resourcePath = null)
        {
            const string controller = "EmbeddedResource";
            var defaults = resourcePath == null
                               ? (object)new { controller = controller, action = action }
                               : (object)new { controller = controller, action = action, resourcePath = resourcePath };
            context.MapRoute(
                                routeName,
                                url,
                                defaults,
                                new string[] { WebConstants.MvcContribPortableAreas });
        }
        #endregion

        #region Internal
        private static void RenderAction(HtmlHelper helper, string controller, string action)
        {
            helper.RenderAction(action, controller, new { Area = AreaRegistration.Name });
        }
        #endregion
    }
}