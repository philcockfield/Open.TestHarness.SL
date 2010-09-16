using System.Web;
using Open.Core.Common;

namespace Open.Core.Web
{
    public static class ServerExtensions
    {
        #region Methods : HttpRequest
        /// <summary>Determines whether the application is running on the 'localhost' domain.</summary>
        /// <param name="request">The HTTP request to examine.</param>
        public static bool IsLocalHost(this HttpRequest request)
        {
            if (request == null) return false;
            return request.Url.ToString().ToLower().StartsWith("http://localhost");
        }
        #endregion

        #region Methods : Strings
        /// <summary>Prepends the server name to the given url.</summary>
        /// <param name="urlPath">The URL path to prepend.</param>
        public static string PrependDomain(this string urlPath)
        {
            if (urlPath.IsNullOrEmpty(true)) return urlPath;
            if (urlPath.StartsWith("http://") || urlPath.StartsWith("https://")) return urlPath;
            urlPath = urlPath.RemoveStart("/");

            var url = HttpContext.Current.Request.Url;
            return string.Format("{0}://{1}/{2}", url.Scheme, url.Authority, urlPath);
        }
        #endregion
    }
}
