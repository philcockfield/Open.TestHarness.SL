using System.Html;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with URL's.</summary>
    public class UrlHelper
    {
        /// <summary>Prepends the server name to the given url.</summary>
        /// <param name="urlPath">The URL path to prepend.</param>
        public string PrependDomain(string urlPath)
        {
            // Setup initial conditions.
            if (!Helper.String.HasValue(urlPath)) return urlPath;
            urlPath = urlPath.Trim();
            if (urlPath.StartsWith("http")) return urlPath;
            urlPath = Helper.String.RemoveStart(urlPath, "/");

            // Format the URL.
            Location url = Window.Location;
            return string.Format("{0}//{1}/{2}", url.Protocol, url.HostnameAndPort, urlPath);
        }
    }
}
