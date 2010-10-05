using System.Web.Mvc;
using Open.Core.Common;

namespace Open.Core.Web
{
    /// <summary>Extensions for working with icons.</summary>
    public static class IconExtensions
    {
        /// <summary>Retrieves the path to the specified icon.</summary>
        /// <param name="helper">The HTML helper to extend.</param>
        /// <param name="icon">The icon.</param>
        public static string IconUrl(this HtmlHelper helper, IconImage icon)
        {
            // Retrieve the folder.
            var iconName = icon.ToString();
            string subPath = null;
            if (iconName.StartsWith("Silk"))
            {
                subPath = string.Format("Silk/{0}.png", iconName);
            }
            if (subPath == null) throw new NotFoundException(string.Format("Cannot resolve URL to the icon '{0}'.", iconName));

            // Create the complete URL.
            return string.Format("/{0}/Icons/{1}", Assets.AreaRegistration.Name, subPath);
        }
    }
}
