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
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace Open.Core.Composition
{
    /// <summary>Handles initialization of server parts.</summary>
    public static class DirectoryPartInitializer
    {
        #region Properties
        /// <summary>Gets whether the catalog(s) have been initialized.</summary>
        public static bool IsInitialized { get { return Container != null; } }

        /// <summary>Gets the path to the directory that this part-initializer represents.</summary>
        public static DirectoryCatalog Catalog { get; private set; }

        /// <summary>Gets the composition container (null if not initialized).</summary>
        public static CompositionContainer Container { get; private set; }
        #endregion

        #region Methods
        /// <summary>Resets the initializer (used for testing).</summary>
        public static void Reset()
        {
            if (!IsInitialized) return;
            Container.Dispose();
            Container = null;
            Catalog = null;
        }

        /// <summary>Satifies imports on the given instance.</summary>
        /// <param name="instance">The instance to satisfy.</param>
        public static void SatisfyImports(object instance)
        {
            // Setup initial conditions.
            if (instance == null) return;
            if (!IsInitialized) Initialize();

            // Compose the container.
            lock (Container)
            {
                Container.ComposeParts(instance);
            }
        }

        /// <summary>Initializes the container to the base-directory the application is running in.</summary>
        public static void Initialize()
        {
            const string prefix = @"file:\";
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            path = path.Substring(prefix.Length, path.Length - prefix.Length);
            Initialize(path);
        }

        /// <summary>Initializes the container.</summary>
        /// <param name="path">The path to the directory.</param>
        public static void Initialize(string path)
        {
            // Setup initial conditions.
            if (IsInitialized) Reset();
            if (path == null || String.IsNullOrEmpty(path.Trim())) throw new ArgumentOutOfRangeException("path", "A path to a directory must be specified.");

            // Setup the container.
            Catalog = new DirectoryCatalog(path);
            Container = new CompositionContainer(Catalog);
        }
        #endregion
    }
}
