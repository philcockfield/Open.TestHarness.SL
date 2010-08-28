using System.Web;

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
    }
}
