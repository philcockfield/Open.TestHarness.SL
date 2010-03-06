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
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;


namespace Open.Core.Common
{
    /// <summary>A service that handles downloading new XAP functionality from the server.</summary>
    [Export(typeof(IPackageDownloadService))]
    public class PackageDownloadService : IPackageDownloadService
    {
        #region Head
        private const string xapExtension = ".xap";
        #endregion

        #region Methods
        /// <summary>Starts the downloading of a XAP package.</summary>
        /// <param name="xapName">The name of the XAP file to download (assumes that it is within the ClientBin of the hosting site).</param>
        /// <param name="callback">The callback to invoke when the operation is complete.</param>
        public void DownloadAsync(string xapName, CallbackAction<IPackage> callback)
        {
            xapName = xapName.RemoveEnd(xapExtension) + xapExtension;
            DownloadAsync(new Uri(xapName, UriKind.Relative), callback);
        }

        /// <summary>Starts the downloading of a XAP package.</summary>
        /// <param name="packageUri">The URI of the XAP file to download.</param>
        /// <param name="callback">The callback to invoke when the operation is complete.</param>
        public void DownloadAsync(Uri packageUri, CallbackAction<IPackage> callback)
        {
            // Setup initial conditions.
            if (packageUri == null) throw new ArgumentNullException("packageUri");

            // Start the download.
            var downloader = new DeploymentCatalog(packageUri);
            downloader.DownloadCompleted += (s, args) =>
                                                {
                                                    if (callback == null) return;
                                                    var callbackPayload = new Callback<IPackage>
                                                                        {
                                                                            Cancelled = args.Cancelled,
                                                                            Error = args.Error,
                                                                        };
                                                    if (!callbackPayload.HasError) callbackPayload.Result = new PackageWrapper(downloader);
                                                    callback(callbackPayload);
                                                };
            downloader.DownloadAsync();
        }

        /// <summary>
        ///    Initializes the MEF container in such a way that the Downloader 
        ///    service can operate (call this at startup exactly once).
        /// </summary>
        /// <returns>The download-serivce.</returns>
        public static PackageDownloadService InitializeContainer()
        {
            CompositionHost.Initialize(new DeploymentCatalog());
            return new PackageDownloadService();
        }
        #endregion

        private class PackageWrapper : IPackage
        {
            #region Head
            public PackageWrapper(DeploymentCatalog downloader)
            {
                Uri = downloader.Uri;
                Parts = downloader.Parts;
            }
            #endregion

            #region Properties
            public Uri Uri { get; private set; }
            public IQueryable<ComposablePartDefinition> Parts { get; private set; }
            #endregion
        }
    }
}
