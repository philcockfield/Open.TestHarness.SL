using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.UI.InputBuilder;

namespace TestHarness.Web
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                        "Embed", // Route name
                        "embed", // URL with parameters
                        new { controller = MVC.TestHarness.Name, action = MVC.TestHarness.ActionNames.Embed});

            routes.MapRoute(
                        "Default", // Route name
                        "{controller}/{action}/{id}", // URL with parameters
                        new { controller = MVC.TestHarness.Name, action = MVC.TestHarness.ActionNames.Index, id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            InputBuilder.BootStrap(); // Enable portable-areas (MvcContrib).
        }
    }
}