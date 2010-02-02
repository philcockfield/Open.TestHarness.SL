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

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Open.Core.Common
{
    /// <summary>Flags indicating where an image resides.</summary>
    public enum ImageLocation
    {
        /// <summary>The image is packaged within the XAP file on the client.</summary>
        Client,

        /// <summary>The image is stored within the ClientBin folder on the server.</summary>
        Server,
    }

    public static partial class StringExtensions
    {
        #region Head
        private const string Xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
        private const string XmlnsX = "http://schemas.microsoft.com/winfx/2006/xaml";
        #endregion

        #region Methods - URL / URI
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

        /// <summary>
        ///    Converts the path to a resource into a fully qualified component URI using the calling assembly 
        ///    (eg "/assembly;component/Images/MyImage.png").
        /// </summary>
        /// <param name="resourcePath">
        ///    The path to the resource from the root of the project.<BR/>
        ///    This may, or may not, contain a leading '/' character.<BR/>
        ///    For example: "Images/MyImage.png"<BR/>
        /// </param>
        /// <returns>A fully qualified URI.</returns>
        public static Uri ToComponentUri(this string resourcePath)
        {
            return ToComponentUri(resourcePath, Assembly.GetCallingAssembly());
        }

        /// <summary>Converts the path to a resource into a fully qualified component URI (eg "/assembly;component/Images/MyImage.png").</summary>
        /// <param name="resourcePath">
        ///    The path to the resource from the root of the project.<BR/>
        ///    This may, or may not, contain a leading '/' character.<BR/>
        ///    For example: "Images/MyImage.png"<BR/>
        /// </param>
        /// <param name="assembly">The assembly the resource is within.</param>
        /// <returns>A fully qualified URI.</returns>
        public static Uri ToComponentUri(this string resourcePath, Assembly assembly)
        {
            resourcePath = resourcePath.TrimStart("/".ToCharArray());
            var url = string.Format("/{0};component/{1}", assembly.GetAssemblyName(), resourcePath);
            return new Uri(url, UriKind.Relative);
        }
        #endregion

        #region Methods - Image
        /// <summary>
        ///    Converts the path to a resource into a fully qualified component URI (eg "/assembly;component/Images/MyImage.png")
        ///    and loads it into a bitmap image which can be used as the 'Source' of an Image.
        /// </summary>
        /// <param name="resourcePath">
        ///    The path to the resource from the root of the project.<BR/>
        ///    This may, or may not, contain a leading '/' character.<BR/>
        ///    For example: "Images/MyImage.png"<BR/>
        /// </param>
        public static BitmapImage ToImageSource(this string resourcePath)
        {
            return resourcePath.ToImageSource(Assembly.GetCallingAssembly());
        }

        /// <summary>Converts the path to a resource into bitmap image which can be used as the 'Source' of an Image.</summary>
        /// <param name="resourcePath">
        ///    The path to the resource from the root of the project or the root of the ClientBin (depending on 'location').<BR/>
        /// </param>
        /// <param name="location">Flag indicating whether the image is packaged in the XAP on the client, or is in the ClientBin on the server.</param>
        /// <returns></returns>
        public static BitmapImage ToImageSource(this string resourcePath, ImageLocation location)
        {
            switch (location)
            {
                case ImageLocation.Client: return resourcePath.ToImageSource(Assembly.GetCallingAssembly());
                case ImageLocation.Server: return new BitmapImage { UriSource = new Uri(resourcePath, UriKind.Relative) };
                default: throw new ArgumentOutOfRangeException(location.ToString());
            }
        }

        /// <summary>
        ///    Converts the path to a resource into a fully qualified component URI (eg "/assembly;component/Images/MyImage.png")
        ///    and loads it into a bitmap image which can be used as the 'Source' of an Image.
        /// </summary>
        /// <param name="resourcePath">
        ///    The path to the resource from the root of the project.<BR/>
        ///    This may, or may not, contain a leading '/' character.<BR/>
        ///    For example: "Images/MyImage.png"<BR/>
        /// </param>
        /// <param name="assembly">The assembly the resource is within.</param>
        public static BitmapImage ToImageSource(this string resourcePath, Assembly assembly)
        {
            return new BitmapImage { UriSource = ToComponentUri(resourcePath, assembly) };
        }

        /// <summary>Creates an image setting the 'Source' to the specified image path.</summary>
        /// <param name="url">The URL path to the image.</param>
        /// <param name="uriKind">The kind of URI passed in the 'url' string.</param>
        /// <returns>A new Image.</returns>
        /// <remarks>
        ///    To load images dynamicaly from the server use the following settings:<BR/>
        ///    1. UriKind = Relative.<BR/>
        ///    2. Prefix the URL with a '/' (eg. /Images/Logo.png).<BR/>
        ///    3. If storing the images in the calling project set the image BuildAction = 'None' and Copy to Output Directory = 'Copy always'<BR/>
        ///        Look for the image in the 'bin' directory.
        ///    3. Copy the images to the 'ClientBin' folder of the hosting web-site.
        /// </remarks>
        public static Image ToImage(this string url, UriKind uriKind)
        {
            var bitmap = new BitmapImage { UriSource = new Uri(url, UriKind.Relative) };
            return new Image
                       {
                           Source = bitmap,
                           Stretch = Stretch.None
                       };
        }        
        #endregion

        #region Methods - Geometry
        /// <summary>Loads the given geometry data string as a XAML Path..</summary>
        /// <param name="self">The geometry data.</param>
        /// <remarks>Don't forget to set the Fill and Stretch properties on the returned object.</remarks>
        public static Path ToPathGeometry(this string self)
        {
            // Setup initial conditions.
            if (self.AsNullWhenEmpty() == null) return null;

            // Prepare the XAML string.
            var xaml = string.Format("<Path xmlns='{0}' xmlns:x='{1}' Data='{2}' />", Xmlns, XmlnsX, self);

            // Finish up.
            return (Path)XamlReader.Load(xaml);
        }
        #endregion
    }
}
