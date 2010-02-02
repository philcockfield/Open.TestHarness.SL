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
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Resources;

namespace Open.Core.Common.Network
{
    /// <summary>Loads an assembly from the server into the application.</summary>
    public class AssemblyLoader : LoaderBase
    {
        #region Head
        private const string AppManifestXaml = "AppManifest.xaml";
        private const string AppBinary = "application/binary";
        private const string AttrSource = "Source";
        private const string AttrEntryPointAssembly = "EntryPointAssembly";

        /// <summary>Constructor.</summary>
        /// <param name="xapFileName">The filename of the XAP to load.</param>
        /// <remarks>
        ///    The XAP file will contain both the specific DLL you are looking to use along with the other dependencies the DLL has.
        /// </remarks>
        public AssemblyLoader(string xapFileName)
        {
            XapFileName = StripExtensions(xapFileName) + ".xap";
        }
        #endregion

        #region Properties - Public
        /// <summary>Gets the filename of the XAP file containing the application.</summary>
        public string XapFileName { get; private set; }

        /// <summary>Gets the root assembly of the XAP that contains the controls of interest.</summary>
        /// <remarks>This property only has a value after the 'Load' method has been run.</remarks>
        public Assembly RootAssembly { get; private set; }

        /// <summary>Gets the complete collection of loaded assemblies (including the RootAssembly).</summary>
        public IEnumerable<Assembly> Assemblies { get; private set; }

        /// <summary>Gets whether the application has already been loaded.</summary>
        public bool IsLoaded
        {
            get { return RootAssembly != null; }
        }
        #endregion

        #region Methods
        /// <summary>Strips XAP and DLL extensions from a file.</summary>
        /// <param name="fileName">The file name to strip.</param>
        /// <returns>The stripped file name.</returns>
        public static string StripExtensions(string fileName)
        {
            return fileName.StripExtension(".xap", ".dll");
        }

        /// <summary>Retrieves the set of loaded assemblies excluding Assemblies with the given prefixes.</summary>
        /// <param name="excludePrefix">A set of assembly-name prefixes to exclude (eg. 'Microsoft.' or 'System.')</param>
        /// <returns>A list of Assembly.</returns>
        public IEnumerable<Assembly> GetAssemblies(params string[] excludePrefix)
        {
            // Setup initial conditions.
            var list = new List<Assembly>();
            if (Assemblies == null) return list;

            // Return the set.
            return from a in Assemblies
                   where IncludeAssembly(a, excludePrefix)
                   select a;
        }

        public override string ToString()
        {
            return XapFileName;
        }
        #endregion

        #region Method - Load
        protected override bool OnPreload()
        {
            // Only load the application once.
            return !IsLoaded;
        }

        protected override Uri GetUri()
        {
            return new Uri(XapFileName, UriKind.Relative);
        }

        protected override void OnLoadCallback(TestableOpenReadCompletedEventArgs e)
        {
            // Setup initial conditions.
            if (State == LoaderState.LoadError) return;
            var resultStream = e.Result;
            var assembliesList = new List<Assembly>();

            // Retrieve the AppManifest.xaml text.
            var info = GetInfo(resultStream, null, AppManifestXaml);
            var appManifest = new StreamReader(info.Stream).ReadToEnd();

            // Extract each of the <AssemblyPart> items from the manifest.
            var xDeploymentRoot = XDocument.Parse(appManifest).Root;
            if (xDeploymentRoot == null) throw new Exception("Failed to read the application manifest.");
            var entryPointAssembly = GetEntryPointAssembly(xDeploymentRoot);

            var ns = xDeploymentRoot.Name.Namespace;
            var deploymentParts = (
                                      from assemblyParts in xDeploymentRoot.Descendants(ns + "AssemblyPart")
                                      select assemblyParts
                                  ).ToList();

            // Load the deployment.
            foreach (var xElement in deploymentParts)
            {
                // Extract the 'Source=' value from the <AssemblyPart>.
                var source = xElement.Attribute(AttrSource).ValueOrNull();
                if (source != null)
                {
                    var streamInfo = GetInfo(resultStream, AppBinary, source);

                    var assemblyPart = new AssemblyPart();
                    if (source == entryPointAssembly)
                    {
                        // The source DLL matches the control being instantiated...load into memory and hold onto a reference of it.
                        RootAssembly = assemblyPart.Load(streamInfo.Stream);
                        assembliesList.Add(RootAssembly);
                    }
                    else
                    {
                        // This is not the DLL containing the control being instantiated, rather it is a dependent DLL.
                        // Load it into memory.
                        var assembly = assemblyPart.Load(streamInfo.Stream);
                        assembliesList.Add(assembly);
                    }
                }
            }

            // Finish up.
            Assemblies = assembliesList;
        }
        #endregion

        #region Internal
        private static StreamResourceInfo GetInfo(Stream stream, string contentType, string uri)
        {
            var uriResource = new Uri(uri, UriKind.Relative);
            return Application.GetResourceStream(new StreamResourceInfo(stream, contentType), uriResource);
        }

        private string GetEntryPointAssembly(XElement xDeploymentRoot)
        {
            var value = xDeploymentRoot.Attribute(AttrEntryPointAssembly).ValueOrNull();
            if (value == null) throw new NotFoundException(string.Format("The entry-point assembly within the XAP file '{0}' could not be found.", XapFileName));
            return StripExtensions(value) + ".dll";
        }

        private static bool IncludeAssembly(Assembly assembly, IEnumerable<string> excludePrefix)
        {
            if (assembly == null) return false;
            if (excludePrefix == null) return true;
            var name = assembly.FullName;
            if (name == null) return false;
            name = name.ToLower();

            foreach (var prefix in excludePrefix)
            {
                if (name.StartsWith(prefix.ToLower())) return false;
            }

            return true;
        }
        #endregion
    }
}
