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
using System.ComponentModel.Composition.Primitives;
using System.Linq;

namespace Open.Core.Common
{
    /// <summary>Represents a downloaded XAP package.</summary>
    public interface IPackage
    {
        /// <summary>Gets the Uri of the package.</summary>
        Uri Uri { get; }

        /// <summary>Gets the collection of defined parts within the package.</summary>
        IQueryable<ComposablePartDefinition> Parts { get; }
    }

    /// <summary>A service that handles downloading new XAP functionality from the server.</summary>
    public interface IPackageDownloadService
    {
        /// <summary>Starts the downloading of a XAP package.</summary>
        /// <param name="packageUri">The URI of the XAP file to download.</param>
        /// <param name="callback">The callback to invoke when the operation is complete.</param>
        void DownloadAsync(Uri packageUri, CallbackAction<IPackage> callback);

        /// <summary>Starts the downloading of a XAP package.</summary>
        /// <param name="xapName">The name of the XAP file to download (assumes that it is within the ClientBin of the hosting site).</param>
        /// <param name="callback">The callback to invoke when the operation is complete.</param>
        void DownloadAsync(string xapName, CallbackAction<IPackage> callback);
    }
}
