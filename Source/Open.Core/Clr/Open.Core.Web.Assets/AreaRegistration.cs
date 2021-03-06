﻿using System.Web.Mvc;
using MvcContrib.PortableAreas;

namespace Open.Core.Web.Assets
{
    /// <summary>
    ///     Registration of Open.Core.Web.Assets as a portable area that 
    ///     can be referenced by an ASP.NET MVC web-server.
    /// </summary>
    public class AreaRegistration : PortableAreaRegistration
    {
        #region Head
        public const string Name = "Open.Assets";
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
            context.MapEmbeddedResource(AreaName, "CommonImages", AreaName + "/Images/{resourceName}", resourcePath: "Content/Images");
            context.MapEmbeddedResource(AreaName, "SilkIconRoute", AreaName + "/Icons/Silk/{resourceName}", resourcePath: "Content/Icons/Silk");
            context.MapEmbeddedResource(AreaName, "SilkIconGreyRoute", AreaName + "/Icons/Silk/Greyscale/{resourceName}", resourcePath: "Content/Icons/Silk.Greyscale");
            context.MapEmbeddedResource(AreaName, "IconFlatDarkRoute", AreaName + "/Icons/Flat/Dark/{resourceName}", resourcePath: "Content/Icons/Flat.Dark");
            context.MapEmbeddedResource(AreaName, "IconSundry", AreaName + "/Icons/Sundry/{resourceName}", resourcePath: "Content/Icons/Sundry");
            context.MapEmbeddedResource(AreaName, "IconApi", AreaName + "/Icons/Api/{resourceName}", resourcePath: "Content/Icons/Api");

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