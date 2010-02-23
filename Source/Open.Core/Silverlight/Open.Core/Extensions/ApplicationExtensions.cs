//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

using System.Collections.Generic;
using System.Windows;
using System.Windows.Browser;

namespace Open.Core.Common
{
    /// <summary>Extensions for working with the Application object.</summary>
    public static class ApplicationExtensions
    {
        /// <summary>Gets the current URL and port formatted with the 'http://' prefix.</summary>
        /// <param name="application">The application to read from (Use 'Application.Current' to retreive this).</param>
        /// <returns>The formatted current URL the application is being hosted on.</returns>
        public static string GetUrl(this Application application)
        {
            var hostSource = Application.Current.Host.Source;
            return string.Format("http://{0}:{1}", hostSource.DnsSafeHost, hostSource.Port);
        }

        /// <summary>Retrieves the query string (as a set of Key:Value pairs).</summary>
        /// <param name="application">The application to read from (Use 'Application.Current' to retreive this).</param>
        /// <returns>The collection of key-value pairs in the query-string</returns>
        /// <remarks>
        ///     The query string is assumed to start at a '?' character, and be a set of 'key=value' pairs
        ///     seperated by the '&' delimiter character.<BR/>
        ///     For example: http://domain.com?a=b&b=c;
        /// </remarks>
        public static IEnumerable<KeyValuePair<string, string>> GetQueryString(this Application application)
        {
            return HtmlPage.Document.DocumentUri.GetQueryString();
        }

        /// <summary>Retrieves the current [http://domain]:[port] value.</summary>
        /// <param name="application">The application instance (use 'Application.Current').</param>
        /// <returns>The current server URL the application is hosted on.</returns>
        public static string GetServerUrl(this Application application)
        {
            var hostSource = application.Host.Source;
            if (hostSource.DnsSafeHost.AsNullWhenEmpty() == null && hostSource.Port < 0)
            {
                // Page is loaded from a file, not a server.  Construct dummy URL.
                var path = hostSource.AbsolutePath.Reverse();
                path = path.Substring(0, path.IndexOf("/")).Reverse();
                return "http://not-from-server/" + path;
            }
            return string.Format("http://{0}:{1}", hostSource.DnsSafeHost, hostSource.Port);
        }

        /// <summary>Retrieves the name of the XAP file of the application.</summary>
        /// <param name="application">The application instance (use 'Application.Current').</param>
        public static string GetXapFileName(this Application application)
        {
            var parts = application.Host.Source.LocalPath.Split("/".ToCharArray());
            return parts[parts.Length - 1];
        }

        /// <summary>Retrieves the path up to the folder the XAP file.</summary>
        /// <param name="application">The application instance (use 'Application.Current').</param>
        public static string GetClientBinPath(this Application application)
        {
            return application.Host.Source.LocalPath.RemoveEnd(application.GetXapFileName());
        }

        /// <summary>Retrieves the URL path up to the folder the XAP file is running within, including the server URL.</summary>
        /// <param name="application">The application instance (use 'Application.Current').</param>
        public static string GetClientBinUrl(this Application application)
        {
            return application.GetServerUrl() + application.GetClientBinPath();
        }

        /// <summary>Retrieves the URL path up to the root of the server application (the folder above the ClientBin).</summary>
        /// <param name="application">The application instance (use 'Application.Current').</param>
        /// <remarks>This assumes the XAP file is hosted in a folder which is at the root of the application.</remarks>
        public static string GetApplicationRootUrl(this Application application)
        {
            // Get the client-bin name.
            var divider = "/".ToCharArray();
            var parts = application.GetClientBinUrl().TrimEnd(divider).Split(divider);
            var clientBinName = parts[parts.Length - 1];

            // Trim off the end of the app URL.
            var url = application.GetClientBinUrl().TrimEnd(divider);
            return url.RemoveEnd(clientBinName);
        }
    }
}
