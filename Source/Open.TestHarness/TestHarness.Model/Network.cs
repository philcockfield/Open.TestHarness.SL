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
using System.Linq;
using System.Net;
using System.Windows;
using System.Xml.Linq;
using Open.Core.Common;

namespace Open.TestHarness.Model
{
    /// <summary>Common access to network resources.</summary>
    public static class Network
    {
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
