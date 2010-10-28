using System.Web.Mvc;
using MvcContrib.PortableAreas;
using Open.Core.Web.Controllers;

namespace Open.Core.Web
{
    /// <summary>
    ///     Registration of Open.Core.Web as a portable area that 
    ///     can be referenced by an ASP.NET MVC web-server.
    /// </summary>
    public class AreaRegistration : PortableAreaRegistration
    {
        #region Head
        public const string Name = "Open.Core";
        #endregion

        #region Properties
        /// <summary>Gets the unqiue name of the Area.</summary>
        public override string AreaName { get { return Name; } }
        #endregion

        #region Methods
        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            // Map embedded resources.
            // NB: Must come before main Area mapping.
            context.MapEmbeddedResource(AreaName, "ResourceContentRoute", AreaName + "/Content/{resourceName}");
            context.MapEmbeddedResource(AreaName, "ResourceImageRoute", AreaName + "/Images/{resourceName}", resourcePath: "Content/Images");
            context.MapEmbeddedResource(AreaName, "ResourceCssRoute", AreaName + "/Css/{resourceName}", resourcePath: "Content/Css");
            context.MapEmbeddedResource(AreaName, "TestHarnessImagesRoute", AreaName + "/TestHarness/Images/{resourceName}", resourcePath: "Content/Images/TestHarness");

            // Script folders:
            context.MapEmbeddedResource(AreaName, "ResourceScriptsRoute", AreaName + "/Scripts/{resourceName}", resourcePath: "Content/Scripts");
            context.MapEmbeddedResource(AreaName, "ResourceScriptsRouteJQuery", AreaName + "/Scripts/JQuery/{resourceName}", resourcePath: "Content/Scripts/JQuery");

            // Map the area.
            context.MapRoute(
                AreaName + "TestHarness",  // Route name
                AreaName,  // URL
                new { controller = TestHarnessController.Name, action = TestHarnessController.ActionPage });

            // Default mapping.
            context.MapRoute(
                AreaName,
                string.Format("{0}/{{controller}}/{{action}}", AreaName));

            // Finish up.
            base.RegisterAreaEmbeddedResources();
        }
        #endregion
    }
}