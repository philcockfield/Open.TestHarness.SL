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
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Open.Core.Common;

namespace Open.Core.Identity
{
    /// <summary>Helper methods for authenticating a user against an OpenId provider.</summary>
    public static class OpenIdAuthentication
    {
        /// <summary>Calls the 'auth_info' method on the JanRain server and parses the response.</summary>
        /// <param name="apiKey">The API key of the JanRain account.  This can be retrieved from the account page.  See link below.</param>
        /// <param name="token">
        ///     The token returned by the initial redirect to the JanRain server in the POST data of the return call.<br/>
        ///     To retrieve this use: Request["token"]
        /// </param>
        /// <remarks>
        ///     See: https://rpxnow.com/relying_parties/listbox-me/setup_tokenurl#steps
        /// </remarks>
        public static OpenIdProfile GetProfileFromJanRain(string apiKey, string token)
        {
            // Fetch authentication info from JanRain.
            var url = new Uri(@"https://rpxnow.com/api/v2/auth_info");
            var parameters = string.Format("format={0}&apiKey={1}&token={2}", "xml", apiKey, token);

            // Auth_info request 
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = parameters.Length;

            var requestWriter = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            requestWriter.Write(parameters);
            requestWriter.Close();

            // Invoke web method.
            var response = (HttpWebResponse)request.GetResponse();
            var responseReader = new StreamReader(response.GetResponseStream());
            var responseXml = responseReader.ReadToEnd();
            responseReader.Close();

            // Finish up.
            return ToProfile(responseXml);
        }

        private static OpenIdProfile ToProfile(string responseXml)
        {
            // Setup initial conditions.
            if (responseXml.IsNullOrEmpty(true)) throw new ArgumentNullException("responseXml");

            // Parse XML.
            //var xmlRoot = XDocument.Parse(responseXml).Root;
            //if (xmlRoot == null) throw new ArgumentException("The given XML did not contains any data.");

            //var xmlProfile = xmlRoot.FirstNode as XElement;
            //if (xmlProfile == null) throw new ArgumentException("The given XML does not contain a <profile> element.");

            //var xmlName = xmlProfile.Element("name");
            //if (xmlName == null) throw new ArgumentException("The given XML does not contain a <name> element.");

            var xmlProfile = XDocument.Parse(responseXml).Root.FirstNode as XElement;
            var xmlName = xmlProfile.Element("name");


            // Construct the profile object.
            return new OpenIdProfile
                       {
                           Identifier = xmlProfile.GetChildValue("identifier"),
                           DisplayName = xmlProfile.GetChildValue("displayName"),
                           Email = xmlProfile.GetChildValue("email"),
                           VerifiedEmail = xmlProfile.GetChildValue("verifiedEmail"),
                           PreferredUserName = xmlProfile.GetChildValue("preferredUsername"),
                           ProviderName = xmlProfile.GetChildValue("providerName"),
                           GivenName = xmlName.GetChildValue("givenName"),
                           FamilyName = xmlName.GetChildValue("familyName"),
                           FormattedName = xmlName.GetChildValue("formatted"),
                       };
        }
    }
}
