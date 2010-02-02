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
using System.Text;
using System.Xml;

namespace Open.TestHarness.Web
{
    /// <summary>Serves an XML representation of the content of the 'ClientBin' folder.</summary>
    /// <remarks>This is used by the TestHarness to present the options a user has for dynamically loading Assemblies.</remarks>
    public partial class ClientBin : System.Web.UI.Page
    {
        #region Head
        protected void Page_Load(object sender, EventArgs e)
        {
            // Write the contents of the ClientBin folder out to XML.
            var writer = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
            WriteClientBinXml(writer);
        }
        #endregion

        #region Internal
        private void WriteClientBinXml(XmlWriter writer)
        {
            // Setup initial conditions.
            writer.WriteStartDocument();
            writer.WriteStartElement("ClientBin");

            // Enumerate the collection of XAP files.
            var folder = new DirectoryInfo(Server.MapPath("ClientBin"));
            foreach (var file in folder.GetFiles("*.xap"))
            {
                var kb = Math.Round(Decimal.Divide(file.Length, 1000), 2);

                writer.WriteStartElement("File");
                writer.WriteAttributeString("Extension", "xap");
                writer.WriteAttributeString("Kb", kb.ToString());
                writer.WriteString(file.Name.TrimEnd(".xap".ToCharArray()));
                writer.WriteEndElement(); // File.
            }

            // Finish up.
            writer.WriteEndElement(); // ClientBin.
            writer.WriteEndDocument();
            writer.Close();
        }
        #endregion
    }
}
