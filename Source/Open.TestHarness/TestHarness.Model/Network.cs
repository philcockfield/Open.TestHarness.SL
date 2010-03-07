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
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using Open.Core.Common;
using Open.Core.Common.Network;

namespace Open.TestHarness.Model
{
    /// <summary>Common access to network resources.</summary>
    public static class Network
    {
        #region Properties
        /// <summary>Gets or sets the catalog within which downloaded assemblies are added.</summary>
        public static AggregateCatalog DownloadCatalog { get; set; }
        #endregion

        #region Methods
        /// <summary>Downloads the specified XAP file.</summary>
        /// <param name="xapFileName">The name of the XAP file to download.</param>
        /// <param name="callback">Action to invoke when complete (passes back the entry-point assembly).</param>
        public static void DownloadXapFile(string xapFileName, Action<Assembly> callback)
        {
            // Setup initial conditions.
            if (DownloadCatalog == null) throw new InitializationException("The 'DownloadCatalog' has not been set. Create this an initialize it with the CompositionHost during startup.");

            // Download the XAP file.
            var loader = new AssemblyLoader(xapFileName);
            loader.Load(() =>
                            {
                                // Check for errors.
                                if (loader.Error != null)
                                {
                                    Output.Write("Failed to load XAP file: " + xapFileName);
                                    Output.Write(loader.Error);
                                    if (callback != null) callback(loader.RootAssembly);
                                    return;
                                }

                                // Register the assemblies with MEF.
                                var currentAssemblies = Deployment.Current.GetAssemblies();
                                foreach (var assembly in loader.Assemblies)
                                {
                                    try
                                    {
                                        if (currentAssemblies.Contains(assembly)) continue; // Ensure assemblies are not added more than once.
                                        DownloadCatalog.Catalogs.Add(new AssemblyCatalog(assembly));
                                    }
                                    catch (Exception error)
                                    {
                                        Output.WriteTitle(Colors.Red, "Load Failure");
                                        Output.Write(
                                            Colors.Red, 
                                            string.Format("Failed to load assembly '{0}' while downloading the XAP file: '{1}'", 
                                                    assembly.GetName(), 
                                                    xapFileName));
                                        Output.WriteException(error);
                                        break;
                                    }
                                }

                                // Finish up.
                                if (callback != null) callback(loader.RootAssembly);


                                //TEMP 
                                // Re-download with the MEF package-downloader to ensure it gets registered correctly with MEF.
                                // NB: This should get the XAP file from cache.
                                //var mefDownloader = new DeploymentCatalog(loader.XapFileName);
                                //mefDownloader.DownloadCompleted += delegate
                                //                                       {
                                //                                           // Finish up.
                                //                                           if (callback != null) callback(loader.RootAssembly);
                                //                                       };
                                //mefDownloader.DownloadAsync();
                            });
        }
        #endregion

        #region Method - GetClientBin
        /// <summary>Starts an asyncronous call to get the client-bin contents.</summary>
        /// <param name="returnValue">Callback that returns the values.</param>
        /// <param name="error">Callback that is invoked when an error occurs.</param>
        public static void GetClientBin(Action<List<XapFile>> returnValue, Action<Exception> error)
        {
            // Construct the URL.
            var url = string.Format("{0}ClientBin.aspx", Application.Current.GetApplicationRootUrl());

            // OnComplete callback.
            var webClient = new WebClient();
            webClient.DownloadStringCompleted += (sender, e) =>
                                                     {
                                                         if (e.Error != null)
                                                         {
                                                             error(e.Error); 
                                                             return;
                                                         }
                                                         try
                                                         {
                                                             returnValue(ToXapFileCollection(e.Result));
                                                         }
                                                         catch (Exception ex) { error(ex); }
                                                     };

            // Invoke request against server.
            webClient.DownloadStringAsync(new Uri(url));
        }

        private static List<XapFile> ToXapFileCollection(string rawXml)
        {
            // Setup initial conditions.
            var xmlDoc = XDocument.Parse(rawXml);

            // Get the <File> elements.
            var files = from file in xmlDoc.Descendants("File")
                        select new XapFile
                                   {
                                       Name = file.Value,
                                       Kilobytes = Convert.ToDouble(file.Attribute("Kb").ValueOrNull())
                                   };
            
            // Build the list.
            var xapFileName = Application.Current.GetXapFileName().RemoveEnd(".xap");
            return files.Where(file => file.Name != xapFileName).ToList();
        }
        #endregion
    }
}
