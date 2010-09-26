using System.IO;
using System.Net;
using System.Web;
using Microsoft.Http;
using Microsoft.Http.Headers;
using Open.Core.Common;

namespace Open.Core.Web
{
    /// <summary>Represents a pass through, retrieveing JSON from another server.</summary>
    public class ServerProxy
    {
        #region Head
        private static bool? useLocalHost;

        public ServerProxy(string id, string url, string localHost)
        {
            // Store values.
            Id = id;
            Url = url;
            LocalHost = localHost;

            // Use the local-host value if there is no URL.
            if (Url.IsNullOrEmpty(true)) Url = LocalHost;

            // Add HTTP if not present.
            Url = FormatUrl(Url);
            LocalHost = FormatUrl(LocalHost);
        }
        #endregion

        #region Properties
        /// <summary>Gets the unique identifier of the server proxy.</summary>
        public string Id { get; private set; }

        /// <summary>Gets the URL to route requests to.</summary>
        public string Url { get; private set; }

        /// <summary>Gets or sets the local-host development server to use when in development.</summary>
        public string LocalHost { get; private set; }

        /// <summary>Gets or sets whether the local-host URL should be used.</summary>
        public bool UseLocalHost
        {
            get
            {
                return useLocalHost != null
                                ? useLocalHost.Value
                                : (useLocalHost = HttpContext.Current.Request.IsLocal).Value;
            }
        }
        #endregion

        #region Methods
        /// <summary>Issues a Get request to the target server to retreive data.</summary>
        /// <param name="urlPath">The part of the path after the base Url (see the 'Url' property).</param>
        public HttpResult Get(string urlPath)
        {
            // Setup initial conditions.
            if (urlPath.IsNullOrEmpty(true)) return null;
            urlPath = urlPath.TrimStart("/".ToCharArray());

            // Pass call to the target server.
            using (var client = new HttpClient(GetBaseUrl()))
            {
                using (var response = client.Get(urlPath))
                {
                    response.EnsureStatusIsSuccessful();
                    StreamReader reader = new StreamReader(response.Content.ReadAsStream());
                    var text = reader.ReadToEnd();
                    reader.Dispose();
                    return new HttpResult(response.Content.ContentType, text);
                }
            }
        }
        #endregion

        #region Internal
        private static string FormatUrl(string url)
        {
            if (url.IsNullOrEmpty(true)) return url;
            if (!url.StartsWith("http://")) url = "http://" + url;
            if (!url.EndsWith("/")) url = url + "/";
            return url;
        }

        private string GetBaseUrl()
        {
            return UseLocalHost ? LocalHost : Url;
        }
        #endregion
    }
}