using System.Web.Mvc;
using MvcContrib.PortableAreas;

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

            // Script folders:
            context.MapEmbeddedResource(AreaName, "ResourceScriptsRoute", AreaName + "/Scripts/{resourceName}", resourcePath: "Content/Scripts");
            context.MapEmbeddedResource(AreaName, "ResourceScriptsRouteJit", AreaName + "/Scripts/Jit/{resourceName}", resourcePath: "Content/Scripts/Jit");
            context.MapEmbeddedResource(AreaName, "ResourceScriptsRouteJQuery", AreaName + "/Scripts/JQuery/{resourceName}", resourcePath: "Content/Scripts/JQuery");

            // Map the area.
            context.MapRoute(
                            AreaName,
                            string.Format("{0}/{{controller}}/{{action}}", AreaName));

            // Finish up.
            base.RegisterAreaEmbeddedResources();
        }
        #endregion
    }
}