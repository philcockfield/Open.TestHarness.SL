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
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Packaging;
using System.Reflection;

namespace Open.Core.Common
{
    /// <summary>A service that handles downloading new XAP functionality from the server.</summary>
    [Export(typeof(IPackageDownloadService))]
    public class PackageDownloadService : IPackageDownloadService
    {
        #region Head
        private readonly PackageCatalog catalog;
        private const string XapExtension = ".xap";

        /// <summary>Constructor.</summary>
        /// <param name="catalog">The package catalog to use.</param>
        public PackageDownloadService(PackageCatalog catalog)
        {
            if (catalog == null) throw new ArgumentNullException("catalog");
            this.catalog = catalog;
        }
        #endregion

        #region Methods
        /// <summary>Starts the downloading of a XAP package.</summary>
        /// <param name="xapName">The name of the XAP file to download (assumes that it is within the ClientBin of the hosting site).</param>
        /// <param name="callback">The callback to invoke when the operation is complete.</param>
        public void DownloadAsync(string xapName, CallbackAction<IPackage> callback)
        {
            xapName = xapName.RemoveEnd(XapExtension) + XapExtension;
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
            Package.DownloadPackageAsync(packageUri, (args, package) =>
                                         {
                                             // Construct a payload to pass back through the callback.
                                             var callbackPayload = new Callback<IPackage>
                                                                 {
                                                                     Cancelled = args.Cancelled,
                                                                     Error = args.Error,
                                                                 };

                                             // Register the package with MEF (if there were no problems).
                                             if (!callbackPayload.HasError && !callbackPayload.Cancelled)
                                             {
                                                 catalog.AddPackage(package);
                                             }

                                             // Finish up.
                                             if (callback != null)
                                             {
                                                 callbackPayload.Result = new PackageWrapper(package);
                                                 callback(callbackPayload);
                                             }
                                         });
        }

        /// <summary>
        ///    Initializes the MEF container in such a way that the Downloader 
        ///    service can operate (call this at startup exactly once).
        /// </summary>
        /// <returns>The download-serivce.</returns>
        public static PackageDownloadService InitializeContainer()
        {
            var catalog = new PackageCatalog();
            var downloadService = new PackageDownloadService(catalog);

            var container = new CompositionContainer(catalog);
            container.ComposeParts(downloadService);

            CompositionHost.InitializeContainer(container);
            return downloadService;
        }
        #endregion

        private class PackageWrapper : IPackage
        {
            #region Head
            private readonly Package package;
            public PackageWrapper(Package package)
            {
                this.package = package;
            }
            #endregion

            #region Properties
            public IEnumerable<Assembly> Assemblies { get { return package.Assemblies; } }
            public Uri Uri { get { return package.Uri; } }
            public Assembly EntryPointAssembly { get { return Assemblies.FirstOrDefault(); } }
            #endregion
        }
    }
}
